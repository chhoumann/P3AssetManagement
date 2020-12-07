using System;
using System.Collections.Generic;

namespace AssetManagement.Server.Shared
{
    public sealed class PageNavigator<T> : IPageNavigator<T>
    {
        private readonly int itemsPerPage;

        private T[] items;

        /// <summary>
        ///     Constructor to initialize TableNavigator which outputs pageItems.
        /// </summary>
        /// <param name="items">Input array with all items.</param>
        /// <param name="pageItems">Output array with sliced items of size itemsPerPage.</param>
        /// <param name="itemsPerPage">The number of items to display on a single page.</param>
        public PageNavigator(T[] items, out T[] pageItems, int itemsPerPage)
        {
            this.itemsPerPage = itemsPerPage;
            this.items = items;
            pageItems = GetPageItems();
        }

        /// <summary>
        ///     The current page index number starting at 0.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        ///     The total number of pages.
        /// </summary>
        public int NumPages
        {
            get
            {
                int numPages = (int) Math.Ceiling((float) items.Length / itemsPerPage);

                return Math.Clamp(numPages, 1, int.MaxValue);
            }
        }

        /// <summary>
        ///     Event that fires when the page changes.
        /// </summary>
        public event Action<T[]> PageChanged;

        /// <summary>
        ///     Changes the shown items depending on which direction is chosen and invokes the event PageChanged.
        /// </summary>
        /// <param name="navigationDirection">The direction to navigate - left or right.</param>
        /// <returns>True if the requested page index was inside the bounds, false if it was clamped.</returns>
        public bool ChangePage(HorizontalDirection navigationDirection) =>
            SetPage(PageIndex + (int) navigationDirection);

        /// <summary>
        ///     Updates current page and returns current page items.
        ///     Should be called if the current page is updated.
        /// </summary>
        /// <param name="items">Array of all items.</param>
        /// <returns>Array of current page items.</returns>
        public T[] OnItemsUpdated(T[] items)
        {
            this.items = items;
            SetPage(PageIndex);

            return GetPageItems();
        }

        /// <summary>
        ///     Set the current page index to a specific value. Invokes the PageChanged event.
        /// </summary>
        /// <param name="requestedPageIndex">The page index to move to. Gets clamped between 0 and the number of pages.</param>
        /// <returns>True if the page was updated, false if it wasn't.</returns>
        public bool SetPage(int requestedPageIndex)
        {
            int oldPageIndex = PageIndex;

            PageIndex = Math.Clamp(requestedPageIndex, 0, NumPages - 1);

            if (oldPageIndex != PageIndex)
            {
                PageChanged?.Invoke(GetPageItems());
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Slices items into a size of itemsPerPage and returns them as an item array.
        /// </summary>
        /// <returns>Array of items given the current page.</returns>
        private T[] GetPageItems()
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