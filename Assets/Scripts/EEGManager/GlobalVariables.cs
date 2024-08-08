using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class GlobalVariables
{
    /// <summary> participant id </summary>
    public static int pID = 0;


    /// <summary>
    /// Session ID 0-7 line from file
    /// </summary>
    public static int sesID = 0;
    
    /// <summary> embodied condition: emb / non </summary>
    public static string conEmb = ""; 
    
    /// <summary> graph dimension: 0 / 2 / 3 </summary>
    public static int conD = 2; 
    
    /// <summary> experiment task: correlation / outlier</summary>
    public static string conTask = "correlation";

    /// <summary> cognitive condition: 1 = baseline, 2 = cognitive quesiton</summary>
    public static int conCog = 1;
    
    /// <summary> expected answer: vlow, low, med, high, vhigh</summary>
    public static string conExpected = "";
    
    /// <summary> list of trials for participant</summary>
    public static List<Trial> conTrials = new List<Trial>();

    public static Trial activeTrial;

    public static int activeTrialIndex;

    public static void NextTrial()
    {
        activeTrialIndex += 1;
        if (conTrials.Count > activeTrialIndex)
        {
            activeTrial = conTrials[activeTrialIndex];
            Debug.Log(activeTrial.ToString());
        }
        else
        {
            if (conEmb == "non")
                EEGTriggerHandler.SendTrigger(19);
            else
                EEGTriggerHandler.SendTrigger(29);


            #if UNITY_STANDALONE
                    Application.Quit();
            #endif
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

}
