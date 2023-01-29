using System;

namespace Proxx
{
    public class ProxxModel
    {
        private const int MaximumBoardSize = 1000;
        private const int MinimumBoardSize = 2;

        private Random random;

        public bool IsGameFinished { get; private set; }
        public int BoardSize { get; private set; }
        public int BlackHolesNumber { get; private set; }
        public CellModel[,] Cells { get; private set; }

        public ProxxModel(int boardSize, int blackHolesNumber)
        {
            if (MinimumBoardSize < 2)
            {
                throw new ArgumentException("The board size is too small");
            }

            if (boardSize > MaximumBoardSize)
            {
                throw new ArgumentException("The board size is too big");
            }

            if (boardSize * boardSize <= blackHolesNumber)
            {
                throw new ArgumentException("Number of black holes is too big");
            }

            this.BoardSize = boardSize;
            this.BlackHolesNumber = blackHolesNumber;

            Initialize();
        }

        private void MakeAllBlackHolesVisible()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Cells[i, j].IsBlackHole)
                    {
                        Cells[i, j].IsVisible = true;
                    }
                }
            }
        }

        public bool OpenCell(int i, int j)
        {
            if (Cells[i, j].IsBlackHole)
            {
                // Uncovering all black holes since the game is finished
                IsGameFinished = true;
                MakeAllBlackHolesVisible();
                return false;
            }

            UpdateVisibleCells(i, j);
            return true;
        }

        private void UpdateVisibleCells(int x, int y)
        {
            if (Cells[x, y].AdjacentBlackHolesCount == 0)
            {
                DFS(x, y);
            }
            else
            {
                Cells[x, y].IsVisible = true;
            }
        }

        // Open other Cells recursively
        private void DFS(int x, int y)
        {
            if (Cells[x, y].IsVisited || Cells[x, y].IsBlackHole) return;
            Cells[x, y].IsVisited = true;
            Cells[x, y].IsVisible = true;
            
            if (Cells[x, y].AdjacentBlackHolesCount == 0)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < BoardSize && j >= 0 && j < BoardSize)
                        {
                            DFS(i, j);
                        }
                    }
                }
            }
        }

        private void ComputeAdjacentBlackHoles()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Cells[i, j].IsBlackHole)
                    {
                        // No calculations are performed for black holes
                        continue;
                    }

                    // Reseting the count of adjacent black holes and recalculating it
                    Cells[i, j].AdjacentBlackHolesCount = 0;
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize 
                                && Cells[x, y].IsBlackHole)
                            {
                                Cells[i, j].AdjacentBlackHolesCount++;
                            }
                        }
                    }
                }
            }
        }

        private void Initialize()
        {
            this.random = new Random();
            this.Cells = new CellModel[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Cells[i, j] = new CellModel();
                }
            }

            PlaceBlackHoles();
            ComputeAdjacentBlackHoles();
        }

        private void PlaceBlackHoles()
        {
            int placed = 0;
            while (placed < BlackHolesNumber)
            {
                int i = random.Next(BoardSize);
                int j = random.Next(BoardSize);

                if (!Cells[i, j].IsBlackHole)
                {
                    Cells[i, j].IsBlackHole = true;
                    placed++;
                }
            }
        }

        private int GetRandomNumberWithUniformDistribution(int range)
        {
            //System.Random class provides random numbers in uniform distribution
            return random.Next(range);
        }
    }
}
