using MinesweeperGame.Model;

namespace MinesweeperGame.Helpers
{
    public static class BoardHelper
    {
        public static bool IsNeighbor(BoardModel board, int index, int rowOffset, int colOffset)
        {
            CalculateNeighborRowAndColumn(board, index, rowOffset, colOffset, out int neighborRow, out int neighborCol);

            return neighborRow >= 0 && neighborRow < board.Height &&
                   neighborCol >= 0 && neighborCol < board.Width &&
                   !(rowOffset == 0 && colOffset == 0);
        }

        public static int GetNeighborIndex(BoardModel board, int index, int rowOffset, int colOffset)
        {
            CalculateNeighborRowAndColumn(board, index, rowOffset, colOffset, out int neighborRow, out int neighborCol);
            return (neighborRow * board.Width) + neighborCol;
        }

        private static void CalculateNeighborRowAndColumn(BoardModel board, int index, int rowOffset, int colOffset, out int neighborRow, out int neighborCol)
        {
            neighborRow = (index / board.Width) + rowOffset;
            neighborCol = (index % board.Width) + colOffset;
        }
    }
}
