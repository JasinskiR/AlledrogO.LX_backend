namespace AlledrogO.Post.Application.DTOs.External;

public class PostSearchWithTagsDto
{
    public string QueryString { get; set; }
    public IEnumerable<string> Tags { get; set; }
}