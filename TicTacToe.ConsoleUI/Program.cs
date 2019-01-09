using System;

namespace TicTacToe.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var inMemoryGatway = new InMemoryBoardGateway();
            var GameUI = new GameUI(new ConsoleAdapter(), new SeeBoard(inMemoryGatway), new PlacePiece(inMemoryGatway, inMemoryGatway));
            
            GameUI.Start();
        }
    }

    class ConsoleAdapter : GameUI.IConsole
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
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