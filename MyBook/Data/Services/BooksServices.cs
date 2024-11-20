using MyBook.Data.Models;
using MyBook.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MyBook.Data.Services
{
    public class BooksServices
    {
        private AppDbContext _context;

        public BooksServices(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverURL = book.CoverURL,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId,
                
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorsId)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }


        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverURL = book.CoverURL,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public Book UpdateBookById(int id, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == id);

            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.DateRead;
                _book.Rate = book.Rate;
                _book.Genre = book.Genre;
                _book.CoverURL = book.CoverURL;
                _book.DateAdded = DateTime.Now;

                _context.SaveChanges();
            }

            return _book;
        }

        public void DeleteBookById(int id)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == id);

            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
