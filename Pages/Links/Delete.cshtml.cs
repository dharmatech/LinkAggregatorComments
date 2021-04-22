using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LinkAggregator.Data;
using LinkAggregator.Models;

namespace LinkAggregator.Pages.Links
{
    public class DeleteModel : PageModel
    {
        private readonly LinkAggregator.Data.ApplicationDbContext _context;

        public DeleteModel(LinkAggregator.Data.ApplicationDbContext context)
        {
            _context = context;
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
