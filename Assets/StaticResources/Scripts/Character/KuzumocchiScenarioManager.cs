using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuzumocchiScenarioManager : MonoBehaviour
{
    [SerializeField] GameObject Mokkun;
    [SerializeField] GameObject TextBox3;

    [SerializeField] bool Flag3 = false;



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
        if (!Mokkun)
        {
            if (!Flag3)
            {
                TextBox3.SetActive(true);
                Flag3 = true;
            }
        }
    }

    public void TriggerExit()
    {
        GetComponent<Animator>().SetTrigger("Exit");
    }

}
