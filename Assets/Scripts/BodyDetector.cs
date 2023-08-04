using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDetector : MonoBehaviour
{
    //Placeholder object representing a human body
    //bool that fires detection event if true and includes the detected object

    [SerializeField] public GameObject placeholderPerson;
    [SerializeField] private bool personHasBeenDetected;

    //events
    public delegate void Detection(GameObject person);
    public static event Detection OnPersonDetected;

    private void OnEnable()
    {
        //subscribe to event
        OnPersonDetected += ResetPersonDetectionStatus;
    }

    private void OnDisable()
    {
        //unsubscribe to event
        OnPersonDetected -= ResetPersonDetectionStatus;

    }

    private void Start()
    {
        personHasBeenDetected = false;
    }

    private void Update()
    {
        DetectPeople();
    }

    //fireevent if personhasbeendetected is true
    private void DetectPeople()
    {
        if (personHasBeenDetected)
        {
            OnPersonDetected(placeholderPerson);
        }
    }

    //sets personhasbeendetected bool true/false if perso has been detected
    private void ResetPersonDetectionStatus(GameObject person)
    {
        personHasBeenDetected = false;
    }

}
