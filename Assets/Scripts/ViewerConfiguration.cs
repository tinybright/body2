using UnityEngine;

namespace AnatomyViewer
{
    /// <summary>
    /// Configuration presets for the anatomy viewer
    /// </summary>
    [CreateAssetMenu(fileName = "ViewerConfig", menuName = "Anatomy Viewer/Viewer Configuration")]
    public class ViewerConfiguration : ScriptableObject
    {
        [Header("Display Settings")]
        [Tooltip("Default layers to show on start")]
        public AnatomyLayer[] defaultVisibleLayers = new AnatomyLayer[]
        {
            AnatomyLayer.Bone,
            AnatomyLayer.Muscle1
        };

        [Header("Camera Settings")]
        [Tooltip("Initial camera distance")]
        public float initialCameraDistance = 10f;

        [Tooltip("Camera rotation speed")]
        public float cameraRotationSpeed = 5f;

        [Tooltip("Camera zoom speed")]
        public float cameraZoomSpeed = 2f;

        [Header("Selection Settings")]
        [Tooltip("Highlight color for selected parts")]
        public Color highlightColor = Color.yellow;

        [Tooltip("Enable auto-rotation when part is selected")]
        public bool autoRotateOnSelection = false;

        [Header("Search Settings")]
        [Tooltip("Minimum characters required for search")]
        public int minimumSearchLength = 2;

        [Tooltip("Auto-select first search result")]
        public bool autoSelectFirstResult = true;

        [Header("UI Settings")]
        [Tooltip("Show info panel by default")]
        public bool showInfoPanel = true;

        [Tooltip("Show layer controls by default")]
        public bool showLayerControls = true;

        [Tooltip("Show search panel by default")]
        public bool showSearchPanel = true;

        [Header("Performance Settings")]
        [Tooltip("Maximum anatomy parts to display simultaneously")]
        public int maxVisibleParts = 1000;

        [Tooltip("Use level of detail (LOD) for performance")]
        public bool useLOD = true;

        [Tooltip("Enable occlusion culling")]
        public bool enableOcclusionCulling = true;

        /// <summary>
        /// Apply this configuration to the viewer
        /// </summary>
        public void ApplyToViewer(AnatomyViewerManager manager)
        {
            if (manager == null)
                return;

            // Apply layer visibility
            if (manager.layerManager != null)
            {
                manager.layerManager.HideAllLayers();
                foreach (var layer in defaultVisibleLayers)
                {
                    manager.layerManager.SetLayerVisibility(layer, true);
                }
            }

            // Apply camera settings
            if (manager.cameraController != null)
            {
                manager.cameraController.rotationSpeed = cameraRotationSpeed;
                manager.cameraController.zoomSpeed = cameraZoomSpeed;
            }

            // Apply search settings
            if (manager.searchManager != null)
            {
                manager.searchManager.minSearchLength = minimumSearchLength;
            }
        }
    }
}
