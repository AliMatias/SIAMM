// Scale your grid layougt in Unity3D
// -- Add this to a gameobject with a Grid Layout Group and you can make the cell size change with resolution, by percentage of screen width.
// -- Updates as you change resolution or width percentage in editor.
// -- Runs once on play, Fix will need to be called if resolution is changed in game.
// -- Script will need expansion if you want to use non-square cells.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScaleGrid : MonoBehaviour {

	[SerializeField][Range(0,1)] float widthPercentage;
	float lWidthPercentage = 0;
	Vector2 viewSize = Vector2.zero;

	void Start()
	{
		Fix ();
	}

	void Update()
	{
		#if UNITY_EDITOR
		//This is used to detect whether in editor view resolution has changed
		if(Application.isPlaying) return;
		if (GetMainGameViewSize() != viewSize || widthPercentage != lWidthPercentage)
		{
			Fix ();
			viewSize = GetMainGameViewSize ();
			lWidthPercentage = widthPercentage;
		}	
		#endif
	}
		
	public void Fix() 
	{
		GridLayoutGroup grid = GetComponent<GridLayoutGroup> ();
		var width = (float)GetMainGameViewSize().x;
		var val = (int)Mathf.Round(width * widthPercentage);
		grid.cellSize = new Vector2 (val, val);
		//Toggle enabled to update screen (is there a better way to do this?)
		grid.enabled = false;
		grid.enabled = true;
	}

	//Thanks to http://kirillmuzykov.com/unity-get-game-view-resolution/
	public static Vector2 GetMainGameViewSize()
	{
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetSizeOfMainGameView.Invoke(null,null);
		return (Vector2)Res;
	}
}
