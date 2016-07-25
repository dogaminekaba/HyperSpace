﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public enum State
    {
        STATE_STANDING,
        STATE_JUMPING,
        STATE_DUCKING,
    };

    public enum LaneState
    {
        STATE_MOVERIGHT,
        STATE_MOVELEFT
    };

    private enum Location
    {
        LOCATION_LEFT,
        LOCATION_CENTER,
        LOCATION_RIGHT
    };

    private enum View
    {
        VIEW_TOP,
        VIEW_CENTER,
        VIEW_BOTTOM
    };

    public GameObject wall;
    public GameObject playerPrefab;
    public GameObject player;
    public float startWait;
    public float speed;
    public float maxSpeed;
    public float jumpVelocity;
    private State currentState = State.STATE_STANDING;
    private Location currentLocation = Location.LOCATION_CENTER;
    private View currentView = View.VIEW_TOP;
    private float timeCounter = 0;
    private float playerStartPosX = 0;
    private float playerEndPosX = 0;
    private float vel = 20F;
    private Vector3 refTopCenter = new Vector3(0, 1, -17);
    private Vector3 refMidCenter = new Vector3(50, 1, -17);
    private Vector3 refBottomCenter = new Vector3(100, 3, -17);
    private Vector3 currentRef;
    private float mouseDownY;
    private float mouseUpY;

    void Start()
    {
        Profiler.maxNumberOfSamplesPerFrame = 3;
        // player initial position
        Vector3 spawnPosition = new Vector3(0, 1, -17);
        // wall spawn
        Quaternion spawnRotation = Quaternion.identity;
        player = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
        HorizontalWallController.speed = speed;
        HorizontalWallController.maxSpeed = maxSpeed;
        currentRef = refTopCenter;
        StartCoroutine(SpawnWalls());
    }

    IEnumerator SpawnWalls()
    {
        yield return new WaitForSeconds(startWait);
        for (int i = 0; i < 3; ++i )
        {
            // create wall for view 1
            yield return new WaitForSeconds(13F / speed);
            Vector3 spawnPosition = new Vector3(0, 0.75F, 20);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(wall, spawnPosition, spawnRotation);
            // create wall for view 3
            spawnPosition = new Vector3(100, 3.4F, 20);
            Instantiate(wall, spawnPosition, spawnRotation);
        }
    }

    public void movePlayer(State playerState)
    {
        switch(playerState)
        {
            case State.STATE_JUMPING:
                if (currentState != playerState && currentView == View.VIEW_TOP)
                {
                    currentState = playerState;
                    jumpVelocity = 15;
                }
                break;
            case State.STATE_DUCKING:
                if (currentState != playerState && currentView == View.VIEW_BOTTOM)
                {
                    currentState = playerState;
                    jumpVelocity = 15;
                }
                break;
        }
    }

    public void movePlayer(LaneState playerState)
    {
        switch(playerState)
        {
            case LaneState.STATE_MOVELEFT:
                if (currentLocation == Location.LOCATION_CENTER)
                    currentLocation = Location.LOCATION_LEFT;
                else if (currentLocation == Location.LOCATION_RIGHT)
                    currentLocation = Location.LOCATION_CENTER;
                break;
            case LaneState.STATE_MOVERIGHT:
                if (currentLocation == Location.LOCATION_CENTER)
                    currentLocation = Location.LOCATION_RIGHT;
                else if (currentLocation == Location.LOCATION_LEFT)
                    currentLocation = Location.LOCATION_CENTER;
                break;
        }

        timeCounter = 0;
        playerStartPosX = player.transform.position.x;
    }

    private float GetLaneXPos(Location targetLocation)
    {
        if(targetLocation == Location.LOCATION_CENTER)
            return 0;
        if(targetLocation == Location.LOCATION_RIGHT)
            return 2.5F;
        if (targetLocation == Location.LOCATION_LEFT)
            return -2.5F;
        return int.MinValue;
    }

    void Update()
    {
        if (player.gameObject == null)
            return;
        switch(currentState)
        {
            case State.STATE_JUMPING:
                if (jumpVelocity > -15)
                {
                    player.transform.Translate(Vector3.up * jumpVelocity * Time.deltaTime);
                    jumpVelocity -= 40 * Time.deltaTime;
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x, refTopCenter.y, player.transform.position.z);
                    jumpVelocity = 15;
                    currentState = State.STATE_STANDING;
                }
                break;
            case State.STATE_DUCKING:
                if (jumpVelocity > -15)
                {
                    player.transform.Translate(Vector3.up * -jumpVelocity * Time.deltaTime);
                    jumpVelocity -= 40 * Time.deltaTime;
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x, refBottomCenter.y, player.transform.position.z);
                    jumpVelocity = 15;
                    currentState = State.STATE_STANDING;
                }
                break;
            default:
                break;
        }
        if (timeCounter < 1.1)
        {
            float playerEndPosX = currentRef.x + GetLaneXPos(currentLocation);
            float newX = Mathf.Lerp(playerStartPosX, playerEndPosX, timeCounter);
            float time = Mathf.Abs(playerEndPosX - playerStartPosX) / vel;
            timeCounter += Time.deltaTime / time;

            player.transform.position = new Vector3(newX, player.transform.position.y, player.transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseUpY = Input.mousePosition.y;
            float tapTreshold = Mathf.Abs(mouseDownY - mouseUpY);

            if (tapTreshold < Screen.height * 0.005f)
            {
                if (mouseDownY > Screen.height * 2 / 3)
                {
                    currentRef = refTopCenter;
                    changeView(View.VIEW_TOP);
                }
                else if ((mouseDownY >= Screen.height * 1 / 3) && (mouseDownY <= Screen.height * 2 / 3))
                {
                    currentRef = refMidCenter;
                    changeView(View.VIEW_CENTER);
                }
                else if (mouseDownY < Screen.height * 1 / 3)
                {
                    currentRef = refBottomCenter;
                    changeView(View.VIEW_BOTTOM);
                }
            }
        }
    }

    void changeView(View newView)
    {
        if(currentView != newView)
        {
            currentView = newView;
            switch(newView)
            {
                case View.VIEW_TOP:
                    player.transform.position = new Vector3(refTopCenter.x + GetLaneXPos(currentLocation), refTopCenter.y, player.transform.position.z);
                    break;
                case View.VIEW_CENTER:
                    player.transform.position = new Vector3(refMidCenter.x + GetLaneXPos(currentLocation), refMidCenter.y, player.transform.position.z);
                    break;
                case View.VIEW_BOTTOM:
                    player.transform.position = new Vector3(refBottomCenter.x + GetLaneXPos(currentLocation), refBottomCenter.y, player.transform.position.z);
                    break;
                default:
                    break;
            }
            currentLocation = Location.LOCATION_CENTER;
            currentState = State.STATE_STANDING;
            jumpVelocity = 15;
        }
    }
    public static void gameOver()
    {
        HorizontalWallController.speed = 0;
    }
}
