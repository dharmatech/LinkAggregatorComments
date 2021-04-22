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
using Microsoft.AspNetCore.Authorization;

namespace LinkAggregator.Pages.Links
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IAuthorizationService AuthorizationService { get; }
        private UserManager<IdentityUser> UserManager { get; }

        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }

        public IList<Link> Links { get;set; }

        public IdentityUser LinkUser(Link link) => UserManager.FindByIdAsync(link.UserId).Result;

        public async Task OnGetAsync()
        {
            Links = await _context.Link
                .Include(link => link.Votes)
                .ToListAsync();

        }

        public string CurrentUserid() => UserManager.GetUserId(User);

        public async Task<IActionResult> OnPostVoteAsync(int id, int score)
        {
            if (User == null)
                return RedirectToPage();

            if (User.Identity.IsAuthenticated == false)
                return RedirectToPage();
                        
            var link = _context.Link
                .Include(link => link.Votes)
                .First(link => link.Id == id);
                        
            await link.Vote(score, UserManager.GetUserId(User));

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
