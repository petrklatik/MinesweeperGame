using MinesweeperGame.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace MinesweeperGame.Model
{
    public static class GameInitializer
    {
        public static void GenerateBombs(BoardModel board, ObservableCollection<NodeViewModel> nodes)
        {
            Random random = new();
            int totalCells = board.Height * board.Width;
            int totalBombs = board.NumberOfBombs;

            for (int i = 0; i < totalBombs; i++)
            {
                int randomIndex = random.Next(0, totalCells);

                while (nodes[randomIndex].IsMine)
                {
                    randomIndex = random.Next(0, totalCells);
                }

                nodes[randomIndex].IsMine = true;
            }
        }

        public static void CalculateAdjacentMines(BoardModel board, ObservableCollection<NodeViewModel> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                int adjacentMines = CountAdjacentMines(board, nodes, i);
                nodes[i].AdjacentMines = adjacentMines;
            }
        }

        private static int CountAdjacentMines(BoardModel board, ObservableCollection<NodeViewModel> nodes, int index)
        {
            int adjacentMines = 0;

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (IsNeighbor(board, index, rowOffset, colOffset))
                    {
                        int neighborIndex = GetNeighborIndex(board, index, rowOffset, colOffset);

                        if (nodes[neighborIndex].IsMine)
                        {
                            adjacentMines++;
                        }
                    }
                }
            }
            return adjacentMines;
        }

        public static bool IsNeighbor(BoardModel board, int index, int rowOffset, int colOffset)
        {
            int neighborRow = (index / board.Width) + rowOffset;
            int neighborCol = (index % board.Width) + colOffset;

            return neighborRow >= 0 && neighborRow < board.Height &&
                   neighborCol >= 0 && neighborCol < board.Width &&
                   !(rowOffset == 0 && colOffset == 0);
        }

        public static int GetNeighborIndex(BoardModel board, int index, int rowOffset, int colOffset)
        {
            int neighborRow = (index / board.Width) + rowOffset;
            int neighborCol = (index % board.Width) + colOffset;
            return (neighborRow * board.Width) + neighborCol;
        }

    }
}