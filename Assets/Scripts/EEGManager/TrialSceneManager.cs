using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialSceneManager : MonoBehaviour
{
    public GameObject trialStartView;
    public float timeTaken;

    protected bool _trialStarted = false;
    private static TrialSceneManager _instance;
    public static TrialSceneManager Instance => _instance;


    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;
    }


    // Start is called before the first frame update
    //void Start()
    //{
    //    // eeg send condition start trigger
    //    _allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
    //}

    // Update is called once per frame
    void Update()
    {
        if (_trialStarted)
            timeTaken += Time.deltaTime;

        // start trial
        if (_trialStarted != true && OVRInput.Get(OVRInput.Button.Four))
            TrialStart();
    }

    private void TrialStart()
    {
        //eeg send start trial
        _trialStarted = true;
        trialStartView.gameObject.SetActive(false);
    }



    public virtual void TrialEnd()
    {
        _trialStarted = false;
        // eeg send trial end
        trialStartView.gameObject.SetActive(true);
        // read next dataset and spawn when UI is hiding
        timeTaken = 0;
        ScatterPlotSceneManager.Instance.LoadData(GlobalVariables.activeTrial.fileID);

    }
}
