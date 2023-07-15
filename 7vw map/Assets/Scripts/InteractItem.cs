using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    public Transform cam;
    public bool activate = false;
    public bool activateWater = false;
    public float activateDistance;
    public GameObject interactText;
    public LayerMask itemMask;
    public LayerMask waterMask;
    public GameObject drinkUI;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        activate = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, activateDistance, itemMask);
        if (activate)
        {
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && activate)
        {
            var item = hit.transform.GetComponent<ObjectItem>().item;
            if (item != null)
            {
                InventoryManager.instance.AddItem(item);
                if (InventoryManager.instance.GetSelectedItem(false))
                {
                    ShowItem.instance.showItem(item);
                }
                Destroy(hit.transform.gameObject);
            }
        }

        //activateWater =  Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, activateDistance, waterMask);
        if (activateWater)
        {
            drinkUI.SetActive(true);
        }
        else
        {
            drinkUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && activateWater)
        {
            SurvivalManager.instance.ReplenishHungerThirst(0, 10);
            SurvivalManager.instance.loseHealth(5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            activateWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            activateWater = false;
        }
    }
}
