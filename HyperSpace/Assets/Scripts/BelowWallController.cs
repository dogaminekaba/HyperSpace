using UnityEngine;
using System.Collections;

public class BelowWallController : MonoBehaviour {

    //public GameObject grid;
    static public float speed;
    static public float maxSpeed;
    private Transform t;

	// Use this for initialization
    void Start()
    {
        t = GetComponent<Transform>();
    }

    void Update()
    {
        //grid.GetComponent<Transform>().Translate(0, 0, -speed * Time.deltaTime);
        t.Translate(0, 0, -speed * Time.deltaTime);
        if (t.position.z < -20)
            t.position = new Vector3(0, 0.75F, 20);
    }
}
