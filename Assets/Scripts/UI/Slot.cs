using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private GameObject indicator;
    private GameObject text;
    private Image image;

    public void Initialize()
    {
        indicator = transform.Find("Indicator").gameObject;
        text = transform.Find("Number").gameObject;
        image = transform.Find("Weapon").GetComponent<Image>();

        indicator.SetActive(false);
        image.gameObject.SetActive(false);
    }

    public void Activate(GameObject _weapon)
    {
        indicator.SetActive(false);
        text.SetActive(false);
        image.gameObject.SetActive(true);
        image.sprite = _weapon.GetComponent<Weapon>().GetWeaponStats().weaponSprite;
    }

    public void ActivateIndicator()
    {
        indicator.SetActive(true);
    }
}
