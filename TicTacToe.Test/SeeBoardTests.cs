using NUnit.Framework;

namespace TicTacToe.Test
{
    public class SeeBoardTests
    {
        [Test]
        public void TestInitialBoardState()
        {
            var boardGateway = new BoardReaderStub(); 

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();
           
            Assert.AreEqual(new int?[3, 3],response.Board);
        }

        [Test]
        public void TestFoo()
        {
            var boardGateway = new PieceInTheMiddleBoardReaderStub(); 

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();
           
            Assert.AreEqual(
                new int?[,]
                {
                    {null, null, null},
                    {null, 1, null},
                    {null, null, null}
                },
                response.Board
            );   
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CanDetectAHorizontalWin(int y)
        {
            var horizontalWinStub = new HorizontalWinStub(y);
            var seeBoard = new SeeBoard(horizontalWinStub);

            var response = seeBoard.Execute();
            
            Assert.That(response.Winner, Is.Zero);
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CanDetectAHorizontalWin2(int y)
        {
            var horizontalWinStub = new HorizontalWinStub(y, 1);
            var seeBoard = new SeeBoard(horizontalWinStub);

            var response = seeBoard.Execute();
            
            Assert.That(response.Winner, Is.EqualTo(1));
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CanDetectAVerticalWin(int x)
        {
            var verticalWinStub = new VerticalWinStub(x);
            var seeBoard = new SeeBoard(verticalWinStub);

            var response = seeBoard.Execute();
            
            Assert.That(response.Winner, Is.EqualTo(0));
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CanDetectAVerticalWin2(int x)
        {
            var verticalWinStub = new VerticalWinStub(x, 1);
            var seeBoard = new SeeBoard(verticalWinStub);

            var response = seeBoard.Execute();
            
            Assert.That(response.Winner, Is.EqualTo(1));
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void CanDetectADiagonalWin(int pieceType)
        {
            var seeBoard = new SeeBoard(new DiagonalWinStub(pieceType));

            var response = seeBoard.Execute();
            
            Assert.That(response.Winner, Is.EqualTo(pieceType));
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void CanDetectAnAntidiagonalWin(int pieceType)
        {
            var seeBoard = new SeeBoard(new AntidiagonalWinStub(pieceType));

            var response = seeBoard.Execute();
            
            Assert.That(response.Winner, Is.EqualTo(pieceType));
        }
    }

    public class DiagonalWinStub : IBoardReader
    {
        private readonly int _pieceType;

        public Board Fetch()
        {
            return new Board().NewBoardWithPieceAt(_pieceType, 0, 0)
                .NewBoardWithPieceAt(_pieceType, 1, 1)
                .NewBoardWithPieceAt(_pieceType, 2, 2);
        }

        public DiagonalWinStub(int pieceType = 0)
        {
            _pieceType = pieceType;
        }
    }
    
    public class AntidiagonalWinStub : IBoardReader
    {
        private readonly int _pieceType;

        public AntidiagonalWinStub(int pieceType)
        {
            _pieceType = pieceType;
        }

        public Board Fetch()
        {
            return new Board().NewBoardWithPieceAt(_pieceType, 2, 0)
                .NewBoardWithPieceAt(_pieceType, 1, 1)
                .NewBoardWithPieceAt(_pieceType, 0, 2);
        }
    }

    public class PieceInTheMiddleBoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board().NewBoardWithPieceAt(1, 1, 1);
        }
    }

    public class HorizontalWinStub : IBoardReader
    {
        private readonly int _y;
        private readonly int _pieceType;

        public Board Fetch()
        {
            return new Board().NewBoardWithPieceAt(_pieceType, 0, _y)
                .NewBoardWithPieceAt(_pieceType, 1, _y)
                .NewBoardWithPieceAt(_pieceType, 2, _y);
        }

        public HorizontalWinStub(int y = 0, int pieceType = 0)
        {
            _y = y;
            _pieceType = pieceType;
        }
    }

    public class VerticalWinStub : IBoardReader
    {
        private readonly int _x;
        private readonly int _pieceType;

        public Board Fetch()
        {
            return new Board().NewBoardWithPieceAt(_pieceType, _x, 0)
                .NewBoardWithPieceAt(_pieceType, _x, 1)
                .NewBoardWithPieceAt(_pieceType, _x, 2);
        }

        public VerticalWinStub(int x = 0, int pieceType = 0)
        {
            _x = x;
            _pieceType = pieceType;
        }
    }
    
    public class BoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board();
        }
    }
}