using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.Repository.Interface
{
    public interface ICrudRepo
    {
        Task<bool> Add<T>(T entity);
        Task<bool> Delete<T>(T entity);
        Task<bool> Update<T>(T entity);

    }
}
