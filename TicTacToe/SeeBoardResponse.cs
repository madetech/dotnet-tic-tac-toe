namespace TicTacToe
{
    public class SeeBoardResponse
    {
        public int? Winner { get; set; }
        public int?[,] Board { get; set; }
    }
}