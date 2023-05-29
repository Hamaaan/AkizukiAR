using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchARObject : MonoBehaviour
{
    public TrackableType type;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "ARObjects")
                {
                    hit.collider.gameObject.SendMessage("GetTouch");
                }
            }

            if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.AllTypes))
            {
                Debug.Log(hitResults[0].pose.position);
            }
        }


    }
}
