using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;

public class TooltipManager : MonoBehaviour
{
    //if person detected event gets called, spawn tooltip and set it as child of the detected transform
    //position = detected transform + offset so that tooltip is hovering above object
    //target transform = first child of detected object --> empty object that sits at top

    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] private float offset;
    [SerializeField] private Vector3 offsetVector;


    private void OnEnable()
    {
        //subscribe to detection event
        BodyDetector.OnPersonDetected += SpawnToolTip;
    }
    private void OnDisable()
    {
        //unsubscribe from event
        BodyDetector.OnPersonDetected -= SpawnToolTip;
    }

    private void Start()
    {
        offsetVector = new Vector3(0, offset, 0);
    }

    private void SpawnToolTip(GameObject person)
    {
        Debug.Log("person: " + person.name);
        GameObject tooltip = Instantiate(tooltipPrefab, person.transform.GetChild(0).position + offsetVector, Quaternion.identity);
        tooltip.transform.localScale = new Vector3(1, 1, 1);
        tooltip.transform.parent = person.transform;
    }

}
