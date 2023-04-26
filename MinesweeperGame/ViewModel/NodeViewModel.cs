using MinesweeperGame.Model;
using MinesweeperGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperGame.ViewModel
{
    public class NodeViewModel : NotifyPropertyChangedBase
    {
        private NodeModel Node;
        public bool IsMine
        {
            get => Node.IsMine;
            set
            {
                Node.IsMine = value;
                OnPropertyChanged();
            }
        }

        public bool IsRevealed
        {
            get => Node.IsRevealed;
            set
            {
                Node.IsRevealed = value;
                OnPropertyChanged();
            }
        }

        public bool IsFlagged
        {
            get => Node.IsFlagged;
            set
            {
                Node.IsFlagged = value;
                OnPropertyChanged();
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

        public NodeViewModel(NodeModel _node)
        {
            Node = _node;
        }
    }
}
