using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using course_marketplace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace course_marketplace.Pages
{
    public class CourseViewModel : Course {
        public bool IsEnrolled;
    }
    
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<CourseViewModel> Courses { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            var coursesQuery = _context.Courses
                .Include(c => c.UserCourses)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                coursesQuery = coursesQuery.Where(c => c.Title.Contains(SearchTerm) || c.Description.Contains(SearchTerm));
            }

            var courses = await coursesQuery
                .Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    IsEnrolled = userId != null && c.UserCourses.Any(uc => uc.UserId == userId)
                })
                .ToListAsync();

            Courses = courses;
        }

        [Authorize]
        public async Task<IActionResult> OnPostEnrollAsync(int courseId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Challenge(); // Redirects to login page if user is not authenticated
            }

            var userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            };

            _context.UserCourses.Add(userCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
