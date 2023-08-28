using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.WebCam;
using System;

//from https://learn.microsoft.com/en-us/windows/mixed-reality/develop/unity/locatable-camera-in-unity

public class TakePicture : MonoBehaviour
{
    private PhotoCapture photoCaptureObject = null;

    public void TakePhoto()
    {
        //creating a PhotoCapture object
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    //store your object, set your parameters, and start Photo Mode
    private void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters camParameters = new CameraParameters();
        camParameters.hologramOpacity = 0.0f;
        camParameters.cameraResolutionWidth = cameraResolution.width;
        camParameters.cameraResolutionHeight = cameraResolution.height;
        camParameters.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(camParameters, OnPhotoModeStarted);

    }

    //clean up
    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    //take photo and store on disk 
    //TODO do not store, but send to server
    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    //after saving the photo, end photomode and clean up
    private void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("saved Photo to disk");
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }

        else
        {
            Debug.Log("Failed to save Ohoto to Disk");
        }
    }
}
