using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public void OnButtonTouchBegin()
    {
        Debug.Log("Begin Touch");
    }
    public void OnButtonTouchEnd()
    {
        Debug.Log("End Touch");
    }
    public void OnButtonTouchPressed()
    {
        Debug.Log("Pressed");
    }
    public void OnButtonTouchReleased()
    {
        Debug.Log("Button Released");
    }
}
