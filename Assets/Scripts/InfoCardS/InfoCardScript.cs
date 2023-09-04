using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfoCardScript : MonoBehaviour
{
    private TextMeshPro name;
    private TextMeshPro role;


    private void Start()
    {
        name = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
        role = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
    }
    public void SetInfo(string name, string role)
    {
        this.name.text = name;
        this.role.text = role;
    }
}
