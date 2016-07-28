﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Text scoreText;
    public static bool gameEnded = false;
    public GameObject horizontalWall;
    public GameObject horizontalUpperWall;
    public GameObject verticalWall;
    public GameObject playerPrefab;
    public GameObject playerShadowPrefab;
    public GameObject gridPrefab;
    private GameObject player;
    private GameObject playerShadow;
    private float startWait = 0;
    private float speed = 5;
    private float maxSpeed = 18;
    private float jumpPosY;
    private float duckPosY;
    private State currentState = State.STATE_STANDING;
    private Location currentLocation = Location.LOCATION_CENTER;
    private View currentView = View.VIEW_TOP;
    private float timeCounter = 0;
    private float playerStartPosX = 0;
    private float vel = 20F;
    private Vector3 refTopCenter = new Vector3(0, 1, -17);
    private Vector3 refMidCenter = new Vector3(50, 1, -17);
    private Vector3 refBottomCenter = new Vector3(100, 3, -17);
    private Vector3 currentRef;
    private float mouseDownY;
    private float mouseUpY;
    private int count;
    private int verticalPosition;
    private float accelerator = 0;
    private bool jumped = false;
    private bool ducked = false;
    private int oldVerticalPos=-1;
    private PickUpFactory pickUpC;
    private PlayerController playerControl;
    

    void Start()
    {
        pickUpC = GetComponent<PickUpFactory>();
        Profiler.maxNumberOfSamplesPerFrame = 3;
        // player initial position
        Vector3 spawnPosition = new Vector3(0, 1, -16);
        // wall rotation
        Quaternion spawnRotation = Quaternion.identity;
        // generate player object
        player = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
        spawnPosition = new Vector3(0, 0.1F, -16);
        playerShadow = (GameObject)Instantiate(playerShadowPrefab, spawnPosition, spawnRotation);
        spawnPosition = new Vector3(0, 1, -16);
        WallController.speed = speed;
        GridController.speed = speed;
        PickUpController.speed = speed;
        WallController.maxSpeed = maxSpeed;
        currentRef = refTopCenter;
        StartCoroutine(SpawnWalls());
        StartCoroutine(UpdateSpeed());
        StartCoroutine(SpawnPickUps());
        Screen.orientation = ScreenOrientation.Portrait;
        playerControl = player.GetComponent<PlayerController>();
        scoreText.text = "Lives: " + playerControl.getLives().ToString();
    }

    IEnumerator SpawnPickUps()
    {
        Vector3 PickUpPos;
        int pickUpPosX = 0;
        while (!gameEnded)
        {
            yield return new WaitForSeconds(13F / speed);
            // 1st gameplay view
            pickUpPosX = Random.Range(-1, 1);
            PickUpPos = new Vector3(refTopCenter.x + pickUpPosX*2.5F, refTopCenter.y, 15);
            pickUpC.createSheild(PickUpPos, Quaternion.identity);
            // 2nd gameplay view
            pickUpPosX = Random.Range(-1, 1);
            PickUpPos = new Vector3(refMidCenter.x + pickUpPosX * 2.5F, refMidCenter.y, 15);
            pickUpC.createSheild(PickUpPos, Quaternion.identity);
            // 3rd gameplay view
            pickUpPosX = Random.Range(-1, 1);
            PickUpPos = new Vector3(refBottomCenter.x + pickUpPosX * 2.5F, refBottomCenter.y, 15);
            pickUpC.createSheild(PickUpPos, Quaternion.identity);
        }
        
    }

    IEnumerator SpawnWalls()
    {
        yield return new WaitForSeconds(startWait);
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(gridPrefab, spawnPosition, spawnRotation);
        spawnPosition = new Vector3(50, 0, 0);
        Instantiate(gridPrefab, spawnPosition, spawnRotation);
        spawnPosition = new Vector3(100, 0, 0);
        Instantiate(gridPrefab, spawnPosition, spawnRotation);

        for (int i = 0; i < 3; ++i )
        {
            // create wall for view 1
            yield return new WaitForSeconds(13F / speed);
            spawnPosition = new Vector3(0, 0.75F, 20);
            Instantiate(horizontalWall, spawnPosition, spawnRotation);

            // create wall for view 2
            count = 2;
            // there are 3 different positions for vertical walls
            verticalPosition = Random.Range(1, 3);
            if (oldVerticalPos == verticalPosition)
                verticalPosition += 1;
            oldVerticalPos = verticalPosition;
            if(count == 1)
            {
                switch (verticalPosition)
                {
                    case 1:
                        spawnPosition = new Vector3(47.5F, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        break;
                    case 2:
                        spawnPosition = new Vector3(50, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        break;
                    case 3:
                        spawnPosition = new Vector3(52.5F, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        break;
                    default:
                        break;

                }
            }
            else
            {
                switch (verticalPosition)
                {
                    case 1:
                        spawnPosition = new Vector3(47.5F, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        spawnPosition = new Vector3(50, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        break;
                    case 2:
                        spawnPosition = new Vector3(50, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        spawnPosition = new Vector3(52.5F, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        break;
                    case 3:
                        spawnPosition = new Vector3(47.5F, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        spawnPosition = new Vector3(52.5F, 10, 20);
                        Instantiate(verticalWall, spawnPosition, spawnRotation);
                        break;
                    default:
                        break;

                }
            }
            // create wall for view 3
            spawnPosition = new Vector3(100, 3, 20);
            Instantiate(horizontalUpperWall, spawnPosition, spawnRotation);
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
                    jumpPosY = player.transform.position.y;
                }
                break;
            case State.STATE_DUCKING:
                if (currentState != playerState && currentView == View.VIEW_BOTTOM)
                {
                    currentState = playerState;
                    duckPosY = player.transform.position.y;
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
        scoreText.text = "Lives: " + playerControl.getLives().ToString();
        if (player.gameObject == null)
            return;
        switch(currentState)
        {
            case State.STATE_JUMPING:
                if (jumpPosY < 4 && !jumped)
                {
                    player.transform.position = new Vector3(player.transform.position.x, jumpPosY, player.transform.position.z );
                    accelerator += 0.01F;
                    jumpPosY += Time.deltaTime * (accelerator + speed);
                }
                else if(player.transform.position.y > currentRef.y)
                {
                    jumped = true;
                    player.transform.position = new Vector3(player.transform.position.x, jumpPosY, player.transform.position.z);
                    accelerator += 0.01F;
                    jumpPosY -= Time.deltaTime * (speed + accelerator);
                }
                else
                {
                    jumped = false;
                    player.transform.position = new Vector3(player.transform.position.x, currentRef.y, player.transform.position.z);
                    jumpPosY = player.transform.position.y;
                    accelerator = 0;
                    currentState = State.STATE_STANDING;
                }
                break;
            // inverse jump
            case State.STATE_DUCKING:
                if (duckPosY > 0.8F && !ducked)
                {
                    player.transform.position = new Vector3(player.transform.position.x, duckPosY, player.transform.position.z);
                    accelerator += 0.01F;
                    duckPosY -= Time.deltaTime * (accelerator + speed);
                }
                else if(player.transform.position.y < currentRef.y)
                {
                    ducked = true;
                    player.transform.position = new Vector3(player.transform.position.x, duckPosY, player.transform.position.z);
                    accelerator += 0.01F;
                    duckPosY += Time.deltaTime * (speed + accelerator);
                }
                else
                {
                    ducked = false;
                    player.transform.position = new Vector3(player.transform.position.x, currentRef.y, player.transform.position.z);
                    duckPosY = player.transform.position.y;
                    accelerator = 0;
                    currentState = State.STATE_STANDING;
                }
                break;
        }
        if (timeCounter < 1.1)
        {
            float playerEndPosX = currentRef.x + GetLaneXPos(currentLocation);
            float newX = Mathf.Lerp(playerStartPosX, playerEndPosX, timeCounter);
            float time = Mathf.Abs(playerEndPosX - playerStartPosX) / vel;
            timeCounter += Time.deltaTime / time;

            player.transform.position = new Vector3(newX, player.transform.position.y, player.transform.position.z);
            playerShadow.transform.position = new Vector3(newX, playerShadow.transform.position.y, player.transform.position.z);
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

    private void changeView(View newView)
    {
        if(currentView != newView)
        {
            currentLocation = Location.LOCATION_CENTER;
            currentView = newView;
            switch(newView)
            {
                case View.VIEW_TOP:
                    player.transform.position = new Vector3(refTopCenter.x + GetLaneXPos(currentLocation), refTopCenter.y, player.transform.position.z);
                    playerShadow.transform.position = new Vector3(refTopCenter.x + GetLaneXPos(currentLocation), playerShadow.transform.position.y, player.transform.position.z);
                    break;
                case View.VIEW_CENTER:
                    player.transform.position = new Vector3(refMidCenter.x + GetLaneXPos(currentLocation), refMidCenter.y, player.transform.position.z);
                    playerShadow.transform.position = new Vector3(refMidCenter.x + GetLaneXPos(currentLocation), playerShadow.transform.position.y, player.transform.position.z);
                    break;
                case View.VIEW_BOTTOM:
                    player.transform.position = new Vector3(refBottomCenter.x + GetLaneXPos(currentLocation), refBottomCenter.y, player.transform.position.z);
                    playerShadow.transform.position = new Vector3(refBottomCenter.x + GetLaneXPos(currentLocation), playerShadow.transform.position.y, player.transform.position.z);
                    break;
                default:
                    break;
            }
            
            currentState = State.STATE_STANDING;
            jumpPosY = 15;
        }
    }

    IEnumerator UpdateSpeed()
    {
        while(speed < maxSpeed)
        {
            yield return new WaitForSeconds(2);
            speed += 0.02F;
            WallController.speed = speed;
            GridController.speed = speed;
            PlayerController.speed = speed;
            PickUpController.speed = speed;
        }
    }

    public static void gameOver()
    {
        gameEnded = true;
        WallController.speed = 0;
        GridController.speed = 0;
        PickUpController.speed = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameEnded = false;
    }

}
