using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int LinkId { get; set; }
        public Link Link { get; set; }

        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public List<CommentVote> Votes { get; set; }

        public int Score() =>
            Votes
                .Where(vote => vote.CommentId == Id)
                .Sum(vote => vote.Score);
    }
}
