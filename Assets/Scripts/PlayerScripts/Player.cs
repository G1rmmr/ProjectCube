// Copyright 2023. Jiwon-Nam All right reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
	// 플레이에 필요한 정보
    private enum EPlayerState
    {
        WAIT,
        ALIVE,
        DEAD
    }
    
    private short mHp;
    private string mType;
    
    private Vector3 mPos;
    private EPlayerState mNowState;
    
    //private Item mNowItem = new Item();
    
    // 네트워크에 필요한 정보
    private int mRank;
    private string mNickName;

    public string mId;
    public string mPassword;
    
    // Getters and Setters
    public string GetType() { return mType; }
    public void SetType(string type) { mType = type; }

    short GetHP() { return mHp; }
    void SetHP(short hp) { mHp = hp; }
    
    // 함수 목록
    
    // Start, Update
    private void Start()
    {
	    mType = null;
	    mHp = 3;
	    mPos = new Vector3(-11, 71, -11);
    }
    
    private void Update()
    {
	    if(Move())
	    {
		    this.gameObject.transform.position = mPos;
	    }
	    else if(UnMove())
	    {
		    GameObject parent = transform.parent.gameObject;
		    GameObject obj = this.gameObject;
		    
		    obj.transform.position = parent.transform.position;
		    obj.transform.rotation = parent.transform.rotation;
	    }
    }
    
    // 다른 행동을 감지
    private bool UnMove()
    {
	    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
	    {
		    return true;
	    }
	    return false;
    }

    // 이동
    // 정중앙 위치와 임계치를 고려
    
    private bool Move()
    {
	    Vector2 pressPos = new Vector2();
	    Vector2 playerPos = Camera.main.WorldToScreenPoint(mPos);
	    
	    if (Input.GetMouseButtonDown(2))
	    {
		    pressPos = Input.mousePosition;
		    pressPos = new Vector2(pressPos.x - playerPos.x, pressPos.y - playerPos.y);
		    pressPos.Normalize();
		    
		    if (UpSwipe(pressPos))
		    {
			    MoveUp();
		    }
		    else if (DownSwipe(pressPos))
		    {
			    MoveDown();
		    }
		    else if (DownLeftSwipe(pressPos))
		    {
			    MoveFront();
		    }
		    else if (UpRightSwipe(pressPos))
		    {
			    MoveBack();
		    }
		    else if (UpLeftSwipe(pressPos))
		    {
			    MoveLeft();
		    }
		    else if (DownRightSwipe(pressPos))
		    {
			    MoveRight();
		    }
		    return true;
	    }
	    return false;
    }
    
    private void MoveUp()
    {
	    if (!((mPos.y < 50 && (mPos.x == 0 || mPos.z == 0)) || mPos.y > 70))
	    {
		    mPos.y += 11;
	    }
    }

    private void MoveDown()
    {
        if (!(((mPos.x == 0 || mPos.z == 0) && mPos.y > 70) || mPos.y < 50))
        {
            mPos.y -= 11;
        }
    }
    private void MoveFront()
    {
	    // x위치 -로
        if (!(((mPos.y < 70 && mPos.y > 50) && mPos.x > 10)
            || ((mPos.z > -10 && mPos.z < 10) && (mPos.y < 50 || mPos.y > 70))
            || mPos.x < -10))
        {
            mPos.x -= 11;
        }
    }
    private void MoveBack()
    {
        if (!(((mPos.y < 70 && mPos.y > 50) && mPos.x < -10)
            || ((mPos.z > -10 && mPos.z < 10) && (mPos.y < 50 || mPos.y > 70))
            || mPos.x > 10))
        {
            mPos.x += 11;
        }
    }
    private void MoveLeft()
    {
        if (!(((mPos.y < 70 && mPos.y > 50) && mPos.z < -10)
            || ((mPos.x > -10 && mPos.x < 10) && (mPos.y < 50 || mPos.y > 70))
            || mPos.z > 10))
        {
            mPos.z += 11;
        }
    }
    private void MoveRight()
    {
        if (!(((mPos.y < 70 && mPos.y > 50) && mPos.z > 10)
            || ((mPos.x > -10 && mPos.x < 10) && (mPos.y < 50 || mPos.y > 70))
            || mPos.z < -10))
        {
	        mPos.z -= 11;
        }
    }
    
    private bool UpSwipe(Vector2 swipe)
    {
	    return swipe.y > 0 && swipe.x > -0.3f && swipe.x < 0.3f;
    }
    	
    private bool DownSwipe(Vector2 swipe)
    {
	    return swipe.y < 0 && swipe.x > -0.3f && swipe.x < 0.3f;
    }
    	
    private bool UpLeftSwipe(Vector2 swipe)
    {
	    return swipe.y > 0 && swipe.x < -0.3f;
    }
	
    private bool UpRightSwipe(Vector2 swipe)
    {
	    return swipe.y > 0 && swipe.x > 0.3f;
    }
	
    private bool DownLeftSwipe(Vector2 swipe)
    {
	    return swipe.y < 0 && swipe.x < -0.3f;
    }
	
    private bool DownRightSwipe(Vector2 swipe)
    {
	    return swipe.y < 0 && swipe.x > 0.3f;
    }
}
