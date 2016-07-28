using UnityEngine;
using System.Collections;

public class PickUpFactory : MonoBehaviour {

    public GameObject sheildPrefab;
    public GameObject greenAlienPrefab;

	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public GameObject createSheild(Vector3 position, Quaternion rotation)
    {
        if (sheildPrefab == null)
        {
            return null;
        }
        return (GameObject)Instantiate(sheildPrefab, position, rotation);
    }

    public GameObject createAlien(Vector3 position, Quaternion rotation)
    {
        if (greenAlienPrefab == null)
        {
            return null;
        }
        return (GameObject)Instantiate(greenAlienPrefab, position, rotation);
    }

}
