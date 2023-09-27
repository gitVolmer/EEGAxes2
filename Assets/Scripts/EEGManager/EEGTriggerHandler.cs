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

    public static void SendTrigger(int eventId)
    {
        if (socketConnection == null) return; // no openvibe connection

        var stream = socketConnection.GetStream();

        if (!stream.CanWrite) return;

        int finalId =  0;

        var buffer = BitConverter.GetBytes((ulong)0);
        var eventTag = BitConverter.GetBytes((ulong)finalId);

        var sendArray = buffer.Concat(eventTag.Concat(buffer)).ToArray();
        stream.Write(sendArray, 0, sendArray.Length);
    }
}
