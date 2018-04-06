using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealWorld.Models;

namespace RealWorld.Services.Impl
{
    public class ProfileService : IProfileService
    {
        private readonly IApiService _api;

        public ProfileService(IApiService api)
        {
            this._api = api;
        }

        public async Task<SingleProfile> FollowAsync(string username)
        {
            return await _api.PostAsync<SingleProfile>($"profiles/{username}/follow");
        }

        public async Task<SingleProfile> GetAsync(string username)
        {
            return await _api.GetAsync<SingleProfile>($"profiles/{username}");
        }

        public async Task<SingleProfile> Unfollow(string username)
        {
            return await _api.DeleteAsync<SingleProfile>($"profiles/{username}/follow");
        }
    }
}
