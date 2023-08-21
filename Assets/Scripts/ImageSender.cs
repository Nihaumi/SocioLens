using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ImageSender : MonoBehaviour
{
    public string UploadImage_URL { get; private set; }
    private string imageName;
    private string pathToImage;

    private void Start()
    {
        imageName = "imageName.jpg";
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", File.ReadAllBytes(Application.streamingAssetsPath + "/test.jpg"), imageName);
        form.AddField("userId", "17ac4c482dcdd");

        UnityWebRequest www = UnityWebRequest.Post(UploadImage_URL, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! " + www.downloadHandler.text);
        }
    }
}
