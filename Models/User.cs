using System.ComponentModel.DataAnnotations;

namespace course_marketplace.Models;
using Microsoft.AspNetCore.Identity;


public class User : IdentityUser
{

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Navigation property for the many-to-many relationship
    public ICollection<UserCourse> UserCourses { get; set; }
}

public class UserCourse
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
}