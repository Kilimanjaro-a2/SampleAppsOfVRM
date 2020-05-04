using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using VRM;

/*
 * You have to attch this script to GameObject named "Scripts"
 * because the FileImporterPlugin uses the name "Scripts" as a clue when calling the callback.
 *
 * このスクリプトはシーン上の"Scripts"という名前のついたGameObjectにアタッチしてください。
 * FileImporterPlugin.jslibの中で、"Scripts"と名のつくGameObjectに対してコールバックの呼び出しを行っているためです。
*/ 
public class Sample : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void FileImporterCaptureClick();

    public void OnButtonClicked()
    {
        #if UNITY_EDITOR
            Debug.Log("This script doesn't support Unity Editor. Try on WebGL Build instead.");
        #elif UNITY_WEBGL
            FileImporterCaptureClick();
        #endif
    }

    public void FileSelected(string url)
    {
        StartCoroutine(LoadJson(url));
    }

    private IEnumerator LoadJson(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.LogError("NetworkError");
            }
            else
            {
                LoadVRMFromBytes(webRequest.downloadHandler.data);
            }
        }
    }

    public void LoadVRMFromBytes(Byte[] bytes)
    {
        var context = new VRMImporterContext();
        try {
            context.ParseGlb(bytes);
            var meta = context.ReadMeta(true);
            context.Load();

            var model = context.Root;
            model.gameObject.name = meta.Title;

            context.ShowMeshes();

        } catch(Exception e) {
            Debug.LogError(e);
        }
    }
}
