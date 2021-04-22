using System;

namespace LinkAggregator.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public DateTime DateTime { get; set; }
    }
}
