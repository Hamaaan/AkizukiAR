using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToUIPosition : MonoBehaviour
{
    public Transform objectToMove;
    public RectTransform uiElement;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        // オブジェクトのワールド座標をスクリーン座標に変換
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(objectToMove.position);

        // スクリーン座標をUIのローカル座標に変換
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiElement, screenPosition, mainCamera, out uiPosition);

        // UIの位置を変更
        uiElement.anchoredPosition = uiPosition;
    }
}
