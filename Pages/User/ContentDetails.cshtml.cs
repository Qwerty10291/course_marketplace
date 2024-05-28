using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using course_marketplace.Models;

namespace course_marketplace.Pages
{
    public class ContentDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ContentDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CourseContent Content { get; set; }
        public string MarkdownContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Content = await _context.CourseContents.FindAsync(id);
            if (Content == null)
            {
                return NotFound();
            }

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            MarkdownContent = Markdown.ToHtml(Content.Content, pipeline);

            return Page();
        }
    }
}
