using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextWindowManager : MonoBehaviour
{
    [Header("TextComponents")]
    [SerializeField] Text TextName;
    [SerializeField] Text TextBox;

    [Header("表示中のテキスト")]
    public float delay = 0.1f; // 1文字表示するのにかかる秒数
    public string fullText;
    private string currentText = "";

    [Header("テキスト")]
    [TextArea(3,20)]
    [SerializeField] string AllText;

    [SerializeField] string[] TextMessage;

    int TextIndex = 0;

    [SerializeField] AudioClip[] ScriptAudio;
    int AudioIndex = 0;

    CanvasGroup _CanvasGroup;
    [SerializeField] float duration = 0.75f;
    [SerializeField] AnimationCurve AnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField] Color[] NameColor;

    [SerializeField] GameObject MessageReceiver;

    // Start is called before the first frame update
    void Start()
    {
        TextBox = TextBox.GetComponent<Text>();
        TextName = TextName.GetComponent<Text>();
        TextMessage = AllText.Split('\n');
        UpdateButton.onClick.AddListener(() => Trigger());
        _CanvasGroup = GetComponent<CanvasGroup>();
    }

    IEnumerator TextUpdate()
    {
        UpdateButton.interactable = false;

        //名前


        //本文
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            TextBox.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        UpdateButton.interactable = true;

    }

    [SerializeField] Button UpdateButton;
    public void Trigger()
    {
        StartCoroutine(TextUpdate());
        

        if (TextMessage[TextIndex].Contains('{'))
        {
            if (TextMessage[TextIndex].Contains('}'))
            {
                //地の文
                TextName.gameObject.transform.parent.gameObject.SetActive(false);
            }
            else if(TextMessage[TextIndex].Contains('/'))
            {
                //message送信
                string message = TextMessage[TextIndex].Substring(2);
                Debug.Log(message);

                MessageReceiver.SendMessage(message);
            }
            else
            {
                //名前の変更
                TextName.gameObject.transform.parent.gameObject.SetActive(true);
                TextName.text = TextMessage[TextIndex].Substring(2);

                Image col = TextName.gameObject.transform.parent.gameObject.GetComponent<Image>();
                int col_n = int.Parse(TextMessage[TextIndex].Substring(1,1));
                col.color = NameColor[col_n];
            }

            TextIndex++;
        }

        if (AudioIndex < ScriptAudio.Length)
        {
            if (ScriptAudio[AudioIndex] != null)
            {
                //台詞音声の再生
                AudioSource source = GetComponent<AudioSource>();
                if (source.isPlaying)
                {
                    source.Stop();
                }
                source.PlayOneShot(ScriptAudio[AudioIndex]);
            }
            AudioIndex++;
        }

        fullText = TextMessage[TextIndex];
        TextIndex++;
        if (TextIndex >= TextMessage.Length)
        {
            //ウィンドウ終了処理
            TextEnd();
        }
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        TextIndex = 0;
        AudioIndex = 0;

        TextBox.text = null;
        TextName.gameObject.transform.parent.gameObject.SetActive(false);
        DOVirtual.Float(0f, 1f, duration, value => { _CanvasGroup.alpha = GetCurveValue(value); })
            .OnComplete(() => {
                Trigger();
            });
        transform.localScale = Vector3.zero;
        this.transform.DOScale(new Vector3(1, 1, 1), duration);
    }

    void TextEnd()
    {
        DOVirtual.Float(1f, 0f, duration, value => { _CanvasGroup.alpha = GetCurveValue(value); })
            .OnComplete(() => {
                this.gameObject.SetActive(false);
            });
        this.transform.DOScale(Vector3.zero, duration);
    }

    public float GetCurveValue(float time)
    {
        return AnimationCurve.Evaluate(time);
    }
    
    public void Increment()
    {
        TextIndex++;
    }
}
