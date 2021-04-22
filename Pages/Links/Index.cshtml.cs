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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> UserManager { get; }

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
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
    }
}
