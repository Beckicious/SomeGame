using System.Runtime.InteropServices;
using UnityEngine;

public class FileHelper : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
#endif
    [DllImport("__Internal")]
    private static extern void downloadToFile(string content, string filename);

    [DllImport("__Internal")]
    public static extern string BrowserTextUpload(string extFilter, string gameObjName, string dataSinkFn);

    public static void DownloadToFile(string content, string filename)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            downloadToFile(content, filename);
#endif
    }

    public static void UploadFileDialog()
    {
        BrowserTextUpload(".csv", "FileHandler", "LoadDocumentString");
    }

    public static void LoadDocumentString(string str)
    {
        Debug.Log(str);
    }
}