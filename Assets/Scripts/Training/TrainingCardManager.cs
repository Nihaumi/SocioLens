using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCardManager : MonoBehaviour
{
    [SerializeField] private GameObject trainingCardPrefab;
    [SerializeField] private GameObject card;
    [SerializeField] private Transform camera;
    [SerializeField] private float offset;


    public void InstantiateCard()
    {
        // Instantiate the info card prefab and set its text values
        card = Instantiate(trainingCardPrefab, camera.position + new Vector3(0,0,offset), Quaternion.identity);
    }

}
