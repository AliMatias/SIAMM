using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GUIButton : MonoBehaviour
{
    public bool debugMode = false;

    public Texture2D buttonImage = null;
    public Rect position = new Rect(15,15,150,150);

    public GUISkin skin = null;
    public string title = string.Empty;

    private void OnGUI()
    {
        GUI.skin = skin;

        if (debugMode)
        {
            if (GUI.Button(position, buttonImage))
                transform.position += new Vector3(0, 0.5f, 0);
        }

    }
}
