using UnityEngine;
using System.Collections;

public class HorizontalWallController : MonoBehaviour
{

    //public GameObject grid;
    static public float speed;
    static public float maxSpeed;
    public GameObject explosion;
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
            t.position = new Vector3(t.position.x, t.position.y, 20);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("game over");
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            GameController.gameOver();
        }
       
    }
}
