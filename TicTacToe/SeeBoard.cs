namespace TicTacToe
{
    public class SeeBoard
    {
        private readonly IBoardReader _boardReader;

        public SeeBoard(IBoardReader boardReader)
        {
            _boardReader = boardReader;
        }

        public SeeBoardResponse Execute()
        {
            var board = _boardReader.Fetch();
            
            
            
            var response = new SeeBoardResponse
            {
                Board = board.Pieces
            };

            return response;
        }
    }
}