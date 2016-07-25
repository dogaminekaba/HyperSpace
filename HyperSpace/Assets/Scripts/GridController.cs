using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour {
    public static float speed;
    public Material mat;
    private Transform t;
    
	// Use this for initialization
	void Start () {
        t = GetComponent<Transform>();
        t.Rotate(new Vector3(90, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
