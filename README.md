# Minesweeper Game

**Author: Petr Klátík**

This is a classic Minesweeper game implemented using C#, WPF and the MVVM design pattern. The game features different levels of difficulty and board sizes.

## Getting Started

1. Clone the repository to your local machine.
2. Open the solution file `MinesweeperGame.sln` in Visual Studio or any other compatible IDE.
3. Run the program from within the IDE.

## How to Play

The objective of the game is to clear a rectangular board containing hidden mines without detonating any of them, with the help of clues about the number of neighboring mines in each field.

1. To reveal a tile, simply left-click on it. If the tile contains a mine, the game is over. If it is a non-mine tile, a number will be revealed, indicating how many mines are adjacent to that tile.

2. To flag a tile that you suspect contains a mine, simply right-click on it. This will place a flag on the tile, which can be used to mark it as potentially containing a mine. To remove a flag, simply right-click on the flagged tile again.

The game is won when all non-mine squares are revealed, or lost when a mine is revealed.

## Features

- Game settings including board size and difficulty.
- The board is be generated using an algorithm that ensures the first tile clicked is always a safe tile.
- The game follows the classic Minesweeper rules, including marking tiles as potential mines and revealing surrounding tiles when a safe tile is clicked.
- Timer to track the duration of the game.

## TODO
- Improve the Window management (creating some sort of window manager).
