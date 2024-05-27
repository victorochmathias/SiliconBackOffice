namespace SiliconBackOffice.Business.Models;

public class CreateCourseCardModel
{
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public string[]? Categories { get; set; }
    public string? Title { get; set; }
    public string? Ingress { get; set; }
    public decimal StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? LikesInProcent { get; set; }
    public string? Likes { get; set; }
    public string? Hours { get; set; }
    public List<AuthorModel>? Authors { get; set; }
    public PricesModel? Prices { get; set; }
    public ContentModel? Content { get; set; }
}

public class AuthorModel
{
    public string? Name { get; set; }
}

public class PricesModel
{
    public string? Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}

public class ContentModel
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public List<ProgramDetailItemModel>? ProgramDetails { get; set; }
}

public class ProgramDetailItemModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}