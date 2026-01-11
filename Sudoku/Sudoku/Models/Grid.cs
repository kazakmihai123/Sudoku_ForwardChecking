namespace Sudoku.Models
{
    /// <summary>
    /// Represents a Sudoku grid, managing the collection of cells,
    /// grid dimensions, and basic access operations.
    /// </summary>
    public class Grid
    {
        private readonly int _size;
        private readonly int _subSize;
        private readonly List<Cell> _cells;

        public Grid(int size)
        {
            _size = size;
            _subSize = (int)Math.Sqrt(size);
            _cells = new List<Cell>(size * size);
            InitializeCells();
        }

        public Grid(Grid other)
        {
            _size = other._size;
            _subSize = other._subSize;
            _cells = new List<Cell>(_size * _size);

            foreach (var cell in other._cells)
            {
                _cells.Add(new Cell(
                    index: cell.Index,
                    value: cell.Value,
                    group: cell.Group,
                    coords: new Coordinates(cell.Coords.X, cell.Coords.Y)
                ));
            }
        }

        private void InitializeCells()
        {
            for (int r = 0; r < _size; r++)
            {
                for (int c = 0; c < _size; c++)
                {
                    int group = (r / _subSize) * _subSize + (c / _subSize);
                    _cells.Add(new Cell(
                        index: r * _size + c,
                        value: 0,
                        group: group,
                        coords: new Coordinates(r, c)
                    ));
                }
            }
        }

        public int Size => _size;
        public int SubSize => _subSize;
        public List<Cell> Cells => _cells;

        public Cell GetCell(int row, int col) => _cells[row * _size + col];
        public int GetValue(int row, int col) => GetCell(row, col).Value;
        public void SetValue(int row, int col, int value) => GetCell(row, col).Value = value;
    }
}
