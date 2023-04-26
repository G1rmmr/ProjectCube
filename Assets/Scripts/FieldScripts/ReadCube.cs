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
            setRayTransforms();
        
            mCubeState = FindObjectOfType<CubeState>();
            mLayerMask = 1 << 6;
        }

        private void setRayTransforms()
        {
            mUpRays = buildRays(tUp, new Vector3(90, 90, 0));
            mDownRays = buildRays(tDown, new Vector3(-90, 90, 0));
            mFrontRays = buildRays(tFront, new Vector3(0, 90, 0));
            mBackRays = buildRays(tBack, new Vector3(0, -90, 0));
            mLeftRays = buildRays(tLeft, new Vector3(0, 180, 0));
            mRightRays = buildRays(tRight, new Vector3(0, 0, 0));
        }

        private List<GameObject> buildRays(Transform rayTransform, Vector3 direction)
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

        public CubeState getCubeState() { return mCubeState; }
        public void setCubeState(CubeState cubeState) { mCubeState = cubeState; }

        public List<GameObject> readFace(List<GameObject> rayStarts, Transform rayTransform)
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

        public void readState()
        {
            mCubeState = FindObjectOfType<CubeState>();

            mCubeState.up = readFace(mUpRays, tUp);
            mCubeState.down = readFace(mDownRays, tDown);
            mCubeState.front = readFace(mFrontRays, tFront);
            mCubeState.back = readFace(mBackRays, tBack);
            mCubeState.left = readFace(mLeftRays, tLeft);
            mCubeState.right = readFace(mRightRays, tRight);
        }
    }
}
