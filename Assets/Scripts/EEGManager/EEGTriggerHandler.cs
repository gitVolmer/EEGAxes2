using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// EEG Event trigger handler. Opens TcpClient to OpenVibe and allows for triggers to be sent
/// </summary>
public class EEGTriggerHandler : MonoBehaviour
{
    private static TcpClient socketConnection;

    private static EEGTriggerHandler _instance;
    public static EEGTriggerHandler Instance => _instance;
    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            socketConnection = new TcpClient("localhost", 15361);
            Debug.Log("Connected to OpenVibe");
        }
        catch (SocketException se)
        {
            Debug.Log("Socket exception: " + se);
            print("ISSUE OPENVIBE");
        }
    }


    //  X00 = condition calculated here
    //  2 2D, 3 3D, 4 cog2D, 6 cog3D
    // X0 = embodiment passed in
    // 1 = non, 2 = emb
    //  0X = event passed in
    //  1 start, 2 trial end,  9 end con 
    public static void SendTrigger(int eventId)
    {
        //add condition
        eventId += (GlobalVariables.conD * GlobalVariables.conCog) * 100;
        print("EVENT: " + eventId);

        if (socketConnection == null) return; // no openvibe connection

  
        var stream = socketConnection.GetStream();

        if (!stream.CanWrite) return;

       // int finalId =  0;

        var buffer = BitConverter.GetBytes((ulong)0);
        var eventTag = BitConverter.GetBytes((ulong)eventId);

        var sendArray = buffer.Concat(eventTag.Concat(buffer)).ToArray();
        stream.Write(sendArray, 0, sendArray.Length);
    }
}
