using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurodaScenarioManager : MonoBehaviour
{
    [SerializeField] GameObject TextBox1;
    [SerializeField] GameObject TextBox2;

    [SerializeField] bool Flag1 = false;
    [SerializeField] bool Flag2 = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag1)
        {
            if (!Flag2)
            {
                if (!TextBox1.activeSelf)
                {
                    TriggerExit();
                    Flag2 = true;
                }
            }
        }
        
    }

    
    public void GetTouch()
    {
        if (!Flag1)
        {
            TextBox1.SetActive(true);
            Flag1 = true;
        }
    }

    public void TriggerExit()
    {
        GetComponent<Animator>().SetTrigger("Exit");
    }

    public void NextEvent()
    {
        TextBox2.SetActive(true);
        Destroy(this.gameObject);
    }
}
