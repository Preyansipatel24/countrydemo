﻿using System;

namespace CountryDemo.Models
{
    public class Pagination
    {

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pagination()
        {

        }

        public Pagination(int totalItems, int page, int pageSize = 10)
        {
           
                int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
                int currentPage = page;

                int startPage = currentPage - 5;
                int endpage = currentPage + 4;


                if (startPage <= 0)
                {
                    endpage = endpage - (startPage - 1);
                    startPage = 1;

                }
                if (endpage > totalPages)

                {
                    endpage = totalPages;
                    if (endpage > 10)
                    {
                        startPage = endpage - 9;
                    }
                }

                TotalItems = totalItems;
                CurrentPage = currentPage;
                PageSize = pageSize;
                TotalPages = totalPages;
                StartPage = startPage;
                EndPage = endpage;
            }
        }
    
}
