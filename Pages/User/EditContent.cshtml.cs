using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using course_marketplace.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
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
        private readonly IWebHostEnvironment _environment;

        public EditContentModel(ApplicationDbContext context, UserManager<Models.User> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public EditCourseContentViewModel Content { get; set; }

        public  List<FileModel> Files {get;set;}

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var content = await _context.CourseContents
            .Include(cc => cc.ContentFiles)
            .Where(cc => cc.ContentId == id)
            .FirstOrDefaultAsync();


            if (content == null)
            {
                return NotFound();
            }
            Content = new EditCourseContentViewModel() {
                ContentId = content.ContentId,
                Title = content.Title,
                Content = content.Content
            };

            Files = content.ContentFiles.ToList();

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

        public async Task<IActionResult> OnPostUploadFileAsync(int id, IFormFile file) {
            if (file == null || file.Length == 0) {
                return Page();
            }
            var courseContent = await _context.CourseContents.FindAsync(id);
            if (courseContent == null){
                return NotFound();
            }

            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileModel = new FileModel
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                CourseContentId = id,
            };
            
            var filePath = Path.Combine(uploadPath, fileModel.Id.ToString());
            fileModel.FilePath = filePath;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            _context.FileModel.Add(fileModel);
            await _context.SaveChangesAsync();
            return RedirectToPage(new { id = id});
        }
    }
}
