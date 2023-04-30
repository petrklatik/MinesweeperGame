using MinesweeperGame.Helpers;
using MinesweeperGame.Model;
using MinesweeperGame.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MinesweeperGame.ViewModel
{
    public class GameViewModel : NotifyPropertyChangedBase
    {
        public BoardModel Board { get; }
        private DispatcherTimer timer;
        private int seconds;

        // Collection which holds all of the NodeViewModel objects for the game
        public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();

        // Gets a value indicating whether the game is won by checking if all non-mine nodes are revealed and all mines are flagged
        public bool IsGameWon => RevealedNodesCount == (Nodes.Count - Board.NumberOfBombs) && FlaggedMinesCount == Board.NumberOfBombs;
        public bool IsFirstClick { get; set; } = true;

        // Properties that represents the size of the generated gameboard
        public int GameWidth => Board.Width * 30;
        public int GameHeight => Board.Height * 30;

        public int SecondsTimer
        {
            get => seconds;
            set
            {
                seconds = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel(BoardModel board)
        {
            Board = board;
            GenerateBoard();
            SetTimer();
        }
        private int RevealedNodesCount => Nodes.Count(node => node.IsRevealed);
        private int FlaggedMinesCount => Nodes.Count(node => node.IsMine && node.IsFlagged);

        // Generates NodeViewModel objects for each node on the game board and adds them to the Nodes collection
        private void GenerateBoard()
        {
            for (int row = 0; row < Board.Height; row++)
            {
                for (int col = 0; col < Board.Width; col++)
                {
                    var nodeViewModel = new NodeViewModel(new NodeModel(), this);
                    Nodes.Add(nodeViewModel);
                }
            }
        }
        public void PopulateWithMines(NodeViewModel startingNode) => GameInitializer.GenerateBombs(Board, Nodes, Nodes.IndexOf(startingNode));

        // Initializes the timer object, sets its interval to one second, and starts it
        private void SetTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (sender, e) => SecondsTimer++;
            timer.Start();
        }

        public async void EndGame(bool isVictory)
        {
            timer.Stop();
            if (isVictory)
            {
                MessageBox.Show("YOU WON! WELL DONE!");
            }
            else
            {
                MessageBox.Show("YOU WERE BOMBED! GAME OVER!");
                ShowAllNodes();
                await Task.Delay(3000);
            }

            //TODO: close current and open menu window
            Application.Current.Shutdown();
        }

        private void ShowAllNodes()
        {
            foreach (var node in Nodes)
            {
                node.IsRevealed = true;
            }
        }

        public void RevealZeroAdjacentNodes(NodeViewModel nodeViewModel)
        {
            nodeViewModel.IsRevealed = true;

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (BoardHelper.IsNeighbor(Board, Nodes.IndexOf(nodeViewModel), rowOffset, colOffset))
                    {
                        int neighborIndex = BoardHelper.GetNeighborIndex(Board, Nodes.IndexOf(nodeViewModel), rowOffset, colOffset);
                        var neighborNode = Nodes[neighborIndex];

                        if (!neighborNode.IsRevealed && neighborNode.AdjacentMines == 0)
                        {
                            RevealZeroAdjacentNodes(neighborNode);
                        }
                        else
                        {
                            neighborNode.IsRevealed = true;
                        }
                    }
                }
            }
        }
    }
}
