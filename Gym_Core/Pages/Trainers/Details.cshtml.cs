﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entity;
using Gym_Core.Data;

namespace Gym_Core.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly Gym_Core.Data.ApplicationDbContext _context;

        public DetailsModel(Gym_Core.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Trainer Trainer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }
            else 
            {
                Trainer = trainer;
            }
            return Page();
        }
    }
}
