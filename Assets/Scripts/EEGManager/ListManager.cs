using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{

   
    // Start is called before the first frame update
    void Start()
    {
        GameObject dataButton = transform.GetChild(0).gameObject;
        GameObject g;
        foreach (SAxis ax in ScatterPlotSceneManager.Instance.sceneAxes)
        {
            g = Instantiate(dataButton, transform);
            string dataText = ax.name.Substring(5);
           
            g.transform.GetChild(0).GetComponent<Text>().text = dataText;
            g.GetComponent<Button>().onClick.AddListener(() => DataClicked(dataText) );
        }

        Destroy(dataButton);
    }


    void DataClicked(string ax)
    {
        print(ax);
    }
}
