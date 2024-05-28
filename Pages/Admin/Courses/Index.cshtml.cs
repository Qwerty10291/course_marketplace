using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using course_marketplace.Models;
using Microsoft.AspNetCore.Authorization;

namespace course_marketplace.Pages.Admin.Courses
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly course_marketplace.Models.ApplicationDbContext _context;

        public IndexModel(course_marketplace.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Course = await _context.Courses.ToListAsync();
        }
    }
}
