namespace nanay.Models;

public class MemorialSettings
{
    public string AuntName { get; set; } = "Arlen Restauro";
    public string MemorialSlug { get; set; } = "arlen-restauro";
    public string Subtitle { get; set; } = "Forever loved. Forever remembered.";
    public string BirthDate { get; set; } = "July 3, 1999";
    public string PassingDate { get; set; } = "September 28, 2026";
    
    // Opening screen texts
    public string OpeningQuote1 { get; set; } = "Some people leave footprints on our hearts...";
    public string OpeningQuote2 { get; set; } = "...that time can never erase.";
    
    // Hero section
    public string HeroImage { get; set; } = "/uploads/portrait-formal.png";
    public string HeroQuote1 { get; set; } = "Your story did not end here.";
    public string HeroQuote2 { get; set; } = "It continues in everyone who remembers you.";
    
    // Chapter 1 - Her
    public string Chapter1Title { get; set; } = "CHAPTER 1 — HER";
    public string Chapter1Quote { get; set; } = "Before she became a memory,\nshe was a person who made ordinary moments special.";
    public string Chapter1Image { get; set; } = "/uploads/home-smile.png";
    
    // Chapter 2 - The Moments
    public string Chapter2Title { get; set; } = "CHAPTER 2 — THE MOMENTS";
    public string Chapter2Quote { get; set; } = "One ordinary afternoon.\nOne memory we would give anything to experience again.";
    
    // Chapter 3 - The People She Loved
    public string Chapter3Title { get; set; } = "CHAPTER 3 — THE PEOPLE SHE LOVED";
    public string Chapter3Quote1 { get; set; } = "She was part of our lives in ways we didn't always notice.";
    public string Chapter3Quote2 { get; set; } = "Until we had to imagine life without her.";
    
    // Photo Reveal Effect
    public string RevealQuote1 { get; set; } = "Some memories are too precious to explain.";
    public string RevealQuote2 { get; set; } = "I wish I could go back to this moment.";
    public string RevealImage { get; set; } = "/uploads/aquarium-memory.jpg";
    
    // Audio / Voice Section (500 Miles by Peter, Paul and Mary)
    public bool EnableMusic { get; set; } = true;
    public string AudioPath { get; set; } = "/audio/500-miles.mp3";
    public string AudioTitle { get; set; } = "500 Miles — Peter, Paul and Mary";
    public string AudioPromptText { get; set; } = "Press play if you want to listen to 500 Miles, her favorite song.";
    public bool IsVoiceRecording { get; set; } = false;
    public string VoicePromptText { get; set; } = "A voice we wish we could hear one more time.";
    
    // Letter Section
    public string LetterTitle { get; set; } = "If I Could Tell You One More Thing...";
    public string LetterParagraph1 { get; set; } = "There are moments when silence fills the room, and all I can think about is your laugh, your voice, and how you made everyone feel so safe and welcomed.";
    public string LetterParagraph2 { get; set; } = "You taught us what unconditional love looks like. You never asked for anything in return, yet you gave everything you had—your time, your prayers, your radiant warmth, and your endless kindness.";
    public string LetterParagraph3 { get; set; } = "If love alone could have kept you with us, you would have lived forever. We miss you deeply, but every single day we will live with the love and courage you gave us.";
    public string LetterClosing1 { get; set; } = "I love you.";
    public string LetterClosing2 { get; set; } = "I miss you.";
    public string LetterClosing3 { get; set; } = "Thank you for being part of my life.";
    
    // Final Photo & Goodbye
    public string FinalImage { get; set; } = "/uploads/strawberries-sunshine.jpg";
    public string FinalQuote1 { get; set; } = "You may be gone from our sight...";
    public string FinalQuote2 { get; set; } = "...but never from our lives.";
    public string FinalQuote3 { get; set; } = "Until we meet again.";
    
    // Privacy & Security
    public bool IsPasswordProtected { get; set; } = false;
    public string VisitorPassword { get; set; } = "";
    public string AdminPassword { get; set; } = "nanay2026";
}
