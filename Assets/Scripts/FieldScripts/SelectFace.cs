// Copyright 2023. Jiwon-Nam All right reserved.

using System.Collections.Generic;
using UnityEngine;

namespace FieldScripts
{
    public class SelectFace : MonoBehaviour
    {
        private CubeState mCubeState;
        private ReadCube mReadCube;
        private int mLayerMask;
    
        private void Start()
        {
            mLayerMask = 1 << 6;
            mReadCube = FindObjectOfType<ReadCube>();
            mCubeState = FindObjectOfType<CubeState>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mReadCube.ReadState();

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 10000.0f, mLayerMask))
                {
                    GameObject face = hit.collider.gameObject;
                    List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                    {
                        mCubeState.up,
                        mCubeState.down,
                        mCubeState.front,
                        mCubeState.back,
                        mCubeState.left,
                        mCubeState.right
                    };

                    foreach (List<GameObject> cubeSide in cubeSides)
                    {
                        if (cubeSide.Contains(face))
                        {
                            mCubeState.pickUp(cubeSide);
                        }
                    }
                }
            }
        }
    }
}
