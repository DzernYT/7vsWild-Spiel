using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Item Zugriff z.B.: Item item =  InventoryManager.instance.GetSelectedItem(false)
    // Allgemein InventoryManager.instance.Methode();
    public static InventoryManager instance;

    public Item[] startItems;

    public int maxStackedItems = 9;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();

    //immer in die Liste hinzufügen!!!! nicht vergessen
    private List<Item> items = new List<Item>();

    int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ChangeSelectedSlot(0);
        foreach (var item in startItems)
        {
            AddItem(item);
        }
    }

    private void Update()
    {

        //check crafting
        if (Input.GetKeyDown(KeyCode.C))
        {
            Craft(craftingRecipes[0]);
        }
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {

        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true)
            {

                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        items.Add(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }

        return null;
    }

    //für crafting Recipe
    public bool containsItems(Item item, int quantity)
    {
        int itemCount = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count >= quantity)
            {
                return true;
            }
            //Fall: z.B. 2 Stacks 1mal Holz und 2 gebraucht
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < quantity)
            {
                itemCount += itemInSlot.count;
            }
            if (itemCount == quantity)
            {
                return true;
            }
        }
        return false;
    }

    public void Remove(Item item, int quantity)
    {
        int[] itempositions = new int[2];
        int next = 0;
        int itemCount = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.item.stackable == true)
            {
                if (itemInSlot.count >= quantity)
                {
                    itemInSlot.count -= quantity;
                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                        itemInSlot = null;
                    }
                    else
                    {
                        itemInSlot.RefreshCount();
                    }
                    break;
                }
                //Fall: z.B. 2 Stacks 1mal Holz und 2 gebraucht
                if (itemInSlot.count < quantity)
                {
                    itemCount += itemInSlot.count;
                    Debug.Log(itemCount);
                    itempositions[0 + next] = i;
                    next++;
                }
                if (itemCount == quantity)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        slot = inventorySlots[itempositions[0 + j]];
                        itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                        Destroy(itemInSlot.gameObject);
                        itemInSlot = null;
                    }
                    /*
                    Debug.Log("2 Bäume gefunden");
                    for (int j = 0; j < inventorySlots.Length; j++)
                    {
                        InventorySlot slot2 = inventorySlots[j];
                        Debug.Log("angekommen" + j);
                        itemInSlot = slot2.GetComponentInChildren<InventoryItem>();
                        if (itemInSlot != null && itemInSlot == item)
                        {
                            Debug.Log("2 Bäume zerstört");
                            Destroy(itemInSlot.gameObject);
                            
                        }
                        
                    }
                    */
                    break;


                }
            }
        }
    }

    public void Craft(CraftingRecipe recipe)
    {
        if (recipe.CanCraft(this))
        {
            recipe.Craft(this);
        }
        else
        {
            Debug.Log("cant craft");
        }
    }

    public bool isFull()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                return false;
            }
        }

        return true;
    }

}
