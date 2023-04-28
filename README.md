# Minesweeper Game

This is a Minesweeper game implemented in C# using the MVVM architecture. The goal of the game is to clear a rectangular board containing hidden "mines" without detonating any of them.

## Getting Started

1. Clone the repository to your local machine.
2. Open the solution file `MinesweeperGame.sln` in Visual Studio or any other compatible IDE.
3. Run the program from within the IDE.

## Features

- Customizable game settings including board size and difficulty.
- The board will be generated using an algorithm that ensures the first tile clicked is always a safe tile.
- The game will follow follow the classic Minesweeper rules, including marking tiles as potential mines and revealing surrounding tiles when a tile is clicked.
- The program will be implemented using the MVVM architecture for cleaner code and easier testing.

## TODO

- Create the algorithm for generating the bombs with the feature of first safe click.
- Implement the game logic
- Add functionality for revealing multiple tiles at once when a safe tile is clicked.
- Implenent the Win and Lose conditions.
