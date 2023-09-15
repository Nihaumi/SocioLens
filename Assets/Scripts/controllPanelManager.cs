using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllPanelManager : MonoBehaviour
{

    [SerializeField]  private GameObject controllPanel;
    private int counter;

    private void Start()
    {
        counter = 0;
        HideControllPanel();
    }
    // on every 1st time function gets called show extended menu, on every 2nd call hide it
    public void ToggleControllPanel()
    {
        if(counter%2 == 0)
        {
            ShowControllPanel();
        }
        else if (counter%2 == 1)
        {
            HideControllPanel();
        }
        counter++;
    }

    private void ShowControllPanel()
    {
        controllPanel.SetActive(true);
    }

    private void HideControllPanel()
    {
        controllPanel.SetActive(false);
    }
}
