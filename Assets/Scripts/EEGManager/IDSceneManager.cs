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
    private string sesID = "";
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
        sesID = GUI.TextArea(new Rect(90, 10, 40, 20), sesID);

        if (GUI.Button(new Rect(280, 10, 100, 20), "Begin!"))
        {
            int nRet;
            if (int.TryParse(taID, out nRet))
            {
                GlobalVariables.pID = nRet;
                GlobalVariables.sesID = int.Parse(sesID);


                // error, 0 = training, int = experiment scene
                if (!LoadTrials(GlobalVariables.pID, GlobalVariables.sesID))
                    Debug.LogError(string.Format("Task file Load Failed : {0}.txt!", taID));
                else if (GlobalVariables.pID == 0)
                    // SceneManager.LoadScene(2);
                    print("Placeholder for loading training scene with id 0");
                else
                {
                    GlobalVariables.activeTrial = GlobalVariables.conTrials[0];
                    SceneManager.LoadScene(GlobalVariables.conEmb == "emb" ? "EmbodiedScene" : "NonEmbodiedScene");
                }
            }
        }
    }

    public void Shuffle(List<Trial> a)
    {
        for (int i = 0; i < a.Count - 1; i++)
        {
            int rnd = UnityEngine.Random.Range(i, a.Count);
            Trial temp = a[rnd];
            a[rnd] = a[i];
            a[i] = temp;
        }
    }


    private bool LoadTrials(int pid, int sesid)
    {

        StreamReader sr = null;
        string line = "";
        // setup global conditions
        switch (sesid)
        {
            case 1:
                GlobalVariables.conEmb = "non";
                GlobalVariables.conD = 2;
                GlobalVariables.conCog = 1;
                break;
            case 2:
                GlobalVariables.conEmb = "non";
                GlobalVariables.conD = 3;
                GlobalVariables.conCog = 1;
                break;
            case 3:
                GlobalVariables.conEmb = "non";
                GlobalVariables.conD = 2;
                GlobalVariables.conCog = 2;
                break;
            case 4:
                GlobalVariables.conEmb = "non";
                GlobalVariables.conD = 3;
                GlobalVariables.conCog = 2;
                break;
            case 5:
                GlobalVariables.conEmb = "emb";
                GlobalVariables.conD = 2;
                GlobalVariables.conCog = 1;
                break;
            case 6:
                GlobalVariables.conEmb = "emb";
                GlobalVariables.conD = 3;
                GlobalVariables.conCog = 1;
                break;
            case 7:
                GlobalVariables.conEmb = "emb";
                GlobalVariables.conD = 2;
                GlobalVariables.conCog = 2;
                break;
            case 8:
                GlobalVariables.conEmb = "emb";
                GlobalVariables.conD = 3;
                GlobalVariables.conCog = 2;
                break;
        }

        // read in trials with expected values and randomize the list
        if (GlobalVariables.conD == 2)
        {
            int[] trialIDs = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            sr = new StreamReader(filePath + "2D.txt");
        }
        else if (GlobalVariables.conD == 3)
        {
            int[] trialIDs = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            sr = new StreamReader(filePath + "3D.txt");
        }
        line = sr.ReadLine();
        do
        {
            string[] trialSplit = line.Split(',');
            Trial t = new Trial(int.Parse(trialSplit[0]), trialSplit[2]);
            GlobalVariables.conTrials.Add(t);
            line = sr.ReadLine();
        }
        while (line != null);
        sr.Close();

        //Shuffle(GlobalVariables.conTrials);

        print(GlobalVariables.conTrials);

        if (sesid > 8) // temp cancel.. TODO improve on
            return false;

        return true;
    }

    private bool LoadTrialFiles(int pid, int sesid)
    {

        try
        {
            StreamReader sr = new StreamReader(filePath + pid + ".txt");

            using (sr)
            {
                string line = sr.ReadLine();
                if (line != null)
                {
                    do
                    {
                        string[] trialSplit = line.Split(',');



                        int conID = int.Parse(trialSplit[GlobalVariables.sesID]);
                        // setup global conditions
                        switch (conID)
                        {
                            case 1:
                                GlobalVariables.conEmb = "non";
                                GlobalVariables.conD = 2;
                                GlobalVariables.conCog = 1;
                                break;
                            case 2:
                                GlobalVariables.conEmb = "non";
                                GlobalVariables.conD = 3;
                                GlobalVariables.conCog = 1;
                                break;
                            case 3:
                                GlobalVariables.conEmb = "non";
                                GlobalVariables.conD = 2;
                                GlobalVariables.conCog = 2;
                                break;
                            case 4:
                                GlobalVariables.conEmb = "non";
                                GlobalVariables.conD = 3;
                                GlobalVariables.conCog = 2;
                                break;
                            case 5:
                                GlobalVariables.conEmb = "emb";
                                GlobalVariables.conD = 2;
                                GlobalVariables.conCog = 1;
                                break;
                            case 6:
                                GlobalVariables.conEmb = "emb";
                                GlobalVariables.conD = 3;
                                GlobalVariables.conCog = 1;
                                break;
                            case 7:
                                GlobalVariables.conEmb = "emb";
                                GlobalVariables.conD = 2;
                                GlobalVariables.conCog = 2;
                                break;
                            case 8:
                                GlobalVariables.conEmb = "emb";
                                GlobalVariables.conD = 3;
                                GlobalVariables.conCog = 2;
                                break;

                        }
                        line = sr.ReadLine();
                    }
                    while (line != null);


                }
                sr.Close();


                // read in trials with expected values and randomize the list
                if (GlobalVariables.conD == 2)
                {
                    int[] trialIDs = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                    sr = new StreamReader(filePath + "2D.txt");
                }
                else if (GlobalVariables.conD == 3)
                {
                    int[] trialIDs = { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                    sr = new StreamReader(filePath +"3D.txt");
                }
                line = sr.ReadLine();
                if (line != null)
                {
                    do
                    {
                        string[] trialSplit = line.Split(',');
                        Trial t = new Trial(int.Parse(trialSplit[0]), trialSplit[2]);
                        GlobalVariables.conTrials.Add(t);
                        line = sr.ReadLine();
                    }
                    while (line != null);
                }
                sr.Close();
            }

            print(GlobalVariables.conTrials.Count);


            Shuffle(GlobalVariables.conTrials);

            print(GlobalVariables.conTrials);




            return true;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }
    }
}
