using System;

namespace Microsoft.Phone.Controls
{
    // =====================================================================
    // 1. CÁC CLASS/DELEGATE PHỤ THUỘC (DEPENDENCIES)
    // Lưu ý: Nếu project WPR của bạn đã có sẵn các class này ở file khác, 
    // bạn có thể xóa vùng #region này đi để tránh lỗi trùng lặp.
    // =====================================================================
    #region Dependencies

    public interface INavigate
    {
        bool Navigate(Uri source);
    }

    public enum JournalOwnership
    {
        Automatic,
        OwnsJournal,
        UsesParentJournal
    }

    public class UriMapperBase { }

    // Các class Argument cho Event
    public class NavigationEventArgs : EventArgs { }
    public class NavigatingCancelEventArgs : EventArgs { }
    public class NavigationFailedEventArgs : EventArgs { }
    public class FragmentNavigationEventArgs : EventArgs { }

    // Các Delegate cho Event
    public delegate void NavigatedEventHandler(object sender, NavigationEventArgs e);
    public delegate void NavigatingCancelEventHandler(object sender, NavigatingCancelEventArgs e);
    public delegate void NavigationFailedEventHandler(object sender, NavigationFailedEventArgs e);
    public delegate void NavigationStoppedEventHandler(object sender, EventArgs e);
    public delegate void FragmentNavigationEventHandler(object sender, FragmentNavigationEventArgs e);

    // Dummy ContentControl (Nếu thư viện UI của WPR chưa có class này)

    public class UIElement
    {
    }
    public class ContentControl : UIElement
    {
        public object Content { get; set; }
    }
    #endregion


    // =====================================================================
    // 2. CLASS FRAME CHÍNH
    // =====================================================================
    public class Frame : ContentControl, INavigate
    {
        public Frame()
        {
            // Gán các giá trị mặc định giống hệt Windows Phone gốc
            CacheSize = 10;
            JournalOwnership = JournalOwnership.Automatic;
        }

        // --- CÁC SỰ KIỆN (EVENTS) ---
        public event NavigatedEventHandler? Navigated;
        public event NavigatingCancelEventHandler? Navigating;
        public event NavigationFailedEventHandler? NavigationFailed;
        public event NavigationStoppedEventHandler? NavigationStopped;
        public event FragmentNavigationEventHandler? FragmentNavigation;

        // --- CÁC THUỘC TÍNH (PROPERTIES) ---
        public Uri? Source { get; set; }

        public JournalOwnership JournalOwnership { get; set; }

        public bool CanGoBack { get; internal set; }

        public bool CanGoForward { get; internal set; }

        public Uri? CurrentSource { get; internal set; }

        public UriMapperBase? UriMapper { get; set; }

        public int CacheSize { get; set; }

        // --- CÁC PHƯƠNG THỨC ĐIỀU HƯỚNG (METHODS) ---
        public void StopLoading()
        {
            // Rỗng
        }

        public void GoBack()
        {
            // Rỗng
        }

        public void GoForward()
        {
            // Rỗng
        }

        public bool Navigate(Uri source)
        {
            // Giả vờ như đã chuyển trang thành công
            Source = source;
            CurrentSource = source;
            return true;
        }
    }
}