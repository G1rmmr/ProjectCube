// Copyright 2023. Jiwon-Nam All right reserved.

using UnityEngine;

namespace FieldScripts
{
	public class CubeBigRotate : MonoBehaviour
	{
		[SerializeField]
		public GameObject target;

		[SerializeField]
		public float speed = 200.0f;
	
		public Vector2 GetFirstPress(){ return mFirstPressPos; }
		public void SetFirstPress(Vector2 firstPress){ mFirstPressPos = firstPress; }
	
		public Vector2 GetSecondPress(){ return mSecondPressPos; }
		public void setSecondPress(Vector2 secondPress){ mSecondPressPos = secondPress; }

		public Vector2 GetCurrentSwipe(){ return mCurrentSwipe; }
		public void SetCurrentSwipe(Vector2 currentSwipe){ mCurrentSwipe = currentSwipe; }
	
		public Vector3 GetPreviousMousePos(){ return mPreviosMousePos; }
		public void SetPreviousMousePos(Vector3 preMousePos){ mPreviosMousePos = preMousePos; }
	
		public Vector3 GetMouseDelta(){ return mMouseDelta; }
		public void SetMouseDelta(Vector3 mouseDelta){ mMouseDelta = mouseDelta; }
	
		private Vector2 mFirstPressPos;
		private Vector2 mSecondPressPos;
		private Vector2 mCurrentSwipe;
		private Vector3 mPreviosMousePos;
		private Vector3 mMouseDelta;

		private void Update()
		{
			Swipe();
			Drag();
		}

		private void Drag()
		{
			if (Input.GetMouseButton(1))
			{
				mMouseDelta = Input.mousePosition - mPreviosMousePos;
				Vector3 dragVector = mMouseDelta.normalized;

				mMouseDelta *= 0.1f;
				
				if (LeftSwipe(dragVector) || RightSwipe(dragVector))
				{
					transform.rotation
						= Quaternion.Euler(0, -mMouseDelta.x, 0) * transform.rotation;
				}
				else if (UpLeftSwipe(dragVector) || DownRightSwipe(dragVector))
				{
					transform.rotation
						= Quaternion.Euler(mMouseDelta.y, 0, 0) * transform.rotation;
				}
				else if (UpRightSwipe(dragVector) || DownLeftSwipe(dragVector))
				{
					transform.rotation
						= Quaternion.Euler(0, 0, -mMouseDelta.y) * transform.rotation;
				}
			}
			else
			{
				if (transform.rotation != target.transform.rotation)
				{
					var step = speed * Time.deltaTime;
			
					transform.rotation = Quaternion.RotateTowards(
						transform.rotation, target.transform.rotation, step);
				}
			}
			mPreviosMousePos = Input.mousePosition;
		}

		private void Swipe()
		{
			if (Input.GetMouseButtonDown(1))
			{
				mFirstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}

			if (Input.GetMouseButtonUp(1))
			{
				mSecondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			
				mCurrentSwipe = new Vector2(mSecondPressPos.x - mFirstPressPos.x, mSecondPressPos.y - mFirstPressPos.y);
				mCurrentSwipe.Normalize();

				if (LeftSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, 90, 0, Space.World);
				}
				else if (RightSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, -90, 0, Space.World);
				}
				else if (UpLeftSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(90, 0, 0, Space.World);
				}
				else if (UpRightSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, 0, -90, Space.World);
				}
				else if (DownLeftSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, 0, 90, Space.World);
				}
				else if (DownRightSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(-90, 0, 0, Space.World);
				}
			}
		}

		private bool LeftSwipe(Vector2 swipe)
		{
			return swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f;
		}
	
		private bool RightSwipe(Vector2 swipe)
		{
			return swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f;
		}
	
		private bool UpLeftSwipe(Vector2 swipe)
		{
			return swipe.y > 0 && swipe.x < 0.0f;
		}
	
		private bool UpRightSwipe(Vector2 swipe)
		{
			return swipe.y > 0 && swipe.x > 0.0f;
		}
	
		private bool DownLeftSwipe(Vector2 swipe)
		{
			return swipe.y < 0 && swipe.x < 0.0f;
		}
	
		private bool DownRightSwipe(Vector2 swipe)
		{
			return swipe.y < 0 && swipe.x > 0.0f;
		}
	}
}
