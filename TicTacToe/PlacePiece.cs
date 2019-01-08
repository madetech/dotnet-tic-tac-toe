namespace TicTacToe
{
    public class PlacePiece
    {
        private readonly IBoardWriter _boardWriter;
        private readonly IBoardReader _boardReader;

        public PlacePiece(IBoardWriter boardWriter, IBoardReader boardReader)
        {
            _boardWriter = boardWriter;
            _boardReader = boardReader;
        }
        
        public void Execute(int pieceType, int x, int y)
        {
            var board = _boardReader.Fetch();
            var pieces = board.Pieces;

            pieces[y][x] = pieceType;
            
            _boardWriter.Write(new Board(pieces));
        }
    }
}