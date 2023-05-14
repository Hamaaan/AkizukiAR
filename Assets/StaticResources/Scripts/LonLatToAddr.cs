using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;

public class LonLatToAddr : MonoBehaviour
{
    [SerializeField] private string appid; // Yahoo! JapanのAPIキー
    public float latitude; // 緯度
    public float longitude; // 経度
    [SerializeField] string GeoPos;

    private const string ApiBaseUrl = "https://map.yahooapis.jp/geoapi/V1/reverseGeoCoder?lat={0}&lon={1}&appid={2}";

    private string _address;

    /// <summary>住所文字列</summary>
    public string Address { get; private set; }
    private void Start()
    {
        /*
        GeoPosConvertFloat();
        StartCoroutine("GetAddressFromLatLong");
        */
    }

    void GeoPosConvertFloat()
    {
        string[] arr = GeoPos.Split(',');

        latitude = float.Parse(arr[0]);
        longitude = float.Parse(arr[1]);
    }

    public IEnumerator GetAddressFromLatLong()
    {
        // API URLにパラメータを設定
        string url = string.Format(ApiBaseUrl, latitude, longitude, appid);
        Debug.Log(url);
        // UnityWebRequestを使用してAPIリクエストを送信
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            // レスポンスから住所情報を取得
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string json = webRequest.downloadHandler.text;

                // XMLドキュメントの読み込み
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(webRequest.downloadHandler.text);

                // 住所文字列を抽出
                XmlNodeList addressList = xmlDoc.GetElementsByTagName("Address");
                if (addressList.Count > 0)
                {
                    Address = addressList[0].InnerText;
                    Debug.Log("ADRESS :" + Address);
                }
            }
            else
            {
                Debug.LogError("Failed to fetch address from " + url + ". Error: " + webRequest.error);
            }

            

        }
    }
}
