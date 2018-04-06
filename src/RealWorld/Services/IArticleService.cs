using RealWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services
{
    public interface IArticleService
    {
        Task<MultipleArticle> ListAsync(string tag = null, string author = null, string favorited = null, int offset = 0, int limit = 20);

        Task<MultipleArticle> FeedAsync(int offset = 0, int limit = 20);

        Task<SingleArticle> GetAsync(string slug);

        Task<SingleArticle> CreateAsync(string title, string description, string body, string[] tagList = null);

        Task<SingleArticle> UpdateAsync(string slug, string title = null, string description = null, string body = null);

        Task DeleteAsync(string slug);

        Task<SingleArticle> FavoriteAsync(string slug);

        Task<SingleArticle> UnfavoriteAsync(string slug);
    }
}
