using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entity;
using Gym_Core.Data;
using Microsoft.AspNetCore.Identity;

namespace Gym_Core.Pages.Checkins
{
    public class IndexModel : PageModel
    {
        private readonly Gym_Core.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public IndexModel(Gym_Core.Data.ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IList<Checkin> Checkin { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Checkins != null)
            {
                Checkin = await _context.Checkins
                .Include(c => c.Member)
                .ThenInclude(c=>c.Memberships)
                .Include(c => c.Plan)
                .Where(x=>x.CreateAt.Date==DateTime.Today)
                .ToListAsync();
            }
        }
    }
}
