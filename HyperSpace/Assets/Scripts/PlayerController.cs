using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    static public float speed;
    public GameObject explosionPrefab;
    private GameObject explosion;
    private int lives = 3;
    private int score = 0;

	// Use this for initialization
	void Start () 
    {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheild")
        {
            ++lives;
            Destroy(other.gameObject);
        }
        else if(other.tag == "Alien")
        {
            ++score;
            Destroy(other.gameObject);
        }
        else
        {
            if (lives < 2)
            {
                if (explosion != null)
                    Destroy(explosion);
                explosion = (GameObject)Instantiate(explosionPrefab, transform.position, other.transform.rotation);
                GameController.gameOver();
            }
            else
            {
                if (explosion != null)
                    Destroy(explosion);
                explosion = (GameObject)Instantiate(explosionPrefab, transform.position, other.transform.rotation);
                --lives;
            }
        }
    }

    public int getLives()
    {
        return lives;
    }

    public int getScore()
    {
        return score;
    }
}
