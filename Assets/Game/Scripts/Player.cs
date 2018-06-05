using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    private AudioSource _shootAudio;

    [SerializeField]
    private GameObject _weapon;

    [SerializeField]
    private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _reloading = false;
    private UIManager _uiManager;

    [SerializeField]
    private int _coins = 0;
    // Use this for initialization
    void Start () {
        _controller = GetComponent<CharacterController>();
        _muzzleFlash.SetActive(false);
        _shootAudio = _weapon.GetComponent<AudioSource>();
        _currentAmmo = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _weapon.SetActive(false);

        if (_weapon.activeSelf)
        {
            _uiManager.UpdateAmmo(_currentAmmo);
        }
    }
	
	// Update is called once per frame
	void Update () {

        CalculateMovement();
        Fire();
        if (Input.GetKeyDown(KeyCode.R) && !_reloading)
        {
            _reloading = true;
            StartCoroutine(Reload());
        }
	}

    public void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"), verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity); // local to global space
        _controller.Move(velocity * Time.deltaTime);
    }

    public void Fire()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            _currentAmmo--;
            _uiManager.UpdateAmmo(_currentAmmo);
            if (!_shootAudio.isPlaying)
            {
                _shootAudio.Play();
            }
            Ray origin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hitInfo;

            if (Physics.Raycast(origin, out hitInfo))
            {
                GameObject hit = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hit, 1);

                if (hitInfo.transform.CompareTag("Crate"))
                {
                    Destructible d = hitInfo.transform.GetComponent<Destructible>();
                    d.DestroyCrate();
                }
            }

            _muzzleFlash.SetActive(true);
        }
        else
        {
            _shootAudio.Stop();
            _muzzleFlash.SetActive(false);
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
        _reloading = false;
    }

    public void CollectCoin()
    {
        _coins++;
    }

    public bool Charge()
    {
        if (_coins > 0)
        {
            _coins--;
            _uiManager.HideCoin();
            _weapon.SetActive(true);
           
            _currentAmmo = _maxAmmo;
            _uiManager.UpdateAmmo(_currentAmmo);
            return true;
        }
        return false;
    }
}
