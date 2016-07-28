using UnityEngine;
using System.Collections;

public class PickUpFactory : MonoBehaviour {

    public GameObject sheildPrefab;

	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public GameObject createSheild(Vector3 position, Quaternion rotation)
    {
        if (sheildPrefab == null)
        {
            Debug.Log("sheild null");
            return null;
        }
        return (GameObject)Instantiate(sheildPrefab, position, rotation);
    }
}
