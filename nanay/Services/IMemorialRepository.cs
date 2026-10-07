using nanay.Models;

namespace nanay.Services;

public interface IMemorialRepository
{
    Task<MemorialSettings> GetSettingsAsync();
    Task UpdateSettingsAsync(MemorialSettings settings);
    
    Task<List<MemoryPhoto>> GetPhotosAsync();
    Task<MemoryPhoto?> GetPhotoByIdAsync(string id);
    Task<MemoryPhoto> AddPhotoAsync(MemoryPhoto photo);
    Task UpdatePhotoAsync(MemoryPhoto photo);
    Task DeletePhotoAsync(string id);
    Task ReorderPhotosAsync(List<string> orderedIds);
    
    Task<List<MemoryEntry>> GetMemoriesAsync();
    Task<MemoryEntry?> GetMemoryByIdAsync(string id);
    Task<MemoryEntry> AddMemoryAsync(MemoryEntry entry);
    Task UpdateMemoryAsync(MemoryEntry entry);
    Task DeleteMemoryAsync(string id);
    Task ReorderMemoriesAsync(List<string> orderedIds);
    
    Task<MemorialViewModel> GetMemorialViewModelAsync(string? slug = null);
    Task<string> SaveUploadedFileAsync(IFormFile file, string subFolder);
}
