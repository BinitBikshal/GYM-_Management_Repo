using Entity;
using Gym_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gym_Core.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [BindProperty]
        public int TotalMember { get; set; } = default!;
        public int TotalTrainers { get; set; } = default!;
        public int ActiveMembers { get; set; } = default!;
        public int InActiveMembers { get; set; } = default!;
        public int ExpiredMemberShips { get; set; } = default!;
        public async Task OnGet()
        {
            if (context.Member != null)
            {
                TotalMember = await context.Member.CountAsync();
            }
            if (context.Trainers != null)
            {
                TotalTrainers = await context.Trainers.CountAsync();
            }
            if (context.Member != null)
            {
                ActiveMembers = await context.Member
                    .Include(m => m.Memberships)
                    .CountAsync(m => m.Memberships!.Any(ms => ms.EndDate > DateTime.Now));
            }
            if (context.Member != null)
            {
                ExpiredMemberShips = await context.Member
                    .Include(m => m.Memberships)
                    .CountAsync(m => m.Memberships!.Any(ms => ms.EndDate < DateTime.Now));

            }
            if (context.Member != null)
            {
                InActiveMembers = await context.Checkins
                    .CountAsync(m => m.CreateAt < DateTime.Now.AddMonths(-1));

            }
        }

    }
}
