using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	
	void Update()
	{
		float delta = Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.W))
		{	
			transform.Translate(0, 60, 60);
			transform.Rotate(90, 0, 0);
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.Translate(0, -60, 60);
			transform.Rotate(-90, 0, 0);
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			transform.Translate(60, 0, 60);
			transform.Rotate(0, -90, 0);
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.Translate(-60, 0, 60);
			transform.Rotate(0, 90, 0);
		}
	}
}