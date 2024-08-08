using System;
using System.Collections.Generic;
using DataBinding;
using UnityEngine;
using UnityEngine.UI;

namespace EEGManager
{
    public class KeyboardSceneManager : MonoBehaviour
    {
        
        public enum Axis{X, Y, Z}
        
        public GameObject mainGraphLocation;
        public GameObject xAxisList;
        public GameObject yAxisList;
        public GameObject zAxisList;
        public AxisListToggle axisListButton;
        public GameObject trialStartView;
        public float timeTaken;

        public int currentXId;
        public int currentYId;
        public int currentZId;

        private List<SAxis> _mainGraph;
        private DataObject _dataObject;
        private bool _trialStarted;
        
        
        
        private static KeyboardSceneManager _instance;
        public static KeyboardSceneManager Instance => _instance;


        private void Awake()
        {
            if(_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _dataObject = ScatterPlotSceneManager.Instance.dataObject;
            PopulateAxisList();
            UpdateGraph();
        }

        private void Update()
        {
            if (_trialStarted)
                timeTaken += Time.deltaTime;

            // start trial
            if (_trialStarted != true && OVRInput.Get(OVRInput.Button.Four))
                TrialStart();
           

            if(_trialStarted != true && OVRInput.Get(OVRInput.Button.Four))
            {
                _trialStarted = true;
                trialStartView.gameObject.SetActive(false);
                _dataObject = ScatterPlotSceneManager.Instance.dataObject;
                PopulateAxisList();
                UpdateGraph();

                // #eeg send start condition
            }

            if (OVRInput.Get(OVRInput.Button.Right))
            {
                print("RIGHT SIDE");
            }
            if (OVRInput.Get(OVRInput.Button.Left))
            {
                print("LEFT");
            }
            if (OVRInput.Get(OVRInput.Button.Two))
            {
                print("two");

            }
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
            {
                ScatterPlotSceneManager.Instance.gameObject.transform.position  += ScatterPlotSceneManager.Instance.transform.forward * Time.deltaTime;

            }
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {
                ScatterPlotSceneManager.Instance.gameObject.transform.position -= ScatterPlotSceneManager.Instance.transform.forward * Time.deltaTime;
            }

        }

        private void TrialStart()
        {
            //eeg send start trial
            _trialStarted = true;
            trialStartView.gameObject.SetActive(false);
            EEGTriggerHandler.SendTrigger(11);
        }

        public void TrialEnd()
        {
            EEGTriggerHandler.SendTrigger(12);
            //_trialStarted = false;
            // trialStartView.gameObject.SetActive(true); // If we want 
            // eeg send trial end trigger

            ScatterPlotSceneManager.Instance.LoadData(GlobalVariables.activeTrial.fileID);
            _dataObject = ScatterPlotSceneManager.Instance.dataObject;
            // clear the tables and spawn the new axis lists while user is in trial menu
            ClearAxisList(xAxisList);
            ClearAxisList(yAxisList);
            ClearAxisList(zAxisList);
            timeTaken = 0;
            PopulateAxisList();
            UpdateGraph();
           
        }

        private void PopulateNone(GameObject axis)
        {
            var toggle = Instantiate(axisListButton, axis.transform);
            toggle.textObject.text = "None";
            toggle.axisID = -1;
            if (axis.name.Contains("Z"))
                toggle.currentAxis = Axis.Z;
            else if (axis.name.Contains('Y'))
                toggle.currentAxis = Axis.Y;
            else
                toggle.currentAxis = Axis.X;
            toggle.GetComponent<Toggle>().Select();
            toggle.GetComponent<Toggle>().group = axis.GetComponent<ToggleGroup>();
            //toggle.currentAxis = ;
            //toggle.GetComponent<Toggle>().group = xAxisList.GetComponent<ToggleGroup>();
            //if (i == 0) toggle.GetComponent<Toggle>().Select();
        }

        private void PopulateAxisList()
        {
            // COME BACK TO
            //PopulateNone(xAxisList); PopulateNone(yAxisList); PopulateNone(zAxisList);
            //populate x
            for (int i = 0; i < _dataObject.Identifiers.Length; i++)
            {
                var toggle = Instantiate(axisListButton, xAxisList.transform);
                
                toggle.textObject.text =  _dataObject.Identifiers[i];
                toggle.axisID = i;
                toggle.currentAxis = Axis.X;
                toggle.GetComponent<Toggle>().group = xAxisList.GetComponent<ToggleGroup>();
                // if (i == 0) toggle.GetComponent<Toggle>().Select();
                // _listButtons.Add(toggle);
            }
            //populate y
            for (int i = 0; i < _dataObject.Identifiers.Length; i++)
            {
                var toggle = Instantiate(axisListButton, yAxisList.transform);
                toggle.textObject.text =  _dataObject.Identifiers[i];
                toggle.axisID = i;
                toggle.currentAxis = Axis.Y;
                toggle.GetComponent<Toggle>().group = yAxisList.GetComponent<ToggleGroup>();
                // if (i == 0) toggle.GetComponent<Toggle>().isOn = true;
                // _listButtons.Add(toggle);
            }
            
            for (int i = 0; i < _dataObject.Identifiers.Length; i++)
            {
                var toggle = Instantiate(axisListButton, zAxisList.transform);
                toggle.textObject.text = _dataObject.Identifiers[i];
                toggle.axisID = i;
                toggle.currentAxis = Axis.Z;
                toggle.GetComponent<Toggle>().group = zAxisList.GetComponent<ToggleGroup>();
                // if (i == 0) toggle.GetComponent<Toggle>().isOn = true;
                // _listButtons.Add(toggle);
            }
            zAxisList.transform.parent.gameObject.SetActive(false);
        }


        public void HighlightAxis(Axis axis)
        {
            foreach (var a in _mainGraph)
            {
                a.highlightedObject.SetActive(a.axis == axis);
            }
        }

        public void ClearAxisList(GameObject axisList)
        {
            foreach (Transform childAxis in axisList.transform)
            {
                Destroy(childAxis.gameObject);
            }
        }


        public void UpdateGraph()
        {
            switch (GlobalVariables.conD)
            {
                case 0:
                    UpdateGraph(0, 1);
                    break;
                case 2:
                    UpdateGraph(0, 1);
                    break;
                case 3:
                    print("AXIS X: " + KeyboardSceneManager.Axis.X);
                    print("AXIS Y: " + KeyboardSceneManager.Axis.Y);
                    UpdateGraph(0, 1, 2);
                    break;
            }
        }

        public void UpdateGraph(int xId, int yId = -1, int zId = -1)
        {
            if (_mainGraph != null)
            {
                foreach (var axis in _mainGraph)
                {
                    Destroy(axis.gameObject);
                }
                _mainGraph = null;
            }
            currentXId = xId;
            
            if (zId >= 0)
            {
                currentZId = zId;  // y and x id reversed?
                currentYId = yId;
                print("YID: " + yId);
                _mainGraph = ScatterPlotSceneManager.Instance.SpawnGraph3D(yId, xId, zId, mainGraphLocation.transform.position, "", this.gameObject);
                zAxisList.transform.parent.gameObject.SetActive(true);
                yAxisList.transform.parent.gameObject.SetActive(true);
            }else if (yId >= 0)
            {
                currentZId = -1;
                currentYId = yId;
                _mainGraph = ScatterPlotSceneManager.Instance.SpawnGraph(yId, xId, mainGraphLocation.transform.position, "", this.gameObject);
                zAxisList.transform.parent.gameObject.SetActive(false);
                yAxisList.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                _mainGraph = new List<SAxis> { ScatterPlotSceneManager.Instance.GenerateAxis(xId, mainGraphLocation.transform.position, Quaternion.Euler(0, 0, 0), "", false) };
                zAxisList.transform.parent.gameObject.SetActive(false);
                yAxisList.transform.parent.gameObject.SetActive(false);
            }
        }
        
    }
}
