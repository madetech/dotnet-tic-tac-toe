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
        private int CurrentPieceType;

        public GameUI(IConsole console,
            ISeeBoard seeBoard,
            IPlacePiece placePiece)
        {
            _console = console;
            _seeBoard = seeBoard;
            _placePiece = placePiece;
            CurrentPieceType = 0;
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

                PrintBoard(boardResponse.Board);

                var input = _console.ReadLine();
                
                if (input == "exit") return;
                
                if (IsInputValid(input))
                {
                    HandleInput(input);                    
                    _console.WriteLine(string.Empty);   
                }
            }
        }

        private static bool IsInputValid(string input)
        {
            return Regex.IsMatch(input, "^[A-C][1-3]$");
        }

        private void PrintBoard(int?[,] board)
        {
            for (var x = 0; x < board.GetLength(0); x++)
            {
                var buffer = "";
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    buffer += Symbol(board, x, y);
                }

                _console.WriteLine(buffer);
            }
        }

        private void HandleInput(string input)
        {
            var x = input[0] - AsciiValueA;
            var y = int.Parse(input[1].ToString()) - 1;

            _placePiece.Execute(CurrentPieceType, x, y);

            CurrentPieceType = CurrentPieceType == 0 ? 1 : 0;
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