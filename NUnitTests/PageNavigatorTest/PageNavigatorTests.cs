using AssetManagement.Server.Shared;
using NUnit.Framework;

namespace AssetManagement.NUnitTests.PageNavigatorTest
{
    public sealed class PageNavigatorTests
    {
        private int[] pageItems;

        [TestCase(15, 10, 2)]
        [TestCase(9, 10, 1)]
        [TestCase(15, 5, 3)]
        [TestCase(0, 1, 0)]
        public void NumPages_CanCalculateNumPagesFromItemsArray_EqualsExpectedAmountOfPages(int itemCount, int itemsPerPage, int expectedNumPages)
        {
            // Arrange
            int[] items = CreateIntArray(itemCount);

            // Act
            PageNavigator<int> pageNavigator = new PageNavigator<int>(items, out pageItems, itemsPerPage);

            // Assert
            Assert.AreEqual(expectedNumPages, pageNavigator.NumPages);
        }
        
        [TestCase(0, 1, 1, false)]
        [TestCase(50, 10, 3, true)]
        [TestCase(50, 10, -4, false)]
        [TestCase(50, 10, 90, false)]
        public void SetPage_CanClampPageIndex_ReturnsInBounds(int itemCount, int itemsPerPage, int pageIndex, bool expectInBounds)
        {
            // Arrange
            int[] items = CreateIntArray(itemCount);

            // Act
            PageNavigator<int> pageNavigator = new PageNavigator<int>(items, out pageItems, itemsPerPage);
            bool isInBounds = pageNavigator.SetPage(items, pageIndex);

            // Assert
            Assert.AreEqual(expectInBounds, isInBounds);
        }
        
        [TestCase(10, 5, 0, HorizontalDirection.Right, 1)]
        [TestCase(10, 5, 0, HorizontalDirection.Left, 0)]
        [TestCase(30, 5, 1, HorizontalDirection.Right, 2)]
        [TestCase(0, 1, 1, HorizontalDirection.Left, 0)]
        [TestCase(50, 10, 2, HorizontalDirection.Right, 3)]
        [TestCase(17, 5, 2, HorizontalDirection.Left, 1)]
        public void PageIndex_CanCorrectlyUpdatePageIndex_ChangePageMethodUpdatesPageIndex(int itemCount, int itemsPerPage, int startPageIndex,
            HorizontalDirection direction, int expectedPageIndex)
        {
            // Arrange
            int[] items = CreateIntArray(itemCount);
            
            // Act
            PageNavigator<int> pageNavigator = new PageNavigator<int>(items, out pageItems, itemsPerPage);
            pageNavigator.SetPage(items, startPageIndex);
            pageNavigator.ChangePage(items, direction);

            // Assert
            Assert.AreEqual(expectedPageIndex, pageNavigator.PageIndex);
        }
        
        [TestCase(17, 5,  2, HorizontalDirection.Right, new[] { 15, 16 })]
        [TestCase(17, 5,  2, HorizontalDirection.Left, new[] { 5, 6, 7, 8, 9 })]
        [TestCase(17, 10, 0, HorizontalDirection.Left, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [TestCase(0, 5,0, HorizontalDirection.Right, new int[0])]
        public void PageItems_CanCreatePageItemArray_PageItemArrayContainsCorrectElements(int itemCount, int itemsPerPage, int startPageIndex,
            HorizontalDirection direction, int[] expectedIntArray)
        {
            // Arrange
            int[] items = CreateIntArray(itemCount);
            
            // Act
            PageNavigator<int> pageNavigator = new PageNavigator<int>(items, out pageItems, itemsPerPage);
            pageNavigator.OnPageChanged += OnPageChanged;
            pageNavigator.SetPage(items, startPageIndex);
            pageNavigator.ChangePage(items, direction);  
            
            // Assert
            CollectionAssert.AreEqual(expectedIntArray, pageItems);
        }

        private void OnPageChanged(int[] newPageItems) => pageItems = newPageItems;

        private int[] CreateIntArray(int itemCount)
        {
            // Create an array of integers to test with
            int[] items = new int[itemCount];
            
            for (int i = 0; i < itemCount; i++)
            {
                items[i] = i;
            }

            return items;
        }
    }
}