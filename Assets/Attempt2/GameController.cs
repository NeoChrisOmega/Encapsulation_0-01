using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("Center");
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
            {
                StartGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
