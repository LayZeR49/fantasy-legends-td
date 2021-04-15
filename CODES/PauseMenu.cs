using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;

	public string menuSceneName = "MainMenu";
	public WaveControl waveControl;
	public static bool KeysEnabled = true;

	public SceneFader sceneFader;


	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle ()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			KeysEnabled = false;
			waveControl.PauseMenuOpen();
		} else
		{
			KeysEnabled = true;
			waveControl.PauseMenuClose();
		}
	}

	public void Retry ()
	{
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}

}
