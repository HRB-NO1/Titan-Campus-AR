using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DialogueFetcher
{
    private const string BaseUrl = "https://eda2-47-147-0-232.ngrok-free.app/dialogue/";

    // 异步获取对话数据并返回TextAsset
    public static IEnumerator FetchDialogueAsTextAsset(string dialogueName, System.Action<TextAsset> onCompleted)
    {
        string url = BaseUrl + dialogueName;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 请求成功，使用获取到的文本创建TextAsset
            TextAsset dialogueTextAsset = new TextAsset(request.downloadHandler.text);
            onCompleted?.Invoke(dialogueTextAsset);
        }
        else
        {
            // 请求失败
            Debug.LogError("Error fetching dialogue: " + request.error);
            onCompleted?.Invoke(null);
        }
    }
}
