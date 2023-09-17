using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoCardManager : MonoBehaviour
{

    public GameObject infoCardPrefab;
    public Transform infoCardContainer;

    InfoCardScript cardScript;
   [SerializeField] private GameObject card;

    private void OnEnable()
    {
        JSONDeserializer.OnInfoCardInitialized += InstantiateInfoCardWithData;
    }
    private void OnDisable()
    {
        JSONDeserializer.OnInfoCardInitialized -= InstantiateInfoCardWithData;
    }

    public void InstantiateInfoCard()
    {
        // Instantiate the info card prefab and set its text values
        card = Instantiate(infoCardPrefab, infoCardContainer);
        cardScript = card.GetComponent<InfoCardScript>();
        cardScript.SetInfo("Jane Doe", "staff", "");
    }

    public class InfoCard
    {
        public int id;
        public string firstName;
        public string lastName;
        public string role;
        public string pathToImage;
        public string placeMet;
    }

    // Instantiate the info card prefab and set its text values
    public void InstantiateInfoCardWithData(InfoCard infoCard)
    {
        if (infoCard != null)
        {
            Debug.Log("received infocard: " + infoCard.firstName + "" + infoCard.lastName);
            card = Instantiate(infoCardPrefab, infoCardContainer);
            card.GetComponent<InfoCardScript>().SetInfo(infoCard.firstName + infoCard.lastName, infoCard.role, infoCard.pathToImage);
        }
        else
        {
            cardScript.SetInfo("no information available", "", "");
        }
    }



}
