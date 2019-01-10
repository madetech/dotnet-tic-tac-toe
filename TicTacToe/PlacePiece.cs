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
                        
            Execute(board.GetCurrentPieceType(), x , y);
        }

        public void Execute(int pieceType, int x, int y)
        {
            var board = _boardReader.Fetch();

            _boardWriter.Write(board.NewBoardWithPieceAt(pieceType, x, y));
        }
    }
}