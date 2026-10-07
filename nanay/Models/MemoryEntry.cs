namespace nanay.Models;

public class MemoryEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = "Memory"; // e.g. "Her Voice", "Her Cooking", "Her Wisdom", "Family Moment"
    public int DisplayOrder { get; set; } = 0;
}
