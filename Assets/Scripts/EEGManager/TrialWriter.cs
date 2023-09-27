using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrialWriter : MonoBehaviour
{

    private TextWriter trialTW;
    private string savePath;
    
    
    // Start is called before the first frame update
    void Start()
    {
        CreateOrOpenTrialFile();
    }

    /// <summary>
    /// Setup the trial writer file, all performance data writes to this file
    /// </summary>
    public void CreateOrOpenTrialFile()
    {
        savePath = "./Save_Data/Trials/" + GlobalVariables.pID + "_trials.csv ";
        trialTW = new StreamWriter(savePath, append: true);
        trialTW.WriteLine("ID,Embodied,Dimension,Task,Expected," +  // obtained from global data
            "TrialNum,Result,Correct,ResponseTime");  // passed in data
    }

    /// <summary>
    /// Write the trial data to file
    /// <para>tnum = trial number for that condition (reset when new condition)</para>
    /// <para>result = user input (vlow-vhigh or data type)</para>
    /// <para>time = time taken during trial</para>
    /// </summary>
    /// <param name="tNum"></param>
    /// <param name="result"></param>
    /// <param name="time"></param>
    public void WriteTrial(int tNum, string result, float time)
    {
        var correctResult = GlobalVariables.activeTrial.expected == result;
        trialTW.WriteLine(GlobalVariables.pID + "," +
            GlobalVariables.activeTrial.embodiment + "," +
            GlobalVariables.activeTrial.dimension + "," +
            GlobalVariables.activeTrial.task + "," +
            GlobalVariables.activeTrial.expected + "," +
            tNum + "," +
            result + "," +
            correctResult + "," +
            time
            );
    }
    private void OnDestroy()
    {
        trialTW.Close();
    }
}
