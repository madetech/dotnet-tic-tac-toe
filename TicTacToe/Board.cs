namespace TicTacToe
{
    public class Board
    {
        public readonly int?[][] Pieces;

        public Board(int? [][] pieces)
        {
            Pieces = pieces;
        }
        
        public int? PieceAt(int x, int y)
        {
            return Pieces[x][y];
        }
    }
}