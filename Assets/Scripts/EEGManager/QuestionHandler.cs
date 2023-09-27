using System;
using System.Collections;
using System.Collections.Generic;
using EEGManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionHandler : MonoBehaviour
{

    public TrialWriter writer;
    private string _currentValue;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentValue = "vhigh";
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetCorrelationValue(string value)
    {
        _currentValue = value;
    }

    public void SubmitValue()
    {
        if(writer != null)
            writer.WriteTrial(GlobalVariables.activeTrialIndex+1, _currentValue, GlobalVariables.activeTrial.embodiment == "emb" ? EmbodiedSceneManager.Instance.timeTaken : KeyboardSceneManager.Instance.timeTaken);
        var oldEmb = GlobalVariables.activeTrial.embodiment;
        GlobalVariables.NextTrial();
        if (oldEmb == GlobalVariables.activeTrial.embodiment)
        {
            if (GlobalVariables.activeTrial.embodiment == "emb")
            {
                EmbodiedSceneManager.Instance.ResetScene();
            }
            else
            {
                KeyboardSceneManager.Instance.ResetScene();
            }
        }
        else
        {
            SceneManager.LoadSceneAsync(GlobalVariables.activeTrial.embodiment == "emb" ? "EmbodiedScene" : "NonEmbodiedScene");
        }
    }
    
    


}
