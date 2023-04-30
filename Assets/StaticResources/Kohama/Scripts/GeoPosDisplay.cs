using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class GeoPosDisplay : MonoBehaviour
{
    public TextMeshPro DisplayText;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        DisplayText = GetComponentInChildren<TextMeshPro>();

        DisplayText.text = "Latitude :" + "null" + "\n";
        DisplayText.text += "Longitude :" + "null";
    }

    public void SetText(string t)
    {
        DisplayText.text = t;
    }
}
