using System.ComponentModel.DataAnnotations;

namespace course_marketplace.Models;

public class FileModel {
    public Guid  Id {get;set;}
    public string FileName {get;set;}
    public string FilePath {get;set;}
    public int CourseContentId {get;set;}
    public CourseContent CourseContent {get;set;}
}