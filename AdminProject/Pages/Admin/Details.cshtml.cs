using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminProject.Data;
using AdminProject.Models;

namespace AdminProject.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly AdminProject.Data.AdminProjectContext _context;

        public DetailsModel(AdminProject.Data.AdminProjectContext context)
        {
            _context = context;
        }

        public AllMembers AllMembers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AllMembers = await _context.AllMembers.FirstOrDefaultAsync(m => m.Id == id);

            if (AllMembers == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
