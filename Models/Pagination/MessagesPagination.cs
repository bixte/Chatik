using Chatik.DataModels;
using System.Collections.Generic;

namespace Chatik.Models.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items )
        {
            AddRange(items);
        }
    }
}
