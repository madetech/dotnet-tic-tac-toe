using NUnit.Framework;

namespace TicTacToe.Test
{
    public class PlacePieceTests
    {
        [Test]
        public void WhenPlacingAPiece_ThenCallsWriter()
        {
            var spy = new BoardWriterSpy();
            var emptyBoardReaderStub = new EmptyBoardReaderStub();
            var placePiece = new PlacePiece(spy, emptyBoardReaderStub);
            
            placePiece.Execute(1, 0, 0);
            Assert.True(spy.Called);
        }
        
        [Test]
        [TestCase(1, 0, 0, 1, 0, 0)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0, 1, 1, null, 0, 0)]
        public void WhenPlacingAnXPiece_ThenUpdatesBoard(
            int pieceType, int x, int y,
            int? expectedType, int expectedX, int expectedY)
        {
            var spy = new BoardWriterSpy();
            var emptyBoardReaderStub = new EmptyBoardReaderStub();
            var placePiece = new PlacePiece(spy, emptyBoardReaderStub);
            
            placePiece.Execute(pieceType, x, y);
            Assert.AreEqual(expectedType, spy.LastBoard.PieceAt(expectedX, expectedY));
        }

        [Test]
        public void GivenAPieceHasBeenPlaced_WhenPlacingAPiece_ThenUpdatedBoardIncludesBothPieces()
        {
            var spy = new BoardWriterSpy();
            var boardReaderStub = new PieceSetOriginBoardReaderStub();
            var placePiece = new PlacePiece(spy, boardReaderStub);
            
            placePiece.Execute(0, 1, 0);
            
            Assert.AreEqual(1, spy.LastBoard.PieceAt(0, 0));
            Assert.AreEqual(0, spy.LastBoard.PieceAt(1, 0));
        }
    }

    public class EmptyBoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board(new []
            {
                new int?[3],
                new int?[3],
                new int?[3]
            });
        }
    }
    
    public class PieceSetOriginBoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board(new []
            {
                new int?[] { 1, null, null },
                new int?[3],
                new int?[3]
            });
        }
    }
    
    public class BoardWriterSpy : IBoardWriter
    {
        public bool Called { get; private set; }
        public Board LastBoard { get; private set; }
        
        public void Write(Board board)
        {
            Called = true;
            LastBoard = board;
        }
    }
}