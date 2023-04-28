using MinesweeperGame.Model;
using MinesweeperGame.Services;
using System;
using System.Collections.ObjectModel;
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
                    var nodeViewModel = new NodeViewModel(new NodeModel());
                    Nodes.Add(nodeViewModel);
                }
            }
            GenerateBombs();
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

        //WILL BE REMOVED, just trying if everything displays correctly
        private void GenerateBombs()
        {
            Random random = new();
            int totalCells = Board.Height * Board.Width;
            int totalBombs = Board.NumberOfBombs;

            for (int i = 0; i < totalBombs; i++)
            {
                int randomIndex = random.Next(0, totalCells);

                while (Nodes[randomIndex].IsMine)
                {
                    randomIndex = random.Next(0, totalCells);
                }

                Nodes[randomIndex].IsMine = true;
            }
        }
    }
}
