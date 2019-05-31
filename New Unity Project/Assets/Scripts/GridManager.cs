using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Declaraciones
    public float[,] grid;
    int vertical, horizontal, columns, rows;
    Camera camara;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        vertical = 5;//(int)Camera.main.orthographicSize;
        horizontal = vertical * (Screen.width / Screen.height); //tiene relacion con la resolucion.. atento porque quiza tenga que ser del panel...
        columns = horizontal * 2;
        rows = vertical * 2;
        grid = new float[columns, rows];
        GenerateGrid(columns,rows);
    }

    private void GenerateGrid (int col, int row)
    {
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                grid[i, j] = Random.Range(0.0f,1.0f);//hay que ver si aca no se puede poner el boton....
                Spaw(i,j, grid[i, j]);
            }
        }

    }


    private void Spaw(int a, int b, float value)
    {
        GameObject go = new GameObject("prueba:" + a + b);//genero un nuevo game objetc
        go.transform.position = new Vector3(a - (horizontal - 0.5f), b - (vertical - 0.5f));
        var s = go.AddComponent<SpriteRenderer>();
        s.color = new Color(a,b,value);
    }

}
