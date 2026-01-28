using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AnatomyViewer.UI
{
    /// <summary>
    /// UI Controller for layer visibility controls
    /// </summary>
    public class LayerControlUI : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Layer manager")]
        public LayerManager layerManager;

        [Header("UI Toggles")]
        [Tooltip("Toggle for bone layer")]
        public Toggle boneToggle;

        [Tooltip("Toggles for muscle layers 1-7")]
        public Toggle[] muscleToggles = new Toggle[7];

        [Header("Buttons")]
        [Tooltip("Button to show all layers")]
        public Button showAllButton;

        [Tooltip("Button to hide all layers")]
        public Button hideAllButton;

        void Start()
        {
            // Initialize toggles
            SetupToggle(boneToggle, AnatomyLayer.Bone);
            
            for (int i = 0; i < muscleToggles.Length && i < 7; i++)
            {
                AnatomyLayer layer = (AnatomyLayer)(i + 1); // Muscle1 to Muscle7
                SetupToggle(muscleToggles[i], layer);
            }

            // Setup buttons
            if (showAllButton != null)
            {
                showAllButton.onClick.AddListener(OnShowAll);
            }

            if (hideAllButton != null)
            {
                hideAllButton.onClick.AddListener(OnHideAll);
            }

            // Sync UI with current layer visibility
            SyncWithLayerManager();
        }

        /// <summary>
        /// Setup a toggle for a specific layer
        /// </summary>
        private void SetupToggle(Toggle toggle, AnatomyLayer layer)
        {
            if (toggle == null || layerManager == null)
                return;

            // Set initial value
            toggle.isOn = layerManager.IsLayerVisible(layer);

            // Add listener
            toggle.onValueChanged.AddListener((value) => OnToggleChanged(layer, value));
        }

        /// <summary>
        /// Called when a toggle is changed
        /// </summary>
        private void OnToggleChanged(AnatomyLayer layer, bool visible)
        {
            if (layerManager != null)
            {
                layerManager.SetLayerVisibility(layer, visible);
            }
        }

        /// <summary>
        /// Show all layers
        /// </summary>
        private void OnShowAll()
        {
            if (layerManager != null)
            {
                layerManager.ShowAllLayers();
                SyncWithLayerManager();
            }
        }

        /// <summary>
        /// Hide all layers
        /// </summary>
        private void OnHideAll()
        {
            if (layerManager != null)
            {
                layerManager.HideAllLayers();
                SyncWithLayerManager();
            }
        }

        /// <summary>
        /// Sync UI toggles with layer manager state
        /// </summary>
        public void SyncWithLayerManager()
        {
            if (layerManager == null)
                return;

            // Update bone toggle
            if (boneToggle != null)
            {
                boneToggle.isOn = layerManager.IsLayerVisible(AnatomyLayer.Bone);
            }

            // Update muscle toggles
            for (int i = 0; i < muscleToggles.Length && i < 7; i++)
            {
                if (muscleToggles[i] != null)
                {
                    AnatomyLayer layer = (AnatomyLayer)(i + 1);
                    muscleToggles[i].isOn = layerManager.IsLayerVisible(layer);
                }
            }
        }
    }
}
