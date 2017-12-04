using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject prefabBackgroundSprite;

    public GameObject panelMainMenu, panelCredits, panelOptions;

    public ToggleGroup toggleGroupMovementType;
    public Toggle toggleWASD, toggleTowardsMouse;

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

    public void OptionsGoBack()
    {
        panelOptions.SetActive(false);
        panelMainMenu.SetActive(true);

        Helpers.SetOptionsMovementType(toggleTowardsMouse.isOn ? Helpers.MovementType.TowardsMouse : Helpers.MovementType.WASD);
    }

    public void Options()
    {
        panelOptions.SetActive(true);
        panelMainMenu.SetActive(false);

        var movementType = Helpers.GetOptionsMovementType();

        toggleGroupMovementType.SetAllTogglesOff();
        if (movementType == Helpers.MovementType.TowardsMouse)
            toggleTowardsMouse.isOn = true;
        else
            toggleWASD.isOn = true;
    }


}
