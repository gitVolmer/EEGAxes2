using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EEGManager
{
    public class AxisListToggle : MonoBehaviour
    {
        

        public KeyboardSceneManager.Axis currentAxis;
        public TextMeshProUGUI textObject;
        public int axisID = -1;
        private Toggle _toggle;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _toggle = GetComponent<Toggle>();
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }

        public void Highlight()
        {
            KeyboardSceneManager.Instance.HighlightAxis(currentAxis);
        }

        public void SetAxis()
        {
            switch (currentAxis)
            {
                case KeyboardSceneManager.Axis.X:
                    KeyboardSceneManager.Instance.UpdateGraph(axisID, KeyboardSceneManager.Instance.currentYId, KeyboardSceneManager.Instance.currentZId);
                    break;
                case KeyboardSceneManager.Axis.Y:
                    KeyboardSceneManager.Instance.UpdateGraph(KeyboardSceneManager.Instance.currentXId, axisID, KeyboardSceneManager.Instance.currentZId);
                    break;
                case KeyboardSceneManager.Axis.Z:
                    KeyboardSceneManager.Instance.UpdateGraph(KeyboardSceneManager.Instance.currentXId, KeyboardSceneManager.Instance.currentYId, axisID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
