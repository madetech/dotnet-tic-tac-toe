namespace TicTacToe
{
    public class PlacePiece
    {
        private readonly IBoardWriter _boardWriter;

        public PlacePiece(IBoardWriter boardWriter)
        {
            _boardWriter = boardWriter;
        }
        
        public void Execute(int pieceType, int x, int y)
        {
            var pieces = new int?[3][]
            {
                new int?[3],
                new int?[3],
                new int?[3]
            };

            pieces[x][y] = pieceType;
            
            _boardWriter.Write(new Board(pieces));
        }
    }
}