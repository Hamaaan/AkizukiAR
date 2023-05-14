using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindowManager : MonoBehaviour
{
    [Header("表示中のテキスト")]
    public float delay = 0.1f; // 1文字表示するのにかかる秒数
    public string fullText;
    private string currentText = "";

    [Header("テキスト")]
    [TextArea(3,20)]
    [SerializeField] string AllText;

    [SerializeField] string[] TextMessage;

    int Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        TextMessage = AllText.Split('\n');
        UpdateButton.onClick.AddListener(() => Trigger());
    }

    IEnumerator TextUpdate()
    {
        UpdateButton.interactable = false;
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        UpdateButton.interactable = true;

    }

    [SerializeField] Button UpdateButton;
    public void Trigger()
    {
        StartCoroutine(TextUpdate());
        fullText = TextMessage[Index];
        Index++;
        if (Index >= TextMessage.Length)
        {
            Index = 0;
        }
    }

    public void Increment()
    {
        Index++;
    }
}
