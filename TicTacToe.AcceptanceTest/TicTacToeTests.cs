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

            Assert.AreEqual(new int?[3, 3], response.Board);
            Assert.That(response.Winner, Is.Null);
        }

        [Test]
        public void GivenAGameWithOnePiecePlaced_WhenISeeTheBoard_ThenTheBoardIsUpdatedCorrectly()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.AreEqual(
                new int?[,]
                {
                    {0, null, null},
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
            placePiece.Execute(0, 0);
            placePiece.Execute(0, 1);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.AreEqual(
                new int?[,]
                {
                    {0, null, null},
                    {1, null, null},
                    {null, null, null}
                },
                response.Board
            );
        }

        [Test]
        public void WhenANewPieceIsPlaced_MustBeDifferentFrom_ThePrevious()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            placePiece.Execute(0, 1);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.AreEqual(
                new int?[,]
                {
                    {0, null, null},
                    {1, null, null},
                    {null, null, null}
                },
                response.Board
            );
        }

        [Test]
        public void CanNotOverwriteExistingPiece()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            
            placePiece.Execute(0, 0);
            placePiece.Execute(0, 2);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.AreEqual(
                new int?[,]
                {
                    {0, null, null},
                    {null, null, null},
                    {1, null, null}
                },
                response.Board
            );
        }
        
       
        [Test]
        public void CanDetectAWinForOHorizontally()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            placePiece.Execute(0, 1);
            
            placePiece.Execute(1, 0);
            placePiece.Execute(1, 1);

            placePiece.Execute(2, 0);
            
            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.Zero);
        }
        
        [Test]
        public void CanDetectAWinForXHorizontally()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            placePiece.Execute(0, 1);
            
            placePiece.Execute(1, 0);
            placePiece.Execute(1, 1);

            placePiece.Execute(0, 2);
            placePiece.Execute(2, 1);
            
            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(1));
        }
        
        [Test]
        public void CanDetectAWinForOVertically()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            placePiece.Execute(1, 0);
            
            placePiece.Execute(0, 1);
            placePiece.Execute(1, 1);

            placePiece.Execute(0, 2);
            
            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(0));
        }
        
        [Test]
        public void CanDetectAWinForXVertically()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            placePiece.Execute(1, 0);
            
            placePiece.Execute(0, 1);
            placePiece.Execute(1, 1);

            placePiece.Execute(2, 2);
            placePiece.Execute(1, 2);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(1));
        }

        [Test]
        public void CanDetectAWinForOMainDiagonally()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(0, 0);
            placePiece.Execute(1, 0);
            
            placePiece.Execute(1, 1);
            placePiece.Execute(2, 1);

            placePiece.Execute(2, 2);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(0));
        }

        [Test]
        public void CanDetectAWinForOAntiDiagonally()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(2, 0);
            placePiece.Execute(1, 0);
            
            placePiece.Execute(1, 1);
            placePiece.Execute(2, 1);

            placePiece.Execute(0, 2);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(0));
        }

        [Test]
        public void CanDetectAWinForXDiagonally()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(1, 0);
            placePiece.Execute(0, 0);
            
            placePiece.Execute(2, 1);
            placePiece.Execute(1, 1);
            
            placePiece.Execute(2, 0);
            placePiece.Execute(2, 2);
            
            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(1));
        }

        [Test]
        public void CanDetectAWinForXAntiDiagonally()
        {
            var boardGateway = new InMemoryBoardGateway();

            var placePiece = new PlacePiece(boardGateway, boardGateway);
            placePiece.Execute(1, 0);
            placePiece.Execute(2, 0);
            
            placePiece.Execute(2, 1);
            placePiece.Execute(1, 1);

            placePiece.Execute(1, 2);
            placePiece.Execute(0, 2);

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();

            Assert.That(response.Winner, Is.EqualTo(1));
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