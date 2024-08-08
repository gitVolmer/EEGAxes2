using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbodiedSceneManager : MonoBehaviour
{

    public List<SAxis> _allGraphs;
    public GameObject trialStartView;
    public float timeTaken;

    private bool _trialStarted = false;        
    private static EmbodiedSceneManager _instance;
    public static EmbodiedSceneManager Instance => _instance;

    
    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
    }

 
    // Start is called before the first frame update
    void Start()
    {
        // eeg send condition start trigger
        _allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(_trialStarted)
            timeTaken += Time.deltaTime;

        // start trial
        if(_trialStarted != true && OVRInput.Get(OVRInput.Button.Four))
        {
            TrialStart();
            //_allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
            //_trialStarted = true;
            //trialStartView.gameObject.SetActive(false);
            // #eeg send start condition
        }
        // trial ended
        //else if(!_trialStarted && !trialStartView.gameObject.active)
        //{
        //  //  TrialEnd();
        //}
    }

    private void TrialStart()
    {
        //eeg send start trial
        _trialStarted = true;
        trialStartView.gameObject.SetActive(false);
        print("TRIAL STARTED CHECK");
        EEGTriggerHandler.SendTrigger(21);

    }

    public void ResetScene()
    {
        ScatterPlotSceneManager.Instance.LoadData(GlobalVariables.activeTrial.fileID);
        _allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
        // _trialStarted = false;
        // trialStartView.gameObject.SetActive(true);
        timeTaken = 0;
    }

    private void DestroyAllAxes()
    {
        foreach (var axis in ScatterPlotSceneManager.Instance.sceneAxes)
        {
            Destroy(axis.gameObject);
        }
        // var allAxes = FindObjectsOfType<SAxis>();
        // foreach (var axis in allAxes)
        // {
        //     Destroy(axis.gameObject);
        // }
    }

    public void TrialEnd()
    {
        EEGTriggerHandler.SendTrigger(22);
        //_trialStarted = false;
        // eeg send trial end
        DestroyAllAxes();
        //  trialStartView.gameObject.SetActive(true);  // if we want the break screen appearing
        // read next dataset and spawn when UI is hiding
        timeTaken = 0;
        ScatterPlotSceneManager.Instance.LoadData(GlobalVariables.activeTrial.fileID);
        _allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
     

    }

    
}
