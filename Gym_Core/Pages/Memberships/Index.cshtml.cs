using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entity;
using Gym_Core.Data;

namespace Gym_Core.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly Gym_Core.Data.ApplicationDbContext _context;

        public IndexModel(Gym_Core.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Membership> Membership { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Memberships != null)
            {
                Membership = await _context.Memberships
                .Include(m => m.Members)
                .Include(m => m.Plan).ToListAsync();
            }
        }
    }
}
