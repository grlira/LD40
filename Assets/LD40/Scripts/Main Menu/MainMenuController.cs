using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject prefabBackgroundSprite;

    public GameObject panelMainMenu, panelCredits;

	// Use this for initialization
	void Start () {
		
        for(var i=0;i<15;i++)
        {
            Instantiate(prefabBackgroundSprite);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        panelMainMenu.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void CreditsGoBack()
    {
        panelCredits.SetActive(false);
        panelMainMenu.SetActive(true);
    }
}
