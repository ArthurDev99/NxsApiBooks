using ApiBooks.Contracts;
using ApiBooks.Helpers;
using ApiBooks.Models.DataModels;
using ApiBooks.Models.DataModels.Books;
using ApiBooks.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NxsApiBooks.Repositories
{
    public class BookRepository : ITransactionsDbModel<BookDM>
    {
        #region Initializers
        private DBContext ContextApp;

        public BookRepository(DBContext Context) 
        {
           this.ContextApp = Context;
        }
        #endregion

        #region TransactionalDbOperations

        #region properFunctions
        // GET FILTER BOOKS
        public List<BookItemListDM> filterBooks(string author = "", string title = "", int year = 0) 
        {
            var repo = ContextApp.Book.ToList();
            if (!String.IsNullOrEmpty(author))
            {
                repo = (from b in repo
                        join a in ContextApp.Author on b.AuthorId equals a.AuthorId
                        where a.Name.Contains(author) select b).ToList();
            }

            if (!String.IsNullOrEmpty(title))
            {
                repo = repo.Where(X => X.Title.Contains(title)).ToList();
            }

            if (year != 0)
            {
                repo = repo.Where(X => X.PublicationYear == year).ToList();
            }

            
            var resultFilter = (from b in repo
                                join a in ContextApp.Author on b.AuthorId equals a.AuthorId
                                select new BookItemListDM
                                {
                                    AuthorId = a.AuthorId,
                                    BookId = b.BookId,
                                    AuthorName = a.Name,
                                    Title = b.Title,
                                    Gender = b.Gender,
                                    PublicationYear = b.PublicationYear,
                                    NumberPages = b.NumberPages
                                }).OrderByDescending(X => X.BookId).ToList();
            return resultFilter;
        }

        //GET ALLL BOOKS
        public List<BookItemListDM> getList()
        {
            // GET BOOKS LIST
            var booksFromDB = ContextApp.Book.ToList();

            List<BookItemListDM> booksList = (from b in booksFromDB
                                              join a in ContextApp.Author on b.AuthorId equals a.AuthorId
                                              orderby b.BookId
                                              select new BookItemListDM
                                              {
                                                  BookId = b.BookId,
                                                  AuthorId = b.AuthorId,
                                                  AuthorName = a.Name,
                                                  Gender = b.Gender,
                                                  NumberPages = b.NumberPages,
                                                  PublicationYear = b.PublicationYear,
                                                  Title = b.Title
                                              }).ToList();

            return booksList;
        }
        #endregion

        #region ImplementsFunctions        

        //INSERT A NEW BOOK
        public BasicResponse insertNew(BookDM param_book)
        {
                if (param_book != null) 
                {
                bool validate_author_exist = ContextApp.Author.Where(x => x.AuthorId == param_book.AuthorId).FirstOrDefault() != null;

                    if (validate_author_exist) 
                    {
                        // CREATE A NEW BOOK
                        Book new_book = new Book();
                        new_book.BookId = getLastId();
                        new_book.AuthorId = param_book.AuthorId;
                        new_book.Title = param_book.Title;
                        new_book.Gender = param_book.Gender;
                        new_book.NumberPages = param_book.NumberPages;
                        new_book.PublicationYear = param_book.PublicationYear;

                        //INSERT BOOK
                        ContextApp.Book.Add(new_book);
                        bool save_changes = ContextApp.SaveChanges() > 0;

                        return save_changes ? ResponseHelper.basicResponse(200, "Se ha registrado el libro exitosamente.")
                        : ResponseHelper.basicResponse(400, "No se pudo registrar el libro");
                        
                    }
                }

            return ResponseHelper.basicResponse(400, "El autor del libro no fué encontrado.");
            
        }

        // GET LAST ID FROM TABLE BOOKS
        public decimal getLastId()
        {
            Book last_book = ContextApp.Book.OrderByDescending(x => x.BookId).FirstOrDefault();
            return last_book != null ? (last_book.BookId + 1) : 1 ;
        }
        #endregion

        #endregion

        #region ResponseBuilders        
        public static SampleResponseBook buildApiSampleResponse(int code = 0, string message = "", BookDM book = null) 
        {
            return new SampleResponseBook() {
                code = code,
                message = message,
                data = book
            };
        }
        public static ListResponseBook buildApiListResponseBook(int code = 0, string message = "", List<BookItemListDM> data = null) 
        {
            return new ListResponseBook() {
                code = code,
                message = message,
                data = data
            };
        }        
        #endregion
    }
}
