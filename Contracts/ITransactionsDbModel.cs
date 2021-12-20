using ApiBooks.Models.DataModels;
using System.Collections.Generic;

namespace ApiBooks.Contracts
{
    interface ITransactionsDbModel<T>
    {
        decimal getLastId();
        BasicResponse insertNew(T param_model); 
    }
}
