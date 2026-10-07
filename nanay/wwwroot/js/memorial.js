/**
 * MEMORIAL CINEMATIC EXPERIENCE ENGINE
 * GSAP ScrollTrigger, Parallax Depth, Procedural Film Grain & Audio Player
 */

document.addEventListener('DOMContentLoaded', () => {
    // 01. Initialize Procedural Film Grain
    initFilmGrain();

    // 02. Detect prefers-reduced-motion
    const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    // 03. Run Opening Experience Sequence
    runOpeningSequence(prefersReducedMotion);

    // 04. Initialize GSAP ScrollTrigger Animations
    if (!prefersReducedMotion && typeof gsap !== 'undefined' && typeof ScrollTrigger !== 'undefined') {
        gsap.registerPlugin(ScrollTrigger);
        initScrollAnimations();
    } else {
        // Fallback for reduced motion: show all elements directly
        document.querySelectorAll('.hero-portrait, .reveal-photo').forEach(el => {
            el.style.filter = 'none';
            el.style.transform = 'none';
        });
        const shroud = document.getElementById('revealShroud');
        if (shroud) shroud.style.opacity = '0';
    }

    // 05. Initialize Audio Player
    initAudioPlayer();
});

/**
 * 01. PROCEDURAL FILM GRAIN GENERATOR
 * Generates lightweight, organic monochrome noise on HTML5 canvas
 */
function initFilmGrain() {
    const canvas = document.getElementById('film-grain-canvas');
    if (!canvas) return;

    const ctx = canvas.getContext('2d');
    let width = (canvas.width = window.innerWidth);
    let height = (canvas.height = window.innerHeight);

    window.addEventListener('resize', () => {
        width = canvas.width = window.innerWidth;
        height = canvas.height = window.innerHeight;
    });

    let frame = 0;
    function renderGrain() {
        // Generate noise every 3 frames for high 60fps performance on mobile
        if (frame % 3 === 0) {
            const imgData = ctx.createImageData(width, height);
            const data = imgData.data;
            const len = data.length;

            for (let i = 0; i < len; i += 16) {
                const noise = (Math.random() * 255) | 0;
                data[i] = noise;
                data[i + 1] = noise;
                data[i + 2] = noise;
                data[i + 3] = 20; // very low alpha for subtle texture
            }
            ctx.putImageData(imgData, 0, 0);
        }
        frame++;
        requestAnimationFrame(renderGrain);
    }

    renderGrain();
}

/**
 * 02. OPENING SEQUENCE TIMELINE
 * NFC Tap -> Silence -> Footprints -> Erase -> Name -> Scroll Indicator
 */
function runOpeningSequence(reducedMotion) {
    const line1 = document.querySelector('.section-opening .line-1');
    const line2 = document.querySelector('.section-opening .line-2');
    const nameBlock = document.querySelector('.opening-name-block');
    const scrollPrompt = document.getElementById('scrollPrompt');

    if (reducedMotion) {
        if (line1) { line1.style.opacity = '1'; line1.style.transform = 'none'; }
        if (line2) { line2.style.opacity = '1'; line2.style.transform = 'none'; }
        if (nameBlock) { nameBlock.style.opacity = '1'; nameBlock.style.transform = 'none'; }
        if (scrollPrompt) { scrollPrompt.style.opacity = '1'; }
        return;
    }

    // Step 1: ~1s pause of near silence/darkness, then Line 1
    setTimeout(() => {
        if (line1) {
            line1.style.opacity = '1';
            line1.style.transform = 'translateY(0)';
        }
    }, 1100);

    // Step 2: Line 2
    setTimeout(() => {
        if (line2) {
            line2.style.opacity = '1';
            line2.style.transform = 'translateY(0)';
        }
    }, 2800);

    // Step 3: Name and Subtitle reveal
    setTimeout(() => {
        if (nameBlock) {
            nameBlock.style.opacity = '1';
            nameBlock.style.transform = 'scale(1)';
        }
    }, 4500);

    // Step 4: Scroll prompt emerges
    setTimeout(() => {
        if (scrollPrompt) {
            scrollPrompt.style.opacity = '0.7';
        }
    }, 6200);
}

/**
 * 03. GSAP SCROLLTRIGGER ANIMATIONS
 * Cinematic parallax, blur-to-sharp, layered drift, and memory reveals
 */
function initScrollAnimations() {
    // HERO SECTION: Blur-to-Sharp & Parallax Scale
    const heroPortrait = document.getElementById('heroPortrait');
    if (heroPortrait) {
        gsap.to(heroPortrait, {
            scrollTrigger: {
                trigger: '#section-hero',
                start: 'top 80%',
                end: 'center center',
                scrub: 1.2
            },
            filter: 'blur(0px) brightness(1)',
            scale: 1.0,
            ease: 'power2.out'
        });

        // Parallax background drift
        const heroBg = document.querySelector('.hero-parallax-bg');
        if (heroBg) {
            gsap.to(heroBg, {
                scrollTrigger: {
                    trigger: '#section-hero',
                    start: 'top bottom',
                    end: 'bottom top',
                    scrub: true
                },
                y: 100,
                ease: 'none'
            });
        }
    }

    // CHAPTER 1: Portrait Reveal
    const chapter1Cards = document.querySelectorAll('.portrait-card');
    chapter1Cards.forEach(card => {
        gsap.from(card, {
            scrollTrigger: {
                trigger: card,
                start: 'top 85%',
                toggleActions: 'play none none none'
            },
            opacity: 0,
            y: 40,
            duration: 1.6,
            ease: 'power2.out'
        });
    });

    // CHAPTER 2: Vertical Moments Stream with Alternating Parallax
    const momentItems = document.querySelectorAll('.moment-item');
    momentItems.forEach(item => {
        const speed = parseFloat(item.getAttribute('data-parallax-speed') || '0.2');
        const img = item.querySelector('.moment-img');

        gsap.from(item, {
            scrollTrigger: {
                trigger: item,
                start: 'top 85%',
                toggleActions: 'play none none none'
            },
            opacity: 0,
            y: 50,
            duration: 1.4,
            ease: 'power3.out'
        });

        if (img) {
            gsap.to(img, {
                scrollTrigger: {
                    trigger: item,
                    start: 'top bottom',
                    end: 'bottom top',
                    scrub: 1
                },
                y: -30 * (speed * 4),
                scale: 1.05,
                ease: 'none'
            });
        }
    });

    // CHAPTER 3: Layered Photographs Gently Separating
    const layer1 = document.querySelector('.layer-card.layer-1');
    const layer2 = document.querySelector('.layer-card.layer-2');
    if (layer1 && layer2) {
        gsap.to(layer1, {
            scrollTrigger: {
                trigger: '#layeredStage',
                start: 'top 80%',
                end: 'bottom 20%',
                scrub: 1.5
            },
            x: -45,
            y: -25,
            rotation: -5,
            ease: 'power1.out'
        });

        gsap.to(layer2, {
            scrollTrigger: {
                trigger: '#layeredStage',
                start: 'top 80%',
                end: 'bottom 20%',
                scrub: 1.5
            },
            x: 45,
            y: 30,
            rotation: 6,
            ease: 'power1.out'
        });
    }

    // 06. PERSONAL MEMORIES REVEAL
    const memoryCards = document.querySelectorAll('.memory-card-reveal');
    memoryCards.forEach((card, idx) => {
        gsap.from(card, {
            scrollTrigger: {
                trigger: card,
                start: 'top 88%',
                toggleActions: 'play none none none'
            },
            opacity: 0,
            y: 35,
            duration: 1.2,
            delay: (idx % 2) * 0.15,
            ease: 'power2.out'
        });
    });

    // 07. PHOTO REVEAL EFFECT (EMERGING FROM DARKNESS)
    const revealPhoto = document.getElementById('revealPhoto');
    const revealShroud = document.getElementById('revealShroud');
    if (revealPhoto && revealShroud) {
        const revealTimeline = gsap.timeline({
            scrollTrigger: {
                trigger: '#section-reveal',
                start: 'top 65%',
                end: 'center center',
                scrub: 1.5
            }
        });

        revealTimeline
            .to(revealShroud, { opacity: 0, ease: 'power2.inOut' }, 0)
            .to(revealPhoto, { filter: 'blur(0px) brightness(1) opacity(1)', scale: 1.0, ease: 'power2.out' }, 0);
    }

    // 08. LETTER PARAGRAPHS REVEAL
    const letterParagraphs = document.querySelectorAll('.letter-paragraph, .signoff-line');
    letterParagraphs.forEach((p, idx) => {
        gsap.from(p, {
            scrollTrigger: {
                trigger: p,
                start: 'top 90%',
                toggleActions: 'play none none none'
            },
            opacity: 0,
            y: 25,
            duration: 1.4,
            ease: 'power2.out'
        });
    });

    // 10. FINAL PHOTO REVEAL
    const finalPhoto = document.querySelector('.final-photo-img');
    if (finalPhoto) {
        gsap.from(finalPhoto, {
            scrollTrigger: {
                trigger: '#section-final-photo',
                start: 'top 75%',
                toggleActions: 'play none none none'
            },
            opacity: 0,
            scale: 1.06,
            filter: 'brightness(0.3)',
            duration: 2.2,
            ease: 'power3.out'
        });
    }
}

/**
 * 04. AUDIO PLAYER
 * Handles audio playback, play/pause UI, and waveform animation
 */
function initAudioPlayer() {
    const audio = document.getElementById('memorialAudio');
    const toggleBtn = document.getElementById('audioToggleBtn');
    const waveform = document.getElementById('waveformBars');
    if (!audio || !toggleBtn) return;

    const playIcon = toggleBtn.querySelector('.play-icon');
    const pauseIcon = toggleBtn.querySelector('.pause-icon');

    toggleBtn.addEventListener('click', () => {
        if (audio.paused) {
            audio.play().then(() => {
                if (playIcon) playIcon.style.display = 'none';
                if (pauseIcon) pauseIcon.style.display = 'block';
                if (waveform) waveform.classList.add('playing');
            }).catch(err => {
                console.warn('Audio playback prevented:', err);
            });
        } else {
            audio.pause();
            if (playIcon) playIcon.style.display = 'block';
            if (pauseIcon) pauseIcon.style.display = 'none';
            if (waveform) waveform.classList.remove('playing');
        }
    });

    audio.addEventListener('ended', () => {
        if (playIcon) playIcon.style.display = 'block';
        if (pauseIcon) pauseIcon.style.display = 'none';
        if (waveform) waveform.classList.remove('playing');
    });
}

/**
 * 05. PASSWORD UNLOCK HANDLER
 */
function handleUnlock(event) {
    event.preventDefault();
    const passwordInput = document.getElementById('accessPassword');
    const errorDiv = document.getElementById('unlockError');
    const overlay = document.getElementById('unlockOverlay');
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

    if (!passwordInput) return;

    fetch('/Home/Unlock', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: new URLSearchParams({
            password: passwordInput.value,
            __RequestVerificationToken: token || ''
        })
    })
    .then(res => res.json())
    .then(data => {
        if (data.success) {
            overlay.style.transition = 'opacity 0.8s ease';
            overlay.style.opacity = '0';
            setTimeout(() => {
                overlay.style.display = 'none';
            }, 800);
        } else {
            if (errorDiv) {
                errorDiv.innerText = data.message || 'Incorrect password.';
                errorDiv.style.display = 'block';
            }
        }
    })
    .catch(err => {
        if (errorDiv) {
            errorDiv.innerText = 'An error occurred. Please try again.';
            errorDiv.style.display = 'block';
        }
    });
}
