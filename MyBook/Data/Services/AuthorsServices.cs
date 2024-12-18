﻿using MyBook.Data.Models;
using MyBook.Data.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyBook.Data.Services
{
    public class AuthorsServices
    {
        private AppDbContext _context;

        public AuthorsServices(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public List<Author> GetAllAuthors() => _context.Authors.ToList();

        public Author GetAuthorById(int id) => _context.Authors.FirstOrDefault(n => n.Id == id);

        public Author UpdateAuthorById(int id, AuthorVM author)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);

            if (_author != null)
            {
                _author.FullName = author.FullName;

                _context.SaveChanges();
            }

            return _author;
        }

        public void DeleteAuthorById(int id)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);

            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int AuthorId)
        {
            var _author = _context.Authors.Where(n => n.Id == AuthorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
    }
}
