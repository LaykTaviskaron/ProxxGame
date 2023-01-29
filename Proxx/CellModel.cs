namespace Proxx
{
    public class CellModel
    {
        public bool IsBlackHole { get; set; }

        public bool IsVisible { get; set; }

        public bool IsVisited { get; set; }

        public int AdjacentBlackHolesCount { get; set; }
    }
}
