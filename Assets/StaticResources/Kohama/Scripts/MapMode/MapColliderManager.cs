using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColliderManager : MonoBehaviour
{
    [SerializeField] MapModeManager mapModeManager;
    // Start is called before the first frame update
    void Start()
    {
        mapModeManager = mapModeManager.GetComponent<MapModeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < mapModeManager.EventNames.Count; i++)
        {
            if(collision.name == mapModeManager.EventNames[i])
            {
                mapModeManager.EventFlags[i] = true;
                break;
            }
        }
    }
}
