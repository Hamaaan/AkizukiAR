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
    public LonLatToAddr LonLatToAddr;

    public string placename;
    public string lat;
    public string lon;
    public string address;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    private void OnEnable()
    {
        DisplayText = GetComponentInChildren<TextMeshPro>();
        LonLatToAddr = LonLatToAddr.GetComponent<LonLatToAddr>();
    }

    public void SetText()
    {
        DisplayText.text = placename + "\n";
        DisplayText.text += lat + "\n";
        DisplayText.text += lon + "\n";
        address = LonLatToAddr.Address;
        DisplayText.text += address + "\n";
        Debug.Log(address);
    }
}
