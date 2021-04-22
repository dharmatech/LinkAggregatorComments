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
    public class DetailsModel : PageModel
    {
        private readonly LinkAggregator.Data.ApplicationDbContext _context;

        public DetailsModel(LinkAggregator.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
