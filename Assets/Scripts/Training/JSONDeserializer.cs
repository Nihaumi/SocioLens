using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JSONDeserializer : MonoBehaviour
{
    string jsonTestString = "{\"employee_id\": 1 , \"firstname\": \"beste\", \"picture\": \"static/employee_pics/beste.jpg\"}";

    public void ReceivedImage()
    {
      //  Debug.Log("id: " + JSONData.id);
    }
}
