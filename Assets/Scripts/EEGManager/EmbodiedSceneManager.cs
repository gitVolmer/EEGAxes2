using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbodiedSceneManager : MonoBehaviour
{

    public List<SAxis> _allGraphs;
    public GameObject trialStartView;
    public float timeTaken;

    private bool _trialStarted;        
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
        _allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(_trialStarted)
            timeTaken += Time.deltaTime;

        if(_trialStarted != true && OVRInput.Get(OVRInput.Button.Four))
        {
            _trialStarted = true;
            trialStartView.gameObject.SetActive(false);
        }
    }

    public void ResetScene()
    {
        _trialStarted = false;
        trialStartView.gameObject.SetActive(true);
        DestroyAllAxes();
        _allGraphs = ScatterPlotSceneManager.Instance.SpawnAllGraphs(1);
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
}
