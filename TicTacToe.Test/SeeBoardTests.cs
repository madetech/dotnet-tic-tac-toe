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
           
            Assert.AreEqual(
                new int?[][]
                {
                    new int?[] {null, null, null},
                    new int?[] {null, null, null},
                    new int?[] {null, null, null}
                },
                response.Board
            );
        }

        [Test]
        public void TestFoo()
        {
            var boardGateway = new PieceInTheMiddleBoardReaderStub(); 

            var seeBoard = new SeeBoard(boardGateway);
            var response = seeBoard.Execute();
           
            Assert.AreEqual(
                new int?[][]
                {
                    new int?[] {null, null, null},
                    new int?[] {null, 1, null},
                    new int?[] {null, null, null}
                },
                response.Board
            );
            
        }
    }

    public class PieceInTheMiddleBoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board(new []{
                new int?[] {null, null, null},
                new int?[] {null, 1, null},
                new int?[] {null, null, null}
            });
        }
    }

    public class BoardReaderStub : IBoardReader
    {
        public Board Fetch()
        {
            return new Board(new []{
                new int?[] {null, null, null},
                new int?[] {null, null, null},
                new int?[] {null, null, null}
            });
        }
    }
}