using Newtonsoft.Json; //unity package com.unity.nuget.newtonsoft-json@3.0 from https://github.com/jilleJr/Newtonsoft.Json-for-Unity
using UnityEngine;

[System.Serializable]
public class JSONData
{
    [JsonProperty("place_met")]
    public string placeMet;

    [JsonProperty("lastname")]
    public string lastName;

    [JsonProperty("picture")]
    public string picture;
    [JsonProperty("firstname")]
    public string firstName;

    [JsonProperty("employee_id")]
    public int id;

    [JsonProperty("role")]
    public string role;
}