using UnityEngine;

namespace FieldScripts
{
	public class CubeBigRotate : MonoBehaviour
	{
		[SerializeField]
		public GameObject target;

		[SerializeField]
		public float speed = 200.0f;
	
		public Vector2 getFirstPress(){ return mFirstPressPos; }
		public void setFirstPress(Vector2 firstPress){ mFirstPressPos = firstPress; }
	
		public Vector2 getSecondPress(){ return mSecondPressPos; }
		public void setSecondPress(Vector2 secondPress){ mSecondPressPos = secondPress; }

		public Vector2 getCurrentSwipe(){ return mCurrentSwipe; }
		public void setCurrentSwipe(Vector2 currentSwipe){ mCurrentSwipe = currentSwipe; }
	
		public Vector3 getPreviousMousePos(){ return mPreviosMousePos; }
		public void setPreviousMousePos(Vector3 preMousePos){ mPreviosMousePos = preMousePos; }
	
		public Vector3 getMouseDelta(){ return mMouseDelta; }
		public void setMouseDelta(Vector3 mouseDelta){ mMouseDelta = mouseDelta; }
	
		private Vector2 mFirstPressPos;
		private Vector2 mSecondPressPos;
		private Vector2 mCurrentSwipe;
		private Vector3 mPreviosMousePos;
		private Vector3 mMouseDelta;

		private void Update()
		{
			swipe();
			drag();
		}

		private void drag()
		{
			if (Input.GetMouseButton(1))
			{
				mMouseDelta = Input.mousePosition - mPreviosMousePos;
				Vector3 dragVector = mMouseDelta.normalized;

				mMouseDelta *= 0.1f;
				
				if (leftSwipe(dragVector) || rightSwipe(dragVector))
				{
					transform.rotation
						= Quaternion.Euler(0, -mMouseDelta.x, 0) * transform.rotation;
				}
				else if (upLeftSwipe(dragVector) || downRightSwipe(dragVector))
				{
					transform.rotation
						= Quaternion.Euler(mMouseDelta.y, 0, 0) * transform.rotation;
				}
				else if (upRightSwipe(dragVector) || downLeftSwipe(dragVector))
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

		private void swipe()
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

				if (leftSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, 90, 0, Space.World);
				}
				else if (rightSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, -90, 0, Space.World);
				}
				else if (upLeftSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(90, 0, 0, Space.World);
				}
				else if (upRightSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, 0, -90, Space.World);
				}
				else if (downLeftSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(0, 0, 90, Space.World);
				}
				else if (downRightSwipe(mCurrentSwipe))
				{
					target.transform.Rotate(-90, 0, 0, Space.World);
				}
			}
		}

		private bool leftSwipe(Vector2 swipe)
		{
			return swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f;
		}
	
		private bool rightSwipe(Vector2 swipe)
		{
			return swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f;
		}
	
		private bool upLeftSwipe(Vector2 swipe)
		{
			return swipe.y > 0 && swipe.x < 0.0f;
		}
	
		private bool upRightSwipe(Vector2 swipe)
		{
			return swipe.y > 0 && swipe.x > 0.0f;
		}
	
		private bool downLeftSwipe(Vector2 swipe)
		{
			return swipe.y < 0 && swipe.x < 0.0f;
		}
	
		private bool downRightSwipe(Vector2 swipe)
		{
			return swipe.y < 0 && swipe.x > 0.0f;
		}
	}
}
