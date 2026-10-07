using System.Text.Json;
using nanay.Models;

namespace nanay.Services;

public class MemorialRepository : IMemorialRepository
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<MemorialRepository> _logger;
    private readonly string _dataFilePath;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private MemorialData? _cachedData;

    public MemorialRepository(IWebHostEnvironment env, ILogger<MemorialRepository> logger)
    {
        _env = env;
        _logger = logger;
        _dataFilePath = Path.Combine(_env.ContentRootPath, "Data", "memorial_data.json");
    }

    private async Task<MemorialData> LoadDataAsync()
    {
        if (_cachedData != null)
        {
            return _cachedData;
        }

        await _semaphore.WaitAsync();
        try
        {
            if (_cachedData != null) return _cachedData;

            if (File.Exists(_dataFilePath))
            {
                var json = await File.ReadAllTextAsync(_dataFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _cachedData = JsonSerializer.Deserialize<MemorialData>(json, options) ?? CreateDefaultData();
            }
            else
            {
                _cachedData = CreateDefaultData();
                await SaveDataInternalAsync(_cachedData);
            }

            return _cachedData;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task SaveDataInternalAsync(MemorialData data)
    {
        var dir = Path.GetDirectoryName(_dataFilePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(data, options);
        await File.WriteAllTextAsync(_dataFilePath, json);
        _cachedData = data;
    }

    private MemorialData CreateDefaultData()
    {
        var settings = new MemorialSettings
        {
            AuntName = "Arlen Restauro",
            MemorialSlug = "arlen-restauro",
            Subtitle = "Forever loved. Forever remembered.",
            BirthDate = "July 3, 1999",
            PassingDate = "September 28, 2026",
            OpeningQuote1 = "Some people leave footprints on our hearts...",
            OpeningQuote2 = "...that time can never erase.",
            HeroImage = "/uploads/portrait-formal.png",
            HeroQuote1 = "Your story did not end here.",
            HeroQuote2 = "It continues in everyone who remembers you.",
            Chapter1Title = "CHAPTER 1 — HER",
            Chapter1Quote = "Before she became a memory,\nshe was a person who made ordinary moments special.",
            Chapter1Image = "/uploads/portrait-formal.png",
            Chapter2Title = "CHAPTER 2 — THE MOMENTS",
            Chapter2Quote = "One ordinary afternoon.\nOne memory we would give anything to experience again.",
            Chapter3Title = "CHAPTER 3 — THE PEOPLE SHE LOVED",
            Chapter3Quote1 = "She was part of our lives in ways we didn't always notice.",
            Chapter3Quote2 = "Until we had to imagine life without her.",
            RevealQuote1 = "Some memories are too precious to explain.",
            RevealQuote2 = "I wish I could go back to this moment.",
            RevealImage = "/uploads/aquarium-memory.jpg",
            EnableMusic = false,
            AudioPath = "",
            AudioTitle = "",
            AudioPromptText = "",
            IsVoiceRecording = false,
            VoicePromptText = "",
            LetterTitle = "If I Could Tell You One More Thing...",
            LetterParagraph1 = "There are moments when silence fills the room, and all I can think about is your laugh, your voice, and how you made everyone feel so safe and welcomed.",
            LetterParagraph2 = "You taught us what unconditional love looks like. You never asked for anything in return, yet you gave everything you had—your time, your prayers, your radiant warmth, and your endless kindness.",
            LetterParagraph3 = "If love alone could have kept you with us, you would have lived forever. We miss you deeply, but every single day we will live with the love and courage you gave us.",
            LetterClosing1 = "I love you.",
            LetterClosing2 = "I miss you.",
            LetterClosing3 = "Thank you for being part of my life.",
            FinalImage = "/uploads/strawberries-sunshine.jpg",
            FinalQuote1 = "You may be gone from our sight...",
            FinalQuote2 = "...but never from our lives.",
            FinalQuote3 = "Until we meet again.",
            IsPasswordProtected = false,
            VisitorPassword = "",
            AdminPassword = "nanay2026"
        };

        var photos = new List<MemoryPhoto>
        {
            new MemoryPhoto
            {
                Id = "p1",
                ImagePath = "/uploads/portrait-formal.png",
                Caption = "Her timeless grace and beautiful gentle smile.",
                MemoryDate = "Celebration of Grace",
                Location = "Manila",
                Chapter = "Hero",
                DisplayOrder = 1,
                IsHero = true,
                IsFinalPhoto = false,
                AltText = "Portrait of Arlen Restauro in formal pink dress"
            },
            new MemoryPhoto
            {
                Id = "p2",
                ImagePath = "/uploads/home-smile.png",
                Caption = "Her vibrant energy lighting up the home in her favorite floral dress.",
                MemoryDate = "Sunday Afternoon",
                Location = "Home",
                Chapter = "Chapter1",
                DisplayOrder = 2,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen Restauro standing happily at home"
            },
            new MemoryPhoto
            {
                Id = "p3",
                ImagePath = "/uploads/beach-surfboard.jpg",
                Caption = "Sunshine, gentle ocean breezes, and a carefree afternoon by the sea.",
                MemoryDate = "Summer Getaway",
                Location = "By the Sea",
                Chapter = "Chapter2",
                DisplayOrder = 3,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen at the beach with sunglasses"
            },
            new MemoryPhoto
            {
                Id = "p4",
                ImagePath = "/uploads/peace-sign-home.jpg",
                Caption = "Playful moments, shared laughter, and warmth that made every day bright.",
                MemoryDate = "Home Sweet Home",
                Location = "Home",
                Chapter = "Chapter2",
                DisplayOrder = 4,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen smiling with peace sign together with her husband"
            },
            new MemoryPhoto
            {
                Id = "p5",
                ImagePath = "/uploads/strawberries-sunshine.jpg",
                Caption = "Joy in the simple things. A basket of freshly picked strawberries and her unforgettable warmth.",
                MemoryDate = "Farm Visit",
                Location = "Strawberry Farm",
                Chapter = "Chapter2",
                DisplayOrder = 5,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen holding a basket of fresh strawberries"
            },
            new MemoryPhoto
            {
                Id = "p6",
                ImagePath = "/uploads/family-gathering-viewpoint.png",
                Caption = "Surrounded by all the family, children, and loved ones whose lives were touched by her boundless love.",
                MemoryDate = "Family Reunion & Gathering",
                Location = "Mountain Overlook",
                Chapter = "Chapter3",
                DisplayOrder = 6,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Big family reunion with Arlen, husband, children, and relatives on the scenic viewpoint"
            },
            new MemoryPhoto
            {
                Id = "p7",
                ImagePath = "/uploads/birds-park-together.jpg",
                Caption = "Surrounded by color and laughter. A joyful afternoon together with the birds.",
                MemoryDate = "Avian Sanctuary Visit",
                Location = "Bird Sanctuary",
                Chapter = "Chapter3",
                DisplayOrder = 7,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen and husband with colorful birds perched happily"
            },
            new MemoryPhoto
            {
                Id = "p8",
                ImagePath = "/uploads/emerald-dress-couple.jpg",
                Caption = "Side by side through every season of life.",
                MemoryDate = "Celebration of Love",
                Location = "Family Gathering",
                Chapter = "Chapter3",
                DisplayOrder = 8,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen in green dress standing lovingly with husband"
            },
            new MemoryPhoto
            {
                Id = "p9",
                ImagePath = "/uploads/navy-jumpsuit-smile.jpg",
                Caption = "Arm in arm, smiling through every journey.",
                MemoryDate = "Weekend Outing",
                Location = "Outdoors",
                Chapter = "Chapter3",
                DisplayOrder = 9,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen in navy jumpsuit with husband"
            },
            new MemoryPhoto
            {
                Id = "p10",
                ImagePath = "/uploads/church-altar-blessing.png",
                Caption = "A shared foundation of faith, quiet prayers, and enduring devotion.",
                MemoryDate = "Sunday Mass & Blessing",
                Location = "Church",
                Chapter = "Chapter3",
                DisplayOrder = 10,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen and husband at church in front of the altar and cross"
            },
            new MemoryPhoto
            {
                Id = "p11",
                ImagePath = "/uploads/aquarium-memory.jpg",
                Caption = "Always caring, always attentive—watching over the little ones with quiet tenderness.",
                MemoryDate = "Family Day Trip",
                Location = "Aquarium Garden",
                Chapter = "Reveal",
                DisplayOrder = 11,
                IsHero = false,
                IsFinalPhoto = false,
                AltText = "Arlen by the koi pond caring for baby"
            }
        };

        var memories = new List<MemoryEntry>
        {
            new MemoryEntry
            {
                Id = "m1",
                Title = "Her Unmistakable Laugh",
                Content = "She had a laugh that could fill any quiet room with instant warmth. Even during hard days, she would find a way to make everyone smile.",
                Category = "Her Spirit",
                DisplayOrder = 1
            },
            new MemoryEntry
            {
                Id = "m2",
                Title = "The Way She Called My Name",
                Content = "A gentle, affectionate voice that immediately meant you were safe, loved, and never alone.",
                Category = "Voice & Warmth",
                DisplayOrder = 2
            },
            new MemoryEntry
            {
                Id = "m3",
                Title = "Her Warm Kitchen & Signature Dishes",
                Content = "No one ever left her home hungry. She cooked with all her heart, serving each meal with love that felt like a warm embrace.",
                Category = "Her Kitchen",
                DisplayOrder = 3
            },
            new MemoryEntry
            {
                Id = "m4",
                Title = "Something She Always Said",
                Content = "\"Huwag kang mag-alala, magiging maayos din ang lahat.\" (Do not worry, everything will be alright.) Words of comfort we hold onto forever.",
                Category = "Words of Wisdom",
                DisplayOrder = 4
            },
            new MemoryEntry
            {
                Id = "m5",
                Title = "A Lesson She Taught Me",
                Content = "To always choose kindness, even when the world makes it difficult, and to always hold your family close.",
                Category = "Life Lesson",
                DisplayOrder = 5
            }
        };

        return new MemorialData
        {
            Settings = settings,
            Photos = photos,
            Memories = memories
        };
    }

    public async Task<MemorialSettings> GetSettingsAsync()
    {
        var data = await LoadDataAsync();
        return data.Settings;
    }

    public async Task UpdateSettingsAsync(MemorialSettings settings)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            data.Settings = settings;
            await SaveDataInternalAsync(data);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<MemoryPhoto>> GetPhotosAsync()
    {
        var data = await LoadDataAsync();
        return data.Photos.OrderBy(p => p.DisplayOrder).ToList();
    }

    public async Task<MemoryPhoto?> GetPhotoByIdAsync(string id)
    {
        var data = await LoadDataAsync();
        return data.Photos.FirstOrDefault(p => p.Id == id);
    }

    public async Task<MemoryPhoto> AddPhotoAsync(MemoryPhoto photo)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            if (string.IsNullOrEmpty(photo.Id))
            {
                photo.Id = Guid.NewGuid().ToString("N");
            }
            if (photo.DisplayOrder == 0)
            {
                photo.DisplayOrder = (data.Photos.Max(p => (int?)p.DisplayOrder) ?? 0) + 1;
            }
            data.Photos.Add(photo);
            await SaveDataInternalAsync(data);
            return photo;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task UpdatePhotoAsync(MemoryPhoto photo)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            var index = data.Photos.FindIndex(p => p.Id == photo.Id);
            if (index >= 0)
            {
                data.Photos[index] = photo;
                await SaveDataInternalAsync(data);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task DeletePhotoAsync(string id)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            var photo = data.Photos.FirstOrDefault(p => p.Id == id);
            if (photo != null)
            {
                data.Photos.Remove(photo);
                await SaveDataInternalAsync(data);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task ReorderPhotosAsync(List<string> orderedIds)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            for (int i = 0; i < orderedIds.Count; i++)
            {
                var p = data.Photos.FirstOrDefault(x => x.Id == orderedIds[i]);
                if (p != null)
                {
                    p.DisplayOrder = i + 1;
                }
            }
            data.Photos = data.Photos.OrderBy(p => p.DisplayOrder).ToList();
            await SaveDataInternalAsync(data);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<MemoryEntry>> GetMemoriesAsync()
    {
        var data = await LoadDataAsync();
        return data.Memories.OrderBy(m => m.DisplayOrder).ToList();
    }

    public async Task<MemoryEntry?> GetMemoryByIdAsync(string id)
    {
        var data = await LoadDataAsync();
        return data.Memories.FirstOrDefault(m => m.Id == id);
    }

    public async Task<MemoryEntry> AddMemoryAsync(MemoryEntry entry)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            if (string.IsNullOrEmpty(entry.Id))
            {
                entry.Id = Guid.NewGuid().ToString("N");
            }
            if (entry.DisplayOrder == 0)
            {
                entry.DisplayOrder = (data.Memories.Max(m => (int?)m.DisplayOrder) ?? 0) + 1;
            }
            data.Memories.Add(entry);
            await SaveDataInternalAsync(data);
            return entry;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task UpdateMemoryAsync(MemoryEntry entry)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            var index = data.Memories.FindIndex(m => m.Id == entry.Id);
            if (index >= 0)
            {
                data.Memories[index] = entry;
                await SaveDataInternalAsync(data);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task DeleteMemoryAsync(string id)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            var item = data.Memories.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                data.Memories.Remove(item);
                await SaveDataInternalAsync(data);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task ReorderMemoriesAsync(List<string> orderedIds)
    {
        await _semaphore.WaitAsync();
        try
        {
            var data = await LoadDataAsync();
            for (int i = 0; i < orderedIds.Count; i++)
            {
                var m = data.Memories.FirstOrDefault(x => x.Id == orderedIds[i]);
                if (m != null)
                {
                    m.DisplayOrder = i + 1;
                }
            }
            data.Memories = data.Memories.OrderBy(m => m.DisplayOrder).ToList();
            await SaveDataInternalAsync(data);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<MemorialViewModel> GetMemorialViewModelAsync(string? slug = null)
    {
        var data = await LoadDataAsync();
        var allPhotos = data.Photos.OrderBy(p => p.DisplayOrder).ToList();
        
        return new MemorialViewModel
        {
            Settings = data.Settings,
            AllPhotos = allPhotos,
            HeroPhotos = allPhotos.Where(p => p.IsHero || p.Chapter.Equals("Hero", StringComparison.OrdinalIgnoreCase)).ToList(),
            Chapter1Photos = allPhotos.Where(p => p.Chapter.Equals("Chapter1", StringComparison.OrdinalIgnoreCase)).ToList(),
            Chapter2Photos = allPhotos.Where(p => p.Chapter.Equals("Chapter2", StringComparison.OrdinalIgnoreCase)).ToList(),
            Chapter3Photos = allPhotos.Where(p => p.Chapter.Equals("Chapter3", StringComparison.OrdinalIgnoreCase)).ToList(),
            RevealPhotos = allPhotos.Where(p => p.Chapter.Equals("Reveal", StringComparison.OrdinalIgnoreCase)).ToList(),
            FinalPhotos = allPhotos.Where(p => p.IsFinalPhoto || p.Chapter.Equals("Final", StringComparison.OrdinalIgnoreCase)).ToList(),
            Memories = data.Memories.OrderBy(m => m.DisplayOrder).ToList(),
            IsUnlocked = !data.Settings.IsPasswordProtected
        };
    }

    public async Task<string> SaveUploadedFileAsync(IFormFile file, string subFolder)
    {
        var uploadsDir = Path.Combine(_env.WebRootPath, subFolder);
        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var safeFileName = $"{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(uploadsDir, safeFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/{subFolder.Replace('\\', '/')}/{safeFileName}";
    }
}
