using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{
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
        t.Translate(0, 0, -speed * Time.deltaTime);
        if (t.position.z < -20)
        {
            if (t.tag == "Horizontal Wall")
                t.position = new Vector3(t.position.x, t.position.y, 20);
            else if (t.tag == "Vertical Wall")
                rePosVerticalWalls();
        }
    }

    void rePosVerticalWalls()
    {
        if (t.position.x < 49)
            t.position = new Vector3(52.5F, t.position.y, 20);
        else if(t.position.x > 51)
            t.position = new Vector3(50, t.position.y, 20);
        else
            t.position = new Vector3(47.5F, t.position.y, 20);
    }

}
