using System.Linq;

namespace TicTacToe
{
    public class Board
    {
        public readonly int?[,] Pieces;

        public Board()
        {
            Pieces = new int?[3, 3];
        }
        
        private Board(int?[,] pieces)
        {
            Pieces = pieces;
        }
        
        public int? PieceAt(int x, int y)
        {
            return Pieces[y, x];
        }

        public int GetCurrentPieceType()
        {
            var flattenedArray = Pieces.Cast<int?>().ToArray();
            return flattenedArray.Count(x => x != null) % 2;
        }

        public Board NewBoardWithPieceAt(int pieceType, int x, int y)
        {
            var pieces = Pieces.Clone() as int? [,];
            pieces[y, x] = pieceType;

            return new Board(pieces);
        }
    }
}