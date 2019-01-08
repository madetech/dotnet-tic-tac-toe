using NUnit.Framework;

namespace TicTacToe.AcceptanceTest
{
    public class TicTacToeTests
    {
        [Test]
        public void GivenANewGame_WhenISeeTheBoard_ItMustBeEmpty()
        {
            var boardGateway = new InMemoryBoardGateway(); 

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();
           
            Assert.AreEqual(new int?[3, 3],response.Board);
        }

        [Test]
        public void GivenAGameWithOnePiecePlaced_WhenISeeTheBoard_ThenTheBoardIsUpdatedCorrectly()
        {
            var boardGateway = new InMemoryBoardGateway(); 
            
            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(1, 0, 0);
            
            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();
           
            Assert.AreEqual(
                new int?[,]
                {
                    {1, null, null},
                    {null, null, null},
                    {null, null, null}
                },
                response.Board
            );
        }
        
        [Test]
        public void GivenAGameWithTwoPiecesPlaced_WhenISeeTheBoard_ThenTheBoardIsUpdatedCorrectly()
        {
            var boardGateway = new InMemoryBoardGateway(); 
            
            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(1, 0, 0);
            placePiece.Execute(1, 0, 1);
            
            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();
           
            Assert.AreEqual(
                new int?[,]
                {
                    {1, null, null},
                    {1, null, null},
                    {null, null, null}
                },
                response.Board
            );
        }
    }

    public class InMemoryBoardGateway : IBoardReader, IBoardWriter
    {
        private Board _board = new Board();

        public void Write(Board board)
        {
            _board = board;
        }

        public Board Fetch()
        {
            return _board;
        }
    }
}
