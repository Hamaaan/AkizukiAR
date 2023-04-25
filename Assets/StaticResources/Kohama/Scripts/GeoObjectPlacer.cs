namespace Google.XR.ARCoreExtensions.Samples.Geospatial
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Google.XR.ARCoreExtensions;

    public class GeoObjectPlacer : MonoBehaviour
    {
        [SerializeField] GeospatialController Geospatial;
        ARCoreExtensions ARCoreExtensions;

        // Start is called before the first frame update
        void Start()
        {
            Geospatial = Geospatial.GetComponent<GeospatialController>();
            ARCoreExtensions = Geospatial.ARCoreExtensions;

            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


