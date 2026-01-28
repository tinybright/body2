using UnityEngine;
using System.Collections.Generic;

namespace AnatomyViewer
{
    /// <summary>
    /// Data structure for anatomy part information
    /// </summary>
    [System.Serializable]
    public class AnatomyPartData
    {
        public string name;
        public string description;
        public AnatomyType type;
        public AnatomyLayer layer;
    }

    /// <summary>
    /// Scriptable object to store anatomy data
    /// </summary>
    [CreateAssetMenu(fileName = "AnatomyDatabase", menuName = "Anatomy Viewer/Anatomy Database")]
    public class AnatomyDatabase : ScriptableObject
    {
        public List<AnatomyPartData> parts = new List<AnatomyPartData>();

        /// <summary>
        /// Get all parts of a specific type
        /// </summary>
        public List<AnatomyPartData> GetPartsByType(AnatomyType type)
        {
            List<AnatomyPartData> result = new List<AnatomyPartData>();
            foreach (var part in parts)
            {
                if (part.type == type)
                {
                    result.Add(part);
                }
            }
            return result;
        }

        /// <summary>
        /// Get all parts in a specific layer
        /// </summary>
        public List<AnatomyPartData> GetPartsByLayer(AnatomyLayer layer)
        {
            List<AnatomyPartData> result = new List<AnatomyPartData>();
            foreach (var part in parts)
            {
                if (part.layer == layer)
                {
                    result.Add(part);
                }
            }
            return result;
        }

        /// <summary>
        /// Find a part by name
        /// </summary>
        public AnatomyPartData FindPartByName(string name)
        {
            foreach (var part in parts)
            {
                if (part.name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
                {
                    return part;
                }
            }
            return null;
        }
    }
}
