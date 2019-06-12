using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private ParticleSpawner spawner;
    private UIFader UIFader;
    public GameObject parent;
    private LoadTper loadTPer;
    private BasicInfoLoader BasicInfoLoader;

    private void Awake()
    {
        spawner = FindObjectOfType<ParticleSpawner>();
        loadTPer = FindObjectOfType<LoadTper>();
        UIFader = FindObjectOfType<UIFader>();
        BasicInfoLoader = FindObjectOfType<BasicInfoLoader>();
    }

    public void Spawn()
    {
        //no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace
        Text text = parent.GetComponentInChildren<Text>();
        spawner.SpawnFromPeriodicTable(text.text);
        UIFader.FadeInAndOut();
    }

    public void GetInfoBasic()
    {
        Text text = parent.GetComponentInChildren<Text>();
        ElementInfoBasic elementInfo = loadTPer.LoadInfoBasica(text.text);
        BasicInfoLoader.SetBasicInfo(elementInfo);
    }

}



