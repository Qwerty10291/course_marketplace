using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using course_marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace course_marketplace.Pages
{
    [Authorize]
    public class MyCoursesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public MyCoursesModel(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<CourseViewModel> EnrolledCourses { get; set; }
        public List<CourseViewModel> OwnedCourses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            if (User.IsInRole("seller"))
            {
                OwnedCourses = await _context.Courses
                    .Where(c => c.CreatorId == userId)
                    .Select(c => new CourseViewModel
                    {
                        CourseId = c.CourseId,
                        Title = c.Title,
                        Description = c.Description,
                        Price = c.Price
                    })
                    .ToListAsync();

                return Page();
            }

            EnrolledCourses = await _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => new CourseViewModel
                {
                    CourseId = uc.Course.CourseId,
                    Title = uc.Course.Title,
                    Description = uc.Course.Description,
                    Price = uc.Course.Price,
                    IsEnrolled = true
                })
                .ToListAsync();

            return Page();
        }
    }
}
