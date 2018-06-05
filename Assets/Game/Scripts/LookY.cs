using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour {

    private float _sensitivity = 1.5f;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = transform.localEulerAngles;
        rotation.x -= mouseY * _sensitivity;

        transform.localEulerAngles = rotation;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}
}
