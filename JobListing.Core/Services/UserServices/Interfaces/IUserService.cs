using JobListing.Models.DTOs;
using JobListingAppUI.DTOs;
using System.Threading.Tasks;

namespace JobListingAppUI.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        public Task<LoginSuccess> Login(string email, string password);
        
        public Task<bool> RetreivePassword(string email);

        
    }
}
