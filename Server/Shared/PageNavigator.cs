using System;
using System.Collections.Generic;

namespace AssetManagement.Server.Components
{
    public sealed class PageNavigator<T>
    {
        /// <summary>
        /// The current page index number starting at 0.
        /// </summary>
        public int PageIndex { get; private set; } = 0;

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int NumPages => itemCount / itemsPerPage + 1;

        /// <summary>
        /// Event that fires when the page changes.
        /// </summary>
        public event Action<T[]> OnPageChanged;

        private readonly int itemCount;
        private readonly int itemsPerPage;

        /// <summary>
        /// Constructor to initialize TableNavigator which outputs pageItems.
        /// </summary>
        /// <param name="items">Input array with all items.</param>
        /// <param name="pageItems">Output array with sliced items of size itemsPerPage.</param>
        /// <param name="itemsPerPage">The number of items to display on a single page.</param>
        public PageNavigator(T[] items, out T[] pageItems, int itemsPerPage)
        {
            this.itemsPerPage = itemsPerPage;
            pageItems = GetPage(items);
            itemCount = items.Length;
        }

        /// <summary>
        /// Changes the shown items depending on which direction is chosen and invokes the event OnPageChanged.
        /// </summary>
        /// <param name="items">The array of all items.</param>
        /// <param name="navigationDirection">The direction to navigate - left or right.</param>
        public void ChangePage(T[] items, HorizontalDirection navigationDirection)
        {
            int direction = (int)navigationDirection;
            int requestedPageIndex = PageIndex + direction;

            if (requestedPageIndex >= NumPages || requestedPageIndex < 0) return;

            PageIndex += direction;
            OnPageChanged?.Invoke(GetPage(items));
        }

        /// <summary>
        /// Slices items into a size of itemsPerPage and returns them as an item array.
        /// </summary>
        /// <param name="items">Array of all the items in the system.</param>
        /// <returns>Array of items given the current page.</returns>
        private T[] GetPage(T[] items)
        {
            List<T> pageItems = new List<T>();

            int start = PageIndex * itemsPerPage;
            int end = Math.Clamp(start + itemsPerPage, 0, items.Length);

            for (int i = start; i < end; i++)
            {
                pageItems.Add(items[i]);
            }

            return pageItems.ToArray();
        }
    }
}