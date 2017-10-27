using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    public int selectWeapon = 0;
	// Use this for initialization
	void Start () {
        SelectedWeapon();
	}
	
	// Update is called once per frame
	void Update () {

        int previousSelectedWeapon = selectWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectWeapon >= transform.childCount - 1)
                selectWeapon = 0;
            else
            selectWeapon++;
           
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectWeapon <= 0)
                selectWeapon = transform.childCount - 1;
            else
                selectWeapon--;

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            selectWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectWeapon = 2;
        }

        if (previousSelectedWeapon != selectWeapon)
        {
            SelectedWeapon();
        }
    }

    void SelectedWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
