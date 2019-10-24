using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScenes : MonoBehaviour
{
    public const int SPLASH_SCENE = 2;
    public const int MAIN_SCENE = 0;
    public const int QUIZ_SCENE = 1;
    
    private SaveLoadManager saveLoadManager;
    private UIPopupQuestionChangeScene popupChangeScene;
    
    private string tempPath;

    private void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        popupChangeScene = FindObjectOfType<UIPopupQuestionChangeScene>();
        tempPath = Application.dataPath + "/tmp.json";
    }

    public void LoadQuizScene()
    {
        saveLoadManager.SaveTempScene();
        SceneManager.LoadSceneAsync(QUIZ_SCENE);
    }

    public void LoadMainScene()
    {
        popupChangeScene.MostrarPopUp("Atención!", "¿Seguro que desea cancelar el exámen y volver al espacio de trabajo?", MAIN_SCENE);
    }

    public void ForceLoadMainScene()
    {
        SceneManager.LoadSceneAsync(MAIN_SCENE);
    }

    private void OnApplicationQuit()
    {
        if (!File.Exists(tempPath)) return;
        File.Delete(tempPath);
    }
}
