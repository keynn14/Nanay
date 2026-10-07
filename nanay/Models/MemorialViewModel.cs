namespace nanay.Models;

public class MemorialData
{
    public MemorialSettings Settings { get; set; } = new();
    public List<MemoryPhoto> Photos { get; set; } = new();
    public List<MemoryEntry> Memories { get; set; } = new();
}

public class MemorialViewModel
{
    public MemorialSettings Settings { get; set; } = new();
    public List<MemoryPhoto> HeroPhotos { get; set; } = new();
    public List<MemoryPhoto> Chapter1Photos { get; set; } = new();
    public List<MemoryPhoto> Chapter2Photos { get; set; } = new();
    public List<MemoryPhoto> Chapter3Photos { get; set; } = new();
    public List<MemoryPhoto> RevealPhotos { get; set; } = new();
    public List<MemoryPhoto> FinalPhotos { get; set; } = new();
    public List<MemoryPhoto> AllPhotos { get; set; } = new();
    public List<MemoryEntry> Memories { get; set; } = new();
    public bool IsUnlocked { get; set; } = true;
    public string MemorialUrl { get; set; } = string.Empty;
}
