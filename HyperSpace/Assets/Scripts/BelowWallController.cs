using UnityEngine;
using System.Collections;

public class BelowWallController : MonoBehaviour {

    public float speed;
    private float nextWall = 0.0F;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.z < -18)
        {
            Destroy(this.gameObject);
        }
        transform.position += new Vector3(0, 0, -speed);
	}
}
