using ABCBrasil.Hackthon.Api.Infra.Swagger;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses
{
    [SwaggerSchemaDecoration("Informações referente à paginação", Title = "PagedList")]
    public class PagedList<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public List<Link> Links { get; set; } = new List<Link>();
        public PageListInfo Meta { get; set; } = new PageListInfo();

        public void AddSelfLink<Tfilter>(Tfilter filter, string baseUrl) where Tfilter : class, new()
        {
            Links.Add(new Link()
            {
                Type = "GET",
                Rel = "self",
                Uri = baseUrl + GetQueryFilter(filter)
            });
        }

        public void AddPreviusLink<Tfilter>(Tfilter filter, string baseUrl) where Tfilter : class, new()
        {
            Links.Add(new Link()
            {
                Type = "GET",
                Rel = "prev",
                Uri = baseUrl + GetQueryFilter(filter)
            });
        }

        public void AddNextLink<Tfilter>(Tfilter filter, string baseUrl) where Tfilter : class, new()
        {
            Links.Add(new Link()
            {
                Type = "GET",
                Rel = "next",
                Uri = baseUrl + GetQueryFilter(filter)
            });
        }

        public void AddFirstLink<Tfilter>(Tfilter filter, string baseUrl) where Tfilter : class, new()
        {
            Links.Add(new Link()
            {
                Type = "GET",
                Rel = "first",
                Uri = baseUrl + GetQueryFilter(filter)
            });
        }

        public void AddLastLink<Tfilter>(Tfilter filter, string baseUrl) where Tfilter : class, new()
        {
            Links.Add(new Link()
            {
                Type = "GET",
                Rel = "last",
                Uri = baseUrl + GetQueryFilter(filter)
            });
        }

        public bool PageNotFound() => Meta.TotalPages > 0 && Meta.CurrentPage > Meta.TotalPages;

        private static string GetQueryFilter<Tfilter>(Tfilter filter) where Tfilter : class, new()
        {
            Type type = filter.GetType();
            StringBuilder query = new();

            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(filter);

                if (value == null)
                {
                    continue;
                }
                var customAttributeTypes = new Type[] { typeof(FromQueryAttribute), typeof(JsonPropertyNameAttribute) };
                var customQueryAttribute = property.GetCustomAttributes(false).First(ca => customAttributeTypes.Contains(ca.GetType()));
                var name = property.Name.ToLower();

                if (customQueryAttribute != null)
                {
                    name = customQueryAttribute is FromQueryAttribute ? (customQueryAttribute as FromQueryAttribute).Name : (customQueryAttribute as JsonPropertyNameAttribute).Name;
                }

                query.Append(query.Length > 0 ? $"&{name}={value}" : $"{name}={value}");
            }

            return query.Length > 0 ? "?" + query.ToString() : default;
        }
    }

    public class PageListInfo
    {
        public long TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}