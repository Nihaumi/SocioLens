using Newtonsoft.Json; //unity package com.unity.nuget.newtonsoft-json@3.0 from https://github.com/jilleJr/Newtonsoft.Json-for-Unity
using UnityEngine;

[System.Serializable]
public class JSONData
{
    [JsonProperty("employee_id")]
    public string id;

    [JsonProperty("firstname")]
    public string firstName;

    [JsonProperty("role")]
    public string role;

    [JsonProperty("place_met")]
    public string placeMet;

    [JsonProperty("lastname")]
    public string lastName;

    [JsonProperty("picture")]
    public string picture;
}