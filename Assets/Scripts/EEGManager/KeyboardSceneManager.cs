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
            if(_trialStarted)
                timeTaken += Time.deltaTime;

            if(_trialStarted != true && OVRInput.Get(OVRInput.Button.Four))
            {
                _trialStarted = true;
                trialStartView.gameObject.SetActive(false);
            }
        }

        private void PopulateAxisList()
        {
            //populate x
            for (int i = 0; i < _dataObject.Identifiers.Length; i++)
            {
                var toggle = Instantiate(axisListButton, xAxisList.transform);
                toggle.textObject.text = _dataObject.Identifiers[i];
                toggle.axisID = i;
                toggle.currentAxis = Axis.X;
                toggle.GetComponent<Toggle>().group = xAxisList.GetComponent<ToggleGroup>();
                if (i == 0) toggle.GetComponent<Toggle>().Select();
                // _listButtons.Add(toggle);
            }
            //populate y
            for (int i = 0; i < _dataObject.Identifiers.Length; i++)
            {
                var toggle = Instantiate(axisListButton, yAxisList.transform);
                toggle.textObject.text = _dataObject.Identifiers[i];
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

        public void ResetScene()
        {
            _trialStarted = false;
            trialStartView.gameObject.SetActive(true);
            UpdateGraph();
        }

        public void UpdateGraph()
        {
            switch (GlobalVariables.activeTrial.dimension)
            {
                case 0:
                    UpdateGraph(0, 1);
                    break;
                case 2:
                    UpdateGraph(0, 1);
                    break;
                case 3:
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
                currentZId = zId;
                currentYId = yId;
                _mainGraph = ScatterPlotSceneManager.Instance.SpawnGraph3D(xId, yId, zId, mainGraphLocation.transform.position, "");
                zAxisList.transform.parent.gameObject.SetActive(true);
                yAxisList.transform.parent.gameObject.SetActive(true);
            }else if (yId >= 0)
            {
                currentZId = -1;
                currentYId = yId;
                _mainGraph = ScatterPlotSceneManager.Instance.SpawnGraph(xId, yId, mainGraphLocation.transform.position, "");
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
