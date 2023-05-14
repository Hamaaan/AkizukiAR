using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CanvasGroupAnimation : MonoBehaviour
{
    CanvasGroup _CanvasGroup;
    [SerializeField] AnimationCurve AnimationCurve = AnimationCurve.EaseInOut( 0, 0, 1, 1 );
    [SerializeField] float TimeLength = 1f;
    [SerializeField] float time = 0f;

    public bool isToggle = false;

    private void Awake()
    {
        _CanvasGroup = GetComponent<CanvasGroup>();
        if (isToggle)
        {
            _CanvasGroup.blocksRaycasts = true;
            _CanvasGroup.interactable = true;
        }
        else
        {
            _CanvasGroup.blocksRaycasts = false;
            _CanvasGroup.interactable = false;
        }
    }

    /// <summary>
    /// アニメーションカーブから値取得
    /// </summary>
    public float GetCurveValue(float time)
    {
        return AnimationCurve.Evaluate(time);
    }

    public void ToggleElements()
    {
        if (isToggle)
        {
            DOVirtual.Float(1f, 0f, TimeLength, value =>
            {
                _CanvasGroup.alpha = GetCurveValue(value);
            }
                );
            
            _CanvasGroup.blocksRaycasts = false;
            _CanvasGroup.interactable = false;

            isToggle = false;
        }
        else
        {
            DOVirtual.Float(0f, 1f, TimeLength, value =>
                {
                    _CanvasGroup.alpha = GetCurveValue(value);
                }
                );

            _CanvasGroup.blocksRaycasts = true;
            _CanvasGroup.interactable = true;

            isToggle = true;
        }
    }
}
