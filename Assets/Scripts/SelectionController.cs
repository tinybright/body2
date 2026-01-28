using UnityEngine;
using UnityEngine.Events;

namespace AnatomyViewer
{
    /// <summary>
    /// Event triggered when an anatomy part is selected
    /// </summary>
    [System.Serializable]
    public class AnatomyPartSelectedEvent : UnityEvent<AnatomyPart> { }

    /// <summary>
    /// Handles selection of anatomy parts through raycasting
    /// </summary>
    public class SelectionController : MonoBehaviour
    {
        [Header("Selection Settings")]
        [Tooltip("Camera used for raycasting")]
        public Camera mainCamera;

        [Tooltip("Layer mask for raycast detection")]
        public LayerMask selectableLayers = -1;

        [Header("Events")]
        [Tooltip("Event triggered when a part is selected")]
        public AnatomyPartSelectedEvent onPartSelected;

        [Tooltip("Event triggered when selection is cleared")]
        public UnityEvent onSelectionCleared;

        private AnatomyPart currentlySelected;

        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            if (onPartSelected == null)
                onPartSelected = new AnatomyPartSelectedEvent();

            if (onSelectionCleared == null)
                onSelectionCleared = new UnityEvent();
        }

        void Update()
        {
            HandleInput();
        }

        /// <summary>
        /// Handle mouse/touch input for selection
        /// </summary>
        private void HandleInput()
        {
            // Handle mouse click on PC or single touch on mobile
            if (Input.GetMouseButtonDown(0))
            {
                TrySelectAtPosition(Input.mousePosition);
            }
            // Handle touch input for mobile
            else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                TrySelectAtPosition(Input.GetTouch(0).position);
            }
        }

        /// <summary>
        /// Try to select an anatomy part at the given screen position
        /// </summary>
        private void TrySelectAtPosition(Vector3 screenPosition)
        {
            if (mainCamera == null)
                return;

            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayers))
            {
                AnatomyPart part = hit.collider.GetComponent<AnatomyPart>();
                if (part != null)
                {
                    SelectPart(part);
                }
                else
                {
                    ClearSelection();
                }
            }
            else
            {
                ClearSelection();
            }
        }

        /// <summary>
        /// Select a specific anatomy part
        /// </summary>
        public void SelectPart(AnatomyPart part)
        {
            // Deselect previous part
            if (currentlySelected != null && currentlySelected != part)
            {
                currentlySelected.Unhighlight();
            }

            // Select new part
            currentlySelected = part;
            currentlySelected.Highlight();

            // Trigger event
            onPartSelected.Invoke(part);
        }

        /// <summary>
        /// Clear current selection
        /// </summary>
        public void ClearSelection()
        {
            if (currentlySelected != null)
            {
                currentlySelected.Unhighlight();
                currentlySelected = null;
                onSelectionCleared.Invoke();
            }
        }

        /// <summary>
        /// Get currently selected part
        /// </summary>
        public AnatomyPart GetSelectedPart()
        {
            return currentlySelected;
        }
    }
}
