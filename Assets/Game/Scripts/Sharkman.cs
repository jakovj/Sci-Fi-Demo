using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharkman : MonoBehaviour {

    

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (other.GetComponent<Player>().Charge())
            {
                GetComponent<AudioSource>().Play();
            }

        }
    }

}
