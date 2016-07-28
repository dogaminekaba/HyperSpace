using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {

    public static float speed = 0;
    private Transform t;

	// Use this for initialization
	void Start () {
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	    t.Translate(0, 0, -speed * Time.deltaTime);
        if (t.position.z < -20)
        {
            Destroy(GetComponent<GameObject>());
        }
	}
}
