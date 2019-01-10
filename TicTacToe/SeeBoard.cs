using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace TicTacToe
{
    public class SeeBoard : ISeeBoard
    {
        private readonly IBoardReader _boardReader;

        public SeeBoard(IBoardReader boardReader)
        {
            _boardReader = boardReader;
        }

        public SeeBoardResponse Execute()
        {
            var board = _boardReader.Fetch();

            var winner0 = IsHorizontalWinner(board.Pieces, 0) || 
                          IsVerticalWinner(board.Pieces, 0) || 
                          IsDiagonalWinner(board.Pieces, 0);
            var winner1 = IsHorizontalWinner(board.Pieces, 1) || 
                          IsVerticalWinner(board.Pieces, 1) || 
                          IsDiagonalWinner(board.Pieces, 1);
            var presentableWinner = ToPresentableWinner(winner0, winner1);
            
            var response = new SeeBoardResponse
            {
                Board = board.Pieces,
                Winner = presentableWinner
            };

            return response;
        }

        private bool IsDiagonalWinner(int?[,] boardPieces, int pieceType)
        {
            return boardPieces[0, 0] == pieceType && boardPieces[1, 1] == pieceType && boardPieces[2, 2] == pieceType ||
                   boardPieces[2, 0] == pieceType && boardPieces[1, 1] == pieceType && boardPieces[0, 2] == pieceType;
        }


        private static int? ToPresentableWinner(bool winner0, bool winner1)
        {
            if (winner0) return 0;

            if (winner1) return 1;

            return null;
        }

        private bool IsVerticalWinner(int?[,] board, int pieceType)
        {
            for(var column = 0; column < board.GetLength(0); column++)
            {
                if (board[0, column] == pieceType && 
                    board[1, column] == pieceType && 
                    board[2, column] == pieceType) return true;
            }

            return false;
        }

        private bool IsHorizontalWinner(int?[,] board, int pieceType)
        {
            for(var row = 0; row < board.GetLength(0); row++)
            {
                if (board[row, 0] == pieceType && 
                    board[row, 1] == pieceType && 
                    board[row, 2] == pieceType) return true;
            }

            return false;
        }
    }
}