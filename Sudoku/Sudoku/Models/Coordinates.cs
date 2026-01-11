namespace Sudoku.Models
{
    /// <summary>
    /// Represents the row and column position of a cell within the Sudoku grid.
    /// </summary>
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
