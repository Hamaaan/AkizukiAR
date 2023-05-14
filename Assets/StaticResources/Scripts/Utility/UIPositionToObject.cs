using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionToObject : MonoBehaviour
{
    private void Start()
    {

    }
    static public Vector3 UIPosToWorld(RectTransform rectTransform)
    {
        Vector3 ScreenPos = rectTransform.transform.position;
        ScreenPos.z = -Camera.main.transform.position.z;
        Vector3 vec = Camera.main.ScreenToWorldPoint(ScreenPos);
        return vec;
    }
}
