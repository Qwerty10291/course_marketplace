using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using course_marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace course_marketplace.Pages
{
    public class CreateCourseView {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Price { get; set; }

    }

    [Authorize(Roles = "seller")]
    public class CreateCourseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public CreateCourseModel(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateCourseView CourseModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            var newCourse = new Models.Course(){
                CreatorId = user.Id,
                Title = CourseModel.Title,
                Description = CourseModel.Description,
                Price = CourseModel.Price
            };
            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("/User/EditCourse", new { id = newCourse.CourseId });
        }
    }
}
