using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text _ammoText;

    [SerializeField]
    private GameObject _coinImage;

    private void Start()
    {
        HideCoin();
    }

    public void UpdateAmmo(int ammoCount)
    {
        _ammoText.text = "Ammo: " + ammoCount.ToString();
    }

    public void ShowCoin()
    {
        _coinImage.SetActive(true);
    }

    public void HideCoin()
    {
        _coinImage.SetActive(false);
    }
}
