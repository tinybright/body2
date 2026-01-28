using UnityEngine;

namespace AnatomyViewer.Utilities
{
    /// <summary>
    /// Utility class for creating and managing materials
    /// </summary>
    public static class MaterialUtility
    {
        /// <summary>
        /// Create a standard highlight material
        /// </summary>
        public static Material CreateHighlightMaterial(Color color)
        {
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = color;
            mat.SetFloat("_Metallic", 0.3f);
            mat.SetFloat("_Glossiness", 0.7f);
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", color * 0.3f);
            return mat;
        }

        /// <summary>
        /// Create a default bone material
        /// </summary>
        public static Material CreateBoneMaterial()
        {
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(0.95f, 0.95f, 0.9f); // Off-white
            mat.SetFloat("_Metallic", 0.1f);
            mat.SetFloat("_Glossiness", 0.4f);
            return mat;
        }

        /// <summary>
        /// Create a default muscle material
        /// </summary>
        public static Material CreateMuscleMaterial(AnatomyLayer layer)
        {
            Material mat = new Material(Shader.Find("Standard"));
            
            // Different colors for different muscle layers
            Color muscleColor = GetMuscleLayerColor(layer);
            mat.color = muscleColor;
            mat.SetFloat("_Metallic", 0.2f);
            mat.SetFloat("_Glossiness", 0.5f);
            
            return mat;
        }

        /// <summary>
        /// Get appropriate color for muscle layer
        /// </summary>
        public static Color GetMuscleLayerColor(AnatomyLayer layer)
        {
            switch (layer)
            {
                case AnatomyLayer.Muscle1:
                    return new Color(0.8f, 0.3f, 0.3f); // Light red
                case AnatomyLayer.Muscle2:
                    return new Color(0.7f, 0.2f, 0.2f); // Red
                case AnatomyLayer.Muscle3:
                    return new Color(0.6f, 0.15f, 0.15f); // Dark red
                case AnatomyLayer.Muscle4:
                    return new Color(0.5f, 0.1f, 0.1f); // Darker red
                case AnatomyLayer.Muscle5:
                    return new Color(0.4f, 0.08f, 0.08f); // Very dark red
                case AnatomyLayer.Muscle6:
                    return new Color(0.35f, 0.06f, 0.06f); // Deep red
                case AnatomyLayer.Muscle7:
                    return new Color(0.3f, 0.05f, 0.05f); // Deepest red
                default:
                    return new Color(0.6f, 0.2f, 0.2f); // Default red
            }
        }

        /// <summary>
        /// Create a transparent material
        /// </summary>
        public static Material CreateTransparentMaterial(Color color, float alpha)
        {
            Material mat = new Material(Shader.Find("Standard"));
            mat.SetFloat("_Mode", 3); // Transparent mode
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            
            Color finalColor = color;
            finalColor.a = alpha;
            mat.color = finalColor;
            
            return mat;
        }
    }

    /// <summary>
    /// Utility class for anatomy viewer helpers
    /// </summary>
    public static class AnatomyUtility
    {
        /// <summary>
        /// Get human-readable name for anatomy layer
        /// </summary>
        public static string GetLayerName(AnatomyLayer layer)
        {
            switch (layer)
            {
                case AnatomyLayer.Bone:
                    return "Skeletal System";
                case AnatomyLayer.Muscle1:
                    return "Superficial Muscles (Layer 1)";
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
                    return "Deep Muscles (Layer 7)";
                default:
                    return "Unknown Layer";
            }
        }

        /// <summary>
        /// Calculate bounds of multiple anatomy parts
        /// </summary>
        public static Bounds CalculateBounds(AnatomyPart[] parts)
        {
            if (parts == null || parts.Length == 0)
                return new Bounds(Vector3.zero, Vector3.one);

            Bounds bounds = new Bounds(parts[0].transform.position, Vector3.zero);
            
            foreach (var part in parts)
            {
                Renderer renderer = part.GetComponent<Renderer>();
                if (renderer != null)
                {
                    bounds.Encapsulate(renderer.bounds);
                }
                else
                {
                    bounds.Encapsulate(part.transform.position);
                }
            }

            return bounds;
        }

        /// <summary>
        /// Get all parts in a specific layer
        /// </summary>
        public static AnatomyPart[] GetPartsInLayer(AnatomyLayer layer)
        {
            var allParts = Object.FindObjectsOfType<AnatomyPart>();
            System.Collections.Generic.List<AnatomyPart> layerParts = new System.Collections.Generic.List<AnatomyPart>();

            foreach (var part in allParts)
            {
                if (part.layer == layer)
                {
                    layerParts.Add(part);
                }
            }

            return layerParts.ToArray();
        }

        /// <summary>
        /// Get all bones
        /// </summary>
        public static AnatomyPart[] GetAllBones()
        {
            var allParts = Object.FindObjectsOfType<AnatomyPart>();
            System.Collections.Generic.List<AnatomyPart> bones = new System.Collections.Generic.List<AnatomyPart>();

            foreach (var part in allParts)
            {
                if (part.partType == AnatomyType.Bone)
                {
                    bones.Add(part);
                }
            }

            return bones.ToArray();
        }

        /// <summary>
        /// Get all muscles
        /// </summary>
        public static AnatomyPart[] GetAllMuscles()
        {
            var allParts = Object.FindObjectsOfType<AnatomyPart>();
            System.Collections.Generic.List<AnatomyPart> muscles = new System.Collections.Generic.List<AnatomyPart>();

            foreach (var part in allParts)
            {
                if (part.partType == AnatomyType.Muscle)
                {
                    muscles.Add(part);
                }
            }

            return muscles.ToArray();
        }
    }

    /// <summary>
    /// Utility for handling touch and mouse input consistently
    /// </summary>
    public static class InputUtility
    {
        /// <summary>
        /// Check if user is currently touching/clicking
        /// </summary>
        public static bool IsTouching()
        {
            return Input.GetMouseButton(0) || Input.touchCount > 0;
        }

        /// <summary>
        /// Check if user just started touching/clicking
        /// </summary>
        public static bool TouchBegan()
        {
            if (Input.GetMouseButtonDown(0))
                return true;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                return true;

            return false;
        }

        /// <summary>
        /// Check if user just ended touching/clicking
        /// </summary>
        public static bool TouchEnded()
        {
            if (Input.GetMouseButtonUp(0))
                return true;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
            }

            return false;
        }

        /// <summary>
        /// Get current touch/mouse position
        /// </summary>
        public static Vector2 GetTouchPosition()
        {
            if (Input.touchCount > 0)
                return Input.GetTouch(0).position;

            return Input.mousePosition;
        }

        /// <summary>
        /// Get number of active touches
        /// </summary>
        public static int GetTouchCount()
        {
            if (Input.touchCount > 0)
                return Input.touchCount;

            return Input.GetMouseButton(0) ? 1 : 0;
        }
    }
}
