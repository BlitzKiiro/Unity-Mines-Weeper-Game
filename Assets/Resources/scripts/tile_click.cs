using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_click : MonoBehaviour
{
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    public Sprite flag;
    public Sprite tile_top;
    public bool isRevealed = false;
    public int content;
    public KeyValuePair<int,int> coordinates;

    private void Update()
    {
        bottom.sprite = Resources.Load<Sprite>($"Images/{content}");
    }
    private void OnMouseOver()
    {
        if (!isRevealed)
        {
            if (Input.GetMouseButtonDown(0) && top.sprite != flag)
            {
                if (content == 0)
                {
                    MyGrid.check_neighbours(coordinates.Key, coordinates.Value);
                }
                else if ( content == 9)
                {
                    Manager.GameOver();
                }
                else
                {
                    Destroy(top);
                    isRevealed = true;
                    Manager.unrevealed--;
                }
               
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (top.sprite == tile_top && Manager.minesnum > 0)
                {
                    top.sprite = flag;
                    Manager.minesnum--;
                }
                else if ( top.sprite == flag)
                {
                    top.sprite = tile_top;
                    Manager.minesnum++;
                }

            }
        }
    }
}
