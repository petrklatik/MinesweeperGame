using MinesweeperGame.Model;
using MinesweeperGame.Services;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MinesweeperGame.ViewModel
{
    public class NodeViewModel : NotifyPropertyChangedBase
    {
        private readonly NodeModel Node;
        private readonly GameViewModel gameViewModel;

        // Commands to handle left and right clicks on the node
        public ICommand LeftClickCommand { get; }
        public ICommand RightClickCommand { get; }

        // Determines whether various UI elements should be visible for the node
        public Visibility NodeImageVisibility => (IsFlagged || IsMine) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility EllipseVisibility => (IsFlagged || IsRevealed) ? Visibility.Collapsed : Visibility.Visible;
        public Visibility NumberVisibility => (IsRevealed && AdjacentMines > 0 && !IsMine) ? Visibility.Visible : Visibility.Collapsed;

        // The number of mines adjacent to this node
        public int AdjacentMines { get; set; }

        public bool IsMine
        {
            get => Node.IsMine;
            set
            {
                Node.IsMine = value;
                OnPropertyChanged(nameof(NodeImageVisibility));
                OnPropertyChanged(nameof(EllipseVisibility));
                OnPropertyChanged(nameof(NodeImage));
            }
        }

        public bool IsRevealed
        {
            get => Node.IsRevealed;
            set
            {
                Node.IsRevealed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NodeImageVisibility));
                OnPropertyChanged(nameof(EllipseVisibility));
                OnPropertyChanged(nameof(NumberVisibility));

                IsFlagged = false;
            }
        }

        public bool IsFlagged
        {
            get => Node.IsFlagged;
            set
            {
                Node.IsFlagged = value;
                OnPropertyChanged(nameof(NodeImageVisibility));
                OnPropertyChanged(nameof(EllipseVisibility));
                OnPropertyChanged(nameof(NodeImage));
            }
        }

        public NodeViewModel(NodeModel node, GameViewModel gameViewModel)
        {
            Node = node;
            this.gameViewModel = gameViewModel;

            // Initialize left and right click commands
            LeftClickCommand = new RelayCommand(ExecuteLeftClick);
            RightClickCommand = new RelayCommand(ExecuteRightClick);
        }

        // Returns the image to display for the node
        public ImageSource NodeImage
        {
            get
            {
                if (IsFlagged)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/MinesweeperGame;component/Assets/flag-48.png"));
                }
                else if (IsMine && IsRevealed)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/MinesweeperGame;component/Assets/bomb-48.png"));
                }
                else
                {
                    return null;
                }
            }
        }
        private void ExecuteLeftClick()
        {
            if (IsRevealed) return;
            if (IsMine)
            {
                MessageBox.Show("YOU WERE BOMBED! GAME OVER!");
                gameViewModel.GameOver();
            }
            else if (AdjacentMines == 0)
            {
                gameViewModel.RevealZeroAdjacentNodes(this);
            }
            else
            {
                IsRevealed = true;
            }
        }
        private void ExecuteRightClick() => IsFlagged = !IsFlagged;
    }
}
