using RealWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services.Impl
{
    public class TagService : ITagService
    {
        private readonly IApiService _api;

        public TagService(IApiService api)
        {
            this._api = api;
        }

        public async Task<string[]> ListAsync()
        {
            return (await _api.GetAsync<TagList>("tags")).tags;
        }
    }
}
