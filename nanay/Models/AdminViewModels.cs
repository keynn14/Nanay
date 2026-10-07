using System.ComponentModel.DataAnnotations;

namespace nanay.Models;

public class AdminLoginViewModel
{
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
    public string? ErrorMessage { get; set; }
}

public class PhotoUploadViewModel
{
    [Required]
    public IFormFile? PhotoFile { get; set; }

    public string Caption { get; set; } = string.Empty;
    public string MemoryDate { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Chapter { get; set; } = "Chapter2"; // Hero, Chapter1, Chapter2, Chapter3, Reveal, Final
    public int DisplayOrder { get; set; } = 0;
    public bool IsHero { get; set; } = false;
    public bool IsFinalPhoto { get; set; } = false;
}

public class PhotoEditViewModel
{
    public string Id { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public string MemoryDate { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Chapter { get; set; } = "Chapter2";
    public int DisplayOrder { get; set; } = 0;
    public bool IsHero { get; set; } = false;
    public bool IsFinalPhoto { get; set; } = false;
    public IFormFile? ReplacePhotoFile { get; set; }
}

public class MemoryEntryViewModel
{
    public string? Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = "Memory";
    public int DisplayOrder { get; set; } = 0;
}

public class AudioUploadViewModel
{
    public IFormFile? AudioFile { get; set; }
    public string AudioTitle { get; set; } = string.Empty;
    public string AudioPromptText { get; set; } = string.Empty;
    public bool IsVoiceRecording { get; set; } = false;
    public string VoicePromptText { get; set; } = string.Empty;
    public bool EnableMusic { get; set; } = true;
}

public class AdminDashboardViewModel
{
    public MemorialSettings Settings { get; set; } = new();
    public List<MemoryPhoto> Photos { get; set; } = new();
    public List<MemoryEntry> Memories { get; set; } = new();
    public string? StatusMessage { get; set; }
    public string AppBaseUrl { get; set; } = string.Empty;
}
