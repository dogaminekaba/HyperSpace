using UnityEngine;
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
        STATE_NOMOVE,
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
    public Vector3 spawnValues;
    public float startWait;
    public float speed;
    public float range;
    public float maxSpeed;
    public float jumpVelocity;
    public float moveVelocity;
    private State currentState = State.STATE_STANDING;
    private LaneState currentLaneState = LaneState.STATE_NOMOVE;
    private Location currentLocation = Location.LOCATION_CENTER;
    private View currentView = View.VIEW_TOP;

    void Start()
    {
        Profiler.maxNumberOfSamplesPerFrame = 3;
        Vector3 spawnPosition = new Vector3(0, 1, -17);
        spawnValues = new Vector3(0, 0.75F, 20);
        Quaternion spawnRotation = Quaternion.identity;
        player = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
        BelowWallController.speed = speed;
        BelowWallController.maxSpeed = maxSpeed;
        StartCoroutine(SpawnWalls());
    }

    IEnumerator SpawnWalls()
    {
        yield return new WaitForSeconds(startWait);
        for (int i = 0; i < 3; ++i )
        {
            yield return new WaitForSeconds(13F / speed);
            Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(wall, spawnPosition, spawnRotation);
        }
    }

    public void movePlayer(State playerState)
    {
        if (currentState != playerState)
        {
            currentState = playerState;
            jumpVelocity = Screen.height*0.03F;
        }
    }

    public void movePlayer(LaneState playerState)
    {
        // Disable user to move to left or right twice
        if (currentLocation == Location.LOCATION_LEFT && playerState == LaneState.STATE_MOVELEFT)
            return;
        else if (currentLocation == Location.LOCATION_RIGHT && playerState == LaneState.STATE_MOVERIGHT)
            return;
        // Disable user to make multiple actions before old action is finished
        if (currentLaneState != LaneState.STATE_NOMOVE)
            return;
        currentLaneState = playerState;
        moveVelocity = 10;
    }

    void Update()
    {
        switch(currentState)
        {
            case State.STATE_JUMPING:
                if (jumpVelocity > -Screen.height * 0.03F)
                {
                    Debug.Log("Jump!");
                    player.transform.Translate(0, jumpVelocity * Time.deltaTime, 0);
                    jumpVelocity -= speed/8;
                }
                else
                {
                    player.GetComponent<Transform>().position = new Vector3(player.transform.position.x, 1, player.transform.position.z);
                    jumpVelocity = Screen.height * 0.03F;
                    currentState = State.STATE_STANDING;
                }
                break;
            default:
                break;
        }
        switch(currentLaneState)
        {
            case LaneState.STATE_MOVELEFT:
                if(moveVelocity > 0)
                {
                    Debug.Log("Move Left!");
                    player.transform.Translate(-moveVelocity * Time.deltaTime, 0, 0);
                    moveVelocity -= 0.5F;
                }
                else
                {
                    // stall until jumping or ducking finished to fix final position
                    if (currentState == State.STATE_STANDING)
                    {
                        // update current location
                        currentLocation = currentLocation - 1;
                        // reset player location when it's on the center lane to avoid calculation errors
                        if (currentLocation == Location.LOCATION_CENTER)
                            player.GetComponent<Transform>().position = new Vector3(0, player.transform.position.y, player.transform.position.z);
                        moveVelocity = 10;
                        currentLaneState = LaneState.STATE_NOMOVE;
                    }
                }
                break;
            case LaneState.STATE_MOVERIGHT:
                if (moveVelocity > 0)
                {
                    Debug.Log("Move Right!");
                    player.transform.Translate(moveVelocity * Time.deltaTime, 0, 0);
                    moveVelocity -= 0.5F;
                }
                else
                {
                    // stall until jumping or ducking finished to fix final position
                    if (currentState == State.STATE_STANDING)
                    {
                        // update current location
                        currentLocation = currentLocation + 1;
                        // reset player location when it's on the center lane to avoid calculation errors
                        if (currentLocation == Location.LOCATION_CENTER)
                            player.GetComponent<Transform>().position = new Vector3(0, player.transform.position.y, player.transform.position.z);
                        moveVelocity = 10;
                        currentLaneState = LaneState.STATE_NOMOVE;
                    }
                }
                break;
            default:
                break;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y > Screen.height * 2 / 3)
                changeView(View.VIEW_TOP);
            else if (Input.mousePosition.y > Screen.height * 1 / 3)
                changeView(View.VIEW_CENTER);
            else if (Input.mousePosition.y < Screen.height * 1 / 3)
                changeView(View.VIEW_BOTTOM);
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
                    player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
                    break;
                case View.VIEW_CENTER:
                    player.transform.position = new Vector3(50, player.transform.position.y, player.transform.position.z);
                    break;
                case View.VIEW_BOTTOM:
                    player.transform.position = new Vector3(100, player.transform.position.y, player.transform.position.z);
                    break;
                default:
                    break;
            }
        }
    }


}
