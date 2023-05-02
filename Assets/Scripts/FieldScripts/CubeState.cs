// Copyright 2023. Jiwon-Nam All right reserved.

using System.Collections.Generic;
using UnityEngine;

namespace FieldScripts
{
    public class CubeState : MonoBehaviour
    {
        public List<GameObject> up = new List<GameObject>();
        public List<GameObject> down = new List<GameObject>();
        public List<GameObject> front = new List<GameObject>();
        public List<GameObject> back = new List<GameObject>();
        public List<GameObject> left = new List<GameObject>();
        public List<GameObject> right = new List<GameObject>();

        public void pickUp(List<GameObject> cubeSide)
        {
            foreach (var face in cubeSide)
            {
                if (face != cubeSide[4])
                {
                    print(face.name);
                    face.transform.parent.transform.parent = cubeSide[4].transform.parent;
                }
            }
            cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
        }

        public void putDown(List<GameObject> rooms, Transform pivot)
        {
            foreach (var room in rooms)
            {
                if (room != rooms[4])
                {
                    room.transform.parent.transform.parent = pivot;
                }
            }
        }
    }
}
