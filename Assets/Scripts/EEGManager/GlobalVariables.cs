using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class GlobalVariables
{
    /// <summary> participant id </summary>
    public static int pID = 0; 
    
    /// <summary> embodied condition: emb / non </summary>
    public static string conEmb = ""; 
    
    /// <summary> graph dimension: 0 / 2 / 3 </summary>
    public static int conD = 2; 
    
    /// <summary> experiment task: correlation / outlier</summary>
    public static string conTask = "correlation";
    
    /// <summary> expected answer: vlow, low, med, high, vhigh</summary>
    public static string conExpected = "";
    
    /// <summary> list of trials for participant</summary>
    public static List<Trial> conTrials = new List<Trial>();

    public static Trial activeTrial;

    public static int activeTrialIndex;

    public static void NextTrial()
    {
        activeTrialIndex += 1;
        activeTrial = conTrials[activeTrialIndex];
        Debug.Log(activeTrial.ToString());
    }

}
