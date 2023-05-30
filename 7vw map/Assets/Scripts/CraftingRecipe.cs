using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCraftingRecipe", menuName = "CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Crafting recipe")]
    public Item[] inputItems;
    public Item outputItem;

    [Header("Menge")]
    public int[] craftQuantity;
    // Start is called before the first frame update
    public bool CanCraft(InventoryManager inventory)
    {
        //hat Inventar platz?
        if (inventory.isFull())
        {
            Debug.Log("Inventory full");
            return false;
        }
        Debug.Log("Inventory not full");
        // hat Inventar alle Items?
        for (int i = 0; i < inputItems.Length; i++)
        {
            //inputItems[i].quantity
            if (!inventory.containsItems(inputItems[i], craftQuantity[i]))
            {
                Debug.Log("alle Items NICHT da");
                return false;
            }
        }
        Debug.Log("alle Items da");
        return true;
    }
    public void Craft(InventoryManager inventory)
    {
        //muss inputItems entfernen
        for (int i = 0; i < inputItems.Length; i++)
        {
            //quantity muss geändert werden
            //inputItems[i].quantity
            inventory.Remove(inputItems[i], craftQuantity[i]);
        }
        // outputItem ins Inventar hinzufügen
        inventory.AddItem(outputItem);

    }

}
