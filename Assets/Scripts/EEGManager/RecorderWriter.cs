using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO.Ports;
using System;
using System.Threading;

/// <summary>
/// Connects to and sends triggers to BrainVisionLiveAmp application via proxy port. Needs repairing.. also project settings changed
/// Likely stick to using OpenVibe but will look into this option if only few triggers are required
/// </summary>
public class RecorderWriter : MonoBehaviour
{

    //SerialPort triggerbox;
    //Byte[] data = { (Byte)0 };
    //// Start is called before the first frame update
    //void Start()
    //{
    //    triggerbox = new SerialPort("COM5");

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        sendTrigger();
    //    }
    //}

    //void sendTrigger()
    //{
    //    triggerbox.Open();
    //    triggerbox.ReadTimeout = 5000;
    //    triggerbox.DataReceived += TriggerBox_DataReceived;

    //    data[0] = 0x00;
    //    triggerbox.Write(data, 0, 1);
    //    Thread.Sleep(10);

    //    data[0] = 0x01;
    //    triggerbox.Write(data, 0, 1);
    //    Thread.Sleep(10);

    //    data[0] = 0x00;
    //    triggerbox.Write(data, 0, 1);
    //    Thread.Sleep(10);

    //    triggerbox.DataReceived -= TriggerBox_DataReceived;
    //    triggerbox.Close();

    //}

    //static void TriggerBox_DataReceived(object sender, SerialDataReceivedEventArgs e)
    //{
    //    SerialPort sp = (SerialPort)sender;
    //    // get all available bytes from the input buffer
    //    while (sp.BytesToRead > 0)
    //    {
    //        Console.WriteLine(sp.ReadByte());
    //    }
    //}
}
