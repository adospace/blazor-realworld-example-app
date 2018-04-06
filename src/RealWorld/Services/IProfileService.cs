using RealWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services
{
    public interface IProfileService
    {
        Task<SingleProfile> GetAsync(string username);

        Task<SingleProfile> FollowAsync(string username);

        Task<SingleProfile> Unfollow(string username);
    }
}
