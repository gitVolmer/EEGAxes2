using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EEGManager;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{

    public TrialWriter writer;
    private string _currentValue;
    public GameObject[] buttons;

    /// <summary>
    /// The question and tooltip to be asked in text mesh format
    /// </summary>
    public TextMeshProUGUI question;
    public TextMeshProUGUI please;

    /// <summary>
    /// The dimensions of their submitted answer, resets each trial
    /// </summary>
    private List<string> dimensions = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
       // _currentValue = "vhigh";
        UpdateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuestion()
    {
        switch (GlobalVariables.conD)
        {
            case 2:
                if (GlobalVariables.conCog == 1)
                    question.text = "Create a scatterplot of " + GlobalVariables.activeTrial.expected +  "\n What two dimensions are correlated?";
                else
                    question.text = "What two dimensions are correlated?";
                please.text = "Please enter two dimensions";
                break;
            case 3:
                if (GlobalVariables.conCog == 1)
                    question.text = "Create a scatterplot of " + GlobalVariables.activeTrial.expected + "\n What three dimensions are correlated?";
                else
                    question.text = "What three dimesnions are correlated?";
                please.text = "Please enter three dimesions";
                break;

        }

        // reset buttons to not being selected and update their text/values to the trial dimensions
        for(int i = 0; i < buttons.Length; i++)
        {
            // dimension value
            string temp = ScatterPlotSceneManager.Instance.dataObject.Identifiers[i];
            // update button text
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = temp;
            // clear existing listener
            buttons[i].GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
            // ensure all toggles are off after a trial (to prevent visual confusion)
            buttons[i].GetComponent<Toggle>().isOn = false;
            // delegate a new method with the dimension value
            buttons[i].GetComponent<Toggle>().onValueChanged.AddListener( delegate {SetCorrelationValue(temp); });

        }
    }

    public void SetCorrelationValue(string value)
    {
       
       // _currentValue = value;
        if(dimensions.Contains(value))
            dimensions.Remove(value);
        else
            dimensions.Add(value);
    }

    public void SubmitValue()
    {
        // only allow submit to work if they enter the valid number of dimensions. Show tooltip is invalid
        if (dimensions.Count != GlobalVariables.conD)
        {
            please.enabled = true;
          
        }
        else
        {
            print("EEG Trigger: submit");
            EEGTriggerHandler.SendTrigger(8);

            // sort and convert all dimensions to one string
            dimensions.Sort();
            _currentValue = String.Join("&", dimensions);
            if (writer != null)
                writer.WriteTrial(GlobalVariables.activeTrialIndex + 1, _currentValue, GlobalVariables.conEmb == "emb" ? EmbodiedSceneManager.Instance.timeTaken : KeyboardSceneManager.Instance.timeTaken);
            var oldEmb = GlobalVariables.conEmb;
            GlobalVariables.NextTrial();
            if (oldEmb == GlobalVariables.conEmb)
            {
                if (GlobalVariables.conEmb == "emb")
                {
                    EmbodiedSceneManager.Instance.TrialEnd();
                    UpdateQuestion();
                }
                else
                {
                    KeyboardSceneManager.Instance.TrialEnd();
                    UpdateQuestion();
                }
            }
            else
            {
                SceneManager.LoadSceneAsync(GlobalVariables.conEmb == "emb" ? "EmbodiedScene" : "NonEmbodiedScene");
            }
            // hide tooltip
            please.enabled = false;
            // clear set answer
            dimensions.Clear();
        }
    }
    
    


}
