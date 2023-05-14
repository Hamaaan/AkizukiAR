using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] string SceneName;

    [SerializeField] bool isAdditive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneName = sceneName;

        List<Scene> Scenes = new List<Scene>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scenes.Add(SceneManager.GetSceneAt(i));
        }

        /*
        for (int i = 1; i < Scenes.Count; i++)
        {
            SceneManager.UnloadSceneAsync(i);
        }
        */
        
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        else
        {
            if (!isAdditive)
            {
                SceneManager.LoadScene(SceneName);
            }
            else
            {
                SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
            }
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
