using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace AnatomyViewer
{
    /// <summary>
    /// Event triggered when search results are updated
    /// </summary>
    [System.Serializable]
    public class SearchResultsEvent : UnityEvent<List<AnatomyPart>> { }

    /// <summary>
    /// Manages search functionality for anatomy parts
    /// </summary>
    public class SearchManager : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Layer manager reference")]
        public LayerManager layerManager;

        [Tooltip("Selection controller reference")]
        public SelectionController selectionController;

        [Header("Search Settings")]
        [Tooltip("Minimum characters required to perform search")]
        public int minSearchLength = 2;

        [Header("Events")]
        [Tooltip("Event triggered when search results change")]
        public SearchResultsEvent onSearchResults;

        private List<AnatomyPart> allParts;
        private List<AnatomyPart> currentResults;
        private string currentQuery = "";

        void Start()
        {
            if (onSearchResults == null)
                onSearchResults = new SearchResultsEvent();

            RefreshAnatomyParts();
        }

        /// <summary>
        /// Refresh the list of all anatomy parts
        /// </summary>
        public void RefreshAnatomyParts()
        {
            allParts = FindObjectsOfType<AnatomyPart>().ToList();
        }

        /// <summary>
        /// Perform search with the given query
        /// </summary>
        public void Search(string query)
        {
            currentQuery = query;

            if (allParts == null || allParts.Count == 0)
            {
                RefreshAnatomyParts();
            }

            // Clear results if query is too short
            if (string.IsNullOrEmpty(query) || query.Length < minSearchLength)
            {
                currentResults = new List<AnatomyPart>();
                onSearchResults.Invoke(currentResults);
                return;
            }

            // Filter parts that match the query
            currentResults = allParts.Where(part => part.MatchesSearch(query)).ToList();

            // Sort results by relevance (exact match first, then contains)
            currentResults = currentResults.OrderBy(part =>
            {
                string lowerName = part.partName.ToLower();
                string lowerQuery = query.ToLower();

                if (lowerName == lowerQuery) return 0; // Exact match
                if (lowerName.StartsWith(lowerQuery)) return 1; // Starts with
                return 2; // Contains
            }).ToList();

            // Trigger event with results
            onSearchResults.Invoke(currentResults);
        }

        /// <summary>
        /// Clear current search
        /// </summary>
        public void ClearSearch()
        {
            currentQuery = "";
            currentResults = new List<AnatomyPart>();
            onSearchResults.Invoke(currentResults);
        }

        /// <summary>
        /// Get current search results
        /// </summary>
        public List<AnatomyPart> GetResults()
        {
            return currentResults ?? new List<AnatomyPart>();
        }

        /// <summary>
        /// Select a part from search results
        /// </summary>
        public void SelectSearchResult(int index)
        {
            if (currentResults != null && index >= 0 && index < currentResults.Count)
            {
                AnatomyPart part = currentResults[index];
                
                // Make sure the part's layer is visible
                if (layerManager != null)
                {
                    layerManager.SetLayerVisibility(part.layer, true);
                }

                // Select the part
                if (selectionController != null)
                {
                    selectionController.SelectPart(part);
                }
            }
        }

        /// <summary>
        /// Highlight all search results
        /// </summary>
        public void HighlightResults()
        {
            if (currentResults != null)
            {
                foreach (var part in currentResults)
                {
                    part.Highlight();
                }
            }
        }

        /// <summary>
        /// Unhighlight all search results
        /// </summary>
        public void UnhighlightResults()
        {
            if (currentResults != null)
            {
                foreach (var part in currentResults)
                {
                    part.Unhighlight();
                }
            }
        }

        /// <summary>
        /// Get the current search query
        /// </summary>
        public string GetCurrentQuery()
        {
            return currentQuery;
        }
    }
}
