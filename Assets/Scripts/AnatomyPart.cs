using UnityEngine;

namespace AnatomyViewer
{
    /// <summary>
    /// Enum representing different anatomy layers for muscles and bones
    /// </summary>
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

    /// <summary>
    /// Type of anatomy part
    /// </summary>
    public enum AnatomyType
    {
        Bone,
        Muscle
    }

    /// <summary>
    /// Base class for all anatomy parts (bones and muscles)
    /// </summary>
    public class AnatomyPart : MonoBehaviour
    {
        [Header("Basic Information")]
        [Tooltip("Name of the anatomy part")]
        public string partName;

        [Tooltip("Detailed description of the anatomy part")]
        [TextArea(3, 6)]
        public string description;

        [Header("Classification")]
        [Tooltip("Type of anatomy part")]
        public AnatomyType partType;

        [Tooltip("Layer this part belongs to")]
        public AnatomyLayer layer;

        [Header("Visual")]
        [Tooltip("Original material of the part")]
        private Material originalMaterial;

        [Tooltip("Material used when part is highlighted")]
        public Material highlightMaterial;

        private Renderer partRenderer;
        private bool isHighlighted = false;

        void Awake()
        {
            partRenderer = GetComponent<Renderer>();
            if (partRenderer != null)
            {
                originalMaterial = partRenderer.material;
            }
        }

        /// <summary>
        /// Highlight this anatomy part
        /// </summary>
        public void Highlight()
        {
            if (partRenderer != null && highlightMaterial != null && !isHighlighted)
            {
                partRenderer.material = highlightMaterial;
                isHighlighted = true;
            }
        }

        /// <summary>
        /// Remove highlight from this anatomy part
        /// </summary>
        public void Unhighlight()
        {
            if (partRenderer != null && originalMaterial != null && isHighlighted)
            {
                partRenderer.material = originalMaterial;
                isHighlighted = false;
            }
        }

        /// <summary>
        /// Set visibility of this anatomy part
        /// </summary>
        public void SetVisible(bool visible)
        {
            if (partRenderer != null)
            {
                partRenderer.enabled = visible;
            }
        }

        /// <summary>
        /// Check if this part matches the search query
        /// </summary>
        public bool MatchesSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
                return true;

            return partName.ToLower().Contains(query.ToLower()) ||
                   description.ToLower().Contains(query.ToLower());
        }
    }
}
