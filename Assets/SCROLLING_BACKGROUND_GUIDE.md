# Scrolling Background Setup Guide

This guide explains how to implement a scrolling background in your 2D Unity project.

## Two Approaches Available

### 1. Material-Based Approach (ScrollingBackground.cs)
**Best for**: Simple setup, GPU-efficient texture scrolling

### 2. Double Buffering Approach (ScrollingBackgroundSprite.cs)
**Best for**: More control, no material required, traditional sprite scrolling

---

## Approach 1: Material-Based (Recommended)

### Step-by-Step Setup

1. **Get or Create a Tileable Background Texture**
   - Place your texture in `Assets/Sprites/`
   - Recommended size: 512x512 or 1024x1024 pixels
   - Must be seamlessly tileable (top connects to bottom)

2. **Configure Texture Import Settings**
   - Select the texture in Unity
   - In Inspector:
     - Texture Type: **Sprite (2D and UI)**
     - Wrap Mode: **Repeat** *(Critical!)*
     - Click **Apply**

3. **Create a Material**
   - Right-click in `Assets/Materials/` → Create → Material
   - Name it "BackgroundMaterial"
   - In Inspector:
     - Shader: **Sprites/Default**
     - Drag your texture to the Texture slot

4. **Create Background GameObject**
   - In Hierarchy: Right-click → 2D Object → Sprite
   - Name it "Background"
   - In Inspector:
     - Transform Position: (0, 0, 10) *(z=10 keeps it behind other objects)*
     - Transform Scale: Adjust to cover screen
     - Sprite Renderer → Sprite: Assign your background texture
     - Sprite Renderer → Material: Assign your BackgroundMaterial

5. **Add and Configure Script**
   - Select the Background GameObject
   - Click Add Component → Search for "ScrollingBackground"
   - In the script component:
     - Scroll Speed: 2-5 (adjust to taste)
     - Background Material: Drag your BackgroundMaterial here

6. **Configure Sorting Layer**
   - Sprite Renderer → Sorting Layer: Set to "Background" or lowest priority
   - Order in Layer: -10 (or lowest value)

### Result
The background will scroll smoothly using texture offset animation.

---

## Approach 2: Double Buffering Sprites

### Step-by-Step Setup

1. **Get a Tileable Background Sprite**
   - Place in `Assets/Sprites/`
   - Must be vertically tileable

2. **Create Background Manager**
   - In Hierarchy: Create Empty GameObject
   - Name it "BackgroundManager"
   - Position: (0, 0, 0)

3. **Create Two Background Sprites**
   - Right-click BackgroundManager → 2D Object → Sprite
   - Name first one "Background1"
     - Position: (0, 0, 10)
     - Sprite: Assign your background sprite
   - Create second one "Background2"
     - Position: (0, spriteHeight, 10) *(you'll need to measure the sprite height)*
     - Sprite: Same as Background1
   - Both should have same scale to cover the screen

4. **Add and Configure Script**
   - Select BackgroundManager
   - Add Component → Search for "ScrollingBackgroundSprite"
   - In the script:
     - Scroll Speed: 2-5
     - Background1: Drag Background1 GameObject
     - Background2: Drag Background2 GameObject

5. **Configure Sorting Layers**
   - Both Background1 and Background2:
     - Sorting Layer: "Background"
     - Order in Layer: -10

### Result
The two sprites will continuously move downward, creating seamless infinite scrolling.

---

## Tips and Best Practices

### Scroll Speed
- **Subtle**: 2-3 (recommended for space themes)
- **Moderate**: 4-6
- **Fast**: 7-10
- Too fast can distract from gameplay

### Performance
- Material approach is more GPU-efficient
- Sprite approach uses more CPU but is simpler to understand

### Visual Quality
- Use power-of-two textures (512, 1024, 2048) for better performance
- Ensure seamless tiling to avoid visible seams
- Test different scroll speeds to match game pace

### Sorting Layers
- Create a "Background" sorting layer in Edit → Project Settings → Tags and Layers
- Set Order in Layer to -10 to ensure it's behind all game objects

### Multiple Parallax Layers
For depth effect, create multiple backgrounds:
- Layer 1 (farthest): scrollSpeed = 1, z = 10
- Layer 2 (middle): scrollSpeed = 2, z = 9
- Layer 3 (nearest): scrollSpeed = 3, z = 8

---

## Troubleshooting

### Background not scrolling
- Check that Material is assigned (Material approach)
- Check that both Transform references are set (Sprite approach)
- Verify scrollSpeed > 0

### Visible seams
- Ensure texture Wrap Mode is set to Repeat
- Use a properly tileable texture
- Check sprite positioning (Sprite approach)

### Background in front of player
- Set z-position to 10 or higher
- Configure Sorting Layer to "Background"
- Set Order in Layer to -10

### Performance issues
- Reduce texture size
- Use compressed texture format
- Switch to Material approach if using Sprite approach

---

## Free Asset Resources

- **Kenney.nl**: Space Shooter Pack (public domain)
- **OpenGameArt.org**: Search "space background"
- **Itch.io**: Free game assets section
- Create your own using tools like Photoshop, GIMP, or Aseprite
