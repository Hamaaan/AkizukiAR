using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurodaScenarioManager : MonoBehaviour
{
    [SerializeField] GameObject TextBox1;
    [SerializeField] bool Flag1 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void GetTouch()
    {
        if (!Flag1)
        {
            TextBox1.SetActive(true);
            Flag1 = true;
        }
    }
}
