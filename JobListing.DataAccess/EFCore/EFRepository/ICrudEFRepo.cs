using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore.EFRepository
{
    public interface ICrudEFRepo
    {
        Task<bool> Add<T>(T entity);
        Task<bool> Delete<T>(T entity);
        Task<bool> Update<T>(T entity);
        Task<bool> SaveChanges();
        Task<int> RowCount();
    }
}
