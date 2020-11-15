using System;
using Daimler.Lib.Domain;

namespace Daimler.Api.Domains.Url
{
    public class Url : AggregateRoot
    {
        public string UrlAddress { get; set; }
        public UrlCode UrlCode { get; set; }

        public int RoutingCount { get; set; }
    }
}