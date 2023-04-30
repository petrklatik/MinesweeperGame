using MinesweeperGame.Helpers;
using MinesweeperGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MinesweeperGame.Model
{
    public static class GameInitializer
    {
        //  Generates bombs on the board and then calculates adjacent mines for each node
        public static void GenerateBombs(BoardModel board, ObservableCollection<NodeViewModel> nodes, int startingIndex)
        {
            Random random = new();
            int freeCells = 1; // Starting from 1 because startingIndex is added when initializing
            int totalCells = nodes.Count;
            int totalBombs = board.NumberOfBombs;

            HashSet<int> selectedIndexes = new() {startingIndex };

            // Loop through each neighbor of the startingIndex that they cannot be populated
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (BoardHelper.IsNeighbor(board, startingIndex, rowOffset, colOffset))
                    {
                        int neighborIndex = BoardHelper.GetNeighborIndex(board, startingIndex, rowOffset, colOffset);
                        selectedIndexes.Add(neighborIndex);
                        freeCells++;
                    }
                }
            }
            // Randomly selects cells to add to the selectedIndexes set until the set contains the total number of bombs.
            while (selectedIndexes.Count < (totalBombs + freeCells))
            {
                int randomIndex = random.Next(0, totalCells);
                if (!selectedIndexes.Contains(randomIndex))
                {
                    selectedIndexes.Add(randomIndex);
                    nodes[randomIndex].IsMine = true;
                }
            }
            CalculateAdjacentMines(board, nodes);
        }

        private static void CalculateAdjacentMines(BoardModel board, ObservableCollection<NodeViewModel> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].AdjacentMines = CountMinesForNode(board, nodes, i);
            }
        }

        private static int CountMinesForNode(BoardModel board, ObservableCollection<NodeViewModel> nodes, int index)
        {
            int adjacentMines = 0;

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    adjacentMines += CountMinesInNeighbor(board, nodes, index, rowOffset, colOffset);
                }
            }
            return adjacentMines;
        }

        private static int CountMinesInNeighbor(BoardModel board, ObservableCollection<NodeViewModel> nodes, int index, int rowOffset, int colOffset)
        {
            int adjacentMines = 0;

            if (BoardHelper.IsNeighbor(board, index, rowOffset, colOffset))
            {
                int neighborIndex = BoardHelper.GetNeighborIndex(board, index, rowOffset, colOffset);

                if (nodes[neighborIndex].IsMine)
                {
                    adjacentMines++;
                }
            }
            return adjacentMines;
        }
    }
}