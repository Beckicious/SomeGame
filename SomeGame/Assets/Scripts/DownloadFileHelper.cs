using System.Runtime.InteropServices;

public static class DownloadFileHelper
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void downloadToFile(string content, string filename);
#endif

    public static void DownloadToFile(string content, string filename)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            downloadToFile(content, filename);
#endif
    }
}