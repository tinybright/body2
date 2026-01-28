using UnityEngine;
using UnityEditor;

namespace AnatomyViewer.Editor
{
    /// <summary>
    /// Custom editor window for setting up the Anatomy Viewer
    /// </summary>
    public class AnatomyViewerSetupWindow : EditorWindow
    {
        [MenuItem("Tools/Anatomy Viewer/Setup Wizard")]
        public static void ShowWindow()
        {
            GetWindow<AnatomyViewerSetupWindow>("Anatomy Viewer Setup");
        }

        void OnGUI()
        {
            GUILayout.Label("Anatomy Viewer Setup Wizard", EditorStyles.boldLabel);
            GUILayout.Space(10);

            if (GUILayout.Button("Create Manager GameObject"))
            {
                CreateManagerGameObject();
            }

            if (GUILayout.Button("Setup Camera"))
            {
                SetupCamera();
            }

            if (GUILayout.Button("Generate Sample Anatomy Parts"))
            {
                GenerateSampleParts();
            }

            if (GUILayout.Button("Create Sample Database Asset"))
            {
                CreateDatabaseAsset();
            }

            GUILayout.Space(20);
            GUILayout.Label("Quick Actions", EditorStyles.boldLabel);

            if (GUILayout.Button("Add Highlight Material to Selected"))
            {
                AddHighlightMaterialToSelected();
            }

            if (GUILayout.Button("Auto-Configure Selected as Anatomy Part"))
            {
                AutoConfigureSelected();
            }
        }

        void CreateManagerGameObject()
        {
            GameObject manager = new GameObject("AnatomyViewerManager");
            manager.AddComponent<LayerManager>();
            manager.AddComponent<SelectionController>();
            manager.AddComponent<SearchManager>();
            manager.AddComponent<AnatomyViewerManager>();

            Selection.activeGameObject = manager;
            EditorGUIUtility.PingObject(manager);
            Debug.Log("Created AnatomyViewerManager GameObject");
        }

        void SetupCamera()
        {
            Camera mainCam = Camera.main;
            if (mainCam == null)
            {
                Debug.LogError("No main camera found in scene");
                return;
            }

            CameraController controller = mainCam.GetComponent<CameraController>();
            if (controller == null)
            {
                controller = mainCam.gameObject.AddComponent<CameraController>();
            }

            // Create camera target if doesn't exist
            GameObject target = GameObject.Find("CameraTarget");
            if (target == null)
            {
                target = new GameObject("CameraTarget");
                target.transform.position = Vector3.zero;
            }

            controller.target = target.transform;
            controller.rotationSpeed = 5f;
            controller.minDistance = 2f;
            controller.maxDistance = 20f;
            controller.zoomSpeed = 2f;

            mainCam.transform.position = new Vector3(0, 5, -10);
            mainCam.transform.LookAt(target.transform);

            EditorUtility.SetDirty(mainCam);
            Debug.Log("Camera setup complete");
        }

        void GenerateSampleParts()
        {
            AnatomyDatabase db = SampleAnatomyData.CreateSampleDatabase();

            GameObject partsContainer = new GameObject("AnatomyParts");

            foreach (var partData in db.parts)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.name = partData.name;
                obj.transform.parent = partsContainer.transform;
                obj.transform.position = Random.insideUnitSphere * 5f;
                obj.transform.localScale = Vector3.one * Random.Range(0.3f, 0.8f);

                AnatomyPart part = obj.AddComponent<AnatomyPart>();
                part.partName = partData.name;
                part.description = partData.description;
                part.partType = partData.type;
                part.layer = partData.layer;

                // Create and assign material
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material mat;
                    if (partData.type == AnatomyType.Bone)
                    {
                        mat = Utilities.MaterialUtility.CreateBoneMaterial();
                    }
                    else
                    {
                        mat = Utilities.MaterialUtility.CreateMuscleMaterial(partData.layer);
                    }
                    renderer.material = mat;
                }

                // Create highlight material
                part.highlightMaterial = Utilities.MaterialUtility.CreateHighlightMaterial(Color.yellow);
            }

            Selection.activeGameObject = partsContainer;
            EditorGUIUtility.PingObject(partsContainer);
            Debug.Log($"Generated {db.parts.Count} sample anatomy parts");
        }

        void CreateDatabaseAsset()
        {
            AnatomyDatabase db = SampleAnatomyData.CreateSampleDatabase();
            
            string path = "Assets/Resources/SampleAnatomyDatabase.asset";
            
            // Create Resources folder if it doesn't exist
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            AssetDatabase.CreateAsset(db, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = db;

            Debug.Log($"Created anatomy database at {path}");
        }

        void AddHighlightMaterialToSelected()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                AnatomyPart part = obj.GetComponent<AnatomyPart>();
                if (part != null && part.highlightMaterial == null)
                {
                    part.highlightMaterial = Utilities.MaterialUtility.CreateHighlightMaterial(Color.yellow);
                    EditorUtility.SetDirty(part);
                }
            }
            Debug.Log("Added highlight materials to selected objects");
        }

        void AutoConfigureSelected()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                AnatomyPart part = obj.GetComponent<AnatomyPart>();
                if (part == null)
                {
                    part = obj.AddComponent<AnatomyPart>();
                }

                if (string.IsNullOrEmpty(part.partName))
                {
                    part.partName = obj.name;
                }

                // Ensure collider exists
                if (obj.GetComponent<Collider>() == null)
                {
                    obj.AddComponent<BoxCollider>();
                }

                // Create highlight material if needed
                if (part.highlightMaterial == null)
                {
                    part.highlightMaterial = Utilities.MaterialUtility.CreateHighlightMaterial(Color.yellow);
                }

                EditorUtility.SetDirty(part);
            }
            Debug.Log("Auto-configured selected objects as anatomy parts");
        }
    }

    /// <summary>
    /// Custom inspector for AnatomyPart
    /// </summary>
    [CustomEditor(typeof(AnatomyPart))]
    public class AnatomyPartEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);
            AnatomyPart part = (AnatomyPart)target;

            if (GUILayout.Button("Create Highlight Material"))
            {
                part.highlightMaterial = Utilities.MaterialUtility.CreateHighlightMaterial(Color.yellow);
                EditorUtility.SetDirty(part);
            }

            if (GUILayout.Button("Test Highlight"))
            {
                part.Highlight();
            }

            if (GUILayout.Button("Remove Highlight"))
            {
                part.Unhighlight();
            }
        }
    }
}
