using System;
using System.Collections.Generic;

namespace AssetManagement.Server.Shared
{
    public sealed class PageNavigator<T>
    {
        /// <summary>
        /// The current page index number starting at 0.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int NumPages => (int)Math.Ceiling((float)itemCount / itemsPerPage);

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
        /// <returns>True if the requested page index was inside the bounds, false if it was clamped.</returns>
        public bool ChangePage(T[] items, HorizontalDirection navigationDirection) => SetPage(items, PageIndex + (int)navigationDirection);

        /// <summary>
        /// Set the current page index to a specific value. Invokes the OnPageChanged event.
        /// </summary>
        /// <param name="items">The array of all items.</param>
        /// <param name="pageIndex">The page index to move to. Gets clamped between 0 and the number of pages.</param>
        /// <returns>True if the requested page index was inside the bounds, false if it was clamped.</returns>
        public bool SetPage(T[] items, int pageIndex)
        {
            bool isInBounds = pageIndex >= 0 && pageIndex < NumPages;

            if (isInBounds)
            {
                PageIndex = Math.Clamp(pageIndex, 0, NumPages - 1);
                OnPageChanged?.Invoke(GetPage(items));
            }

            return isInBounds;
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