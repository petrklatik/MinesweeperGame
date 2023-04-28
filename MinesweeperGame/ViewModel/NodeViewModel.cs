using MinesweeperGame.Model;
using MinesweeperGame.Services;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MinesweeperGame.ViewModel
{
    public class NodeViewModel : NotifyPropertyChangedBase
    {
        private readonly NodeModel Node;

        public ICommand LeftClickCommand { get; }
        public ICommand RightClickCommand { get; }

        public Visibility NodeImageVisibility => (IsFlagged || IsMine) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility EllipseVisibility => (IsFlagged || IsRevealed) ? Visibility.Collapsed : Visibility.Visible;

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

        public int AdjacentMines
        {
            get => Node.AdjacentMines;
            set
            {
                Node.AdjacentMines = value;
                OnPropertyChanged();
            }
        }

        public NodeViewModel(NodeModel node)
        {
            Node = node;

            LeftClickCommand = new RelayCommand(ExecuteLeftClick);
            RightClickCommand = new RelayCommand(ExecuteRightClick);
        }

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
        private void ExecuteLeftClick() => IsRevealed = true;
        private void ExecuteRightClick() => IsFlagged = !IsFlagged;

    }
}
