using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isConsumable = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item item = InventoryManager.instance.GetSelectedItem(false);
        if(item != null)
        {
            isConsumable = item.isConsumable;
        }
       
        if (Input.GetMouseButtonDown(1) && isConsumable)
        {
            
            consume(item);
        }
    }

    public void consume(Item item)
    {
        if (item.isConsumable)
        {
            SurvivalManager.instance.ReplenishHungerThirst(20, 0);
            InventoryManager.instance.Remove(item, 1);
            if (InventoryManager.instance.itemSelectedCount() <= 0)
            {
                ShowItem.instance.hideAll();
            }
        }
    }
}
