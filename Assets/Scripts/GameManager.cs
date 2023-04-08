using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public void ChangeToStart()
	{
		SceneManager.LoadScene("TestScene");
	}

	public void GameExit()
	{
		Application.Quit();
	}
}
