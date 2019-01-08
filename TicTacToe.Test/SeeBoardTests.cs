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
    }

    public class PieceInTheMiddleBoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board().NewBoardWithPieceAt(1, 1, 1);
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