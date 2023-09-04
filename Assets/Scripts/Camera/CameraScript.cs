using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;
using Newtonsoft.Json;

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


