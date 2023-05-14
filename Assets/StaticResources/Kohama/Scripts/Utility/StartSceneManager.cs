using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] string IntroScene;
    [SerializeField] string HomeScene;

    [Header("飛ぶ先のシーン")]
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        /*
        int isIntro = PlayerPrefs.GetInt("isIntro", 1);
        if (isIntro == 1)
        {
            sceneName = IntroScene;
            PlayerPrefs.SetInt("isIntro", 0);
            PlayerPrefs.Save();
        }
        else
        {
            sceneName = HomeScene;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChangeTo()
    {
        SceneManager.LoadScene(sceneName);
    }
}
