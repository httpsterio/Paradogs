using UnityEngine;
using System.Collections;

public class HUDCanvasController : MonoBehaviour {

    GameObject pauseMenu;

	// Use this for initialization
	void Start () {
        pauseMenu = transform.FindChild("PauseMenu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause") == true)
        {

            pauseMenu.SetActive(!pauseMenu.activeSelf);


            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }
	}

    public void continueGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void exitToMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
