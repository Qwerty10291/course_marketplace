using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using course_marketplace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace course_marketplace.Pages
{
    public class CourseDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CourseDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }
        public List<ContentViewModel> CourseContents { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses.FindAsync(id);
            if (Course == null)
            {
                return NotFound();
            }

            CourseContents = await _context.CourseContents
                .Where(cc => cc.CourseId == id)
                .Select(cc => new ContentViewModel
                {
                    ContentId = cc.ContentId,
                    Title = cc.Title,
                    Type = cc.Type,
                })
                .ToListAsync();

            return Page();
        }
    }

    public class ContentViewModel
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
