using UnityEngine;

namespace AnatomyViewer.Examples
{
    /// <summary>
    /// Example script showing how to setup and use the anatomy viewer programmatically
    /// </summary>
    public class ExampleSetup : MonoBehaviour
    {
        [Header("Prefab References")]
        [Tooltip("Prefab for a bone object")]
        public GameObject bonePrefab;

        [Tooltip("Prefab for a muscle object")]
        public GameObject musclePrefab;

        [Header("Manager References")]
        public AnatomyViewerManager viewerManager;
        public LayerManager layerManager;
        public SelectionController selectionController;
        public SearchManager searchManager;
        public CameraController cameraController;

        void Start()
        {
            // This is an example of how to programmatically create anatomy parts
            // In a real scenario, you would load 3D models and configure them
            CreateSampleAnatomyParts();

            // Setup event listeners
            SetupEventListeners();
        }

        /// <summary>
        /// Create sample anatomy parts for demonstration
        /// </summary>
        void CreateSampleAnatomyParts()
        {
            // Get sample data
            AnatomyDatabase sampleDB = SampleAnatomyData.CreateSampleDatabase();

            // Create GameObjects for each part
            foreach (var partData in sampleDB.parts)
            {
                CreateAnatomyPartObject(partData);
            }

            // Refresh managers
            if (layerManager != null)
            {
                layerManager.RefreshAnatomyParts();
            }

            if (searchManager != null)
            {
                searchManager.RefreshAnatomyParts();
            }
        }

        /// <summary>
        /// Create a GameObject for an anatomy part
        /// </summary>
        GameObject CreateAnatomyPartObject(AnatomyPartData data)
        {
            // Choose prefab based on type
            GameObject prefab = data.type == AnatomyType.Bone ? bonePrefab : musclePrefab;
            
            if (prefab == null)
            {
                // Create a simple cube if no prefab is available
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.name = data.name;
                
                // Add AnatomyPart component
                AnatomyPart part = obj.AddComponent<AnatomyPart>();
                ConfigureAnatomyPart(part, data);
                
                // Position randomly for demonstration
                obj.transform.position = Random.insideUnitSphere * 5f;
                
                return obj;
            }
            else
            {
                // Instantiate prefab
                GameObject obj = Instantiate(prefab);
                obj.name = data.name;
                
                // Get or add AnatomyPart component
                AnatomyPart part = obj.GetComponent<AnatomyPart>();
                if (part == null)
                {
                    part = obj.AddComponent<AnatomyPart>();
                }
                
                ConfigureAnatomyPart(part, data);
                
                return obj;
            }
        }

        /// <summary>
        /// Configure an AnatomyPart component with data
        /// </summary>
        void ConfigureAnatomyPart(AnatomyPart part, AnatomyPartData data)
        {
            part.partName = data.name;
            part.description = data.description;
            part.partType = data.type;
            part.layer = data.layer;
            
            // Create a highlight material if not set
            if (part.highlightMaterial == null)
            {
                Material highlightMat = new Material(Shader.Find("Standard"));
                highlightMat.color = Color.yellow;
                highlightMat.SetFloat("_Metallic", 0.5f);
                part.highlightMaterial = highlightMat;
            }
        }

        /// <summary>
        /// Setup event listeners for various components
        /// </summary>
        void SetupEventListeners()
        {
            if (selectionController != null)
            {
                selectionController.onPartSelected.AddListener(OnPartSelected);
                selectionController.onSelectionCleared.AddListener(OnSelectionCleared);
            }

            if (searchManager != null)
            {
                searchManager.onSearchResults.AddListener(OnSearchResults);
            }
        }

        /// <summary>
        /// Called when an anatomy part is selected
        /// </summary>
        void OnPartSelected(AnatomyPart part)
        {
            Debug.Log($"Selected: {part.partName} ({part.partType}, Layer: {part.layer})");
            Debug.Log($"Description: {part.description}");
        }

        /// <summary>
        /// Called when selection is cleared
        /// </summary>
        void OnSelectionCleared()
        {
            Debug.Log("Selection cleared");
        }

        /// <summary>
        /// Called when search results are updated
        /// </summary>
        void OnSearchResults(System.Collections.Generic.List<AnatomyPart> results)
        {
            Debug.Log($"Search found {results.Count} results");
            foreach (var part in results)
            {
                Debug.Log($"- {part.partName}");
            }
        }

        /// <summary>
        /// Example: Show only superficial muscles (Layer 1)
        /// </summary>
        public void ShowSuperficialMuscles()
        {
            if (layerManager != null)
            {
                layerManager.ShowOnlyLayers(AnatomyLayer.Bone, AnatomyLayer.Muscle1);
            }
        }

        /// <summary>
        /// Example: Show deep muscles (Layer 7)
        /// </summary>
        public void ShowDeepMuscles()
        {
            if (layerManager != null)
            {
                layerManager.ShowOnlyLayers(AnatomyLayer.Bone, AnatomyLayer.Muscle7);
            }
        }

        /// <summary>
        /// Example: Progressive layer reveal
        /// </summary>
        public void RevealLayersProgressively(int upToLayer)
        {
            if (layerManager == null)
                return;

            layerManager.HideAllLayers();
            layerManager.SetLayerVisibility(AnatomyLayer.Bone, true);

            for (int i = 1; i <= upToLayer && i <= 7; i++)
            {
                AnatomyLayer layer = (AnatomyLayer)i;
                layerManager.SetLayerVisibility(layer, true);
            }
        }
    }
}
