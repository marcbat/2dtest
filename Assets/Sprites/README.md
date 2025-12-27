# Sprites Folder

This folder should contain background sprite textures for the scrolling background.

## Background Sprite Requirements

For the **ScrollingBackgroundSprite.cs** (double buffering) approach:
- Use a tileable texture that repeats seamlessly
- Recommended dimensions: 512x512 or 1024x1024 pixels
- Format: PNG
- Should be vertically tileable (top edge matches bottom edge)

## Import Settings

After importing your sprite:
1. Select the sprite in Unity
2. In the Inspector:
   - Texture Type: **Sprite (2D and UI)**
   - Wrap Mode: **Repeat** (for material approach) or **Clamp** (for sprite approach)
   - Filter Mode: **Bilinear** or **Trilinear**
   - Compression: Adjust based on quality needs
3. Click Apply

## Free Resources

- **Itch.io**: Search for "space background" or "scrolling background"
- **OpenGameArt.org**: Space-themed textures and backgrounds
- **Kenney.nl**: Space shooter asset packs with tileable backgrounds

## Example Themes

- Star fields
- Deep space with nebulae
- Grid/tech patterns
- Abstract space backgrounds
