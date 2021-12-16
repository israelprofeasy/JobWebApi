using JobListing.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.Repository.Interface
{
    public interface IADOOperations
    {
        Task<bool> ExecuteForQuery(string stmt); // Create, Insert, Update, Delete

        Task<bool> ExecuteForTransactionQuery(string stmt, string stmt2);
        Task<bool> ExecuteForTransactionQuery(params string[] stmt);

        Task<List<ExecuterReaderResult>> ExecuteForReader(string stmt, params string[] fields);
        Task<bool> IsTableExist(string name);
        Task<bool> CreateTable(string name);
    }
}
