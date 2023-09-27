using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IDSceneManager : MonoBehaviour
{

    private Camera cam;

    private string taID = "";
    private string filePath = "./Trials/";

    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cam.backgroundColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnGUI()
    {
        
        taID = GUI.TextArea(new Rect(30, 10, 40, 20), taID);

        if (GUI.Button(new Rect(280, 10, 100, 20), "Begin!"))
        {
            int nRet;
            if (int.TryParse(taID, out nRet))
            {
                GlobalVariables.pID = nRet;


                // error, 0 = training, int = experiment scene
                if (!LoadTrialFiles(GlobalVariables.pID))
                    Debug.LogError(string.Format("Task file Load Failed : {0}.dat!", taID));
                else if (GlobalVariables.pID == 0)
                    // SceneManager.LoadScene(2);
                    print("Placeholder for loading training scene with id 0");
                else
                {
                    GlobalVariables.activeTrial = GlobalVariables.conTrials[0];
                    SceneManager.LoadScene(GlobalVariables.activeTrial.embodiment == "emb" ? "EmbodiedScene" : "NonEmbodiedScene");
                }
            }
        }
    }

    private bool LoadTrialFiles(int pid)
    {

        try
        {
            StreamReader sr = new StreamReader(filePath + pid + ".txt");

            using(sr)
            {
                string line = sr.ReadLine();
                if(line != null)
                {
                    do
                    {
                        string[] trialSplit = line.Split(' ');
                        if(trialSplit.Length > 0)
                        {
                            Trial t = new Trial(trialSplit[0], int.Parse(trialSplit[1]), trialSplit[2], trialSplit[3]);
                            GlobalVariables.conTrials.Add(t);
                        }
                        line = sr.ReadLine();
                    }
                    while (line != null);
                   

                }
                sr.Close();
            }

            print(GlobalVariables.conTrials.Count);

            return true;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }
    }
}
