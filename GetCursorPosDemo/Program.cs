using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;


namespace GetCursorPosDemo
{


    class Program
    {
        static int _x, _y;
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out System.Windows.Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, int lParam);

        [DllImport("user32.dll")]
        static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern IntPtr RealChildWindowFromPoint(IntPtr hwndParent, POINT ptParentClientCoords);

        static uint WM_LBUTTONDOWN = 0x0201;
        static uint WM_LBUTTONUP = 0x0202;
        static uint WM_MBUTTONDBLCLK = 0x0209;
        static int MAKELPARAM(int p, int p_2)
        {
            return ((p_2 << 16) | (p & 0xFFFF));
        }

        static void Main(string[] args)
        {

            
            
            Console.CursorVisible = false;
            while (!Console.KeyAvailable)
            {
                ElementFromCursor();
            }
            Console.CursorVisible = true;
            
        }

        /*
        static void ShowMousePosition()
        {
            POINT point;
            GetCursorPos(out point);
            //Console.Clear();
            Console.WriteLine("({0},{1})", point.X, point.Y);
            _x = point.X;
            _y = point.Y;   
        }
        */

        static void ElementFromCursor()
        {
            //POINT point;
            POINT points;
            //Thread.Sleep(2000);
            GetCursorPos(out points);
            
            // Convert mouse position from System.Drawing.Point to System.Windows.Point.
            System.Windows.Point point = new System.Windows.Point(points.X, points.Y);
            Console.WriteLine(point);

            
            AutomationElement element = AutomationElement.FromPoint(point);
            Thread.Sleep(300);
            //Console.WriteLine(element.Current.BoundingRectangle);
            // Get Window instance from Point
            IntPtr hwnd = WindowFromPoint(points);
            IntPtr realhWnd = RealChildWindowFromPoint(hwnd, points);

            // IntPtr lparam = MAKELPARAM(points.X, points.Y);
            //System.Windows.Point clickablePoint = element.GetClickablePoint();
            //Thread.Sleep(300);

            //SENDING MESSAGES TO HWND

            /*
            SendMessage(hwnd, WM_LBUTTONDOWN, IntPtr.Zero, MAKELPARAM(points.X, points.Y));
            //Thread.Sleep(100);
            SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MAKELPARAM(points.X, points.Y));
            //Thread.Sleep(300);

            SendMessage(hwnd, WM_LBUTTONDOWN, IntPtr.Zero, MAKELPARAM(points.X, points.Y));
            //Thread.Sleep(100);
            SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MAKELPARAM(points.X, points.Y));
            //Thread.Sleep(300);
            */



            // Double click

            //SendMessage(hwnd, WM_MBUTTONDBLCLK, IntPtr.Zero, MAKELPARAM(points.X, points.Y));
            //Thread.Sleep(300);

            /* Tree Walker

            Condition condition1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
            TreeWalker walker = new TreeWalker(condition1);
            AutomationElement elementNode = TreeWalker.RawViewWalker.GetFirstChild(element);

            */

            // Get Any Value Property from Element
            //string autoId = element.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
            Console.WriteLine(element.Current.Name);

        }
    }
}
