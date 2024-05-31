using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace course_marketplace.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }

    public string CreatorId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    // Navigation property for the many-to-many relationship
    public virtual ICollection<UserCourse> UserCourses { get; set; }
    public virtual ICollection<CourseContent> CourseContents { get; set; }
}


public class CourseContent
{
    [Key]
    public int ContentId { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public string Type { get; set; }
    
    [Required]
    public string Content {get; set;}

    public ICollection<FileModel> ContentFiles{get;set;}
}
