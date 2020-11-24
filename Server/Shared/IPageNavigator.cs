using System;

namespace AssetManagement.Server.Shared
{
    public interface IPageNavigator<T>
    {
        int PageIndex { get; }

        int NumPages { get; }
        
        event Action<T[]> PageChanged;
        
        bool ChangePage(HorizontalDirection navigationDirection);
        
        T[] OnItemsUpdated(T[] items);
        
        bool SetPage(int requestedPageIndex);
    }
}