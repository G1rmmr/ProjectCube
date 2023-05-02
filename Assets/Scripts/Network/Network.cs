// Copyright 2023. Jiwon-Nam All right reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
	[SerializeField]
	public string mAPIKey = "nam";

	[System.Serializable]
	public class DebugClients
	{
		public List<DebugData> player;
	}
	
	[System.Serializable]
	public class DebugData
	{
		public int id;
		public string name;
		public int money;
	}

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

	private void Start()
	{
		StartCoroutine(GetRequest());
		StartCoroutine(PostRequest());
	}
	
	IEnumerator GetRequest()
	{
		string url = "http://15.165.55.55:8080/test/findByName?name=" + mAPIKey;
		UnityWebRequest request = UnityWebRequest.Get(url);
		
		yield return request.SendWebRequest();

		if (request.error != null)
		{
			Debug.Log(request.error);
		}
		Debug.Log(request.downloadHandler.text);
	}
	
	IEnumerator PostRequest()
	{
		string url = "http://15.165.55.55:8080/test/mockList";

		var form = new WWWForm();
		form.AddField("pw", "admin");

		using (var request = UnityWebRequest.Post(url, form))
		{
			yield return request.SendWebRequest();
		
			if (request.error != null)
			{
				Debug.Log(request.error);
			}
			Debug.Log(request.downloadHandler.text); 
			
			DebugClients debugClients = JsonUtility.FromJson<DebugClients>(request.downloadHandler.text);
			
			foreach (var client in debugClients.player)
			{
				Debug.Log(client.id.ToString() + ' ' + client.name + ' ' + client.money);
			}
		}
	}
}