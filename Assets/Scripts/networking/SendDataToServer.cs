using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
public class SendDataToServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeRequests());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator MakeRequests()
    {
        //GET
        var getRequest = CreateRequest("https://serverplaceholder");
        yield return getRequest.SendWebRequest();
        var deserializedGetData = JsonUtility.FromJson<Todo>(getRequest.downloadHandler.text);

        //POST
        var dataToPost = new PostData() { Hero = "herowoman" };
        var postRequest = CreateRequest("https://placeholderserver", RequestType.POST, dataToPost);
        yield return postRequest.SendWebRequest();
        var deserializedPostData = JsonUtility.FromJson<PostResult>(postRequest.downloadHandler.text);
    }


    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            //grab this infor when POST
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }
        //grab this infor when GET
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2
    }
    public class Todo
    {
        public int userID;
        public int id;
    }

    [SerializeField]
    public class PostData
    {
        public string Hero;
    }

    public class PostResult
    {
        public string success { get; set; }
    }
}

