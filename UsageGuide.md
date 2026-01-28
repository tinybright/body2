# Usage Guide - Body2 Anatomy Viewer

## Quick Start Guide

### 1. Basic Setup in Unity Editor

#### Step 1: Create a New Scene
1. Open Unity
2. Create a new scene: `File > New Scene`
3. Save it in `Assets/Scenes/AnatomyViewer.unity`

#### Step 2: Setup the Camera
1. Select the Main Camera
2. Add the `CameraController` component
3. Configure settings:
   - Rotation Speed: 5
   - Min Distance: 2
   - Max Distance: 20
   - Zoom Speed: 2

#### Step 3: Create Manager GameObject
1. Create an empty GameObject: `GameObject > Create Empty`
2. Name it "AnatomyViewerManager"
3. Add these components:
   - `LayerManager`
   - `SelectionController`
   - `SearchManager`
   - `AnatomyViewerManager`
4. In `SelectionController`, assign the Main Camera
5. In `SearchManager`, assign references to LayerManager and SelectionController

#### Step 4: Create Camera Target
1. Create an empty GameObject at (0, 0, 0)
2. Name it "CameraTarget"
3. In the Main Camera's `CameraController`, assign this as the Target

### 2. Adding Anatomy Parts

#### Method A: Using Primitives (for testing)
1. Create a Cube: `GameObject > 3D Object > Cube`
2. Name it appropriately (e.g., "Humerus")
3. Add the `AnatomyPart` component
4. Configure:
   - Part Name: "Humerus"
   - Description: "The bone of the upper arm"
   - Part Type: Bone
   - Layer: Bone
5. Ensure it has a Collider (auto-added with primitives)
6. Create a highlight material and assign it

#### Method B: Using 3D Models
1. Import your 3D model (FBX, OBJ, etc.)
2. Drag the model into the scene
3. Add the `AnatomyPart` component
4. Add a Collider if not present (Box, Mesh, or Capsule Collider)
5. Configure the component as above

#### Method C: Using the Example Setup Script
1. Add `ExampleSetup` component to your manager GameObject
2. This will auto-generate sample anatomy parts with proper configuration
3. Press Play to see the generated parts

### 3. Creating UI

#### Setup Canvas
1. Create UI Canvas: `GameObject > UI > Canvas`
2. Set Canvas Scaler to "Scale with Screen Size"
3. Reference Resolution: 1920 x 1080

#### Info Panel
1. Create a Panel under Canvas
2. Add a Script component: `InfoPanelUI`
3. Create TextMeshPro text fields for:
   - Name (larger, bold)
   - Description (multi-line)
   - Type
   - Layer
4. Assign these fields in the `InfoPanelUI` component
5. Assign the SelectionController reference

#### Layer Control Panel
1. Create another Panel under Canvas
2. Add `LayerControlUI` component
3. Create Toggle UI elements:
   - 1 toggle for Bones
   - 7 toggles for Muscle layers 1-7
4. Create buttons for "Show All" and "Hide All"
5. Assign all references in `LayerControlUI`

#### Search Panel
1. Create a Panel for search
2. Add `SearchUI` component
3. Create:
   - TMP Input Field for search query
   - Button for search
   - Button for clear
   - Scroll View for results
   - Result item prefab (button with text)
   - "No Results" text
4. Assign all references

### 4. Building for Mobile

#### Android Build
1. Go to `File > Build Settings`
2. Select Android
3. Click "Switch Platform"
4. Go to `Player Settings`
5. Configure:
   - Company Name
   - Product Name
   - Package Name (com.yourcompany.body2)
   - Minimum API Level: 21
   - Target API Level: 30+
6. Under Other Settings:
   - Scripting Backend: IL2CPP
   - Target Architectures: ARM64
7. Click "Build" or "Build and Run"

#### iOS Build
1. Go to `File > Build Settings`
2. Select iOS
3. Click "Switch Platform"
4. Configure Player Settings:
   - Bundle Identifier
   - Minimum iOS Version: 11.0
5. Click "Build"
6. Open the generated Xcode project
7. Build and deploy from Xcode

## User Controls

### PC Controls
- **Left Mouse Button + Drag**: Rotate camera around model
- **Middle Mouse Button + Drag**: Pan camera
- **Mouse Wheel**: Zoom in/out
- **Left Click on Part**: Select anatomy part

### Mobile Controls
- **One Finger Drag**: Rotate camera
- **Two Finger Pinch**: Zoom in/out
- **Two Finger Drag**: Pan camera
- **Tap on Part**: Select anatomy part

## Programming Interface

### Accessing Components
```csharp
// Get the main manager
AnatomyViewerManager manager = FindObjectOfType<AnatomyViewerManager>();

// Access sub-components
LayerManager layerMgr = manager.layerManager;
SelectionController selector = manager.selectionController;
SearchManager search = manager.searchManager;
```

### Controlling Layers Programmatically
```csharp
// Show only bones
layerManager.ShowOnlyLayers(AnatomyLayer.Bone);

// Show bones and first muscle layer
layerManager.ShowOnlyLayers(AnatomyLayer.Bone, AnatomyLayer.Muscle1);

// Toggle a specific layer
layerManager.ToggleLayer(AnatomyLayer.Muscle3);

// Show all layers
layerManager.ShowAllLayers();
```

### Searching Programmatically
```csharp
// Perform a search
searchManager.Search("humerus");

// Get results
List<AnatomyPart> results = searchManager.GetResults();

// Select first result
if (results.Count > 0)
{
    selectionController.SelectPart(results[0]);
}
```

### Listening to Events
```csharp
// Listen for selection changes
selectionController.onPartSelected.AddListener((part) =>
{
    Debug.Log($"Selected: {part.partName}");
});

// Listen for search results
searchManager.onSearchResults.AddListener((results) =>
{
    Debug.Log($"Found {results.Count} results");
});
```

## Common Tasks

### Task 1: Progressive Layer Reveal
Show layers progressively from superficial to deep:
```csharp
public void RevealToLayer(int layerNumber)
{
    layerManager.HideAllLayers();
    layerManager.SetLayerVisibility(AnatomyLayer.Bone, true);
    
    for (int i = 1; i <= layerNumber && i <= 7; i++)
    {
        layerManager.SetLayerVisibility((AnatomyLayer)i, true);
    }
}
```

### Task 2: Focus on Specific Body Region
```csharp
public void FocusOnRegion(string regionName)
{
    // Search for all parts in the region
    searchManager.Search(regionName);
    var results = searchManager.GetResults();
    
    if (results.Count > 0)
    {
        // Calculate center of region
        Vector3 center = Vector3.zero;
        foreach (var part in results)
        {
            center += part.transform.position;
        }
        center /= results.Count;
        
        // Focus camera
        cameraController.FocusOnPoint(center);
    }
}
```

### Task 3: Educational Mode
Create a quiz or educational sequence:
```csharp
public class EducationalMode : MonoBehaviour
{
    public AnatomyDatabase database;
    private int currentIndex = 0;
    
    public void ShowNextPart()
    {
        if (currentIndex < database.parts.Count)
        {
            var partData = database.parts[currentIndex];
            searchManager.Search(partData.name);
            var results = searchManager.GetResults();
            
            if (results.Count > 0)
            {
                selectionController.SelectPart(results[0]);
            }
            
            currentIndex++;
        }
    }
}
```

## Troubleshooting

### Issue: Parts not selectable
- Ensure each anatomy part has a Collider component
- Check that SelectionController has the correct LayerMask
- Verify that the camera is assigned in SelectionController

### Issue: Layers not hiding/showing
- Ensure all anatomy parts have AnatomyPart component
- Call `layerManager.RefreshAnatomyParts()` after adding new parts
- Check that parts have correct layer assignments

### Issue: Search not working
- Verify SearchManager has references to LayerManager and SelectionController
- Call `searchManager.RefreshAnatomyParts()` after adding new parts
- Check minimum search length setting

### Issue: Camera controls not working
- Ensure CameraController has a target assigned
- Check that the camera component is attached to an active GameObject
- Verify Input System is enabled in Project Settings

## Performance Optimization

### For Large Anatomy Models
1. Use Level of Detail (LOD) groups
2. Implement occlusion culling
3. Use texture atlasing
4. Enable GPU instancing on materials
5. Reduce polygon count on distant parts

### For Mobile Devices
1. Limit texture sizes (1024x1024 max for most parts)
2. Use compressed texture formats (ASTC)
3. Reduce shadow quality
4. Use simplified shaders
5. Implement object pooling for UI elements

## Best Practices

1. **Organization**: Keep anatomy parts organized in hierarchy by system (skeletal, muscular, etc.)
2. **Naming**: Use consistent naming conventions for parts
3. **Materials**: Create a library of reusable materials
4. **Data**: Store anatomy data in ScriptableObjects for easy editing
5. **Testing**: Test on actual mobile devices, not just the editor
6. **Performance**: Profile regularly to identify bottlenecks

## Additional Resources

- Unity Documentation: https://docs.unity3d.com/
- TextMeshPro Guide: https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest
- Mobile Optimization: https://docs.unity3d.com/Manual/MobileOptimization.html
- Z-Anatomy Reference: https://github.com/LluisV/Z-Anatomy
