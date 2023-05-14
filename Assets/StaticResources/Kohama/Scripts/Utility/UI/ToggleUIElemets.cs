using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIElemets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isToggle = false;

    public void ToggleElements()
    {
        if (isToggle)
        {
            this.gameObject.SetActive(false);
            isToggle = false;
        }
        else
        {
            this.gameObject.SetActive(true);
            isToggle = true;
        }
    }
}
