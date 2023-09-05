using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoCardManager : MonoBehaviour
{

    public GameObject infoCardPrefab;
    public Transform infoCardContainer;

    InfoCardScript cardScript;
    GameObject card;
    string name;
    string role;
    InfoCard testCard;

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
        cardScript.SetInfo("Jane", "Doe", "static/employee_pics/WIN_20230904_13_22_51_Pro.jpg");
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

    public void InstantiateInfoCardWithData(InfoCard infoCard)
    {
        Debug.Log("I Am called with infocard: " + infoCard.id);
        testCard = infoCard;
        InstantiateInfoCard();
        Debug.Log("I Am called with infocard: " + infoCard.firstName);
        if (infoCard != null)
        {
            Debug.Log("received infocard: " + infoCard.firstName + "" + infoCard.lastName);
            cardScript.SetInfo(infoCard.firstName + "" + infoCard.lastName, infoCard.role, infoCard.pathToImage);
        }
        else
        {
            cardScript.SetInfo("nothing", "atall", "static/employee_pics/WIN_20230904_13_22_51_Pro.jpg");
        }
    }


}
