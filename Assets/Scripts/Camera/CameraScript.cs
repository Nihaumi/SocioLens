using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Windows.WebCam;

namespace csharp_client
{
    
    public class CameraScript : MonoBehaviour
    {
        //TODO use hololens cam instead of webcam
        public float framecounter = 0.0f;
        public TextMeshPro startStopText;
        public TextMeshPro camText;
        int currentCamIndex = 0;
        WebCamTexture tex;
        public RawImage display;
        WebSocket ws;
        public byte[] mybytes;

        //events
        public delegate void ONReceive(string data);
        public static event ONReceive OnReceivedData;

        //for using hololens camera
        private PhotoCapture photoCaptureObject = null;

        //hololens Cam tutorial
#if !UNITY_EDITOR
        #region
        public void Start()
        {
            PhotoCapture.CreateAsync(OnPhotoCaptureCreated);
        }

        private void OnPhotoCaptureCreated(PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;

            Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

            CameraParameters c = new CameraParameters();
            c.hologramOpacity = 0.0f;
            c.cameraResolutionWidth = cameraResolution.width;
            c.cameraResolutionHeight = cameraResolution.height;
            c.pixelFormat = CapturePixelFormat.BGRA32;

            captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
        }

        private object OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
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
        void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
        {
            if (result.success)
            {
                Debug.Log("Saved Photo to disk!");
                photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
            }
            else
            {
                Debug.Log("Failed to save Photo to disk");
            }
        }

        void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
        {
            photoCaptureObject.Dispose();
            photoCaptureObject = null;
        }

        #endregion
#endif
        public void SwapCam_Clicked()
        {

            if (WebCamTexture.devices.Length > 0)
            {
                currentCamIndex += 1;
                currentCamIndex %= WebCamTexture.devices.Length;

                if (startStopText.text != null)
                {
                    StopWebcam();
                    StartStopCam_Clicked();
                }
            }
        }

        public async void StartStopCam_Clicked()
        {
            if (tex != null) // Stop the camera
            {
                StopWebcam();
                startStopText.text = "Start Camera";

            }
            else    // Start the camera
            {
                WebCamDevice device = WebCamTexture.devices[currentCamIndex];
                tex = new WebCamTexture(device.name);
                display.texture = tex;
                camText.text = device.name;
                tex.Play();
                startStopText.text = "Stop Camera";

                //open Websocket-Connection
                
                ws = new WebSocket("ws://127.0.0.1:8000/ws");
                ws.OnMessage += (sender, e) => {
                    if (e.IsText)
                    {
                        //TODO event send to desirializer
                        Debug.Log("SERVER SAYS: " + e.Data);
                        OnReceivedData(e.Data);
                    }
                    else
                    {
                        Debug.Log("NO TEXT RECEIVED");
                    }
                };

                ws.Connect();

            }
        }

        private void StopWebcam()
        {
            // close Websocket-connection
            //ws.Send("close");
            ws.Close();

            display.texture = null;
            tex.Stop();
            tex = null;
        }
        void Start()
        {

            WebCamDevice[] devices = WebCamTexture.devices;
            for (int i = 0; i < devices.Length; i++)
            {

                Debug.Log(devices[i].name);
            }
        }

        async void Update()
        {

            framecounter += Time.deltaTime;

            if (framecounter >= 2 && tex != null)
            {
                Texture2D snap = new Texture2D(tex.width, tex.height);
                snap.SetPixels(tex.GetPixels());
                snap.Apply();

                mybytes = snap.EncodeToJPG();

                // SEND IMAGE VIA WEBSOCKET TO SERVER
                ws.Send(mybytes);
                framecounter = 0;
            }

        }


    }
}


