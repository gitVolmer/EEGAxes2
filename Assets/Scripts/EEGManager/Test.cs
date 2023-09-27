using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 
        foreach(Trial t in GlobalVariables.conTrials)
        {
            print(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
