using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealWorld.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace RealWorld.Services.Impl
{
    public class ArticleService : IArticleService
    {
        private readonly IApiService _api;

        public ArticleService(IApiService api)
        {
            this._api = api;
        }

        public async Task<SingleArticle> CreateAsync(string title, string description, string body, string[] tagList = null)
        {
            /*{
  "article": {
    "title": "How to train your dragon",
    "description": "Ever wonder how?",
    "body": "You have to believe",
    "tagList": ["reactjs", "angularjs", "dragons"]
  }
}*/
            return await _api.PostAsync<SingleArticle>("articles", new { article = new { title, description, body, tagList } }, autheticate: true);
        }

        public async Task DeleteAsync(string slug)
        {
            //DELETE /api/articles/:slug
            await _api.DeleteAsync($"articles/{slug}", autheticate: true);
        }

        public async Task<SingleArticle> FavoriteAsync(string slug)
        {
            //POST /api/articles/:slug/favorite
            return await _api.PostAsync<SingleArticle>($"articles/{slug}/favorite", autheticate: true);
        }

        public async Task<MultipleArticle> FeedAsync(int offset = 0, int limit = 20)
        {
            return await _api.GetAsync<MultipleArticle>("articles/feed", 
                autheticate: true,
                queryParams: new Dictionary<string, string>()
                {
                    { "offset", offset.ToString() },
                    { "limit", limit.ToString() },
                });
        }

        public async Task<SingleArticle> GetAsync(string slug)
        {
            return await _api.GetAsync<SingleArticle>($"articles/{slug}");
        }

        public async Task<MultipleArticle> ListAsync(string tag = null, string author = null, string favorited = null, int offset = 0, int limit = 20)
        {
            return await _api.GetAsync<MultipleArticle>("articles",
                queryParams: new Dictionary<string, string>()
                {
                    { "tag", tag },
                    { "author", author },
                    { "favorited", favorited },
                    { "offset", offset.ToString() },
                    { "limit", limit.ToString() },
                });            
        }

        public async Task<SingleArticle> UnfavoriteAsync(string slug)
        {
            //DELETE /api/articles/:slug/favorite
            return await _api.DeleteAsync<SingleArticle>($"articles/{slug}/favorite", autheticate: true);
        }

        public async Task<SingleArticle> UpdateAsync(string slug, string title = null, string description = null, string body = null)
        {
            return await _api.PutAsync<SingleArticle>($"articles/{slug}", new { article = new { title, description, body } }, autheticate: true);
        }
    }
}
