namespace TicTacToe
{
    public class PlacePiece : IPlacePiece
    {
        private readonly IBoardWriter _boardWriter;
        private readonly IBoardReader _boardReader;

        public PlacePiece(IBoardWriter boardWriter, IBoardReader boardReader)
        {
            _boardWriter = boardWriter;
            _boardReader = boardReader;
        }

        public void Execute(int x, int y)
        {
            var board = _boardReader.Fetch();

            if (board.PieceAt(x, y) != null) return;
            
            int pieceType = board.GetCurrentPieceType();
            
            _boardWriter.Write(board.NewBoardWithPieceAt(pieceType, x, y));
        }
    }
}