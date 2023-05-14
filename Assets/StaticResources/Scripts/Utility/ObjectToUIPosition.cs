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
        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ɕϊ�
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(objectToMove.position);

        // �X�N���[�����W��UI�̃��[�J�����W�ɕϊ�
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiElement, screenPosition, mainCamera, out uiPosition);

        // UI�̈ʒu��ύX
        uiElement.anchoredPosition = uiPosition;
    }
}
