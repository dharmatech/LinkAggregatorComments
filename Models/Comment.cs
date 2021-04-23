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

        public int? LinkId { get; set; }
        //[ForeignKey("LinkId")]
        public Link Link { get; set; }

        public int? ParentCommentId { get; set; }
        //[ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }

        public List<Comment> Comments { get; set; }

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

        public CommentVote UserVote(string userId) =>
            Votes.FirstOrDefault(vote => vote.UserId == userId);

        public int UserScore(string userId)
        {
            var vote = UserVote(userId);

            return vote == null ? 0 : vote.Score;
        }

        public async Task Vote(int score, string voterUserId)
        {
            var vote = UserVote(voterUserId);

            if (vote == null)
            {
                vote = new CommentVote()
                {
                    UserId = voterUserId,
                    CommentId = Id,
                    Score = score,
                    DateTime = DateTime.Now
                };

                Votes.Add(vote);
            }
            else
            {
                vote.Score = vote.Score == score ? 0 : score;
            }
        }

        public async Task AddComment(string text, string commenterUserId)
        {
            var comment = new Comment()
            { 
                UserId = commenterUserId,
                ParentCommentId = Id,
                Text = text,
                DateTime = DateTime.Now
            };

            Comments.Add(comment);
        }
    }
}
