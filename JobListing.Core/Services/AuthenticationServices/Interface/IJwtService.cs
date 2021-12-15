using JobListingAppUI.Models;
using System.Collections.Generic;

namespace JobListingAppUI.Services
{
    public interface IJwtService
    {
        string JwtGen(User user, List<string> role);
    }
}
