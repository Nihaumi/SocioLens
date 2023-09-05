using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class InfoCardScript : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    private TMP_Text role;
    [SerializeField] private RawImage image;
    [SerializeField] private string imagePath;
    [SerializeField] private string imagePathPart2;

    private void Start()
    {
        ReferenceInfoCardComponents();
        LoadImage();
    }
    public void SetInfo(string name, string role, string pathToImage)
    {
        ReferenceInfoCardComponents();
        this.name.text = name;
        this.role.text = role;
        this.imagePathPart2 = pathToImage;
        LoadImage();
    }

    private void ReferenceInfoCardComponents()
    {
        this.name = gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        this.role = gameObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        this.image = gameObject.transform.GetChild(0).gameObject.GetComponent<RawImage>();
    }
    private void LoadImage()
    {
        //load image from path as texture for the raw image in info card
        //get absolute path to kp-mci project, the second part including image name comes with JSON from server
        imagePath = "D:/Uni/SoS23/MCI/kp-mci/";
        //imagePath = "../../../../kp-mci/";
        if (imagePathPart2 == null || imagePathPart2 == "")
        {
            imagePathPart2 = "static/employee_pics/WIN_20230904_13_22_51_Pro.jpg";
        }
        byte[] bytes = File.ReadAllBytes(imagePath + imagePathPart2);
        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);
        image.texture = loadTexture;
    }

}
