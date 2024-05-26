
using System.ComponentModel.DataAnnotations;

namespace AlledrogO.Post.Application.Services;

public class ImageServiceConfiguration
{
    [Required]
    public string StaticFilesDir { get; set; }
    [Required]
    public string ImageDir { get; set; }
    [Required]
    public int MaxSizeMB { get; set; }
    [Required]
    public string[] AllowedFormats { get; set; }
}