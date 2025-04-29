using UnityEngine;
using System.Runtime.InteropServices;

public class ChangeWallpaper : MonoBehaviour
{
    const int SpiSetdeskwallpaper = 20;
    const int SpifUpdateinifile = 0x01;
    const int SpifSendwininichange = 0x02;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    public void SetDesktopWallpaper(string imagePath)
    {
        try
        {
            SystemParametersInfo(SpiSetdeskwallpaper, 0, imagePath, SpifUpdateinifile | SpifSendwininichange);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error setting wallpaper: " + e.Message);
        }
    }

    // Example usage:
    void Start()
    {
        // Ensure the image file exists at this path
        string wallpaperPath = Application.dataPath + "/Wallpapers/Sistina-interno.jpg";
        SetDesktopWallpaper(wallpaperPath);
        Debug.Log("Attempted to set wallpaper to: " + wallpaperPath);
    }
}