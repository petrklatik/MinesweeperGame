# Minesweeper Game

This is a Minesweeper game implemented in C# using the MVVM architecture. The goal of the game is to clear a rectangular board containing hidden "mines" without detonating any of them.

## Getting Started

1. Clone the repository to your local machine.
2. Open the solution file `MinesweeperGame.sln` in Visual Studio or any other compatible IDE.
3. Run the program from within the IDE.

## Features

- Customizable game settings including board size and difficulty.
- The board is generated using an algorithm that ensures the first tile clicked is always a safe tile.
- The game follows the classic Minesweeper rules, including marking tiles as potential mines and revealing surrounding tiles when a tile is clicked.
- The program is implemented using the MVVM architecture for cleaner code and easier testing.

## TODO

- Implement functionality to open a new game window when the "Start Game" button is clicked.
- Create the game grid based on user input from the menu.
- Create the algorithm for generating the board with the feature of first safe click.
- Add functionality for revealing multiple tiles at once when a safe tile is clicked.
- Implenent the Win and Lose conditions.
- Implement a timer to keep track of the player's score.
