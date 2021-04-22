using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LinkAggregator.Data;
using LinkAggregator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LinkAggregator.Authorization;

namespace LinkAggregator.Pages.Links
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IAuthorizationService AuthorizationService { get; }
        private UserManager<IdentityUser> UserManager { get; }

        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
            )
        {
            _context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }

        [BindProperty]
        public Link Link { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await _context.Link.FirstOrDefaultAsync(m => m.Id == id);

            if (Link == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await _context.Link.FindAsync(id);

            if (Link != null)
            {
                _context.Link.Remove(Link);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
