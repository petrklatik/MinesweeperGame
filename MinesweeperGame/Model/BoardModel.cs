using MinesweeperGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MinesweeperGame.Model
{
    public class BoardModel
    {
        public int Height { get; }
        public int Width { get; }
        public int NumberOfBombs { get; }

        public BoardModel(string gameSize, int gameDifficulty)
        {
            int[] sizes = ParseGameSize(gameSize);
            Height = sizes[0];
            Width = sizes[1];

            NumberOfBombs = CalculateNumberOfBombs(Height, Width, gameDifficulty);
        }

        private static int[] ParseGameSize(string gameSize)
        {
            int[] sizes = new int[2];
            MatchCollection matches = Regex.Matches(gameSize, @"\d+");

            for (int i = 0; i < 2; i++)
            {
                sizes[i] = int.Parse(matches[i].Value);
            }
            return sizes;
        }

        private static int CalculateNumberOfBombs(int height, int width, int difficultyIndex)
        {
            int numberOfCells = height * width;
            double percentBombs = difficultyIndex / 100.0;
            int numberOfBombs = (int)Math.Round(numberOfCells * percentBombs);
            return numberOfBombs;
        }
    }
}
