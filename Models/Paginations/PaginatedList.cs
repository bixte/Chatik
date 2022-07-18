using Chatik.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatik.Models.Paginations
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int MaxPage { get; private set; }

        public bool HasPrev => CurrentPage > 1;
        public bool HasNext => CurrentPage < MaxPage;
        private PaginatedList(List<T> items, int count, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            MaxPage = (int)Math.Ceiling(count / (decimal)pageSize);
            AddRange(items);
        }

        static public async Task<PaginatedList<T>> CreateAsync(IQueryable<T> sourse, int currentPage, int pageSize)
        {
            int count = await sourse.CountAsync();
            var items = await sourse.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, currentPage, pageSize);
        }

       

        static public  PaginatedList<T> Create(IEnumerable<T> sourse, int currentPage, int pageSize)
        {
            int count = sourse.Count();
            var items = sourse.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, currentPage, pageSize);
        }


    }
}
