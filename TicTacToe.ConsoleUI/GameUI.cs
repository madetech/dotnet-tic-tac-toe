using System;
using System.Text.RegularExpressions;

namespace TicTacToe.ConsoleUI
{
    public class GameUI
    {
        private const int AsciiValueA = 65;
        private readonly IConsole _console;
        private readonly ISeeBoard _seeBoard;
        private readonly IPlacePiece _placePiece;

        public GameUI(IConsole console,
            ISeeBoard seeBoard,
            IPlacePiece placePiece)
        {
            _console = console;
            _seeBoard = seeBoard;
            _placePiece = placePiece;
        }

        public interface IConsole
        {
            void WriteLine(string line);
            string ReadLine();
        }

        public void Start()
        {
            while (true)
            {
                var boardResponse = _seeBoard.Execute();

                for (var x = 0; x < boardResponse.Board.GetLength(0); x++)
                {
                    var buffer = "";
                    for (var y = 0; y < boardResponse.Board.GetLength(1); y++)
                    {
                        buffer += Symbol(boardResponse.Board, x, y);
                    }

                    _console.WriteLine(buffer);
                }

                var input = _console.ReadLine();
                if (input == "exit") return;
                if (!Regex.IsMatch(input, "^[A-C][1-3]$")) continue;
            
                HandleInput(input);   
                
                _console.WriteLine(string.Empty);
            }
        }

        private void HandleInput(string input)
        {
            int x = input[0] - AsciiValueA;
            int y = int.Parse(input[1].ToString()) - 1;

            _placePiece.Execute(0, x, y);
        }

        private static string Symbol(int?[,] board, int x, int y)
        {
            switch (board[x, y])
            {
                case 0:
                    return "o";
                case 1:
                    return "x";
                default:
                    return "-";
            }
        }
    }
}