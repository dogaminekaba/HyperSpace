using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    public GameController gc;
    protected virtual void OnEnable()
    {
        // Hook into the OnSwipe event
        Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnSwipe event
        Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
    }

    public void OnFingerSwipe(Lean.LeanFinger finger)
    {
        var swipe = finger.SwipeDelta;

        if (swipe.x < -Mathf.Abs(swipe.y))
        {
            Debug.Log("left swipe");
            gc.movePlayer(GameController.LaneState.STATE_MOVELEFT);
        }

        if (swipe.x > Mathf.Abs(swipe.y))
        {
            Debug.Log("right swipe");
            gc.movePlayer(GameController.LaneState.STATE_MOVERIGHT);
        }

        if (swipe.y < -Mathf.Abs(swipe.x))
        {
            Debug.Log("down swipe");
        }

        if (swipe.y > Mathf.Abs(swipe.x))
        {
            Debug.Log("up swipe");
            gc.movePlayer(GameController.State.STATE_JUMPING);
        }
    }
}
