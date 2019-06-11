using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private ParticleSpawner spawner;
    private UIFader UIFader;
    public GameObject parent;
    private LoadTper loadTPer;

    private void Awake()
    {
        spawner = FindObjectOfType<ParticleSpawner>();
        loadTPer = FindObjectOfType<LoadTper>();
        UIFader = FindObjectOfType<UIFader>();
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
        loadTPer.LoadInfoBasica(text.text);
    }

}



