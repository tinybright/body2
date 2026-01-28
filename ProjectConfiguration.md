# Unity Project Configuration

This file contains configuration details for the Body2 Unity project.

## Unity Version
- Recommended: Unity 2020.3 LTS or later
- Minimum: Unity 2019.4 LTS

## Required Packages
- TextMeshPro (com.unity.textmeshpro)
- Unity UI (com.unity.ugui)

## Build Settings

### Android
- Minimum API Level: 21 (Android 5.0)
- Target API Level: 30 or higher
- Scripting Backend: IL2CPP recommended for release builds
- Graphics API: OpenGL ES 3.0 or Vulkan

### iOS
- Minimum iOS Version: 11.0
- Target SDK: Latest
- Architecture: ARM64

## Platform Specific Settings

### Mobile Optimization
- Use GPU Instancing where possible
- Enable Static Batching
- Use Level of Detail (LOD) for complex models
- Compress textures appropriately (ASTC for mobile)

## Project Settings

### Quality
- Shadow Quality: Medium-High
- Anti-Aliasing: 2x-4x MSAA on mobile
- VSync: On for smooth experience

### Physics
- Fixed Timestep: 0.02 (50 Hz)
- Raycasting is used for selection - ensure anatomy parts have colliders

### Input
- Both old Input Manager and new Input System supported
- Touch input automatically detected on mobile devices
