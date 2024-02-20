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

namespace Gym_Core.Pages.Trainers
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
            return Page();
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (ModelState.IsValid || _context.Trainers == null || Trainer == null)
            {
                return Page();
            }
          var loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Trainer.UpdateAt = DateTime.Now;
            Trainer.CreateAt = DateTime.Now;
            Trainer.CreatedBy = loggedInUser.UserName; //Trainer.
            _context.Trainers.Add(Trainer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
