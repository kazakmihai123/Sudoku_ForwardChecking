namespace Sudoku.Models
{
    /// <summary>
    /// Represents a single cell in the Sudoku grid, storing its position,
    /// current value, and subgrid membership.
    /// </summary>
    public class Cell
    {
        public int Index { get; set; }
        public int Value { get; set; }
        public int Group { get; }
        public Coordinates Coords { get; set; }

        public Cell(int index, int value, int group, Coordinates coords)
        {
            Index = index;
            Value = value;
            Group = group;
            Coords = coords;
        }
    }
}
