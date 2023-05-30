using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    public Transform cam;
    public bool activate = false;
    public float activateDistance;
    public GameObject interactText;
    public LayerMask itemMask;
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
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
