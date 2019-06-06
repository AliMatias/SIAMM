using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTper : MonoBehaviour
{

    public int rows = 4;
    public int columns = 4;
    private float buttonWidth;                                        //Change
    private float buttonHeight;                                        //Change
    public Button prefab;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform myRect = GetComponent<RectTransform>();        //Change
        buttonHeight = myRect.rect.height / (float)rows;            //Change
        buttonWidth = myRect.rect.width / (float)columns;            //Change
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(buttonWidth, buttonHeight);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                button = (Button)Instantiate(prefab);
                button.transform.SetParent(transform, false);        //Change
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
