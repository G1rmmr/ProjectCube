// Copyright 2023. Jiwon-Nam All right reserved.

using System.Collections.Generic;
using UnityEngine;

namespace FieldScripts
{
    public class ReadCube : MonoBehaviour
    {
        private int mLayerMask;
        private CubeState mCubeState;

        private List<GameObject> mUpRays = new List<GameObject>();
        private List<GameObject> mDownRays = new List<GameObject>();
        private List<GameObject> mFrontRays = new List<GameObject>();
        private List<GameObject> mBackRays = new List<GameObject>();
        private List<GameObject> mLeftRays = new List<GameObject>();
        private List<GameObject> mRightRays = new List<GameObject>();

        private void Start()
        {
            SetRayTransforms();
        
            mCubeState = FindObjectOfType<CubeState>();
            mLayerMask = 1 << 6;
        }

        private void SetRayTransforms()
        {
            mUpRays = BuildRays(tUp, new Vector3(90, 90, 0));
            mDownRays = BuildRays(tDown, new Vector3(-90, 90, 0));
            mFrontRays = BuildRays(tFront, new Vector3(0, 90, 0));
            mBackRays = BuildRays(tBack, new Vector3(0, -90, 0));
            mLeftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
            mRightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        }

        private List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
        {
            int rayCount = 0;
            List<GameObject> rays = new List<GameObject>();

            for (int y = 11; y >= -11; y -= 11)
            {
                for (int x = -11; x <= 11; x += 11)
                {
                    Vector3 rayLocalPos = rayTransform.position;
                    Vector3 startPos = new Vector3(rayLocalPos.x + x, rayLocalPos.y + y, rayLocalPos.z);
                
                    GameObject rayStart = Instantiate(emptyGo, startPos, Quaternion.identity, rayTransform);
                
                    rayStart.name = rayCount.ToString();
                    rays.Add(rayStart);
                    rayCount++;
                }
            }
            rayTransform.localRotation = Quaternion.Euler(direction);
            return rays;
        }

        [SerializeField]
        public Transform tUp;
    
        [SerializeField]
        public Transform tDown;
    
        [SerializeField]
        public Transform tFront;
    
        [SerializeField]
        public Transform tBack;
    
        [SerializeField]
        public Transform tLeft;
    
        [SerializeField]
        public Transform tRight;

        [SerializeField]
        public GameObject emptyGo;

        public CubeState GetCubeState() { return mCubeState; }
        public void SetCubeState(CubeState cubeState) { mCubeState = cubeState; }

        public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
        {
            List<GameObject> facesHit = new List<GameObject>();

            foreach (GameObject rayStart in rayStarts)
            {
                Vector3 ray = rayStart.transform.position;
                RaycastHit hit;

                if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, mLayerMask))
                {
                    Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                    facesHit.Add(hit.collider.gameObject);
                    //print(hit.collider.gameObject.name);
                }
                else
                {
                    Debug.DrawRay(ray, rayTransform.forward * 1000.0f, Color.green);
                }
            }
            return facesHit;
        }

        public void ReadState()
        {
            mCubeState = FindObjectOfType<CubeState>();

            mCubeState.up = ReadFace(mUpRays, tUp);
            mCubeState.down = ReadFace(mDownRays, tDown);
            mCubeState.front = ReadFace(mFrontRays, tFront);
            mCubeState.back = ReadFace(mBackRays, tBack);
            mCubeState.left = ReadFace(mLeftRays, tLeft);
            mCubeState.right = ReadFace(mRightRays, tRight);
        }
    }
}
