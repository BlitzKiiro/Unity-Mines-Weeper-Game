using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Text FlagsNo;
    public Text CounterText;
    public static int minesnum;
    static float TimeCounter = 0;
    public static bool start = false;
    public static int unrevealed;
    public GameObject winimg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlagsNo.text = "0" + minesnum.ToString();
        TimeCounter += Time.deltaTime;
        CounterText.text = ((int)TimeCounter).ToString();
        if ( unrevealed == minesnum)
        {
            win();
        }
    }
    public void RevealAll()
    {
        foreach (GameObject e in MyGrid.grid)
        {
            if ( e.GetComponent<tile_click>().isRevealed != true )
            {
                e.transform.GetChild(0).gameObject.SetActive(false);
            }
            
        }
    }
    public void HideAll()
    {
        foreach (GameObject e in MyGrid.grid)
        {
            if (e.GetComponent<tile_click>().isRevealed != true)
            {
                e.transform.GetChild(0).gameObject.SetActive(true); 
            }
        }
    }
   public void NewGame()
   {
        SceneManager.LoadScene(0);
        TimeCounter = 0;
   }
   public static void GameOver()
   {
        foreach (KeyValuePair<int, int> mine in MyGrid.mines)
        {
            if (MyGrid.grid[mine.Key, mine.Value].GetComponent<tile_click>().isRevealed != true)
            {
                MyGrid.grid[mine.Key, mine.Value].GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
            }
        }
        foreach (GameObject e in MyGrid.grid)
        {
            if (e.GetComponent<tile_click>().isRevealed != true)
            {
                e.GetComponent<tile_click>().isRevealed = true;
            }
        }
        Time.timeScale = 0;
    }
    void win()
    {
        foreach (GameObject e in MyGrid.grid)
        {
            if (e.GetComponent<tile_click>().isRevealed != true)
            {
                e.GetComponent<tile_click>().isRevealed = true;
            }
        }
        Time.timeScale = 0;
        winimg.SetActive(true);
    }
}
