using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject explosionPrefab;
    public bool firstPlay = true;
    private GameObject explosion;
    private int lives = 3;
    private int scoreTop = 0;
    private int scoreMid = 0;
    private int scoreBottom = 0;
    private GameController.View currentView = GameController.View.VIEW_TOP;


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
            increaseScore();
            Destroy(other.gameObject);
        }
        else
        {
            if (lives < 2)
            {
                if (explosion != null)
                    Destroy(explosion);
                explosion = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);
                --lives;
            }
            else
            {
                if (explosion != null)
                    Destroy(explosion);
                explosion = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);
                --lives;
            }
        }
    }

    public void decreaseLives()
    {
        if (explosion != null)
            Destroy(explosion);
        explosion = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);
        --lives;
    }

    public int getLives()
    {
        return lives;
    }

    public int getScore()
    {
        return scoreTop + scoreMid + scoreBottom;
    }

    public int getTopScore()
    {
        return scoreTop;
    }

    public int getMidScore()
    {
        return scoreMid;
    }

    public int getBottomScore()
    {
        return scoreBottom;
    }

    public void changeView(GameController.View view)
    {
        currentView = view;
    }

    public void resetScore()
    {
        lives = 3;
        scoreTop = 0;
        scoreMid = 0;
        scoreBottom = 0;
    }

    private void increaseScore()
    {
        switch(currentView)
        {
            case GameController.View.VIEW_TOP:
                ++scoreTop;
                break;
            case GameController.View.VIEW_MID:
                ++scoreMid;
                break;
            case GameController.View.VIEW_BOTTOM:
                ++scoreBottom;
                break;
        }
    }
}
