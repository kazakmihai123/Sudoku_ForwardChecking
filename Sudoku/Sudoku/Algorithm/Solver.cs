using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;

namespace Sudoku.Algoritm
{
    /// <summary>
    /// Sudoku solver implemented using Backtracking + Forward Checking.
    /// </summary>
    public class Solver
    {
        // Callback used to notify UI after each step
        public Action? OnStep;

        // Delay (ms) between steps for slow visualization
        public int StepDelayMs { get; set; } = 0;

        // Grid being solved (modified in-place)
        private readonly Grid _grid;

        // Enable / disable MRV
        public bool UseMRV { get; set; } = true;

        // Current domains: cellIndex -> possible values
        private Dictionary<int, HashSet<int>> _domains = new();

        /// <summary>
        /// Creates a solver for the given Sudoku grid.
        /// </summary>
        /// <param name="grid">Grid to solve.</param>
        public Solver(Grid grid)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            InitializeDomains();
        }

        /// <summary>
        /// Starts the solving process.
        /// </summary>
        /// <returns>True if a solution is found, otherwise false.</returns>
        public bool Solve()
        {
            // Check initial consistency
            if (!InitializeDomains()) return false;
            return Backtrack();
        }

        /// <summary>
        /// Executes one visualization step (UI callback + optional delay).
        /// </summary>
        private void Step()
        {
            OnStep?.Invoke();
            if (StepDelayMs > 0)
                System.Threading.Thread.Sleep(StepDelayMs);
        }

        /// <summary>
        /// Recursive backtracking search with forward checking.
        /// </summary>
        /// <returns>True if a complete solution is found.</returns>
        private bool Backtrack()
        {
            // Select next unassigned variable
            int varIndex = SelectUnassignedVariableMRV();
            if (varIndex == -1) return true; // solved

            // Try values from the current domain
            var values = _domains[varIndex].OrderBy(v => v).ToList();

            foreach (int val in values)
            {
                if (!IsConsistent(varIndex, val)) continue;

                // Save state for backtracking
                var domainsSnapshot = CloneDomains(_domains);
                int oldValue = _grid.Cells[varIndex].Value;

                // Assign value
                _grid.Cells[varIndex].Value = val;
                Step();

                // Forward checking
                if (ForwardCheck(varIndex, val))
                    if (Backtrack()) return true;

                // Undo assignment
                _grid.Cells[varIndex].Value = oldValue;
                Step();
                _domains = domainsSnapshot;
            }

            return false;
        }

        /// <summary>
        /// Applies forward checking after assigning a value:
        /// removes the assigned value from all neighbors' domains.
        /// </summary>
        /// <param name="varIndex">Assigned cell index.</param>
        /// <param name="val">Assigned value.</param>
        /// <returns>False if any neighbor domain becomes empty.</returns>
        private bool ForwardCheck(int varIndex, int val)
        {
            _domains[varIndex].Clear();
            _domains[varIndex].Add(val);

            foreach (int n in GetNeighbors(varIndex))
            {
                int nVal = _grid.Cells[n].Value;
                if (nVal != 0)
                {
                    if (nVal == val) return false;
                    continue;
                }

                if (_domains[n].Remove(val))
                    if (_domains[n].Count == 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Selects the next unassigned variable:
        /// - If UseMRV is true, uses MRV (minimum remaining values)
        /// - Otherwise selects the first unassigned cell
        /// </summary>
        /// <returns>Cell index, or -1 if all cells are assigned.</returns>
        private int SelectUnassignedVariableMRV()
        {
            if (!UseMRV)
            {
                for (int i = 0; i < _grid.Cells.Count; i++)
                    if (_grid.Cells[i].Value == 0)
                        return i;
                return -1;
            }

            int bestIdx = -1;
            int bestSize = int.MaxValue;

            for (int i = 0; i < _grid.Cells.Count; i++)
            {
                if (_grid.Cells[i].Value != 0) continue;

                int dSize = _domains[i].Count;
                if (dSize < bestSize)
                {
                    bestSize = dSize;
                    bestIdx = i;
                    if (bestSize == 1) break;
                }
            }

            return bestIdx;
        }

        /// <summary>
        /// Checks whether assigning the value to the given cell
        /// respects Sudoku constraints (row, column, subgrid).
        /// </summary>
        /// <param name="cellIndex">Index of the target cell.</param>
        /// <param name="value">Candidate value to test.</param>
        /// <returns>True if consistent, otherwise false.</returns>
        private bool IsConsistent(int cellIndex, int value)
        {
            var cell = _grid.Cells[cellIndex];
            int r = cell.Coords.X;
            int c = cell.Coords.Y;

            // Row
            for (int j = 0; j < _grid.Size; j++)
                if (j != c && _grid.GetValue(r, j) == value) return false;

            // Column
            for (int i = 0; i < _grid.Size; i++)
                if (i != r && _grid.GetValue(i, c) == value) return false;

            // Subgrid
            int startR = (r / _grid.SubSize) * _grid.SubSize;
            int startC = (c / _grid.SubSize) * _grid.SubSize;

            for (int i = startR; i < startR + _grid.SubSize; i++)
                for (int j = startC; j < startC + _grid.SubSize; j++)
                    if ((i != r || j != c) && _grid.GetValue(i, j) == value)
                        return false;

            return true;
        }

        /// <summary>
        /// Initializes domains for all cells based on the current grid state.
        /// </summary>
        /// <returns>False if the current grid contains a conflict or an empty domain.</returns>
        private bool InitializeDomains()
        {
            _domains = new Dictionary<int, HashSet<int>>(_grid.Cells.Count);

            // Check initial conflicts
            for (int i = 0; i < _grid.Cells.Count; i++)
            {
                int v = _grid.Cells[i].Value;
                if (v != 0 && !IsConsistentIgnoringSelf(i, v))
                    return false;
            }

            // Build domains
            for (int i = 0; i < _grid.Cells.Count; i++)
            {
                int v = _grid.Cells[i].Value;

                if (v != 0)
                {
                    _domains[i] = new HashSet<int> { v };
                    continue;
                }

                var d = new HashSet<int>();
                for (int val = 1; val <= _grid.Size; val++)
                    if (IsConsistent(i, val))
                        d.Add(val);

                if (d.Count == 0) return false;
                _domains[i] = d;
            }

            return true;
        }

        /// <summary>
        /// Checks consistency for a fixed value while temporarily clearing the cell,
        /// to avoid comparing the cell with itself.
        /// </summary>
        /// <param name="cellIndex">Index of the cell.</param>
        /// <param name="value">Fixed value to validate.</param>
        /// <returns>True if consistent, otherwise false.</returns>
        private bool IsConsistentIgnoringSelf(int cellIndex, int value)
        {
            int old = _grid.Cells[cellIndex].Value;
            _grid.Cells[cellIndex].Value = 0;
            bool ok = IsConsistent(cellIndex, value);
            _grid.Cells[cellIndex].Value = old;
            return ok;
        }

        /// <summary>
        /// Creates a deep copy of the domains dictionary (used for backtracking).
        /// </summary>
        /// <param name="src">Source domains dictionary.</param>
        /// <returns>Deep-copied domains dictionary.</returns>
        private static Dictionary<int, HashSet<int>> CloneDomains(Dictionary<int, HashSet<int>> src)
        {
            var copy = new Dictionary<int, HashSet<int>>(src.Count);
            foreach (var kv in src)
                copy[kv.Key] = new HashSet<int>(kv.Value);
            return copy;
        }

        /// <summary>
        /// Returns all neighboring cells of a given cell (same row, column, or subgrid).
        /// </summary>
        /// <param name="cellIndex">Index of the target cell.</param>
        /// <returns>Indices of all neighbor cells.</returns>
        private IEnumerable<int> GetNeighbors(int cellIndex)
        {
            var cell = _grid.Cells[cellIndex];
            int r = cell.Coords.X;
            int c = cell.Coords.Y;

            var neighbors = new HashSet<int>();

            // Row
            for (int j = 0; j < _grid.Size; j++)
                if (j != c)
                    neighbors.Add(r * _grid.Size + j);

            // Column
            for (int i = 0; i < _grid.Size; i++)
                if (i != r)
                    neighbors.Add(i * _grid.Size + c);

            // Subgrid
            int startR = (r / _grid.SubSize) * _grid.SubSize;
            int startC = (c / _grid.SubSize) * _grid.SubSize;

            for (int i = startR; i < startR + _grid.SubSize; i++)
                for (int j = startC; j < startC + _grid.SubSize; j++)
                    if (i != r || j != c)
                        neighbors.Add(i * _grid.Size + j);

            return neighbors;
        }
    }
}
