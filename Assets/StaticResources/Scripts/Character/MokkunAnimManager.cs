using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MokkunAnimManager : MonoBehaviour
{
    [SerializeField] AnimationCurve EaseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    Animator _animator;
    Vector3 prePos;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        prePos = this.transform.position;
        StartCoroutine("PositionUpdate");
    }


    // Update is called once per frame
    void Update()
    {
        float speed = Vector3.Distance(prePos, transform.position) / Time.deltaTime;

        _animator.SetFloat("IsWalking", speed);

        if(speed > 0.05f)
        {
            Vector3 dir = (transform.position - prePos).normalized + this.transform.position;
            this.transform.DOLookAt(dir, 1f);
        }

        prePos = this.transform.position;
    }

    List<Vector3> vector3s = new List<Vector3>();

    IEnumerator PositionUpdate()
    {
        float time = Random.Range(6, 12);
        
        //transform.DOLocalMove(new Vector3(x1, 0, z1), time).SetRelative().SetEase(EaseCurve);

        Vector3[] vector3 = new Vector3[3]; 
        for (int i = 0; i < 3; i++)
        {
            float x1 = Random.Range(-8, 8) * time / 12;
            float z1 = Random.Range(-8, 8) * time / 12;
            vector3[i] = new Vector3(x1, 0, z1);
        }
        transform.DOLocalPath(vector3, time, PathType.CatmullRom).SetRelative().SetEase(EaseCurve);

        yield return new WaitForSeconds(time + 1f);

        StartCoroutine("PositionUpdate");
    }

    public void IsIdle()
    {
        _animator.SetFloat("IdleState", 0);
    }
}
