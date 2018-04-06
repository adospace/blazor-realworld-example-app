using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services
{
    public interface IUserService 
    {
        Models.User CurrentUser { get; }

        event EventHandler CurrentUserChanged;

        Task GetCurrentAsync();

        Task LoginAsync(string username, string password);

        void Logout();

        Task RegisterAsync(string email, string username, string password);

        Task UpdateAsync(string email = null, string username = null, string password = null, string image = null, string bio = null);
    }
}
