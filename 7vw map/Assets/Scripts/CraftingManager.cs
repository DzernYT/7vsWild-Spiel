using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<RecipeofButton>().recipe.CanCraft(InventoryManager.instance))
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = Color.grey;
            }
        }
    }

    public void CraftItem(GameObject button)
    {
        InventoryManager.instance.Craft(button.GetComponent<RecipeofButton>().recipe);
    }

    public bool CanCraft(CraftingRecipe recipe)
    {
        return recipe.CanCraft(InventoryManager.instance);
    }

}
