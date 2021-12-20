using ApiBooks.Contracts;
using ApiBooks.Helpers;
using ApiBooks.Models.DataModels;
using ApiBooks.Models.DataModels.Authors;
using ApiBooks.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiBooks.Repositories
{
    public class AuthorRepository:ITransactionsDbModel<AuthorDM>
    {
        #region Initializers
        private DBContext ContextApp;

        public AuthorRepository(DBContext Context)
        {
            this.ContextApp = Context;
        }
        #endregion

        #region TransactionalDbOperations
        public BasicResponse insertNew(AuthorDM param_model)
        {
            if (param_model != null) {
                Author new_author = new Author();
                new_author.AuthorId = getLastId();
                new_author.Name = param_model.Name;
                new_author.Birthday = param_model.Birthday;
                new_author.Hometown = param_model.Hometown;
                new_author.Email = param_model.Email;

                ContextApp.Author.Add(new_author);
                var save_data = ContextApp.SaveChanges() > 0;

                return save_data ? 
                    ResponseHelper.basicResponse(200, "Se ha registrado el autor correctamente.")
                    : ResponseHelper.basicResponse(400,"No se pudo registrar al autor.");
            }

            return ResponseHelper.basicResponse(400, "El autor no puede ser vacío.");
        }

        public List<AuthorDM> getList()
        {
            var authors_from_db = ContextApp.Author.ToList().OrderByDescending(x => x.AuthorId);
            List<AuthorDM> author_list_to_return = new List<AuthorDM>();

            foreach (var author_from_db in authors_from_db) {
                AuthorDM author_to_add = new AuthorDM() {
                    AuthorId = author_from_db.AuthorId,
                    Name = author_from_db.Name,
                    Birthday = author_from_db.Birthday,
                    Email = author_from_db.Email,
                    Hometown = author_from_db.Hometown
                };

                author_list_to_return.Add(author_to_add);
            }

            return author_list_to_return;
        }

        public decimal getLastId() 
        {
            Author last_author = ContextApp.Author.OrderByDescending(x => x.AuthorId).FirstOrDefault();

            return last_author != null ? (last_author.AuthorId + 1) : 1;
        }
        #endregion

        #region ControllersResponseBuilders        
        public static SampleResponseAuthor buildApiSampleResponse(int code, string message, AuthorDM data = null)
        {
            return new SampleResponseAuthor()
            {
                code = code,
                message = message,
                data = data
            };
        }
        public static ListResponseAuthor buildApiListResponse(int code, string message, List<AuthorDM> data = null)
        {
            return new ListResponseAuthor()
            {
                code = code,
                message = message,
                data = data
            };
        }
        
        #endregion
    }
}
