using RealWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services
{
    public interface ICommentService
    {
        Task<MultipleComment> ListAsync(string articleSlug);

        Task<SingleComment> AddAsync(string articleSlug, string body);

        Task DeleteAsync(string articleSlug, int commentId);


    }
}
