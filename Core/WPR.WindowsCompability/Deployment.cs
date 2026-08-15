using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPR.WindowsCompability
{
    internal class Deployment
    {
        // Singleton pattern để giả lập Deployment.Current của Silverlight
        private static readonly Deployment _current = new Deployment();
        public static Deployment Current => _current;

        // Lưu trữ tên file DLL chính của game (không có đuôi .dll)
        public string EntryPointAssembly { get; set; } = string.Empty;
    }
}
