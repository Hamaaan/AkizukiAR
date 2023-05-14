using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public bool IsEnabled { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnabled)
        {
            // ƒJ[ƒ\ƒ‹‚ÌŒü‚«‚ğ‡‚í‚¹‚é
            this.transform.localEulerAngles = new Vector3(0, 0, 360 - Input.compass.trueHeading);
        }
    }
}
