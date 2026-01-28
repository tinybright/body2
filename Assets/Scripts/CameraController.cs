using UnityEngine;

namespace AnatomyViewer
{
    /// <summary>
    /// Controls camera movement, rotation, and zoom for anatomy viewing
    /// Supports both mouse (PC) and touch (mobile) input
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [Tooltip("Target object to orbit around")]
        public Transform target;

        [Header("Rotation Settings")]
        [Tooltip("Rotation speed for mouse/touch drag")]
        public float rotationSpeed = 5f;

        [Tooltip("Enable rotation damping for smooth movement")]
        public bool enableDamping = true;

        [Tooltip("Damping factor for smooth rotation")]
        public float dampingFactor = 5f;

        [Header("Zoom Settings")]
        [Tooltip("Minimum distance from target")]
        public float minDistance = 2f;

        [Tooltip("Maximum distance from target")]
        public float maxDistance = 20f;

        [Tooltip("Zoom speed for mouse wheel")]
        public float zoomSpeed = 2f;

        [Tooltip("Pinch zoom speed for mobile")]
        public float pinchZoomSpeed = 0.01f;

        [Header("Pan Settings")]
        [Tooltip("Pan speed for middle mouse/two-finger drag")]
        public float panSpeed = 0.5f;

        private Vector3 previousMousePosition;
        private float currentDistance;
        private Vector3 targetRotation;
        private Vector3 currentRotation;
        private Vector3 targetPanOffset;
        private Vector3 currentPanOffset;

        void Start()
        {
            if (target == null)
            {
                // Create a default target at origin if none is set
                GameObject targetObj = new GameObject("CameraTarget");
                target = targetObj.transform;
                target.position = Vector3.zero;
            }

            // Initialize distance
            currentDistance = Vector3.Distance(transform.position, target.position);
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            // Initialize rotation
            currentRotation = transform.eulerAngles;
            targetRotation = currentRotation;

            // Initialize pan offset
            currentPanOffset = Vector3.zero;
            targetPanOffset = Vector3.zero;
        }

        void LateUpdate()
        {
            HandleInput();
            UpdateCameraPosition();
        }

        /// <summary>
        /// Handle all input (mouse and touch)
        /// </summary>
        private void HandleInput()
        {
            // Handle mobile touch input
            if (Input.touchCount > 0)
            {
                HandleTouchInput();
            }
            // Handle PC mouse input
            else
            {
                HandleMouseInput();
            }
        }

        /// <summary>
        /// Handle mouse input for PC
        /// </summary>
        private void HandleMouseInput()
        {
            // Rotation with left mouse button
            if (Input.GetMouseButton(0))
            {
                float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
                float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

                targetRotation.y += rotX;
                targetRotation.x -= rotY;

                // Clamp vertical rotation
                targetRotation.x = Mathf.Clamp(targetRotation.x, -89f, 89f);
            }

            // Pan with middle mouse button
            if (Input.GetMouseButton(2))
            {
                float panX = -Input.GetAxis("Mouse X") * panSpeed;
                float panY = -Input.GetAxis("Mouse Y") * panSpeed;

                Vector3 right = transform.right;
                Vector3 up = transform.up;

                targetPanOffset += right * panX + up * panY;
            }

            // Zoom with mouse wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                currentDistance -= scroll * zoomSpeed;
                currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            }
        }

        /// <summary>
        /// Handle touch input for mobile
        /// </summary>
        private void HandleTouchInput()
        {
            // Single touch - rotation
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float rotX = touch.deltaPosition.x * rotationSpeed * 0.1f;
                    float rotY = touch.deltaPosition.y * rotationSpeed * 0.1f;

                    targetRotation.y += rotX;
                    targetRotation.x -= rotY;

                    // Clamp vertical rotation
                    targetRotation.x = Mathf.Clamp(targetRotation.x, -89f, 89f);
                }
            }
            // Two touches - pinch zoom and pan
            else if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                // Calculate previous touch positions
                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                // Calculate previous and current distance between touches
                float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
                float touchDeltaMag = (touch0.position - touch1.position).magnitude;

                // Pinch zoom
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                currentDistance += deltaMagnitudeDiff * pinchZoomSpeed;
                currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

                // Two-finger pan
                if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    Vector2 avgDelta = (touch0.deltaPosition + touch1.deltaPosition) / 2f;
                    
                    float panX = -avgDelta.x * panSpeed * 0.01f;
                    float panY = -avgDelta.y * panSpeed * 0.01f;

                    Vector3 right = transform.right;
                    Vector3 up = transform.up;

                    targetPanOffset += right * panX + up * panY;
                }
            }
        }

        /// <summary>
        /// Update camera position based on rotation, zoom, and pan
        /// </summary>
        private void UpdateCameraPosition()
        {
            // Apply damping to rotation
            if (enableDamping)
            {
                currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * dampingFactor);
                currentPanOffset = Vector3.Lerp(currentPanOffset, targetPanOffset, Time.deltaTime * dampingFactor);
            }
            else
            {
                currentRotation = targetRotation;
                currentPanOffset = targetPanOffset;
            }

            // Calculate camera position
            Quaternion rotation = Quaternion.Euler(currentRotation);
            Vector3 direction = rotation * Vector3.back;
            Vector3 finalTargetPos = target.position + currentPanOffset;

            transform.position = finalTargetPos + direction * currentDistance;
            transform.LookAt(finalTargetPos);
        }

        /// <summary>
        /// Reset camera to default position
        /// </summary>
        public void ResetCamera()
        {
            targetRotation = new Vector3(0, 0, 0);
            currentRotation = targetRotation;
            currentDistance = 10f;
            targetPanOffset = Vector3.zero;
            currentPanOffset = Vector3.zero;
        }

        /// <summary>
        /// Focus camera on a specific point
        /// </summary>
        public void FocusOnPoint(Vector3 point)
        {
            targetPanOffset = point - target.position;
        }
    }
}
