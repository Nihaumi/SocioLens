using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using csharp_client;

public class JSONDeserializer : MonoBehaviour
{
    //extract all json object from list -> make each an object person?
    //deserialize json stuff

    //event
    public delegate void InfoCard(string data);
    public static event InfoCard OnFirstName;
    public static event InfoCard OnLastName;
    public static event InfoCard OnRole;
    public static event InfoCard OnPlace;
    public static event InfoCard OnID;
    public static event InfoCard OnPicturePath;


    private void OnEnable()
    {
        CameraScript.OnReceivedData += HandleDataInput;
    }
    private void OnDisable()
    {
        CameraScript.OnReceivedData -= HandleDataInput;
    }
    public void OutputReceivedData(string data)
    {
        //get json object in list and deserialize it
        List<JSONData> jsonDataList = JsonConvert.DeserializeObject<List<JSONData>>(data);
        JSONData jsonData = jsonDataList[0];

        //trigger event for infocard
        //give infor for name + last name, 
        //only if ID not yet existing

        OnFirstName(jsonData.firstName);
        OnLastName(jsonData.lastName);
        OnRole(jsonData.role);
        OnPlace(jsonData.placeMet);
        OnID(jsonData.id.ToString());
        OnID(jsonData.picture);

        Debug.Log("giving strings");
        Debug.Log("id: " + jsonData.id);
        Debug.Log("name: " + jsonData.firstName);
        Debug.Log("picturepath: " + jsonData.picture);
        Debug.Log("role: " + jsonData.role);
        Debug.Log("lastname " + jsonData.lastName);
        Debug.Log("place met" + jsonData.placeMet);
    }

    //data = list of json objects
    private void HandleDataInput(string data)
    {
        Debug.Log("DATA RECEIVED");
        OutputReceivedData(data);
    }
}
