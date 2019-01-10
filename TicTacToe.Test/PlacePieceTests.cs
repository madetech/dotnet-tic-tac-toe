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
            
            placePiece.Execute(0, 0);
            Assert.True(spy.Called);
        }
        
        [Test]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 1, null, 0, 0)]
        public void WhenPlacingTheFirstPiece_ThenUpdatesBoard(
            int x, int y,
            int? expectedPieceType, int expectedX, int expectedY)
        {
            var spy = new BoardWriterSpy();
            var emptyBoardReaderStub = new EmptyBoardReaderStub();
            var placePiece = new PlacePiece(spy, emptyBoardReaderStub);
            
            placePiece.Execute(x, y);
            Assert.AreEqual(expectedPieceType, spy.LastBoard.PieceAt(expectedX, expectedY));
        }

        [Test]
        public void GivenAPieceHasBeenPlaced_WhenPlacingAPiece_ThenUpdatedBoardIncludesBothPieces()
        {
            var spy = new BoardWriterSpy();
            var boardReaderStub = new PieceSetOriginBoardReaderStub();
            var placePiece = new PlacePiece(spy, boardReaderStub);
            
            placePiece.Execute(1, 0);
            
            Assert.AreEqual(0, spy.LastBoard.PieceAt(0, 0));
            Assert.AreEqual(1, spy.LastBoard.PieceAt(1, 0));
        }
        
        [Test]
        public void DoesNotMutateTheBoard()
        {
            var spy = new BoardWriterSpy();
            var boardReaderStub = new PieceSetOriginBoardReaderStub();
            var originalBoard = boardReaderStub.Fetch();
            
            var placePiece = new PlacePiece(spy, boardReaderStub);
            
            placePiece.Execute(1, 0);
            
            Assert.IsNull(originalBoard.PieceAt(1, 0));
        }
    }

    public class EmptyBoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board();
        }
    }
    
    public class PieceSetOriginBoardReaderStub : IBoardReader
    {
        private readonly Board board = new Board()
            .NewBoardWithPieceAt(0, 0, 0);

        public Board Fetch()
        {
            return board;
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