﻿using MyBook.Data.Models;
using MyBook.Data.Paging;
using MyBook.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBook.Data.Services
{
    public class PublishersServices
    {
        private AppDbContext _context;

        public PublishersServices(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }


        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var _allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_descending":
                        _allPublishers = _allPublishers.OrderByDescending(n => n.Name).ToList();
                    break;

                    default:
                    
                    break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                _allPublishers = _allPublishers
                    .Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            int pageSize = 5;
            _allPublishers = PaginatedList<Publisher>.Create(_allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return _allPublishers;  
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public Publisher UpdatePublisherById(int id, PublisherVM publisher)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (_publisher != null)
            {
                _publisher.Name = publisher.Name;

                _context.SaveChanges();
            }

            return _publisher;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList(),
            }).FirstOrDefault();

            return _publisherData;
        }
    }
}
