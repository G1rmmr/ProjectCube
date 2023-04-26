using System.Collections.Generic;
using UnityEngine;

namespace FieldScripts
{
    public class PivotRotation : MonoBehaviour
    {
        public void Rotate(List<GameObject> side)
        {
            mActiveSide = side;
            mMouseRef = Input.mousePosition;

            mDragging = true;
            mLocalForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        }

        public void rotateToRightAngle()
        {
            Vector3 euler = transform.localEulerAngles;

            euler.x = Mathf.Round(euler.x / 90) * 90;
            euler.y = Mathf.Round(euler.y / 90) * 90;
            euler.z = Mathf.Round(euler.z / 90) * 90;

            mTargetQuater.eulerAngles = euler;
            mAutoRotating = true;
        }
        
        private ReadCube mReadCube;
        private CubeState mCubeState;
        private List<GameObject> mActiveSide;

        private Vector3 mLocalForward;
        private Vector3 mMouseRef;
        private Vector3 mRotation;

        private Quaternion mTargetQuater;

        private float mSensitivity;
        private float mSpeed;
        
        private bool mDragging;
        private bool mAutoRotating;

        private void Start()
        {
            mSensitivity = 0.4f;
            mSpeed = 300f;
            
            mDragging = false;
            mAutoRotating = false;
        
            mReadCube = FindObjectOfType<ReadCube>();
            mCubeState = FindObjectOfType<CubeState>();
        }

        private void Update()
        {
            if (mDragging)
            {
                spinSide(mActiveSide);

                if (Input.GetMouseButtonUp(0))
                {
                    mDragging = false;
                    rotateToRightAngle();
                }
            }

            if (mAutoRotating)
            {
                autoRotate();
            }
        }
        
        private void spinSide(List<GameObject> side)
        {
            mRotation = Vector3.zero;
            Vector3 mouseOffset = (Input.mousePosition - mMouseRef);

            if (side == mCubeState.up)
            {
                mRotation.y = -mSensitivity * (mouseOffset.x + mouseOffset.y);
            }
            
            if (side == mCubeState.down)
            {
                mRotation.y = mSensitivity * (mouseOffset.x + mouseOffset.y);
            }
            
            if (side == mCubeState.front)
            {
                mRotation.x = -mSensitivity * (mouseOffset.x + mouseOffset.y);
            }
            
            if (side == mCubeState.back)
            {
                mRotation.x = mSensitivity * (mouseOffset.x + mouseOffset.y);
            }
            
            if (side == mCubeState.left)
            {
                mRotation.z = mSensitivity * (mouseOffset.x + mouseOffset.y);
            }
            
            if (side == mCubeState.right)
            {
                mRotation.z = -mSensitivity * (mouseOffset.x + mouseOffset.y);
            }
        
            transform.Rotate(mRotation, Space.World);
            mMouseRef = Input.mousePosition;
        }

        private void autoRotate()
        {
            mDragging = false;
            var step = mSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, mTargetQuater, step);

            if (Quaternion.Angle(transform.localRotation, mTargetQuater) <= 1)
            {
                transform.localRotation = mTargetQuater;
                mCubeState.putDown(mActiveSide, transform.parent);

                mAutoRotating = false;
                mDragging = false;
            }
        }
    }
}
