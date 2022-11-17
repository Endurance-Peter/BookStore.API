using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Helper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();
            //or
            //CreateMap<BookModel,Books>();// in other to map bookmodel to books
        }
    }
}
