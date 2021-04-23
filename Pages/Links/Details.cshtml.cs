using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LinkAggregator.Data;
using LinkAggregator.Models;
using Microsoft.AspNetCore.Identity;

namespace LinkAggregator.Pages.Links
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> UserManager { get; }

        public DetailsModel(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }


        public Link Link { get; set; }
                
        public string CurrentUserId() => UserManager.GetUserId(User);

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await _context.Link
                .Include(link => link.User)
                .Include(link => link.Votes)
                .Include(link => link.Comments)
                //.Include(link => link.Comments).ThenInclude(comment => comment.User)
                //.Include(link => link.Comments).ThenInclude(comment => comment.Votes)
                .FirstOrDefaultAsync(m => m.Id == id);

            //_context.Entry(Link).Collection(link => link.Comments).Load();

            void load_comments(List<Comment> comments)
            {
                foreach (var comment in comments)
                {
                    _context.Entry(comment).Reference(comment => comment.User).Load();
                    
                    _context.Entry(comment).Collection(comment => comment.Comments).Load();

                    _context.Entry(comment).Collection(comment => comment.Votes).Load();

                    //_context.Entry(comment).Reference(comment => comment.User).Load();

                    load_comments(comment.Comments);
                }
            }

            load_comments(Link.Comments);


            //void load_comment(Comment comment)
            //{
            //    _context.Entry(comment).Collection(comment => comment.Comments).Load();
            //    _context.Entry(comment).Collection(comment => comment.Votes).Load();

            //    foreach (var elt in comment.Comments)
            //        load_comment(elt);
            //}






            if (Link == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostVoteAsync(int id, int score)
        {
            if (User == null)
                return RedirectToPage();

            if (User.Identity.IsAuthenticated == false)
                return RedirectToPage();

            var link = await _context.Link
                .Include(link => link.Votes)
                .FirstOrDefaultAsync(link => link.Id == id);

            await link.Vote(score, UserManager.GetUserId(User));

            await _context.SaveChangesAsync();

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public async Task<IActionResult> OnPostAddCommentAsync(int id, string text)
        {
            if (User == null)
                return Redirect(HttpContext.Request.Headers["Referer"]);

            if (User.Identity.IsAuthenticated == false)
                return Redirect(HttpContext.Request.Headers["Referer"]);
                        
            var link = await _context.Link
                .Include(link => link.Comments)
                .FirstOrDefaultAsync(link => link.Id == id);
                        
            await link.AddComment(text, CurrentUserId());
                        
            await _context.SaveChangesAsync();

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public async Task<IActionResult> OnPostCommentVoteAsync(int id, int score)
        {
            if (User == null)
                return Redirect(HttpContext.Request.Headers["Referer"]);

            if (User.Identity.IsAuthenticated == false)
                return Redirect(HttpContext.Request.Headers["Referer"]);

            //var comment = new CommentAlt(await Context.Comment.FirstOrDefaultAsync(comment => comment.Id == id), this);

            var comment = await _context.Comment
                .Include(comment => comment.Votes)
                .FirstOrDefaultAsync(comment => comment.Id == id);

            await comment.Vote(score, UserManager.GetUserId(User));

            await _context.SaveChangesAsync();

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public async Task<IActionResult> OnPostAddReplyAsync(int id, string text)
        {
            if (User == null)
                return Redirect(HttpContext.Request.Headers["Referer"]);

            if (User.Identity.IsAuthenticated == false)
                return Redirect(HttpContext.Request.Headers["Referer"]);
                        
            var comment = await _context.Comment
                .Include(link => link.Comments)
                .FirstOrDefaultAsync(comment => comment.Id == id);

            await comment.AddComment(text, CurrentUserId());

            await _context.SaveChangesAsync();

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
    }
}
