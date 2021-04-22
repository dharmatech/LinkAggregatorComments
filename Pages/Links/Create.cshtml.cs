using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LinkAggregator.Data;
using LinkAggregator.Models;

namespace LinkAggregator.Pages.Links
{
    public class CreateModel : PageModel
    {
        private readonly LinkAggregator.Data.ApplicationDbContext _context;

        public CreateModel(LinkAggregator.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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

            _context.Link.Add(Link);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
