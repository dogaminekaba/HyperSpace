using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour {

    public ParticleSystem particles;
    [SerializeField]
    private Image alienTreeImage;
    private float fillAmount = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine(alienTreeAnimation());
	}

    IEnumerator alienTreeAnimation()
    {
        while (fillAmount > 0)
        {
            alienTreeImage.fillAmount = fillAmount;
            fillAmount -= 0.005F;
            yield return new WaitForSeconds(0.5F * Time.fixedDeltaTime);
            if (fillAmount <= 0)
            {
                particles.transform.position = new Vector3(alienTreeImage.transform.position.x, alienTreeImage.transform.position.y, alienTreeImage.transform.position.z - 5);
                particles.transform.localScale = new Vector3(5,0,5);
                particles.Play();
                fillAmount = 1;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay Scene");
    }
}
