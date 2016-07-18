using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (touchDeltaPosition.x < 0.8F && touchDeltaPosition.y < 0.8F)
            {
                // click stub
                if(Input.GetTouch(0).position.y > (2 * ((float)Screen.height) / 3))
                {
                    Debug.Log("1st view \n");
                    rb.transform.position = new Vector3(0, 1, -17);
                }
                else if (Input.GetTouch(0).position.y > ((float)Screen.height) / 3)
                {
                    Debug.Log("2nd view \n");
                    rb.transform.position = new Vector3(50, 1, -17);
                }
                else
                {
                    Debug.Log("3rd view \n");
                    rb.transform.position = new Vector3(100, 1, -17);
                }
            }
            else
            {
                // swipe action
                Debug.Log("x: " + touchDeltaPosition.x + " y: " + touchDeltaPosition.y + "\n");

            }
            
        }
	}
}
