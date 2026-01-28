using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AnatomyViewer
{
    /// <summary>
    /// Manages layer visibility for anatomy parts
    /// </summary>
    public class LayerManager : MonoBehaviour
    {
        [Header("Layer Settings")]
        [Tooltip("Which layers are currently visible")]
        public bool[] layerVisibility = new bool[8]; // 1 bone layer + 7 muscle layers

        private List<AnatomyPart> allParts;

        void Start()
        {
            // Initialize all layers as visible by default
            for (int i = 0; i < layerVisibility.Length; i++)
            {
                layerVisibility[i] = true;
            }

            // Find all anatomy parts in the scene
            RefreshAnatomyParts();
            
            // Apply initial visibility
            UpdateAllVisibility();
        }

        /// <summary>
        /// Refresh the list of anatomy parts from the scene
        /// </summary>
        public void RefreshAnatomyParts()
        {
            allParts = FindObjectsOfType<AnatomyPart>().ToList();
        }

        /// <summary>
        /// Toggle visibility of a specific layer
        /// </summary>
        public void ToggleLayer(AnatomyLayer layer)
        {
            int layerIndex = (int)layer;
            if (layerIndex >= 0 && layerIndex < layerVisibility.Length)
            {
                layerVisibility[layerIndex] = !layerVisibility[layerIndex];
                UpdateLayerVisibility(layer);
            }
        }

        /// <summary>
        /// Set visibility of a specific layer
        /// </summary>
        public void SetLayerVisibility(AnatomyLayer layer, bool visible)
        {
            int layerIndex = (int)layer;
            if (layerIndex >= 0 && layerIndex < layerVisibility.Length)
            {
                layerVisibility[layerIndex] = visible;
                UpdateLayerVisibility(layer);
            }
        }

        /// <summary>
        /// Get visibility state of a specific layer
        /// </summary>
        public bool IsLayerVisible(AnatomyLayer layer)
        {
            int layerIndex = (int)layer;
            if (layerIndex >= 0 && layerIndex < layerVisibility.Length)
            {
                return layerVisibility[layerIndex];
            }
            return false;
        }

        /// <summary>
        /// Update visibility for all parts in a specific layer
        /// </summary>
        private void UpdateLayerVisibility(AnatomyLayer layer)
        {
            bool visible = IsLayerVisible(layer);
            foreach (var part in allParts)
            {
                if (part.layer == layer)
                {
                    part.SetVisible(visible);
                }
            }
        }

        /// <summary>
        /// Update visibility for all anatomy parts based on current layer settings
        /// </summary>
        public void UpdateAllVisibility()
        {
            if (allParts == null || allParts.Count == 0)
                return;

            foreach (var part in allParts)
            {
                bool visible = IsLayerVisible(part.layer);
                part.SetVisible(visible);
            }
        }

        /// <summary>
        /// Show only specific layers, hide all others
        /// </summary>
        public void ShowOnlyLayers(params AnatomyLayer[] layers)
        {
            // Hide all layers first
            for (int i = 0; i < layerVisibility.Length; i++)
            {
                layerVisibility[i] = false;
            }

            // Show specified layers
            foreach (var layer in layers)
            {
                int layerIndex = (int)layer;
                if (layerIndex >= 0 && layerIndex < layerVisibility.Length)
                {
                    layerVisibility[layerIndex] = true;
                }
            }

            UpdateAllVisibility();
        }

        /// <summary>
        /// Show all layers
        /// </summary>
        public void ShowAllLayers()
        {
            for (int i = 0; i < layerVisibility.Length; i++)
            {
                layerVisibility[i] = true;
            }
            UpdateAllVisibility();
        }

        /// <summary>
        /// Hide all layers
        /// </summary>
        public void HideAllLayers()
        {
            for (int i = 0; i < layerVisibility.Length; i++)
            {
                layerVisibility[i] = false;
            }
            UpdateAllVisibility();
        }
    }
}
