namespace Core.DTOs
{
    public class Paginated<T>
    {
        public IEnumerable<T> Content { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 0;

        public int GetOffSet()
        {
            return (PageNumber * PageSize);
        }
    }
}
