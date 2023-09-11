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
    public delegate void InfoCard(InfoCardManager.InfoCard infoCard);
    public static event InfoCard OnInfoCardInitialized;


    private void OnEnable()
    {
        CameraScript.OnReceivedData += HandleDataInput;
    }
    private void OnDisable()
    {
        CameraScript.OnReceivedData -= HandleDataInput;
    }

    //deserializes JSON string and creates InfoCard with the extracted data, triggers event for InfoCardManager
    public void DeserializAndSendReceivedData(string data)
    {
        //get json object in list and deserialize it
        List<JSONData> jsonDataList = JsonConvert.DeserializeObject<List<JSONData>>(data);
        JSONData jsonData = jsonDataList[0];

        //trigger event for infocard
        //give infor for name + last name, 
        //only if ID not yet existing


        //create instance of InfoCard class
        InfoCardManager.InfoCard infoCard = new InfoCardManager.InfoCard
        {
           id = jsonData.id,
           firstName = jsonData.firstName,
           lastName = jsonData.lastName,
           role = jsonData.role,
           pathToImage = jsonData.picture,
           placeMet = jsonData.placeMet
        };

        //extraction Logs
        #region
        Debug.Log("giving strings");
        Debug.Log("id: " + jsonData.id);
        Debug.Log("name: " + jsonData.firstName);
        Debug.Log("picturepath: " + jsonData.picture);
        Debug.Log("role: " + jsonData.role);
        Debug.Log("lastname " + jsonData.lastName);
        Debug.Log("place met" + jsonData.placeMet);
        #endregion

        //trigger event and give it the created instance of an ionfocard with received data
        OnInfoCardInitialized(infoCard);
    }

    //data = list of json objects
    private void HandleDataInput(string data)
    {
        Debug.Log("DATA RECEIVED");
        DeserializAndSendReceivedData(data);
    }
}
