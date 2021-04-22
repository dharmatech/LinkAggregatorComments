using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LinkAggregator.Data;
using LinkAggregator.Models;
using Microsoft.AspNetCore.Identity;

namespace LinkAggregator.Pages.Links
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> UserManager { get; }

        public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.Name == null)
                return RedirectToPage("./Index");

            return Page();
        }

        [BindProperty]
        public Link Link { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Link.UserId = UserManager.GetUserId(User);

            _context.Link.Add(Link);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
