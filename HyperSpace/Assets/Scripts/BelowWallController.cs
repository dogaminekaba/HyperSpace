using UnityEngine;
using System.Collections;

public class BelowWallController : MonoBehaviour {

    static public float speed;
    private Rigidbody rb;

	// Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // move backwards
        rb.velocity = speed * transform.forward * -1;
    }
}
