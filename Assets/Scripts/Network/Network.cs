// Copyright 2023. Jiwon-Nam All right reserved.

using System;
using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
	[SerializeField]
	public string mAPIKey;
	
	[System.Serializable]
	public class LoginData
	{
		public string mId;
		public string mPwd;
	}

	[System.Serializable]
	public class MemberData
	{
		public int mRank;
		public string mNickName;
	}

	public MemberData mClient;

	private void Start()
	{
		StartCoroutine(GetRequest());
		Debug.Log(mClient.mRank);
		
		mClient.mNickName = "G1rmmr";
		mClient.mRank = 0;
		StartCoroutine(PostRequest());
	}

	IEnumerator GetRequest()
	{
		string url = "https://" + mAPIKey;
		UnityWebRequest request = UnityWebRequest.Get(url);
		
		yield return request.SendWebRequest();

		if (request.error == null)
		{
			string json = request.downloadHandler.text;
			mClient = JsonUtility.FromJson<MemberData>(json);
			
			Debug.Log(mClient.mNickName);
		}
		else
		{
			Debug.Log("ERROR!");
		}
	}
	
	IEnumerator PostRequest()
	{
		string url = "https://";
		string json = JsonUtility.ToJson(mClient);

		UnityWebRequest request = UnityWebRequest.Post(url, json);
		yield return request.SendWebRequest();

		if (request.error == null)
		{
			Debug.Log(request.downloadHandler.text);
		}
		else
		{
			Debug.Log("ERROR!");
		}
	}
}