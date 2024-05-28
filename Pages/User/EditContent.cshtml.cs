using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using course_marketplace.Models;
using System.ComponentModel.DataAnnotations;
namespace course_marketplace.Pages
{
    public class EditCourseContentViewModel {
        [Required]
        public int ContentId {get; set;}

        [Required]
        public string Title {get; set;}
        [Required]
        public string Content {get; set;}
    }

    [Authorize(Roles = "seller")]
    public class EditContentModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public EditContentModel(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public EditCourseContentViewModel Content { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var content = await _context.CourseContents.FindAsync(id);


            if (content == null)
            {
                return NotFound();
            }
            Content = new EditCourseContentViewModel() {
                ContentId = content.ContentId,
                Title = content.Title,
                Content = content.Content
            };


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
           var oldContent = await  _context.CourseContents.FindAsync(Content.ContentId);
           if (oldContent == null) {
                return NotFound();
           }
           
           oldContent.Title = Content.Title;
           oldContent.Content = Content.Content;
           _context.CourseContents.Update(oldContent);
           await _context.SaveChangesAsync();
           return RedirectToPage("/User/EditCourse", new { id = oldContent.CourseId});
        }

    }
}
