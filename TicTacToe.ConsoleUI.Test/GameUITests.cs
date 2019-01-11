using System.Collections.Generic;
using NUnit.Framework;

namespace TicTacToe.ConsoleUI.Test
{
    public class GameUITests : GameUI.IConsole, ISeeBoard, IPlacePiece
    {
        private GameUI Game;
        private List<string> LineBuffer;

        private SeeBoardResponse _seeBoardResponse;
        private Stack<string> input;
        private int? PlacedPieceX;
        private int? PlacedPieceY;
        public void WriteLine(string line)
        {
            LineBuffer.Add(line);
        }

        public string ReadLine()
        {
            return input.Pop();
        }

        public SeeBoardResponse Execute()
        {
            return _seeBoardResponse;
        }

        public void Execute(int x, int y)
        {
            PlacedPieceX = x;
            PlacedPieceY = y;
        }

        [SetUp]
        public void SetUp()
        {
            input = new Stack<string>();
            input.Push("exit");
            
            Game = new GameUI(
                this,
                this,
                this
            );
            _seeBoardResponse = new SeeBoardResponse
            {
                Board = new int?[3, 3]
            };

            LineBuffer = new List<string>();
            
            PlacedPieceX = null;
            PlacedPieceY = null;
        }

        [Test]
        public void CanDisplayEmptyBoard()
        {
            Game.Start();

            Assert.AreEqual(
                new List<string> {" ABC", 
                                           "1---", 
                                           "2---", 
                                           "3---",
                                           "Player: o"                                           
                                           
                }
                , LineBuffer
            );
        }

        [Test]
        public void CanDisplayBoard()
        {
            _seeBoardResponse.Board[1, 2] = 1;

            Game.Start();
            
            Assert.AreEqual(
                new List<string> {" ABC", 
                    "1---", 
                    "2--x", 
                    "3---",
                    "Player: x"
                }
                , LineBuffer
            );
        }

        [Test]
        public void CanDisplayBoard2()
        {
            _seeBoardResponse.Board[1, 2] = 0;

            Game.Start();

            Assert.AreEqual(
                new List<string> {" ABC", 
                    "1---", 
                    "2--o", 
                    "3---",
                    "Player: x"
                }
                , LineBuffer
            );
        }

        [Test]
        public void CanDisplayBoardFilledWithXs()
        {
            _seeBoardResponse.Board = new int?[,]
            {
                {1, 1, 1}, {1, 1, 1}, {1, 1, 1}
            };

            Game.Start();
            
            Assert.AreEqual(
                new List<string> {" ABC", 
                    "1xxx", 
                    "2xxx", 
                    "3xxx",
                    "Player: x"
                }
                , LineBuffer
            );
        }

        [Test]
        public void CanDisplayBoardFilledWithOs()
        {
            _seeBoardResponse.Board = new int?[,]
            {
                {0, 0, 0},
                {0, 0, 0},
                {0, 0, 0}
            };

            Game.Start();
            
            Assert.AreEqual(
                new List<string> {" ABC", 
                    "1ooo", 
                    "2ooo", 
                    "3ooo",
                    "Player: x"
                }
                , LineBuffer
            );
        }

        [Test]
        public void CanPlaceAPiece()
        {
            input.Push("A1");

            Game.Start();

            Assert.That(PlacedPieceX, Is.EqualTo(0));
            Assert.That(PlacedPieceY, Is.EqualTo(0));
        }

        [Test]
        public void CanPlaceAPiece2()
        {
            input.Push("B2");

            Game.Start();

            Assert.That(PlacedPieceX, Is.EqualTo(1));
            Assert.That(PlacedPieceY, Is.EqualTo(1));
        }

        [Test]
        public void CanPlaceAPieceWithLowerCaseNotation()
        {
            input.Push("c2");

            Game.Start();

            Assert.That(PlacedPieceX, Is.EqualTo(2));
            Assert.That(PlacedPieceY, Is.EqualTo(1));
        }

        [Test]
        public void AfterPlacingPieceReprintsBoard()
        {
            input.Push("C2");

            Game.Start();
            
            Assert.AreEqual(
                new List<string> {
                    " ABC", 
                    "1---", 
                    "2---", 
                    "3---",
                    "Player: o",
                    string.Empty,
                    " ABC", 
                    "1---", 
                    "2---", 
                    "3---",
                    "Player: o"
                }
                , LineBuffer
            );
        }

        [Test]
        [TestCase(0, "Congratulations o! You have won!")]
        [TestCase(1, "Congratulations x! You have won!")]
        public void CanShowWinMessage(int winner, string expectedWinMessage)
        {
            input = new Stack<string>();
            
            _seeBoardResponse.Board = new int?[,]
            {
                {0, 1, 1},
                {null, 0, 1},
                {null, null, 0}
            };
            _seeBoardResponse.Winner = winner;
            _seeBoardResponse.HasGameEnded = true;
                
            Game.Start();

            Assert.AreEqual(
                new List<string> {" ABC", 
                    "1oxx", 
                    "2-ox", 
                    "3--o",
                    expectedWinMessage
                }
                , LineBuffer
            );
        }

        [Test]
        public void CanShowTieMessage()
        {
            _seeBoardResponse.Board = new int?[,]
            {
                {1, 0, 1},
                {0, 1, 0},
                {0, 1, 0}
            };
            _seeBoardResponse.HasGameEnded= true;

            Game.Start();

            Assert.AreEqual(
                new List<string> {" ABC", 
                    "1xox", 
                    "2oxo", 
                    "3oxo",
                    "It's a tie. Game over!"
                }
                , LineBuffer
            );
        }
        
        [Test]
        public void WhenInvalidInput_ReTry()
        {
            input.Push("A1");
            input.Push("~");

            Game.Start();

            Assert.That(PlacedPieceX, Is.EqualTo(0));
            Assert.That(PlacedPieceY, Is.EqualTo(0));
        }
    }
}
