using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AnatomyViewer.UI
{
    /// <summary>
    /// UI Controller for displaying information about selected anatomy part
    /// </summary>
    public class InfoPanelUI : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Panel container")]
        public GameObject panel;

        [Tooltip("Text field for part name")]
        public TextMeshProUGUI nameText;

        [Tooltip("Text field for part description")]
        public TextMeshProUGUI descriptionText;

        [Tooltip("Text field for part type")]
        public TextMeshProUGUI typeText;

        [Tooltip("Text field for layer information")]
        public TextMeshProUGUI layerText;

        [Header("References")]
        [Tooltip("Selection controller")]
        public SelectionController selectionController;

        void Start()
        {
            // Hide panel initially
            if (panel != null)
            {
                panel.SetActive(false);
            }

            // Subscribe to selection events
            if (selectionController != null)
            {
                selectionController.onPartSelected.AddListener(OnPartSelected);
                selectionController.onSelectionCleared.AddListener(OnSelectionCleared);
            }
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            if (selectionController != null)
            {
                selectionController.onPartSelected.RemoveListener(OnPartSelected);
                selectionController.onSelectionCleared.RemoveListener(OnSelectionCleared);
            }
        }

        /// <summary>
        /// Called when an anatomy part is selected
        /// </summary>
        private void OnPartSelected(AnatomyPart part)
        {
            if (part == null)
            {
                HideInfo();
                return;
            }

            ShowInfo(part);
        }

        /// <summary>
        /// Called when selection is cleared
        /// </summary>
        private void OnSelectionCleared()
        {
            HideInfo();
        }

        /// <summary>
        /// Display information about the selected part
        /// </summary>
        public void ShowInfo(AnatomyPart part)
        {
            if (panel != null)
            {
                panel.SetActive(true);
            }

            if (nameText != null)
            {
                nameText.text = part.partName;
            }

            if (descriptionText != null)
            {
                descriptionText.text = part.description;
            }

            if (typeText != null)
            {
                typeText.text = part.partType.ToString();
            }

            if (layerText != null)
            {
                layerText.text = GetLayerDisplayName(part.layer);
            }
        }

        /// <summary>
        /// Hide the info panel
        /// </summary>
        public void HideInfo()
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        /// <summary>
        /// Get display name for a layer
        /// </summary>
        private string GetLayerDisplayName(AnatomyLayer layer)
        {
            switch (layer)
            {
                case AnatomyLayer.Bone:
                    return "Bone Layer";
                case AnatomyLayer.Muscle1:
                    return "Muscle Layer 1";
                case AnatomyLayer.Muscle2:
                    return "Muscle Layer 2";
                case AnatomyLayer.Muscle3:
                    return "Muscle Layer 3";
                case AnatomyLayer.Muscle4:
                    return "Muscle Layer 4";
                case AnatomyLayer.Muscle5:
                    return "Muscle Layer 5";
                case AnatomyLayer.Muscle6:
                    return "Muscle Layer 6";
                case AnatomyLayer.Muscle7:
                    return "Muscle Layer 7";
                default:
                    return "Unknown Layer";
            }
        }
    }
}
