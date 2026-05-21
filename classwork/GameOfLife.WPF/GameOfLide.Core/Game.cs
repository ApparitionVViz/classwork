using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLide.Core
{
    public class Game
    {
        public int Rows { get; }
        public int Cols { get; }

        public int Generation { get; private set; }

        private bool[,] _grid;
        public bool[,] Grid => (bool[,])_grid.Clone();

        private bool[,] _nextGrid;

        public Game(int rows = 20, int cols = 30)
        {
            Rows = rows;
            Cols = cols;
            Generation = 1;
            _grid = new bool[Rows, Cols];
            _nextGrid = new bool[Rows, Cols];
        }

        public void Clean()
        {
            Generation = 1;
            Array.Clear(_grid, 0, _grid.Length);
        }
        public void ToggleCell(int row, int col)
        {
            if (IsValidCell(row, col)) return;

            _grid[row, col] = !_grid[row, col];

        }

        private bool IsValidCell(int row, int col)
        {
            if (row <0 || row >= Rows || col < 0 || col >= Cols)
            {
                return false;
            }
            return true;
        }

        public void NextGeneration()
        {
            for (int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    // посчитать соседей
                    int n = CountNeighbours(i, j);

                    if (_grid[i, j])
                    {
                        if( n==2 || n == 3) _nextGrid[i, j] = true;
                        else _nextGrid[i, j] = false;
                    }
                    else
                    {
                        if (n == 3) _nextGrid[i, j] = true;
                        else _nextGrid[i, j] = false;
                    }

                }
            }
            Array.Copy(_nextGrid, _grid, _grid.Length);
            Generation += 1;
        }
        


        private int CountNeighbours(int row, int col)
        {
            int count = 0;
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int trgRow = row + i;
                    int trgCol = col + j;
                    if (!IsValidCell(trgRow, trgCol)) continue;

                    if (_grid[trgRow, trgCol])
                        count++;
                    
                }
            }
            return count;
        }
    }
}
