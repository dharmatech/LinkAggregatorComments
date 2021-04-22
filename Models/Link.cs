using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        public string Title { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        public List<Vote> Votes { get; set; }

        public int Score() => Votes.Sum(vote => vote.Score);

        public Vote UserVote(string userId) => Votes.FirstOrDefault(vote => vote.UserId == userId);

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
                vote = new Vote()
                {
                    UserId = voterUserId,
                    LinkId = Id,
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
    }
}
