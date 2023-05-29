using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("GPS?????W???????????????E???l")]
    [SerializeField] double top;
    [SerializeField] double bottom;
    [SerializeField] double left;
    [SerializeField] double right;
    [SerializeField] double height;
    [SerializeField] double width;

    /*
    [SerializeField] float imagePixel_height;
    [SerializeField] float imagePixel_width;
    */

    [SerializeField] Image Map;
    [SerializeField] Image Cursor;

    [Header("??????GPS???W(public)")]
    public double currentLatitude;
    public double currentLongitude;

    //[Header("UI?????J?[?\???????W")]
    [SerializeField] float map_x;
    [SerializeField] float map_y;


    // Start is called before the first frame update
    void Start()
    {
        // GPS ??????
        Input.location.Start();
        Input.compass.enabled = true;

        //
        currentLongitude = (right - left) / 2 + left;
        currentLatitude = (top - bottom) / 2 + bottom;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo = Input.location.lastData;
            currentLatitude = locationInfo.latitude; //???x
            currentLongitude = locationInfo.longitude; //?o?x
        }

        MappingCalculation(currentLongitude, currentLatitude);
    }

    void MappingCalculation(double x, double y)
    {
        double dx = right - x; //?????????{
        width = right - left;
        double wx = dx / width;
        //???????????????Y?????????????p?[?Z???g???\??

        map_x = Map.rectTransform.rect.xMax - (float)(wx * Map.rectTransform.rect.width);

        double dy = top - y;
        height = top - bottom; //
        double wy = dy / height;

        map_y = Map.rectTransform.rect.yMax - (float)(wy * Map.rectTransform.rect.height);

        Cursor.rectTransform.anchoredPosition = new Vector2(map_x, map_y);
    }
}
