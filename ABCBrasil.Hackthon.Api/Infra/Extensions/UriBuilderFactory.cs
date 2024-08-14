using System;
using System.Linq;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class UriBuilderFactory
    {
        public static UriBuilder Build(params string[] paths)
        {
            var path = string.Join('/', paths.Select(p => p.Trim().Trim('/')));
            return new UriBuilder(path);
        }
    }
}