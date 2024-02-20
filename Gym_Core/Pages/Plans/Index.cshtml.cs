using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entity;
using Gym_Core.Data;

namespace Gym_Core.Pages.Plans
{
    public class IndexModel : PageModel
    {
        private readonly Gym_Core.Data.ApplicationDbContext _context;

        public IndexModel(Gym_Core.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Plan> Plan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Plans != null)
            {
                Plan = await _context.Plans.ToListAsync();
            }
        }
    }
}
