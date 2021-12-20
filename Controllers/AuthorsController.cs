using ApiBooks.Helpers;
using ApiBooks.Models.DataModels;
using ApiBooks.Models.DbModels;
using ApiBooks.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        #region Properties
        private DBContext AppContext;
        private AuthorRepository AuthorRepository;
        public AuthorsController()
        {
            AppContext = new DBContext();
            AuthorRepository = new AuthorRepository(AppContext);
        }
        #endregion

        #region APIs
        [Route("GetAllAuthors")]
        [HttpGet]
        public IActionResult getAllAuthors() 
        {
            try
            {
                var arthur_list = AuthorRepository.getList();
                return Ok(AuthorRepository.buildApiListResponse(200, $"Total datos encontrados: {arthur_list.Count}", arthur_list));
            }
            catch (Exception e) 
            {
                return Ok(AuthorRepository.buildApiSampleResponse(400, ResponseHelper.basicMessageForExceptions(e)));
            }
        }

        [Route("InsertNewAuthor")]
        [HttpPost]
        public IActionResult insertNewAuthor(AuthorDM param_author)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(AuthorRepository.buildApiSampleResponse(400, ResponseHelper.getFirstErrorFromModelState(ModelState)));
                }

                if (param_author != null) 
                {
                    var save_author = AuthorRepository.insertNew(param_author);

                    var response = AuthorRepository.buildApiSampleResponse(save_author.code, save_author.message);
                    return Ok(response);
                }
                return Ok(AuthorRepository.buildApiSampleResponse(400, "El autor no puede ser vacío."));
            }
            catch (Exception e)
            {
                return Ok(AuthorRepository.buildApiSampleResponse(400, ResponseHelper.basicMessageForExceptions(e)));
            }
        }
        #endregion
    }
}
