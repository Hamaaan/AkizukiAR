using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapModeManager : MonoBehaviour
{
    [Header("イベントフラグ管理")]
    public List<String> EventNames = new List<string>();
    public List<bool>EventFlags = new List<bool>();

    [SerializeField] Text DisplatFlags;

    // Start is called before the first frame update
    void Start()
    {
        EventFlags.Clear() ;
        for (int i = 0; i < EventNames.Count; i++)
        {
            EventFlags.Add(false);
        }

        DisplatFlags = DisplatFlags.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText();

        
    }

    void DisplayText()
    {
        DisplatFlags.text = "";
        bool isAllTrue = true;
        for (int i = 0; i < EventNames.Count; i++)
        {
            DisplatFlags.text += EventNames[i] + " : ";
            if (EventFlags[i])
            {
                DisplatFlags.text += "イベント発生";
            }
            else
            {
                DisplatFlags.text += "未到達";
                isAllTrue = false;
            }
            DisplatFlags.text += "\n";
        }
        if (isAllTrue)
        {
            DisplatFlags.text += "+++++++++クリア!!!++++++++";
        }
    }
}
