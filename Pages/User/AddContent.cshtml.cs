using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using course_marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace course_marketplace.Pages
{
    public class AddCourseContentViewModel {
        [Required]
        public string Title {get; set;}
        [Required]
        public string Content {get; set;}
    }

    [Authorize(Roles = "seller")]
    public class AddContentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddContentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddCourseContentViewModel Content { get; set; }

        [BindProperty]
        public int CourseId { get; set; }

        public void OnGet(int courseId)
        {
            CourseId = courseId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var courseContent = new CourseContent
            {
                CourseId = CourseId,
                Title = Content.Title,
                Content = Content.Content,
                Type = "Material" // или другой тип, в зависимости от вашей логики
            };

            _context.CourseContents.Add(courseContent);
            await _context.SaveChangesAsync();

            return RedirectToPage("/User/EditCourse", new { id = CourseId });
        }
    }
}
