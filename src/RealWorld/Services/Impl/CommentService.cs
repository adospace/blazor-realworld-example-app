using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealWorld.Models;

namespace RealWorld.Services.Impl
{
    public class CommentService : ICommentService
    {
        private readonly IApiService _api;

        public CommentService(IApiService api)
        {
            this._api = api;
        }

        public async Task<SingleComment> AddAsync(string articleSlug, string body)
        {
            //POST /api/articles/:slug/comments
            return await _api.PostAsync<SingleComment>($"articles/{articleSlug}/comments", new { comment = new { body } }, autheticate: true);
        }

        public async Task DeleteAsync(string articleSlug, int commentId)
        {
            //DELETE /api/articles/:slug/comments/:id
            await _api.DeleteAsync<MultipleComment>($"articles/{articleSlug}/comments/{commentId}", autheticate: true);
        }

        public async Task<MultipleComment> ListAsync(string articleSlug)
        {
            //GET /api/articles/:slug/comments
            return await _api.GetAsync<MultipleComment>($"articles/{articleSlug}/comments");
        }
    }
}
