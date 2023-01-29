using Xunit;

namespace Proxx.Tests
{
    public class ProxxTests
    {
        [Fact]
        public void GivenProxxModel_WhenPassingBlackHolesAmount_EnsuresBlackHolesAreAddedCorrectly()
        {
            var boardSize = 4;
            var blackHolesCount = 2;

            var sut = new ProxxModel(boardSize, blackHolesCount);

            var actualBlackHolesCount = 0;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (sut.Cells[i, j].IsBlackHole)
                    {
                        actualBlackHolesCount++;
                    }
                }
            }

            Assert.Equal(sut.Cells.Length, boardSize*boardSize);
            Assert.Equal(blackHolesCount, actualBlackHolesCount);
        }


        [Fact]
        public void GivenProxxModel_WhenBlackHoleIsOpened_ReturnsFalse()
        {
            var boardSize = 4;
            var blackHolesCount = 2;
            
            var sut = new ProxxModel(boardSize, blackHolesCount);

            var selectionResult = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (sut.Cells[i, j].IsBlackHole)
                    {
                        selectionResult = sut.OpenCell(i, j);
                        break;
                    }
                }
                
                if (!selectionResult)
                {
                    break;
                }
            }

            Assert.False(selectionResult);
            Assert.True(sut.IsGameFinished);
        }

        [Fact]
        public void GivenProxxModel_WhenNoBlackHoleIsOpened_ReturnsTrue()
        {
            var boardSize = 4;
            var blackHolesCount = 2;

            var sut = new ProxxModel(boardSize, blackHolesCount);

            var selectionResult = false;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (!sut.Cells[i, j].IsBlackHole)
                    {
                        selectionResult = sut.OpenCell(i, j);
                        break;
                    }
                }

                if (selectionResult)
                {
                    break;
                }
            }

            Assert.True(selectionResult);
            Assert.False(sut.IsGameFinished);
        }
    }
}
