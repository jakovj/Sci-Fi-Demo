using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private GameObject _player;
    private UIManager _uiManager;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Player");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            AudioSource pickUpSound = GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(pickUpSound.clip, transform.position, 1f);

            _player.GetComponent<Player>().CollectCoin();
            _uiManager.ShowCoin();

            Destroy(gameObject);
        }
    }
}
