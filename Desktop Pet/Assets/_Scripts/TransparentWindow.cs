
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
    
    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x00080000;
    const int WS_EX_TRANSPARENT = 0x00000020;
    
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    
    const uint LWA_COLORKEY = 0x00000001;

    private IntPtr hWnd;
    
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static int GetTaskbarHeight() {
        IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);
        if (taskbarHandle == IntPtr.Zero) {
            Rect taskbarRect;
            if (GetWindowRect(taskbarHandle, out taskbarRect)) {
                return taskbarRect.Bottom - taskbarRect.Top;
            }
        }
        return 0;
    }

    public static Rect GetTaskbarRectUnity() {
        IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);
        Rect untiyRect = new Rect();
        if (taskbarHandle != IntPtr.Zero) {
            GetWindowRect(taskbarHandle, out untiyRect);
        }
        return untiyRect;
    }
#endif
    private void Start() {
        // Application.OpenURL("https://www.youtube.com/watch?v=xvFZjo5PgG0");
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        // SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);
        
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, WS_EX_TRANSPARENT);
        int taskbarHeight = GetTaskbarHeight();
        Debug.Log("Taskbar Height: " + taskbarHeight);

        Rect taskbarRect = GetTaskbarRectUnity();
        Debug.Log($"Taskbar Rect (Left: {taskbarRect.Left}, Top: {taskbarRect.Top}, Right: {taskbarRect.Right}, Bottom: {taskbarRect.Bottom})")
#endif
        Application.runInBackground = true;
    }

    private void Update() {
        Vector2 mousePos =  Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        SetClickthrough(Physics2D.OverlapPoint(mousePos) == null);
    }

    private void SetClickthrough(bool clickthrough) {
        if (clickthrough) {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }
}
