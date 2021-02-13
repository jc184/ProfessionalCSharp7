﻿using BooksLib.Models;
using BooksLib.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksService : IBooksService
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private IBooksRepository _booksRepository;
        public BooksService(IBooksRepository repository)
        {
            _booksRepository = repository;
        }

        public async Task<IEnumerable<Book>> LoadBooksAsync()
        {
            if (_books.Count > 0) return _books;

            IEnumerable<Book> books = await _booksRepository.GetItemsAsync();
            _books.Clear();
            foreach (var b in books)
            {
                _books.Add(b);
            }

            return books;
        }

        public Book GetBook(int bookId) =>
            _books.SingleOrDefault(b => b.BookId == bookId);

        public async Task<Book> AddOrUpdateBookAsync(Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));

            Book updated = null;
            if (book.BookId == 0)
            {
                updated = await _booksRepository.AddAsync(book);
                _books.Add(updated);
            }
            else
            {
                updated = await _booksRepository.UpdateAsync(book);
                if (updated == null) throw new InvalidOperationException();

                Book old = _books.Single(b => b.BookId == updated.BookId);
                int ix = _books.IndexOf(old);
                _books.RemoveAt(ix);
                _books.Insert(ix, updated);
            }
            return updated;
        }

        public IEnumerable<Book> Books => _books;
    }
}
