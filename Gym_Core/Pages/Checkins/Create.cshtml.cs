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
using Microsoft.EntityFrameworkCore;

namespace Gym_Core.Pages.Checkins
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
            GetMember();
            return Page();
        }

        private void GetMember()
        {
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FirstName");
            ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
        }

        [BindProperty]
        public Checkin Checkin { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (ModelState.IsValid || _context.Checkins == null || Checkin == null)
            {
                return Page();
            }
            var loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            //if (_context.Members.Include(x => Membership).Any(x => x.Id == Checkin.MemberId && x.Memberships.Any(x => x.EndDate < DateTime.Now)))
            //{
            //    ViewData["Message"] = "Membership is expired for this member.";
            //    return Page();
            //}
            //if(_context.Checkins.Any(x=>x.MemberId==Checkin.MemberId && x.CreateAt.Date ==DateTime.Today))
            //{
            //    GetMember();
            //    ViewData["Message"] = "You have already CheckedId In.";
            //    return Page();
            //}
            Checkin.UpdateAt = DateTime.Now;
            Checkin.CreateAt = DateTime.Now;
            Checkin.CreatedBy = loggedInUser.UserName;
            _context.Checkins.Add(Checkin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
