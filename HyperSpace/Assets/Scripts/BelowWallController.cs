using UnityEngine;
using System.Collections;

public class BelowWallController : MonoBehaviour {

    static public float speed;
    static public float maxSpeed;
    Transform t;

	// Use this for initialization
    void Start()
    {
        t = GetComponent<Transform>();
        // move backwards
    }

    void Update()
    {
        t.Translate(0, 0, -speed * Time.deltaTime);
        if (t.position.z < -20)
        {
            t.position = new Vector3(0, 1, 20);
            if (speed < maxSpeed)
            {
                speed += 0.5F;
            }
        }
    }
}
