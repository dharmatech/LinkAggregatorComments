using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Models
{
    public class CommentVote
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public int Score { get; set; }
        public DateTime DateTime { get; set; }
    }
}
