using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// 싱글톤 패턴을 사용하기 위한 인스턴스 변수
	private static GameManager mInstance = null;
	// 인스턴스에 접근하기 위한 프로퍼티
	public static GameManager Instance
	{
		get {
			// 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
			if(!mInstance)
			{
				mInstance = FindObjectOfType(typeof(GameManager)) as GameManager;

				if ( !mInstance )
				{
					Debug.Log("<NO SINGLETON OBJECT>");
				}
			}
			return mInstance;
		}
	}

	private void Awake()
	{
		if (mInstance == null)
		{
			mInstance = this;
		}
		// 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
		else if (mInstance != this)
		{
			Destroy(gameObject);
		}
		// 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
		DontDestroyOnLoad(gameObject);
	}
	public void ChangeToStart()
	{
		SceneManager.LoadScene("TestScene");
	}

	public void GameExit()
	{
		Application.Quit();
	}
}
