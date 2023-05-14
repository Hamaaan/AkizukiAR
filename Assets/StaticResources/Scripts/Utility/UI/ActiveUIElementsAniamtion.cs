using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActiveUIElementsAniamtion : MonoBehaviour
{
    [SerializeField] Button[] Button;
    [SerializeField] float duration = 1f;
    [SerializeField] Vector3 Pos;
    [SerializeField] AnimationCurve AnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool isToggle;

    // Start is called before the first frame update
    void Start()
    {
        if (Button != null)
        {
            for (int i = 0; i < Button.Length; i++)
            {
                Button[i].onClick.AddListener(() => Toggle());
            }
        }
    }

    public void Toggle()
    {
        if (isToggle)
        {
            isToggle = false;
            transform.DOLocalMove(new Vector3(0, 0, 0), duration).SetEase(AnimationCurve);
        }
        else
        {
            isToggle = true;
            transform.DOLocalMove(Pos, duration).SetEase(AnimationCurve);
        }
    }

}
