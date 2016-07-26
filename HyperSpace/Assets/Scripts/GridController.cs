using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour {
    public static float speed;
    private float posY = 0;
    private Material mat;
    private Transform t;
    
	// Use this for initialization
	void Start () {
        t = GetComponent<Transform>();
        t.Rotate(new Vector3(90, 0, 0));
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        posY += speed * Time.deltaTime;
        mat.mainTextureOffset = new Vector2(0, posY);
        Debug.Log("offset");
	}
}
