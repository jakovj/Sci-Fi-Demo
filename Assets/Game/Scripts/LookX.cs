using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour {

    private float _sensitivity = 1.5f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mouseX = Input.GetAxis("Mouse X");

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX * _sensitivity, transform.localEulerAngles.z);
	}
}
