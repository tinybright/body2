# API Documentation - Body2 Anatomy Viewer

## Core Classes

### AnatomyPart

Base component for all anatomy objects.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `partName` | string | Display name of the anatomy part |
| `description` | string | Detailed description |
| `partType` | AnatomyType | Type (Bone or Muscle) |
| `layer` | AnatomyLayer | Layer assignment (Bone, Muscle1-7) |
| `highlightMaterial` | Material | Material used for highlighting |

#### Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `Highlight()` | void | Apply highlight to this part |
| `Unhighlight()` | void | Remove highlight |
| `SetVisible(bool)` | void | Show or hide this part |
| `MatchesSearch(string)` | bool | Check if part matches search query |

---

### LayerManager

Manages visibility of anatomy layers.

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `ToggleLayer` | AnatomyLayer | void | Toggle visibility of a layer |
| `SetLayerVisibility` | AnatomyLayer, bool | void | Set layer visibility |
| `IsLayerVisible` | AnatomyLayer | bool | Check if layer is visible |
| `ShowAllLayers` | - | void | Show all layers |
| `HideAllLayers` | - | void | Hide all layers |
| `ShowOnlyLayers` | params AnatomyLayer[] | void | Show only specified layers |
| `RefreshAnatomyParts` | - | void | Refresh list of parts from scene |
| `UpdateAllVisibility` | - | void | Update visibility based on settings |

---

### SelectionController

Handles user input for selecting anatomy parts.

#### Events

| Event | Type | Description |
|-------|------|-------------|
| `onPartSelected` | AnatomyPartSelectedEvent | Triggered when part is selected |
| `onSelectionCleared` | UnityEvent | Triggered when selection is cleared |

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `SelectPart` | AnatomyPart | void | Programmatically select a part |
| `ClearSelection` | - | void | Clear current selection |
| `GetSelectedPart` | - | AnatomyPart | Get currently selected part |

---

### CameraController

Controls camera movement, rotation, and zoom.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `target` | Transform | Target to orbit around |
| `rotationSpeed` | float | Rotation speed (default: 5) |
| `minDistance` | float | Minimum zoom distance (default: 2) |
| `maxDistance` | float | Maximum zoom distance (default: 20) |
| `zoomSpeed` | float | Zoom speed (default: 2) |
| `panSpeed` | float | Pan speed (default: 0.5) |
| `enableDamping` | bool | Enable smooth damping (default: true) |

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `ResetCamera` | - | void | Reset to default position |
| `FocusOnPoint` | Vector3 | void | Focus camera on a point |

---

### SearchManager

Provides search functionality.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `minSearchLength` | int | Minimum search query length (default: 2) |

#### Events

| Event | Type | Description |
|-------|------|-------------|
| `onSearchResults` | SearchResultsEvent | Triggered when results update |

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `Search` | string | void | Perform search with query |
| `ClearSearch` | - | void | Clear current search |
| `GetResults` | - | List<AnatomyPart> | Get current search results |
| `SelectSearchResult` | int | void | Select result by index |
| `HighlightResults` | - | void | Highlight all results |
| `UnhighlightResults` | - | void | Remove highlight from results |
| `RefreshAnatomyParts` | - | void | Refresh parts list |

---

### AnatomyViewerManager

Main coordinator for all components.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `layerManager` | LayerManager | Reference to layer manager |
| `selectionController` | SelectionController | Reference to selection controller |
| `cameraController` | CameraController | Reference to camera controller |
| `searchManager` | SearchManager | Reference to search manager |
| `autoFindComponents` | bool | Auto-find components (default: true) |

#### Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `ResetViewer()` | void | Reset all components to default |
| `ShowBonesOnly()` | void | Show only bone layer |
| `ShowMusclesOnly()` | void | Show only muscle layers |
| `ToggleBonesAndMuscles()` | void | Toggle between bones and muscles |

---

### AnatomyDatabase

ScriptableObject for storing anatomy data.

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `GetPartsByType` | AnatomyType | List<AnatomyPartData> | Get all parts of a type |
| `GetPartsByLayer` | AnatomyLayer | List<AnatomyPartData> | Get parts in a layer |
| `FindPartByName` | string | AnatomyPartData | Find part by name |

---

## Enums

### AnatomyLayer

```csharp
public enum AnatomyLayer
{
    Bone = 0,
    Muscle1 = 1,
    Muscle2 = 2,
    Muscle3 = 3,
    Muscle4 = 4,
    Muscle5 = 5,
    Muscle6 = 6,
    Muscle7 = 7
}
```

### AnatomyType

```csharp
public enum AnatomyType
{
    Bone,
    Muscle
}
```

---

## UI Components

### InfoPanelUI

Displays information about selected anatomy part.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `panel` | GameObject | Panel container |
| `nameText` | TextMeshProUGUI | Name display field |
| `descriptionText` | TextMeshProUGUI | Description field |
| `typeText` | TextMeshProUGUI | Type field |
| `layerText` | TextMeshProUGUI | Layer field |

#### Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `ShowInfo` | AnatomyPart | Display info for a part |
| `HideInfo` | - | Hide the info panel |

---

### LayerControlUI

Controls for layer visibility.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `boneToggle` | Toggle | Toggle for bone layer |
| `muscleToggles` | Toggle[] | Toggles for muscle layers 1-7 |
| `showAllButton` | Button | Show all button |
| `hideAllButton` | Button | Hide all button |

#### Methods

| Method | Description |
|--------|-------------|
| `SyncWithLayerManager()` | Sync UI with layer manager state |

---

### SearchUI

UI for search functionality.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `searchInput` | TMP_InputField | Search input field |
| `searchButton` | Button | Search button |
| `clearButton` | Button | Clear button |
| `resultsContainer` | GameObject | Container for results |
| `resultItemPrefab` | GameObject | Prefab for result items |
| `noResultsText` | TextMeshProUGUI | No results message |

---

## Utility Classes

### MaterialUtility

Static utility for creating materials.

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `CreateHighlightMaterial` | Color | Material | Create highlight material |
| `CreateBoneMaterial` | - | Material | Create bone material |
| `CreateMuscleMaterial` | AnatomyLayer | Material | Create muscle material |
| `GetMuscleLayerColor` | AnatomyLayer | Color | Get color for layer |
| `CreateTransparentMaterial` | Color, float | Material | Create transparent material |

### AnatomyUtility

Static utility for anatomy operations.

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `GetLayerName` | AnatomyLayer | string | Get display name for layer |
| `CalculateBounds` | AnatomyPart[] | Bounds | Calculate combined bounds |
| `GetPartsInLayer` | AnatomyLayer | AnatomyPart[] | Get all parts in layer |
| `GetAllBones` | - | AnatomyPart[] | Get all bone parts |
| `GetAllMuscles` | - | AnatomyPart[] | Get all muscle parts |

### InputUtility

Static utility for input handling.

#### Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `IsTouching()` | bool | Check if touching/clicking |
| `TouchBegan()` | bool | Check if touch just started |
| `TouchEnded()` | bool | Check if touch just ended |
| `GetTouchPosition()` | Vector2 | Get touch/mouse position |
| `GetTouchCount()` | int | Get number of touches |

---

## Usage Examples

### Example 1: Programmatic Layer Control

```csharp
// Get the layer manager
LayerManager layerMgr = FindObjectOfType<LayerManager>();

// Show only superficial muscles
layerMgr.ShowOnlyLayers(AnatomyLayer.Bone, AnatomyLayer.Muscle1);

// Toggle muscle layer 3
layerMgr.ToggleLayer(AnatomyLayer.Muscle3);
```

### Example 2: Search and Select

```csharp
// Get the search manager
SearchManager searchMgr = FindObjectOfType<SearchManager>();

// Perform search
searchMgr.Search("humerus");

// Listen for results
searchMgr.onSearchResults.AddListener((results) =>
{
    if (results.Count > 0)
    {
        Debug.Log($"Found {results.Count} results");
    }
});
```

### Example 3: Custom Selection Handler

```csharp
// Get selection controller
SelectionController selector = FindObjectOfType<SelectionController>();

// Listen for selection
selector.onPartSelected.AddListener((part) =>
{
    Debug.Log($"Selected: {part.partName}");
    Debug.Log($"Description: {part.description}");
    Debug.Log($"Layer: {part.layer}");
});
```

### Example 4: Progressive Layer Reveal

```csharp
public IEnumerator RevealLayersSequentially()
{
    LayerManager layerMgr = FindObjectOfType<LayerManager>();
    layerMgr.HideAllLayers();
    
    // Show bone layer first
    layerMgr.SetLayerVisibility(AnatomyLayer.Bone, true);
    yield return new WaitForSeconds(2);
    
    // Reveal muscle layers progressively
    for (int i = 1; i <= 7; i++)
    {
        layerMgr.SetLayerVisibility((AnatomyLayer)i, true);
        yield return new WaitForSeconds(1);
    }
}
```

---

## Events

### AnatomyPartSelectedEvent

Triggered when an anatomy part is selected.

**Signature:** `void(AnatomyPart part)`

**Example:**
```csharp
selectionController.onPartSelected.AddListener((part) =>
{
    // Handle selection
});
```

### SearchResultsEvent

Triggered when search results are updated.

**Signature:** `void(List<AnatomyPart> results)`

**Example:**
```csharp
searchManager.onSearchResults.AddListener((results) =>
{
    // Handle results
});
```

---

## Configuration

### ViewerConfiguration

ScriptableObject for viewer settings.

Create via: `Assets > Create > Anatomy Viewer > Viewer Configuration`

#### Properties

- `defaultVisibleLayers`: Layers visible on start
- `initialCameraDistance`: Starting camera distance
- `cameraRotationSpeed`: Rotation speed
- `highlightColor`: Selection highlight color
- `minimumSearchLength`: Min search characters
- `autoSelectFirstResult`: Auto-select first search result

#### Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `ApplyToViewer` | AnatomyViewerManager | Apply config to viewer |

---

## Localization

### LocalizationManager

Manages translations for multiple languages.

#### Supported Languages

- English
- Chinese (中文)

#### Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `GetString` | string | string | Get translated string |
| `SetLanguage` | Language | void | Set current language |
| `GetLayerName` | AnatomyLayer | string | Get localized layer name |

---

## Editor Tools

### AnatomyViewerSetupWindow

Unity Editor window for setup.

**Access:** `Tools > Anatomy Viewer > Setup Wizard`

#### Features

- Create Manager GameObject
- Setup Camera
- Generate Sample Anatomy Parts
- Create Sample Database Asset
- Add Highlight Materials
- Auto-Configure Anatomy Parts
