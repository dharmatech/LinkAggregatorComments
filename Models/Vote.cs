using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }

        public int LinkId { get; set; }
        public Link Link { get; set; }
    }
}
