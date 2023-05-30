using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode changeWeaponKey = KeyCode.Q;
    public KeyCode grabKey = KeyCode.F;
    public KeyCode attackWeaponKey = KeyCode.Mouse0;
    public KeyCode openInventoryKey = KeyCode.E;

    private bool isWeaponChanging;
    private bool isGrabing;

    public event Action <bool> OnWeaponChangeUpdate;
    public event Action OnAttackUpdate;
    public event Action <bool> OnGrabingUpdate;

    private int inventoryOn = -1;
    public GameObject inventoryPanel;
    public PlayerCam camScript;


    void Update()
    {
        if (Input.GetKeyDown(changeWeaponKey))
        {
            isWeaponChanging = true;
            OnWeaponChangeUpdate?.Invoke(isWeaponChanging);
        }
        else
            isWeaponChanging=false;

        if (Input.GetKeyDown(attackWeaponKey))
        {
            OnAttackUpdate?.Invoke(); 
        }

        if (Input.GetKeyDown(grabKey))
        {
            isGrabing = true;
            OnGrabingUpdate?.Invoke(isGrabing);
        }
        else 
            isGrabing=false;

        if (Input.GetKeyDown(openInventoryKey))
        {
            if (inventoryOn == 1)
            {
                inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                //camScript.enabled = true;
                camScript.useScript = true;
            }
            else
            {
                inventoryPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //camScript.enabled = false;
                camScript.useScript = false;
            }
            inventoryOn *= (-1);
        }
    }
}
