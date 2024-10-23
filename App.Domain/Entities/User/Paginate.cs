using System.Collections.Generic;

namespace App.Domain
{
    public class Paginate<T>
    {
        public List<T> Items { get; private set; }
        public int TotalItems { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public Paginate(List<T> items, int totalItems, int pageIndex, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            PageIndex = pageIndex;
            TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

    }
}
