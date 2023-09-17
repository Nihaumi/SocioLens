using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuNavigation : MonoBehaviour
{

    //Menu Texts
    string menuText1 = "SocioLens can recognize the Faces of your coworkers Here is how it works:";
    string menuText2 = "Look at your palm to show a hand menu.";
    string menuText3 = "The Recognition Process will be started by using the start camera button. ";
    string menuText4 = "The name, role and time and place of the last encounter of recognized people will be displayed.";
    string menuText5 = "Have a fun time socializing!";
    //counter for pagenumbers
    private int counter = 1;
    private int numberOfMenuTexts = 5;
    //TMP
    [SerializeField] private TMP_Text tmpMenuText;
    [SerializeField] private TMP_Text pagination;
    //tutorial canavs
    [SerializeField] private GameObject tutorialCanvas;

    private void Start()
    {
        ShowTutorial();
        SetPagination();
    }
    //displays current page out of max number of pages
    private void SetPagination()
    {
        pagination.text = counter + "/" + numberOfMenuTexts;
    }

    //showsprevious page
    public void Previous()
    {
        Debug.Log("End Touch");
        counter--;
        ChangeMenuText();
    }

    //shows next page
    public void Next()
    {
        Debug.Log("End Touch");
        counter++;
        ChangeMenuText();
    }

    //deactivates the tutorial
    public void EndTutorial()
    {
        counter = 1;
        SetPagination();
        tutorialCanvas.SetActive(false);
    }

    //activates tutorial
    public void ShowTutorial()
    {
        counter = 1;
        SetPagination();
        ChangeMenuText();
        tutorialCanvas.SetActive(true);
    }

    //shows/hides tutorial, resets tutorial to page 1
    public void ToggleTutorial()
    {
        counter = 1;
        SetPagination();
        if (tutorialCanvas.activeInHierarchy)
        {
            tutorialCanvas.SetActive(false);
        }
        else
        {
            tutorialCanvas.SetActive(true);
        }
    }

    // displays text according to current page
    private void ChangeMenuText()
    {
        if (counter < 1)
        {
            counter = 1;
        }
        if (counter > numberOfMenuTexts)
        {
            counter = numberOfMenuTexts;
        }
        switch (counter)
        {
            case 1:
                tmpMenuText.text = menuText1;
                break;
            case 2:
                tmpMenuText.text = menuText2;
                break;
            case 3:
                tmpMenuText.text = menuText3;
                break;
            case 4:
                tmpMenuText.text = menuText4;
                break;
            case 5:
                tmpMenuText.text = menuText5;
                break;
            default:
                break;

        }
        SetPagination();
    }


}
