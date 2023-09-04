using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using csharp_client;

public class JSONDeserializer : MonoBehaviour
{
    //List<var> jsonTestString = "{"place_met": null, "lastname": "ever", "picture": "static/employee_pics/beste.jpg", "last_seen": null, "employee_id": 1, "firstname": "bescht", "role": "sonnenschein"}]";
    //TODO get e.data from camera script 
    //extract all json object from list -> make each an object person?
    //deserialize json stuff

    //string from json object
    private string jsonString;

    JSONData jsonData;

    private void OnEnable()
    {
        CameraScript.OnReceivedData += HandleDataInput;
    }
    private void OnDisable()
    {
        CameraScript.OnReceivedData -= HandleDataInput;
    }
    public void ReceivedImage(string data)
    {
        jsonData.id = jsonString; //?????
        Debug.Log("id: " + jsonData.id);
        Debug.Log("name: " + jsonData.firstName);
        Debug.Log("picturepath: " + jsonData.picture);
    }

    //data = list of json objects
    private void HandleDataInput(string data)
    {
       jsonString = data[0].ToString();
       jsonData = JsonConvert.DeserializeObject<JSONData>(data);
    }
}
