# Digital Memorial Experience & NFC Card Setup

A cinematic, deeply personal digital memorial website built with **C# / ASP.NET Core**, **GSAP & ScrollTrigger**, and tailored for mobile NFC card taps.

---

## 🌹 The Emotional Journey Architecture

The experience is structured as an intimate digital photo album:

```
[ NFC CARD TAP ] 
       │
       ▼
1. [ NEAR SILENCE & DARKNESS ] 
   "Some people leave footprints on our hearts..." 
   "...that time can never erase."
   ✦ [AUNT'S NAME] ✦
   "Forever loved. Forever remembered."
       │
       ▼
2. [ HERO PORTRAIT REVEAL ]
   Blur-to-sharp parallax transition as you gently scroll down
   "Your story did not end here. It continues in everyone who remembers you."
       │
       ▼
3. [ CHAPTER I — HER ]
   Slow reveal portrait & reflections on ordinary moments made extraordinary
       │
       ▼
4. [ CHAPTER II — THE MOMENTS ]
   Vertical cinematic stream with dates, locations, and heartfelt captions
       │
       ▼
5. [ CHAPTER III — THE PEOPLE SHE LOVED ]
   Layered photographs that gently separate and drift on scroll
       │
       ▼
6. [ THINGS I WILL ALWAYS REMEMBER ]
   Interactive memory cards: Her laugh, her kitchen, signature phrases, life lessons
       │
       ▼
7. [ THE PHOTO REVEAL ]
   Emerges slowly from deep darkness: "Some memories are too precious to explain."
       │
       ▼
8. [ HER VOICE & MELODY ]
   Custom audio player with ambient glow & waveform visualization
       │
       ▼
9. [ IF I COULD TELL YOU ONE MORE THING... ]
   Handwritten-style letter with closing words: "I love you. I miss you."
       │
       ▼
10. [ FINAL PHOTO & GOODBYE ]
    "You may be gone from our sight... but never from our lives. Until we meet again."
    Quiet candle tribute with birth and passing dates.
```

---

## 🛠️ Project Structure

- **`Controllers/HomeController.cs`**: Handles root `/`, NFC tap `/tap` or `/nfc`, and slug-based routing (`/memorial/{slug}`).
- **`Controllers/AdminController.cs`**: Protected management dashboard for uploading photos, editing captions, updating memories, letters, and audio.
- **`Models/`**: `MemoryPhoto.cs`, `MemoryEntry.cs`, `MemorialSettings.cs`, `MemorialViewModel.cs`, `AdminViewModels.cs`.
- **`Services/MemorialRepository.cs`**: Thread-safe JSON persistence (`Data/memorial_data.json`) pre-seeded with your uploaded photographs.
- **`Views/Home/Index.cshtml`**: The 11-step cinematic memorial experience.
- **`Views/Admin/Dashboard.cshtml` & `Login.cshtml`**: Management portal.
- **`wwwroot/css/memorial.css`**: Film grain, vignettes, serif typography, and mobile-optimized layouts.
- **`wwwroot/js/memorial.js`**: GSAP ScrollTrigger parallax, procedural canvas film grain, audio player, and `prefers-reduced-motion` support.
- **`wwwroot/uploads/`**: High-resolution photographs.
- **`wwwroot/audio/`**: Memorial audio track.

---

## 🔑 Accessing the Admin Management Panel

1. Navigate to: **`http://localhost:5000/admin/login`** (or click the discreet `✦` at the very bottom of the memorial page).
2. Default Administrator Password: **`nanay2026`** *(can be changed inside the dashboard)*.
3. In the Admin Dashboard, you can:
   - Upload new photographs and assign them to specific chapters (Hero, Chapter 1, Chapter 2, Chapter 3, Reveal, Final).
   - Write captions, dates, and locations.
   - Add/edit personal memory cards under *"Things I Will Always Remember"*.
   - Edit the letter *"If I Could Tell You One More Thing..."*.
   - Upload favorite songs or real voice recordings.
   - Set an optional private family visitor password.

---

## 📡 Linking Your NFC Card

To program your physical NFC card (e.g., NTAG213, NTAG215, or NTAG216 card/sticker):

1. **Get an NFC Writer App** on your iPhone or Android:
   - **NFC Tools** (Free on iOS App Store and Google Play Store).
2. **Write the URL**:
   - Open **NFC Tools** → tap **Write** → **Add a record** → **URL / URI**.
   - Enter your memorial URL, e.g.: `https://your-domain.com/memorial/nanay` or `https://your-domain.com/tap`.
   - Tap **Write / 74 Bytes** and hold the NFC card to the top back of your smartphone until it confirms success.
3. **Test**:
   - Lock your phone or return to the home screen.
   - Tap the NFC card against the back of your phone.
   - The memorial website will immediately open in the browser.

---

## 🚀 Running & Deploying the Project

### Running Locally:
In your terminal, run:
```bash
dotnet run --project nanay/nanay.csproj
```
Open `https://localhost:5001` or `http://localhost:5000` in your mobile browser or desktop.

### Deploying to Production:
- **Publish**:
  ```bash
  dotnet publish -c Release -o ./publish
  ```
- Deploy to **Azure App Service**, **AWS**, or a **Linux VPS with Nginx / systemd**.
- Point your custom domain with SSL (HTTPS is required for modern phone browsers when scanning NFC tags).
