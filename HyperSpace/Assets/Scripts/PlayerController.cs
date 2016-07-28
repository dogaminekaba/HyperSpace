﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    static public float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheild")
        {
            Destroy(other.gameObject);
        }
    }
}
