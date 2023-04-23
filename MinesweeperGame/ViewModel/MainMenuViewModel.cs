using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MinesweeperGame.Model;
using MinesweeperGame.Services;
using MinesweeperGame.View;

namespace MinesweeperGame.ViewModel
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public List<string> GameSizes { get; } = new List<string> { "Small (9x9)", "Medium (16x16)", "Large (32x32)" };
        private string _selectedGameSize;

        public List<string> GameDifficulties { get; } = new List<string>(GameDifficultiesMap.Keys);
        private string _selectedGameDifficulty;

        // Dictionary to map game difficulties to integer values
        private static readonly Dictionary<string, int> GameDifficultiesMap = new()
        {
            { "Easy", 10 },
            { "Medium", 20 },
            { "Hard", 30 }
        };

        public ICommand StartGameCommand { get; }

        public string SelectedGameSize
        {
            get => _selectedGameSize;
            set
            {
                _selectedGameSize = value;
                OnPropertyChanged();
                ((RelayCommand)StartGameCommand).RaiseCanExecuteChanged();
            }
        }

        public string SelectedGameDifficulty
        {
            get => _selectedGameDifficulty;
            set
            {
                _selectedGameDifficulty = value;
                OnPropertyChanged();
                ((RelayCommand)StartGameCommand).RaiseCanExecuteChanged();
            }
        }

        public MainMenuViewModel()
        {
            // Initialize the StartGameCommand with a RelayCommand
            StartGameCommand = new RelayCommand(StartGame, CanStartGame);
        }

        private bool CanStartGame()
        {
            // Logic to determine if the Start Game button can be enabled
            return !string.IsNullOrEmpty(SelectedGameSize) && !string.IsNullOrEmpty(SelectedGameDifficulty);
        }

        private void StartGame()
        {
            GameDifficultiesMap.TryGetValue(SelectedGameDifficulty, out int DifficultyValue);
            BoardModel boardModel = new(SelectedGameSize, DifficultyValue);
        }

        // Event and method for property changed notifications
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
