using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransparentWindow : MonoBehaviour
{
    [SerializeField] private LayerMask floorLayer;
    private LayerMask excludeFloorLayer;

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

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

    [DllImport("user32.dll")]
    private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref RECT pvParam, uint fWinIni);

    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private const uint SPI_GETWORKAREA = 0x0030;

    private IntPtr hWnd;

    private void Start() {
        excludeFloorLayer = ~floorLayer;
        // Application.OpenURL("https://www.youtube.com/watch?v=xvFZjo5PgG0");
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        // SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);
        
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, WS_EX_TRANSPARENT);
#endif
        Application.runInBackground = true;

        int taskbarHeight = GetTaskbarHeight();
        float unitHeight = PixelsToUnits(taskbarHeight);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - unitHeight, Camera.main.transform.position.z);
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        SetClickThrough(Physics2D.OverlapPoint(mousePos, excludeFloorLayer) == null);
    }

    private void SetClickThrough(bool clickThrough) {
        if (clickThrough) {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }

    private int GetTaskbarHeight() {
        // Calculate by comparing screen and work area
        RECT workArea = new RECT();
        SystemParametersInfo(SPI_GETWORKAREA, 0, ref workArea, 0);

        int screenHeight = Screen.currentResolution.height;
        int workAreaHeight = workArea.Bottom - workArea.Top;

        // Determine taskbar position
        IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);
        if (taskbarHandle != IntPtr.Zero) {
            RECT taskbarRect;
            GetWindowRect(taskbarHandle, out taskbarRect);

            // Check if taskbar is at the bottom (most common)
            if (taskbarRect.Top > workArea.Top) {
                return screenHeight - workAreaHeight;
            }
            // Taskbar might be at the top
            else if (taskbarRect.Bottom < workArea.Bottom) {
                return workArea.Top;
            }
            // Taskbar might be on the left or right side
            else {
                // In this case, we'd return a width, but the question asked for height
                return screenHeight - workAreaHeight;
            }
        }

        // Fallback calculation - difference between screen height and work area height
        return screenHeight - workAreaHeight;
    }

    private float PixelsToUnits(float pixels) {
        Camera cam = Camera.main;
        if (cam == null) {
            Debug.LogWarning("No camera found!");
            return pixels / 100f;
        }

        float unitsPerPixel = (cam.orthographicSize * 2f) / Screen.height;
        return pixels * unitsPerPixel;
    }
}