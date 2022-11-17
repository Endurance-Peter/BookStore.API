using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();

            return records;
        }
        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            //var records = await _context.Books.Where(x=>x.Id==bookId).Select(x => new BookModel
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).FirstOrDefaultAsync();

            //return records;
            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);
        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            //var book = new Books
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description
            //};
            var book = _mapper.Map<Books>(model);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }
        public async Task UpdateBookAsync(int bookId, BookModel book)
        {
            var records = await _context.Books.FindAsync(bookId);

            if (records!=null)
            {
                //records.Id = book.Id;
                //records.Title = book.Title;
                //records.Description = book.Description;

                var newBook = new Books
                {
                    Id = book.Id,
                    Description = book.Description,
                    Title = book.Title
                };
                _context.Books.Update(newBook);
                _context.SaveChanges();
            }
        }
    }
}
