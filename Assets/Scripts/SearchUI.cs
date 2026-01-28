using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace AnatomyViewer.UI
{
    /// <summary>
    /// UI Controller for search functionality
    /// </summary>
    public class SearchUI : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Search manager")]
        public SearchManager searchManager;

        [Header("UI References")]
        [Tooltip("Input field for search query")]
        public TMP_InputField searchInput;

        [Tooltip("Button to perform search")]
        public Button searchButton;

        [Tooltip("Button to clear search")]
        public Button clearButton;

        [Tooltip("Container for search results")]
        public GameObject resultsContainer;

        [Tooltip("Prefab for search result item")]
        public GameObject resultItemPrefab;

        [Tooltip("Text to show when no results")]
        public TextMeshProUGUI noResultsText;

        private List<GameObject> resultItems = new List<GameObject>();

        void Start()
        {
            // Setup input field
            if (searchInput != null)
            {
                searchInput.onValueChanged.AddListener(OnSearchInputChanged);
            }

            // Setup buttons
            if (searchButton != null)
            {
                searchButton.onClick.AddListener(OnSearchButtonClicked);
            }

            if (clearButton != null)
            {
                clearButton.onClick.AddListener(OnClearButtonClicked);
            }

            // Subscribe to search events
            if (searchManager != null)
            {
                searchManager.onSearchResults.AddListener(OnSearchResults);
            }

            // Hide results initially
            if (resultsContainer != null)
            {
                resultsContainer.SetActive(false);
            }

            if (noResultsText != null)
            {
                noResultsText.gameObject.SetActive(false);
            }
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            if (searchManager != null)
            {
                searchManager.onSearchResults.RemoveListener(OnSearchResults);
            }
        }

        /// <summary>
        /// Called when search input changes
        /// </summary>
        private void OnSearchInputChanged(string query)
        {
            // Optionally perform real-time search
            // For better performance, you might want to debounce this
            if (searchManager != null && query.Length >= searchManager.minSearchLength)
            {
                searchManager.Search(query);
            }
            else if (string.IsNullOrEmpty(query))
            {
                ClearResults();
            }
        }

        /// <summary>
        /// Called when search button is clicked
        /// </summary>
        private void OnSearchButtonClicked()
        {
            if (searchInput != null && searchManager != null)
            {
                searchManager.Search(searchInput.text);
            }
        }

        /// <summary>
        /// Called when clear button is clicked
        /// </summary>
        private void OnClearButtonClicked()
        {
            if (searchInput != null)
            {
                searchInput.text = "";
            }

            if (searchManager != null)
            {
                searchManager.ClearSearch();
            }

            ClearResults();
        }

        /// <summary>
        /// Called when search results are updated
        /// </summary>
        private void OnSearchResults(List<AnatomyPart> results)
        {
            ClearResults();

            if (results == null || results.Count == 0)
            {
                ShowNoResults();
                return;
            }

            // Show results container
            if (resultsContainer != null)
            {
                resultsContainer.SetActive(true);
            }

            // Hide no results text
            if (noResultsText != null)
            {
                noResultsText.gameObject.SetActive(false);
            }

            // Create result items
            for (int i = 0; i < results.Count; i++)
            {
                CreateResultItem(results[i], i);
            }
        }

        /// <summary>
        /// Create a result item in the UI
        /// </summary>
        private void CreateResultItem(AnatomyPart part, int index)
        {
            if (resultItemPrefab == null || resultsContainer == null)
                return;

            GameObject item = Instantiate(resultItemPrefab, resultsContainer.transform);
            resultItems.Add(item);

            // Setup item text
            TextMeshProUGUI nameText = item.GetComponentInChildren<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = part.partName;
            }

            // Setup click handler
            Button button = item.GetComponent<Button>();
            if (button != null)
            {
                int resultIndex = index;
                button.onClick.AddListener(() => OnResultClicked(resultIndex));
            }
        }

        /// <summary>
        /// Called when a result item is clicked
        /// </summary>
        private void OnResultClicked(int index)
        {
            if (searchManager != null)
            {
                searchManager.SelectSearchResult(index);
            }
        }

        /// <summary>
        /// Clear all result items
        /// </summary>
        private void ClearResults()
        {
            foreach (var item in resultItems)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
            resultItems.Clear();

            if (resultsContainer != null)
            {
                resultsContainer.SetActive(false);
            }

            if (noResultsText != null)
            {
                noResultsText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Show "no results" message
        /// </summary>
        private void ShowNoResults()
        {
            if (noResultsText != null && !string.IsNullOrEmpty(searchInput.text))
            {
                noResultsText.gameObject.SetActive(true);
            }
        }
    }
}
