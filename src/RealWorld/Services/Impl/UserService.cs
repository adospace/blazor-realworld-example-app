using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RealWorld.Models;
using Microsoft.AspNetCore.Blazor;

namespace RealWorld.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IApiService _api;

        public UserService(IApiService api)
        {
            Console.WriteLine("Load UserService");
            this._api = api;
        }

        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    CurrentUserChanged?.Invoke(this, EventArgs.Empty);
                    Console.WriteLine($"CurrentUser={_currentUser?.username}");
                }
            }
        }

        public event EventHandler CurrentUserChanged;

        public async Task GetCurrentAsync()
        {
            if (CurrentUser != null)
                return;

            CurrentUser = (await _api.GetAsync<SingleUser>("user"))?.user;
        }

        public async Task LoginAsync(string email, string password)
        {
            CurrentUser = (await _api.PostAsync<SingleUser>("users/login", new { user = new { email, password } }))?.user;
        }

        public async Task RegisterAsync(string email, string username, string password)
        {
            CurrentUser = (await _api.PostAsync<SingleUser>("users", new { user = new { username, email, password } }))?.user;
        }

        public async Task UpdateAsync(string email = null, string username = null, string password = null, string image = null, string bio = null)
        {
            CurrentUser = (await _api.PutAsync<SingleUser>("user", new { user = new { username, email, password, image, bio } }, autheticate: true))?.user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

    }
}
