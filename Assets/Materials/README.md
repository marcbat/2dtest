# Materials Folder

This folder should contain the Material used for the scrolling background.

## Creating a Background Material

1. In Unity, right-click in this folder → Create → Material
2. Name it "BackgroundMaterial"
3. In the Inspector:
   - Set Shader to: **Sprites/Default**
   - Assign your tileable background texture to the Texture field
4. Select your background texture in the Assets folder:
   - In the Inspector, set Wrap Mode to **Repeat**
   - Click Apply
5. Attach the ScrollingBackground.cs script to your Background GameObject
6. Assign this Material to the `backgroundMaterial` field in the script

## Recommended Background Textures

- Tileable space backgrounds (stars, nebulae, etc.)
- Size: 512x512 or 1024x1024 pixels
- Format: PNG
- Free resources:
  - Itch.io (space backgrounds)
  - OpenGameArt.org (space textures)
  - Kenney.nl (space shooter asset packs)
