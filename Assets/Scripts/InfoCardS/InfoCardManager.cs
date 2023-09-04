using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoCardManager : MonoBehaviour
{

    public GameObject infoCardPrefab;
    public Transform infoContainer;
    public Vector2 initialPosition;
    [SerializeField] float yOffset;
    string name;
    string role;

    private void Start()
    {
       initialPosition = new Vector2(276.830017f, 244.5f);
    }
    public void RecognizePerson()
    {
        // Instantiate the info card prefab and set its text values
        GameObject infoCard = Instantiate(infoCardPrefab, infoContainer);
        InfoCardScript cardScript = infoCard.GetComponent<InfoCardScript>();
        cardScript.SetInfo(name, role);

        // Calculate the position for the new card
        Vector2 newPosition = initialPosition + Vector2.up * (infoContainer.childCount - 1) * yOffset;

        // Set the calculated position for the new card
        infoCard.transform.localPosition = newPosition;
    }

}
