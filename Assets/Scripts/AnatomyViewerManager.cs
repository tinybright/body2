using UnityEngine;

namespace AnatomyViewer
{
    /// <summary>
    /// Main manager that coordinates all anatomy viewer components
    /// </summary>
    public class AnatomyViewerManager : MonoBehaviour
    {
        [Header("Core Components")]
        [Tooltip("Layer manager component")]
        public LayerManager layerManager;

        [Tooltip("Selection controller component")]
        public SelectionController selectionController;

        [Tooltip("Camera controller component")]
        public CameraController cameraController;

        [Tooltip("Search manager component")]
        public SearchManager searchManager;

        [Header("Settings")]
        [Tooltip("Auto-find components if not assigned")]
        public bool autoFindComponents = true;

        void Awake()
        {
            if (autoFindComponents)
            {
                FindComponents();
            }

            InitializeComponents();
        }

        /// <summary>
        /// Find components in the scene if not assigned
        /// </summary>
        private void FindComponents()
        {
            if (layerManager == null)
            {
                layerManager = FindObjectOfType<LayerManager>();
            }

            if (selectionController == null)
            {
                selectionController = FindObjectOfType<SelectionController>();
            }

            if (cameraController == null)
            {
                cameraController = FindObjectOfType<CameraController>();
            }

            if (searchManager == null)
            {
                searchManager = FindObjectOfType<SearchManager>();
            }
        }

        /// <summary>
        /// Initialize all components
        /// </summary>
        private void InitializeComponents()
        {
            // Link search manager with layer manager and selection controller
            if (searchManager != null)
            {
                if (searchManager.layerManager == null)
                {
                    searchManager.layerManager = layerManager;
                }

                if (searchManager.selectionController == null)
                {
                    searchManager.selectionController = selectionController;
                }
            }
        }

        /// <summary>
        /// Reset all viewer components to default state
        /// </summary>
        public void ResetViewer()
        {
            // Reset camera
            if (cameraController != null)
            {
                cameraController.ResetCamera();
            }

            // Clear selection
            if (selectionController != null)
            {
                selectionController.ClearSelection();
            }

            // Show all layers
            if (layerManager != null)
            {
                layerManager.ShowAllLayers();
            }

            // Clear search
            if (searchManager != null)
            {
                searchManager.ClearSearch();
            }
        }

        /// <summary>
        /// Show only bones
        /// </summary>
        public void ShowBonesOnly()
        {
            if (layerManager != null)
            {
                layerManager.ShowOnlyLayers(AnatomyLayer.Bone);
            }
        }

        /// <summary>
        /// Show only muscles
        /// </summary>
        public void ShowMusclesOnly()
        {
            if (layerManager != null)
            {
                layerManager.ShowOnlyLayers(
                    AnatomyLayer.Muscle1,
                    AnatomyLayer.Muscle2,
                    AnatomyLayer.Muscle3,
                    AnatomyLayer.Muscle4,
                    AnatomyLayer.Muscle5,
                    AnatomyLayer.Muscle6,
                    AnatomyLayer.Muscle7
                );
            }
        }

        /// <summary>
        /// Toggle between bones and muscles
        /// </summary>
        public void ToggleBonesAndMuscles()
        {
            if (layerManager == null)
                return;

            bool bonesVisible = layerManager.IsLayerVisible(AnatomyLayer.Bone);
            
            if (bonesVisible)
            {
                ShowMusclesOnly();
            }
            else
            {
                ShowBonesOnly();
            }
        }
    }
}
