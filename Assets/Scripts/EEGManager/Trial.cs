using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial
{
    /// <summary> embodied condition: emb / non </summary>
    public string embodiment;
    /// <summary> graph dimension: 0 / 2 / 3 </summary>
    public int dimension;
    /// <summary> experiment task: correlation / outlier</summary>
    public string task;
    /// <summary> expected answer: vlow, low, med, high, vhigh</summary>
    public string expected;


    /// <summary>
    /// Constructor for a single trial
    /// </summary>
    /// <param name="embodiment"></param>
    /// <param name="dimension"></param>
    /// <param name="task"></param>
    /// <param name="expected"></param>
    public Trial(string embodiment, int dimension, string task, string expected)
    {
        this.embodiment = embodiment;
        this.dimension = dimension;
        this.task = task;
        this.expected = expected;
    }

    public override string ToString()
    {
        return "Embodiment: " + embodiment + ", Dimension: " + dimension + ", Task: " + task + ", Expected: " + expected;
    }
}
