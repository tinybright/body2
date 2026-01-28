# Body2 - Unity Anatomy Model Viewer

A mobile Unity application for viewing anatomical structures (bones and muscles) with interactive features.

## Features

### 1. Layer-Based Display Control
- **Bone Layer**: Display skeletal structures
- **Muscle Layers 1-7**: Display muscle groups in 7 different layers
- Toggle individual layers on/off
- Show/hide all layers at once

### 2. Interactive Selection
- Click/tap on any bone or muscle to select it
- Display detailed information including:
  - Anatomy part name
  - Description
  - Type (Bone/Muscle)
  - Layer information
- Visual highlighting of selected parts

### 3. Camera Controls
- **Rotation**: Drag with one finger (mobile) or left mouse button (PC) to orbit around the model
- **Zoom**: Pinch with two fingers (mobile) or mouse wheel (PC) to zoom in/out
- **Pan**: Drag with two fingers (mobile) or middle mouse button (PC) to move the view
- Touch-optimized for mobile devices

### 4. Search Functionality
- Search anatomy parts by name
- Real-time search results
- Click search results to select and highlight the anatomy part
- Auto-show the layer containing the searched part

## Project Structure

```
Assets/
├── Scripts/
│   ├── AnatomyPart.cs              # Base class for anatomy parts
│   ├── AnatomyLayer.cs             # Layer definitions (included in AnatomyPart.cs)
│   ├── LayerManager.cs             # Controls layer visibility
│   ├── SelectionController.cs      # Handles part selection via raycasting
│   ├── CameraController.cs         # Camera movement and controls
│   ├── SearchManager.cs            # Search functionality
│   ├── AnatomyViewerManager.cs     # Main coordinator
│   ├── AnatomyDatabase.cs          # Data structure for anatomy information
│   ├── SampleAnatomyData.cs        # Sample data generator
│   ├── InfoPanelUI.cs              # UI for displaying part information
│   ├── LayerControlUI.cs           # UI for layer controls
│   └── SearchUI.cs                 # UI for search functionality
├── Prefabs/                        # Prefabs for anatomy parts and UI
├── Scenes/                         # Unity scenes
├── Materials/                      # Materials for anatomy parts
├── UI/                            # UI assets
└── Resources/                      # Runtime resources
```

## Core Components

### AnatomyPart
The base component for all anatomy objects. Attach this to any GameObject representing a bone or muscle.

**Properties:**
- `partName`: Display name
- `description`: Detailed description
- `partType`: Bone or Muscle
- `layer`: Which layer (Bone, Muscle1-7)
- `highlightMaterial`: Material used when selected

### LayerManager
Manages visibility of different anatomy layers.

**Methods:**
- `ToggleLayer(AnatomyLayer)`: Toggle a specific layer
- `SetLayerVisibility(AnatomyLayer, bool)`: Set layer visibility
- `ShowAllLayers()`: Show all layers
- `HideAllLayers()`: Hide all layers
- `ShowOnlyLayers(params AnatomyLayer[])`: Show specific layers only

### SelectionController
Handles user input for selecting anatomy parts.

**Features:**
- Raycasting for both mouse and touch input
- Highlights selected parts
- Triggers events when selection changes

### CameraController
Controls camera movement with support for both PC and mobile.

**Controls:**
- **PC**: Left mouse to rotate, scroll wheel to zoom, middle mouse to pan
- **Mobile**: One finger to rotate, two-finger pinch to zoom, two-finger drag to pan

### SearchManager
Provides search functionality for anatomy parts.

**Features:**
- Real-time search
- Results sorted by relevance
- Auto-visibility of searched parts

## Setup Instructions

### For Unity Development

1. **Import the Project**
   - Open Unity Hub
   - Add the project folder
   - Open with Unity 2020.3 or later

2. **Create Your Scene**
   - Create a new scene
   - Add anatomy part GameObjects with 3D models
   - Attach `AnatomyPart` component to each object
   - Configure name, description, type, and layer

3. **Setup Managers**
   - Create an empty GameObject named "Managers"
   - Add the following components:
     - `LayerManager`
     - `SelectionController`
     - `CameraController`
     - `SearchManager`
     - `AnatomyViewerManager`

4. **Setup Camera**
   - Configure the main camera
   - The `CameraController` will handle movement
   - Set initial position and target

5. **Setup UI**
   - Create Canvas for UI elements
   - Add UI components:
     - Info Panel (using `InfoPanelUI`)
     - Layer Controls (using `LayerControlUI`)
     - Search Panel (using `SearchUI`)

6. **Build for Mobile**
   - Go to File > Build Settings
   - Select Android or iOS
   - Configure player settings
   - Build and deploy

## Data Format

Anatomy parts can be defined in the `AnatomyDatabase` ScriptableObject:

```csharp
{
    "name": "Humerus",
    "description": "The bone of the upper arm, extending from shoulder to elbow.",
    "type": "Bone",
    "layer": "Bone"
}
```

For muscles, specify the appropriate layer (Muscle1 through Muscle7).

## Sample Data

The project includes sample anatomy data with:
- 15 bone structures
- Multiple muscle groups across 7 layers

To use sample data, call `SampleAnatomyData.CreateSampleDatabase()`.

## Extending the System

### Adding New Anatomy Parts
1. Create a 3D model or primitive
2. Attach `AnatomyPart` component
3. Configure properties (name, description, type, layer)
4. Add collider for selection
5. Assign materials

### Adding Custom Layers
Modify the `AnatomyLayer` enum in `AnatomyPart.cs` to add more layers.

### Custom Highlighting
Create custom highlight materials and assign them to anatomy parts.

## Technical Requirements

- Unity 2020.3 or later
- TextMeshPro package (for UI text)
- Mobile device with touch support (for mobile deployment)

## License

This project is provided as-is for educational and development purposes.

## References

Based on the concept from [Z-Anatomy](https://github.com/LluisV/Z-Anatomy)