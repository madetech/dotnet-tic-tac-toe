using System.Collections.Generic;
using System.Linq;
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
        private List <int> PlacedPieceTypes { get; set; }        

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

        public void Execute(int pieceType, int x, int y)
        {
            PlacedPieceTypes.Add(pieceType);
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
            PlacedPieceTypes = new List<int>();
        }

        [Test]
        public void CanDisplayEmptyBoard()
        {
            Game.Start();

            Assert.AreEqual(
                new List<string> {"---", "---", "---"}
                , LineBuffer
            );
        }

        [Test]
        public void CanDisplayBoard()
        {
            _seeBoardResponse.Board[1, 2] = 1;

            Game.Start();

            Assert.AreEqual(
                new List<string> {"---", "--x", "---"}
                , LineBuffer
            );
        }

        [Test]
        public void CanDisplayBoard2()
        {
            _seeBoardResponse.Board[1, 2] = 0;

            Game.Start();

            Assert.AreEqual(
                new List<string> {"---", "--o", "---"}
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
                new List<string> {"xxx", "xxx", "xxx"}
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
                new List<string> {"ooo", "ooo", "ooo"}
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
        public void CanPlaceAPiece3()
        {
            input.Push("C2");

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
                new List<string>
                {
                    "---", 
                    "---", 
                    "---", 
                    string.Empty,
                    "---", 
                    "---", 
                    "---"
                }, 
                LineBuffer
            );
        }

        [Test]
        public void PlacedPieceTypeIsEqualTo0()
        {
            input.Push("C2");
            
            Game.Start();
            
            Assert.AreEqual(0, PlacedPieceTypes.Single());        
        }

        [Test]
        public void WhenInvalidInput_ReTry()
        {
            input.Push("A1");
            input.Push("~");

            Game.Start();

            Assert.That(PlacedPieceX, Is.EqualTo(0));
            Assert.That(PlacedPieceY, Is.EqualTo(0));
            Assert.AreEqual(0, PlacedPieceTypes.Single());
        }

        [Test]
        public void WhenANewPieceIsPlaced_MustBeDifferentFrom_ThePrevious()
        {
            input.Push("A1");
            input.Push("A1");
            input.Push("A1");
            
            Game.Start();

            Assert.AreEqual(new List <int> {0, 1, 0}, PlacedPieceTypes);
        }

    }
}
