using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Phone.Controls
{
    // 1. CÁC ENUM VÀ LỚP BỔ TRỢ (Nếu project WPR của bạn đã tạo các class này ở file khác rồi thì có thể xóa đoạn này đi)
    #region Phụ thuộc (Dependencies)
    public enum PageOrientation
    {
        None = 0,
        Portrait = 1,
        Landscape = 2,
        PortraitUp = 5,
        PortraitDown = 9,
        LandscapeLeft = 18,
        LandscapeRight = 34
    }

    public class OrientationChangedEventArgs : EventArgs
    {
        public PageOrientation Orientation { get; private set; }
        public OrientationChangedEventArgs(PageOrientation orientation) { Orientation = orientation; }
    }

    public class ObscuredEventArgs : EventArgs
    {
        public bool IsLocked { get; private set; }
        public ObscuredEventArgs(bool isLocked) { IsLocked = isLocked; }
    }

    public class JournalEntry { }
    #endregion


    // 2. CLASS CHÍNH
    public class PhoneApplicationFrame : Frame
    {
        private static PhoneApplicationFrame? _Current;
        // Biến static để game lấy được Frame hiện tại
        public static PhoneApplicationFrame Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new PhoneApplicationFrame();
                }
                return _Current;
            }
        }

        public PhoneApplicationFrame()
        {
            // Giả lập Singleton: Nếu chưa có Current thì gán chính nó
            if (_Current == null)
            {
                _Current = this;
            }

            Orientation = PageOrientation.PortraitUp;
        }

        // --- CÁC EVENT GAME HAY ĐĂNG KÝ (+=) ---
        public event EventHandler<OrientationChangedEventArgs>? OrientationChanged;
        public event EventHandler<ObscuredEventArgs>? Obscured;
        public event EventHandler? Unobscured;
        public event EventHandler<CancelEventArgs>? BackKeyPress;
        public event EventHandler<CancelEventArgs>? BackKeyPressPreview;

        // --- CÁC THUỘC TÍNH (PROPERTIES) ---
        public PageOrientation Orientation { get; set; }

        public bool FullScreen { get; set; }

        // Trả về một list rỗng để tránh lỗi NullReferenceException khi game quét lịch sử trang
        public IEnumerable<JournalEntry> BackStack
        {
            get { return new List<JournalEntry>(); }
        }

        // --- CÁC PHƯƠNG THỨC (METHODS) ---
        public JournalEntry? RemoveBackEntry()
        {
            // Trả về null vì chúng ta không thực sự điều hướng trang (Navigate) trong game XNA
            return null;
        }

        // Hàm này do bạn (WPR) tự gọi nếu muốn giả vờ báo cho game biết màn hình vừa xoay
        internal void FireOrientationChanged(PageOrientation newOrientation)
        {
            Orientation = newOrientation;
            OrientationChanged?.Invoke(this, new OrientationChangedEventArgs(newOrientation));
        }
    }
}