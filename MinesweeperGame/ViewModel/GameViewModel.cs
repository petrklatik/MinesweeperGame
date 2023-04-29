using MinesweeperGame.Model;
using MinesweeperGame.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MinesweeperGame.ViewModel
{
    public class GameViewModel : NotifyPropertyChangedBase
    {
        public BoardModel Board { get; }
        public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();

        public int GameWidth => Board.Width * 30;
        public int GameHeight => Board.Height * 30;

        private DispatcherTimer _timer;
        private int _seconds;
        public int SecondsTimer
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel(BoardModel board)
        {
            Board = board;
            GenerateBoard();
            SetTimer();
        }


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
            GameInitializer.GenerateBombs(Board, Nodes);
            GameInitializer.CalculateAdjacentMines(Board, Nodes);
        }

        // Initializes the timer object, sets its interval to one second, and starts it
        private void SetTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SecondsTimer++;
        }

        public async void GameOver()
        {
            _timer.Stop();
            ShowAllNodes();
            await Task.Delay(3000);

            //TODO: open and close the windows
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
                    if (GameInitializer.IsNeighbor(Board, Nodes.IndexOf(nodeViewModel), rowOffset, colOffset))
                    {
                        int neighborIndex = GameInitializer.GetNeighborIndex(Board, Nodes.IndexOf(nodeViewModel), rowOffset, colOffset);
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
