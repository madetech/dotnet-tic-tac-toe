using NUnit.Framework;

namespace TicTacToe.Test
{
    public class PlacePieceTests
    {
        [Test]
        public void WhenPlacingAPiece_ThenCallsWriter()
        {
            var spy = new BoardWriterSpy();
            var placePiece = new PlacePiece(spy);
            
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
            var placePiece = new PlacePiece(spy);
            
            placePiece.Execute(pieceType, x, y);
            Assert.AreEqual(expectedType, spy.LastBoard.PieceAt(expectedX, expectedY));
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