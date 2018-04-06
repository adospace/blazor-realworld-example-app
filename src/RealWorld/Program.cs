using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using RealWorld.Services;
using System;

namespace RealWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(configure =>
            {
                configure.Add(ServiceDescriptor.Transient<IApiService, Services.Impl.ApiService>());
                configure.Add(ServiceDescriptor.Singleton<IUserService, Services.Impl.UserService>());
                configure.Add(ServiceDescriptor.Transient<IArticleService, Services.Impl.ArticleService>());
                configure.Add(ServiceDescriptor.Transient<ITagService, Services.Impl.TagService>());
                configure.Add(ServiceDescriptor.Transient<ICommentService, Services.Impl.CommentService>());
                configure.Add(ServiceDescriptor.Transient<IProfileService, Services.Impl.ProfileService>());
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
