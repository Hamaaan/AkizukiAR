using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiSpriteImages : MonoBehaviour
{
    [SerializeField] Sprite[] Sprites;
    public int Index = 0;
    Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _image.sprite = Sprites[Index];
    }

    public void SetIndex(int a)
    {
        Index = a;
    }

    public void IncrementIndex()
    {
        Index++;
        if (Index >= Sprites.Length)
        {
            Index = 0;
        }
    }
}
