using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial
{
    /// <summary> embodied condition: emb / non </summary>
    //public string embodiment;
    /// <summary> cognitive condition 1 / 2  (no cog, cognitive question asked) </summary>
    //public int cog;
    /// <summary> graph dimension: 0 / 2 / 3 </summary>
    //public int dimension;
    /// <summary> experiment task: correlation / outlier</summary>
    //public string task;


    /// <summary> the dataset file id we should read from for this trial</summary>
    public int fileID;

    /// <summary> expected answer: vlow, low, med, high, vhigh OR based on the data dimensions</summary>
    public string expected;


    /// <summary>
    /// Constructor for a single trial
    /// </summary>
    /// <param name="embodiment"></param>
    /// <param name="dimension"></param>
    /// <param name="task"></param>
    /// <param name="expected"></param>
    public Trial(int fileID, string expected)
    {
        this.fileID = fileID;
        this.expected = expected;


    }

    public override string ToString()
    {
        return "FileID: " + fileID + ", Expected: " + expected;
    }
}
