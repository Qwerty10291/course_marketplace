using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using course_marketplace.Models;
using System.Linq;
using System.Threading.Tasks;

namespace course_marketplace.Pages
{
    [Authorize(Roles = "seller")]
    public class EditCourseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public EditCourseModel(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);

            Course = await _context.Courses
                .Include(c => c.CourseContents)
                .FirstOrDefaultAsync(m => m.CourseId == id && m.CreatorId == userId);

            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/User/MyCourses");
        }

        public async Task<IActionResult> OnPostDeleteContentAsync(int id)
        {
            var content = await _context.CourseContents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            _context.CourseContents.Remove(content);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = Course.CourseId });
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
