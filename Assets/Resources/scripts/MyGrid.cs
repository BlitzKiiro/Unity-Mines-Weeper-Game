using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public GameObject tile;
    public static GameObject[,] grid;
    public static HashSet<KeyValuePair<int, int>> mines = new HashSet<KeyValuePair<int, int>> ();
    public int minesnum;
    public int rows, cols;
    public float startx, starty;
    public float spacex, spacey;
    private void Start()
    {
        Manager.minesnum = this.minesnum;
        //creating tiles grid
        grid = new GameObject[rows, cols];
        Manager.unrevealed = grid.Length;
        for ( int i = 0; i < rows; i++)
        {
            for ( int j = 0; j < cols ; j++)
            {
                grid[i, j] = Instantiate(tile, new Vector3(startx + j * spacex, starty), Quaternion.identity);
                grid[i, j].GetComponent<tile_click>().coordinates = new KeyValuePair<int, int>(i, j);
                
            }
            starty -= spacey;
        }

        //filling tiles
        KeyValuePair<int, int> tmp;
        while ( mines.Count != minesnum )
        {
            tmp = new KeyValuePair<int, int>(Random.Range(0,rows),Random.Range(0,cols));
            mines.Add(tmp);
        }
       foreach (KeyValuePair<int, int> i in mines )
       {
            grid[i.Key, i.Value].GetComponent<tile_click>().content = 9;
            label_neighbours(i.Key, i.Value);
       }

    }
    void label_neighbours(int x, int y)
    {
        x = x - 1;
        y = y - 1;
        for ( int i = x ; i < x+3; i++ )
        {
            if (i < 0 || i >= rows) continue;
            for ( int j = y; j < y + 3; j++)
            {
                if (  j < 0 || j >= cols ) continue;
                if (grid[i, j].GetComponent<tile_click>().content != 9)
                    grid[i, j].GetComponent<tile_click>().content++;
            }
        }
    }

    public static void check_neighbours(int x, int y)
    {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1)) return;
        if (grid[x, y].GetComponent<tile_click>().isRevealed == true) return;
        Destroy(grid[x, y].GetComponent<Transform>().GetChild(0).gameObject);
        grid[x, y].GetComponent<tile_click>().isRevealed = true;
        Manager.unrevealed--;
        if (grid[x, y].GetComponent<tile_click>().content != 0) return;
        check_neighbours(x + 1, y);
        check_neighbours(x - 1, y);
        check_neighbours(x, y + 1);
        check_neighbours(x, y - 1);

    }
   
}

