namespace nanay.Models;

public class MemoryPhoto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ImagePath { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public string MemoryDate { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Chapter { get; set; } = "Chapter2"; // Hero, Chapter1, Chapter2, Chapter3, Reveal, Final
    public int DisplayOrder { get; set; } = 0;
    public bool IsHero { get; set; } = false;
    public bool IsFinalPhoto { get; set; } = false;
    public string AltText { get; set; } = "A cherished memory";
}
