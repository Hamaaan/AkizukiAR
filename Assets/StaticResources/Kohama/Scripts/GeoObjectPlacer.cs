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
        [SerializeField] GameObject GeoPosDisplayerPrefab;

        [SerializeField] string[] GeoPos;

        [SerializeField] string[] Place_Name;
        [SerializeField] double[] Latitude;
        [SerializeField] double[] Longitude;
        [SerializeField] double[] Altitude;
        [SerializeField] Quaternion[] EunRotation;


        // Start is called before the first frame update
        void Start()
        {
            Geospatial = Geospatial.GetComponent<GeospatialController>();
            EarthManager = Geospatial.EarthManager;
            GeoPosConvertDouble();
        }

        // Update is called once per frame
        void Update()
        {

        }



        /*  
         * kohama_house, 33.563112349410176, 130.43208554048962
         *
         *
         */


        void GeoPosConvertDouble()
        {
            //these should be <List>
            Place_Name = new string[GeoPos.Length];
            Latitude = new double[GeoPos.Length];
            Longitude = new double[GeoPos.Length];

            for (int i = 0; i < GeoPos.Length; i++)
            {
                string[] arr = GeoPos[i].Split(',');

                double lat_d = double.Parse(arr[arr.Length - 2]);
                double lon_d = double.Parse(arr[arr.Length - 1]);

                if (arr.Length == 3)
                {
                    Place_Name[i] = arr[0];
                    Debug.Log(arr[0] + " :" + lat_d + "/" + lon_d);

                }
                else
                {
                    Debug.Log("null :" + lat_d + "/" + lon_d);
                }


                Latitude[i] = lat_d;
                Longitude[i] = lon_d;
            }
        }


        public void AnchorsPlaced()
        {
            for (int i = 0; i < Latitude.Length; i++)
            {
                AddAnchor(Place_Name[i], Latitude[i], Longitude[i], 0, new Quaternion(0, 0, 0, 0));
                //  緯度と経度だけ入力
            }
        }

        //add reference to "Place_Name"
        public void AddAnchor(string Place_Name, double Latitude, double Longitude, double Altitude, Quaternion EunRotation)
        {
            GeospatialAnchorHistory history = new GeospatialAnchorHistory(
                    Latitude, //Latitude
                    Longitude, //Longitude
                    Altitude, //Altitude
                    EunRotation //EunRotation  ... これよくわからんのでとりあえず0にする
                );
            var anchor = PlaceGeospatialAnchor(Place_Name ,history);
            Debug.Log(history);
        }

        private ARGeospatialAnchor PlaceGeospatialAnchor(
            string Place_Name,
            GeospatialAnchorHistory history)
        {
            Quaternion eunRotation = history.EunRotation;
            if (eunRotation == Quaternion.identity)
            {
                // This history is from a previous app version and EunRotation was not used.
                eunRotation = Quaternion.AngleAxis(180f - (float)history.Heading, Vector3.up);
            }

            var anchor = AnchorManager.ResolveAnchorOnTerrain(
                    history.Latitude, history.Longitude, 0, eunRotation);
            if (anchor != null)
            {
                GameObject anchorGO = Instantiate(ObjectPrefab, anchor.transform);

                GameObject anchorText = Instantiate(GeoPosDisplayerPrefab, anchor.transform);
                GeoPosDisplay geoPos = anchorText.GetComponent<GeoPosDisplay>();

                geoPos.placename = Place_Name + "\n";
                geoPos.lat = history.Latitude.ToString() + "\n";
                geoPos.lon = history.Longitude.ToString();

                LonLatToAddr toAddr = anchorText.GetComponent<LonLatToAddr>();
                toAddr.latitude = (float)history.Latitude;
                toAddr.longitude = (float)history.Longitude;
                StartCoroutine(toAddr.GetAddressFromLatLong());

            }
            else
            {

            }

            return anchor;
        }
    }
}


