namespace Google.XR.ARCoreExtensions.Samples.Geospatial
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Google.XR.ARCoreExtensions;
    using UnityEngine.UI;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;

    public class GeoObjectPlacer : MonoBehaviour
    {
        [SerializeField] GeospatialController Geospatial;
        ARCoreExtensions ARCoreExtensions;
        AREarthManager EarthManager;
        ARAnchorManager AnchorManager;
        [SerializeField] GameObject ObjectPrefab;

        [SerializeField] double Latitude;
        [SerializeField] double Longitude;
        [SerializeField] double Altitude;
        [SerializeField] Quaternion EunRotation;

        // Start is called before the first frame update
        void Start()
        {
            Geospatial = Geospatial.GetComponent<GeospatialController>();
            EarthManager = Geospatial.EarthManager;

            AddAnchor();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddAnchor()
        {
            GeospatialAnchorHistory history = new GeospatialAnchorHistory(
                    Latitude, //Latitude
                    Longitude, //Longitude
                    Altitude, //Altitude
                    EunRotation //EunRotation  ... これよくわからんのでとりあえず0にする
                );
            var anchor = PlaceGeospatialAnchor(history);
        }

        ARGeospatialAnchor PlaceGeospatialAnchor(
            GeospatialAnchorHistory history)
        {
            Quaternion eunRotation = history.EunRotation;
            if (eunRotation == Quaternion.identity)
            {
                // This history is from a previous app version and EunRotation was not used.
                eunRotation = Quaternion.AngleAxis(180f - (float)history.Heading, Vector3.up);
            }

            var anchor = AnchorManager.AddAnchor(
                    history.Latitude, history.Longitude, history.Altitude, eunRotation);
            if (anchor != null)
            {
                GameObject anchorGO = Instantiate(ObjectPrefab, anchor.transform);
            }
            else
            {

            }

            return anchor;
        }
    }
}


