namespace API.Specifications
{
    public class OrdenSpecParams
    {
        private int _pageSize = 6;
        private string _search;
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Sort { get; set; }
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}