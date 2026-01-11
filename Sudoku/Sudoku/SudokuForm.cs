using Sudoku.Algoritm;
using Sudoku.Models;

namespace Sudoku
{
    public partial class SudokuForm : Form
    {
        // Current working grid
        private Grid _grid;

        // Snapshot of the grid before solving (used for reset)
        private Grid _gridSnapshot;

        // UI labels corresponding to Sudoku cells (index-based)
        readonly List<Label> cellControls = new List<Label>();

        // Currently selected cell in the UI
        private Label? _selectedCell;

        // Solver instance (Backtracking + Forward Checking)
        private Solver _solver;

        // Used to measure solving time
        private DateTime _startTime;

        /// <summary>
        /// Initializes the Sudoku form and enables global key handling.
        /// </summary>
        public SudokuForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += SudokuForm_KeyDown;
        }

        private void SudokuForm_Load(object sender, EventArgs e)
        {
            // Default grid size selection
            comboBoxGrid.SelectedIndex = 0;
        }

        /// <summary>
        /// Dynamically creates the Sudoku grid UI based on grid size.
        /// Handles spacing so subgrids are visually separated.
        /// </summary>
        private void CreateGrid()
        {
            const int totalSize = 576;
            const int smallPadding = 1;
            const int bigPadding = 5;

            int gridSize = _grid.Size;
            int subGridSize = _grid.SubSize;

            int separators = gridSize / subGridSize - 1;

            int cellSize = (totalSize - smallPadding * (gridSize - 1) - bigPadding * separators) / gridSize;

            int cellFontSize = gridSize == 9 ? 16 : 22;

            var cellTopLocation = 6;

            for (int x = 0; x < gridSize; x++)
            {
                var cellLeftLocation = 5;

                for (int y = 0; y < gridSize; y++)
                {
                    var cell = new Label
                    {
                        Text = "",
                        Tag = x * _grid.Size + y, // linear index mapping
                        Width = cellSize,
                        Height = cellSize,
                        Left = cellLeftLocation,
                        Top = cellTopLocation,
                        Cursor = Cursors.Hand,
                        ForeColor = Color.Black,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Trebuchet MS", 18F, FontStyle.Bold, GraphicsUnit.Point, 0),
                        BackColor = Color.Transparent,
                        BorderStyle = BorderStyle.FixedSingle,
                    };

                    // Handle cell selection
                    cell.Click += Cell_Click;

                    cellLeftLocation += cellSize + ((y + 1) % _grid.SubSize == 0 ? bigPadding : smallPadding);

                    cellControls.Add(cell);
                    dataGridViewSudoku.Controls.Add(cell);
                }

                cellTopLocation += cellSize + ((x + 1) % subGridSize == 0 ? bigPadding : smallPadding);
            }
        }

        /// <summary>
        /// Rebuilds the grid when grid size selection changes.
        /// Clears previous UI and state.
        /// </summary>
        private void comboBoxGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            _solver = null;
            _selectedCell = null;
            _gridSnapshot = null;
            lblTimer.Text = "";

            foreach (var c in cellControls)
                c.Dispose();

            cellControls.Clear();
            dataGridViewSudoku.Controls.Clear();

            _grid = new Grid((comboBoxGrid.SelectedIndex == 0) ? 4 : 9);
            CreateGrid();
        }

        /// <summary>
        /// Handles cell selection and visual highlighting.
        /// </summary>
        private void Cell_Click(object? sender, EventArgs e)
        {
            if (_selectedCell != null)
                _selectedCell.BackColor = Color.Transparent;

            _selectedCell = (Label)sender!;
            _selectedCell.BackColor = Color.LightBlue;
        }

        /// <summary>
        /// Handles keyboard input for editing Sudoku cells.
        /// Supports digits, delete, and backspace.
        /// </summary>
        private void SudokuForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_selectedCell == null) return;

            int digit = 0;

            if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
                digit = e.KeyCode - Keys.D0;
            else if (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9)
                digit = e.KeyCode - Keys.NumPad0;

            int idx = (int)_selectedCell.Tag;
            int n = _grid.Size;
            int row = idx / n;
            int col = idx % n;

            // Assign digit if valid
            if (digit != 0)
            {
                if (_grid.Size == 4 && digit > 4)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    return;
                }

                _selectedCell.Text = digit.ToString();
                _grid.SetValue(row, col, digit);

                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            // Clear cell value
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                _selectedCell.Text = "";
                _grid.SetValue(row, col, 0);

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Synchronizes UI labels with the current grid state.
        /// </summary>
        private void RefreshGridUI()
        {
            int n = _grid.Size;

            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    int idx = r * n + c;
                    int value = _grid.GetValue(r, c);

                    cellControls[idx].Text = value == 0 ? "" : value.ToString();
                }
            }
        }

        /// <summary>
        /// Starts the solving process asynchronously.
        /// Enables optional slow visualization and MRV heuristic.
        /// </summary>
        private async void btnSolve_Click(object sender, EventArgs e)
        {
            _gridSnapshot = new Grid(_grid);
            _solver = new Solver(_grid);

            // UI update callback for visualization
            _solver.OnStep = chkSlowMode.Checked
                ? () => Invoke(new Action(RefreshGridUI))
                : null;

            _solver.StepDelayMs = chkSlowMode.Checked ? 50 : 0;
            _solver.UseMRV = chkUseMRV.Checked;

            _startTime = DateTime.Now;
            timer1.Start();

            bool ok = await Task.Run(() => _solver.Solve());

            timer1.Stop();

            TimeSpan elapsed = DateTime.Now - _startTime;
            lblTimer.Text = elapsed.ToString(@"mm\:ss\:fff");

            RefreshGridUI();

            if (!ok)
                MessageBox.Show("No solution exists (or the initial grid is invalid).");
        }

        /// <summary>
        /// Restores the grid to its state before solving.
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_gridSnapshot == null)
                return;

            _grid = _gridSnapshot;
            RefreshGridUI();
        }

        /// <summary>
        /// Clears the grid completely.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            _grid = new Grid(_grid.Size);
            _gridSnapshot = null;
            RefreshGridUI();
        }

        /// <summary>
        /// Updates the timer label while solving.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _startTime;
            lblTimer.Text = elapsed.ToString(@"mm\:ss\:fff");
        }
    }
}
