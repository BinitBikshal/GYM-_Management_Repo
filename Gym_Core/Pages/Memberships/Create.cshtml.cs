using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entity;
using Gym_Core.Data;
using Microsoft.AspNetCore.Identity;

namespace Gym_Core.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly Gym_Core.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public CreateModel(Gym_Core.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FirstName");
        ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (ModelState.IsValid || _context.Memberships == null || Membership == null)
            {
                return Page();
            }
            var loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Membership.UpdateAt = DateTime.Now;
            Membership.CreateAt = DateTime.Now;
            Membership.CreatedBy = loggedInUser?.UserName;
            _context.Memberships.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
