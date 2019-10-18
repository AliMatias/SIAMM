using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public const int SPLASH_SCENE = 0;
    public const int MAIN_SCENE = 1;
    public const int QUIZ_SCENE = 2;

    private SaveLoadManager saveLoadManager;
    private string tempPath;

    private void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        tempPath = Application.dataPath + "/tmp.json";
    }

    public void loadQuizScene()
    {
        saveLoadManager.SaveTempScene();
        SceneManager.LoadSceneAsync(QUIZ_SCENE);
    }

    public void loadMainScene()
    {
        SceneManager.LoadSceneAsync(MAIN_SCENE);
    }

    private void OnApplicationQuit()
    {
        if (!File.Exists(tempPath)) return;
        File.Delete(tempPath);
    }
}
