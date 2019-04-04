using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;

	public GameObject pauseButton;
	public GameObject resumeButton;

	public GameObject moveJoystick;
	public GameObject weaponJoystick;

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
		pauseButton.SetActive(true);
		resumeButton.SetActive(false);
		moveJoystick.SetActive(true);
		weaponJoystick.SetActive(true);
	}
	public void PauseGame()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
		pauseButton.SetActive(false);
		resumeButton.SetActive(true);
		moveJoystick.SetActive(false);
		weaponJoystick.SetActive(false);
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		gameIsPaused = false;
		SceneManager.LoadScene("Menu");
	}
	public void QuitMenu()
	{
		Application.Quit();
	}


}
