using ApiBooks.Helpers;
using ApiBooks.Models.DataModels;
using ApiBooks.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using NxsApiBooks.Repositories;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Properties
        private DBContext AppContext;
        private BookRepository BooksRepository;
        public BooksController()
        {
            AppContext = new DBContext();
            BooksRepository = new BookRepository(AppContext);
        }
        #endregion

        #region APIs
        [Route("GetFilterBooks")]
        [HttpGet]
        public IActionResult getFilterBooks(string author = "", string title = "", int year = 0) 
        {
            try
            {
                var filter_books_list = BooksRepository.filterBooks(author, title, year);

                return Ok(BookRepository.buildApiListResponseBook(200, $"Total datos encontrados: {filter_books_list.Count}", filter_books_list));
            }
            catch (Exception e) 
            {
                return Ok(ResponseHelper.basicMessageForExceptions(e));
            }
        }

        [Route("GetAllBooks")]
        [HttpGet]
        public IActionResult getAllBooks()
        {
            try
            {
                var books_list = BooksRepository.getList();
                return Ok(BookRepository.buildApiListResponseBook(200,$"Total datos encontrados: {books_list.Count}", books_list));
            }
            catch (Exception e)
            {
                return Ok(BookRepository.buildApiSampleResponse(400, ResponseHelper.basicMessageForExceptions(e)));
            }
        }

        [Route("InsertNewBook")]
        [HttpPost]
        public IActionResult insertNewBook([FromBody] BookDM new_book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(BookRepository.buildApiSampleResponse(400, ResponseHelper.getFirstErrorFromModelState(ModelState)));
                }

                var insert_book = BooksRepository.insertNew(new_book);

                var response = BookRepository.buildApiSampleResponse(insert_book.code, insert_book.message);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(BookRepository.buildApiSampleResponse(400, ResponseHelper.basicMessageForExceptions(e)));
            }
        }
        #endregion
    }
}
