using Microsoft.Phone.Controls;
using System;

namespace WPR.WindowsCompability
{
    public class Application
    {
        private static Application? _Current;
        public event EventHandler<ApplicationUnhandledExceptionEventArgs>? UnhandledException;

        public string? ProductId { get; set; }
        private ResourceDictionary _Resources;

        public UIElement RootVisual {
            get
            {
                return PhoneApplicationFrame.Current;
            }
        }

        internal Application()
        {
            _Resources = new ResourceDictionary();
        }

        public static Application Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new Application();
                }
                return _Current;
            }
        }

        public ResourceDictionary Resources => _Resources;
    }
}