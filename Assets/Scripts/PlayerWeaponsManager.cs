using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    public int selectedIndex;
    public GunScript activeWeapon;

    private void Start()
    {
        activeWeapon = transform.GetChild(selectedIndex).GetComponent<GunScript>();
        activeWeapon.ShowWeapon(true);
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            return;
        }

        HandleMouseWheelInput();
        HandleWeaponSelectInput();
    }

    private void HandleMouseWheelInput()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        var addition = scroll > 0 ? 1 : -1;
        var index = selectedIndex;
        Transform weapon = null;
        while (!weapon)
        {
            index += addition;
            if (index >= transform.childCount)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = transform.childCount - 1;
            }

            weapon = transform.GetChild(index);
        }
    }

    private void HandleWeaponSelectInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToIndex(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToIndex(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchToIndex(3);
        }
    }

    private bool SwitchToIndex(int index)
    {
        if (index > transform.childCount && selectedIndex == index)
        {
            return false;
        }

        if (activeWeapon)
        {
            // TODO put down active weapon
            activeWeapon.ShowWeapon(false);
        }

        // TODO put up new weapon
        selectedIndex = index;
        activeWeapon = transform.GetChild(selectedIndex).GetComponent<GunScript>();
        activeWeapon.ShowWeapon(true);
        return true;
    }
}