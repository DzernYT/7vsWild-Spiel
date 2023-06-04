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
    PlayerCam camScript;
    PlayerMovment playerMovment;
    private bool inventoryIsClosed = true;

    private void Start()
    {
        playerMovment = GetComponent<PlayerMovment>();
        camScript = FindAnyObjectByType<PlayerCam>();
    }

    void Update()
    {
        if (inventoryIsClosed)
        {
            if (Input.GetKeyDown(changeWeaponKey))
            {
                isWeaponChanging = true;
                OnWeaponChangeUpdate?.Invoke(isWeaponChanging);
            }
            else
                isWeaponChanging = false;

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
                isGrabing = false;
        }

        if (Input.GetKeyDown(openInventoryKey))
        {
            if (inventoryIsClosed)
            {
                inventoryPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                playerMovment.enabled = false;
                camScript.useScript = false;

                inventoryIsClosed = false;
            }
            else
            {
                inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                playerMovment.enabled = true;
                camScript.useScript = true;

                inventoryIsClosed =true;
            }
        }
       
    }
}
