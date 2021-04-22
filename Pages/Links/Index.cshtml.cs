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
    public class IndexModel : PageModel
    {
        private readonly LinkAggregator.Data.ApplicationDbContext _context;

        public IndexModel(LinkAggregator.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Link> Link { get;set; }

        public async Task OnGetAsync()
        {
            Link = await _context.Link.ToListAsync();
        }
    }
}
