using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이에 필요한 정보
    private enum PlayerState
    {
        eWAIT,
        eALIVE,
        eDEAD
    }

    [SerializeField]
    private string mType = null;
    
    [SerializeField]
    private short mHP = 3;

    [SerializeField]
    private Vector3 mPos = new Vector3(0, 0, 0);

    [SerializeField]
    private PlayerState mNowState;
    
    [SerializeField]
    //private Item mNowItem = new Item();

    // Getters and Setters
    string GetType() { return mType; }
    void SetType(string type) { mType = type; }

    short GetHP() { return mHP; }
    void SetHP(short hp) { mHP = hp; }

    float GetX() { return mPos.x; }
    void SetX(float x) { mPos.x = x;}
    
    // 네트워크에서 필요한 정보
    [SerializeField]
    private string mNickName = null;
    
    [SerializeField]
    private int mScore = 0;
    
    [SerializeField]
    private int mRank = 0;
    
}
