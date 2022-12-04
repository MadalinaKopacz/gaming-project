using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int SelectedWeapon;
    
    void Start()
    {
        SelectedWeapon = 0;
        SelectWeapon();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            SelectedWeapon = (SelectedWeapon + 1) % transform.childCount;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            SelectedWeapon = (SelectedWeapon + transform.childCount - 1) % transform.childCount;
        SelectWeapon();

    }

    public void SelectWeapon()
    {
        int i = 0;
        
        foreach(Transform weapon in transform)
        {
            if (i == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }

    }


}
