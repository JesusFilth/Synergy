using System;
using System.Threading;

namespace TetrisGame
{
    // Enumeration for the different types of Tetrominoes
    enum TetrominoType { I, O, T, S, Z, J, L }

    class Figure
    {
        public int[,] Shape { get; private set; }
        public int X { get; set; } // Horizontal position on the game field
        public int Y { get; set; } // Vertical position on the game field

        // Define the shapes of the Tetrominoes
        private static readonly int[,,] Tetrominoes = new int[7, 4, 4]
        {
            // I
            {
                { 0, 0, 0, 0 },
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            // O
            {
                { 0, 1, 1, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            // T
            {
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            // S
            {
                { 0, 1, 1, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            // Z
            {
                { 1, 1, 0, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            // J
            {
                { 1, 0, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            // L
            {
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            }
        };
        public Figure(TetrominoType type)
        {
            // Initialize the shape of the figure based on the type
            int size = Tetrominoes.GetLength(1);
            Shape = new int[size, size];
            for (int i = 0; i < Tetrominoes.GetLength(1); i++)
            {
                for (int j = 0; j < Tetrominoes.GetLength(2); j++)
                {
                    Shape[i, j] = Tetrominoes[(int)type, i, j];
                }
            }

            // Starting position
            X = 3;
            Y = 0;
        }

        // Rotate the figure clockwise
        public void Rotate()
        {
            int n = Shape.GetLength(0);
            int[,] rotatedShape = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    rotatedShape[j, n - i - 1] = Shape[i, j];

            Shape = rotatedShape;
        }
    }

    class GameField
    {
        public int Width { get; }
        public int Height { get; }
        public int[,] Field { get; }

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            Field = new int[height, width];
        }

        // Check if the figure can be placed at the position
        public bool IsPositionValid(Figure figure, int newX, int newY)
        {
            for (int i = 0; i < figure.Shape.GetLength(0); i++)
            {
                for (int j = 0; j < figure.Shape.GetLength(1); j++)
                {
                    if (figure.Shape[i, j] == 1)
                    {
                        int x = newX + j;
                        int y = newY + i;

                        if (x < 0 || x >= Width || y < 0 || y >= Height)
                            return false;
                        if (Field[y, x] == 1)
                            return false;
                    }
                }
            }

            return true;
        }

        // Merge the figure into the field when it cannot move down further
        public void MergeFigure(Figure figure)
        {
            for (int i = 0; i < figure.Shape.GetLength(0); i++)
            {
                for (int j = 0; j < figure.Shape.GetLength(1); j++)
                {
                    if (figure.Shape[i, j] == 1)
                    {
                        int x = figure.X + j;
                        int y = figure.Y + i;
                        if (y >= 0 && y < Height && x >= 0 && x < Width)
                        {
                            Field[y, x] = 1;
                        }
                    }
                }
            }
        }

        // Clear full lines
        public void ClearLines()
        {
            for (int i = Height - 1; i >= 0; i--)
            {
                bool isFullLine = true;

                for (int j = 0; j < Width; j++)
                {
                    if (Field[i, j] == 0)
                    {
                        isFullLine = false;
                        break;
                    }
                }
                if (isFullLine)
                {
                    // Move all lines above down
                    for (int k = i; k > 0; k--)
                    {
                        for (int l = 0; l < Width; l++)
                        {
                            Field[k, l] = Field[k - 1, l];
                        }
                    }
                    // Clear the top line
                    for (int l = 0; l < Width; l++)
                    {
                        Field[0, l] = 0;
                    }

                    i++; // Recheck the same line
                }
            }
        }

        // Draw the field and the current figure
        public void Draw(Figure figure)
        {
            Console.Clear();

            int[,] displayField = new int[Height, Width];
            // Copy the field
            Array.Copy(Field, displayField, Field.Length);

            // Overlay the figure
            for (int i = 0; i < figure.Shape.GetLength(0); i++)
            {
                for (int j = 0; j < figure.Shape.GetLength(1); j++)
                {
                    if (figure.Shape[i, j] == 1)
                    {
                        int x = figure.X + j;
                        int y = figure.Y + i;

                        if (y >= 0 && y < Height && x >= 0 && x < Width)
                        {
                            displayField[y, x] = 1;
                        }
                    }
                }
            }

            // Draw the field
            for (int i = 0; i < Height; i++)
            {
                Console.Write("|");

                for (int j = 0; j < Width; j++)
                {
                    Console.Write(displayField[i, j] == 1 ? "[]" : "  ");
                }

                Console.WriteLine("|");
            }
            // Draw the bottom border
            Console.WriteLine(new string('-', Width * 2 + 2));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int fieldWidth = 10;
            const int fieldHeight = 20;

            GameField gameField = new GameField(fieldWidth, fieldHeight);
            Random random = new Random();
            bool gameOver = false;

            while (!gameOver)
            {
                Figure currentFigure = new Figure((TetrominoType)random.Next(0, 7));
                bool figurePlaced = false;

                while (!figurePlaced)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                if (gameField.IsPositionValid(currentFigure, currentFigure.X - 1, currentFigure.Y))
                                    currentFigure.X--;
                                break;
                            case ConsoleKey.RightArrow:
                                if (gameField.IsPositionValid(currentFigure, currentFigure.X + 1, currentFigure.Y))
                                    currentFigure.X++;
                                break;
                            case ConsoleKey.UpArrow:
                                // Rotate the figure and check if the new position is valid
                                currentFigure.Rotate();
                                if (!gameField.IsPositionValid(currentFigure, currentFigure.X, currentFigure.Y))
                                {
                                    // If not valid, rotate back
                                    for (int i = 0; i < 3; i++) currentFigure.Rotate();
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                // Move down faster
                                if (gameField.IsPositionValid(currentFigure, currentFigure.X, currentFigure.Y + 1))
                                    currentFigure.Y++;
                                break;
                        }
                    }

                    // Move the figure down by one
                    if (gameField.IsPositionValid(currentFigure, currentFigure.X, currentFigure.Y + 1))
                    {
                        currentFigure.Y++;
                    }
                    else
                    {
                        // Merge the figure into the field
                        gameField.MergeFigure(currentFigure);
                        gameField.ClearLines();
                        figurePlaced = true;

                        // Check for game over
                        if (currentFigure.Y == 0)
                        {
                            gameOver = true;
                        }
                    }

                    gameField.Draw(currentFigure);
                    Thread.Sleep(500);
                }
            }

            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine("Press 'R' to restart or any other key to exit.");

            ConsoleKeyInfo restartKey = Console.ReadKey();

            if (restartKey.Key == ConsoleKey.R)
            {
                Main(args); // Restart the game
            }
        }
    }
}