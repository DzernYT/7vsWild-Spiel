using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShowInfoText : MonoBehaviour
{
    public Image[] images;
    public TMP_Text[] texts;
    public GameObject infoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            infoText.SetActive(false);
        }

    }

    public void addStuff(Item[] items, int[] quantities, CraftingRecipe recipe)
    {
        for(int i = 0; i < items.Length; i++)
        {
            images[i].sprite = items[i].image;
        }
        for (int i = 0; i < quantities.Length; i++)
        {
            texts[i].text = quantities[i].ToString();
            changeColor(recipe);
            
        }
    }

    public void changeColor(CraftingRecipe recipe)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if (!recipe.CanCraft(InventoryManager.instance))
            {
                texts[i].color = Color.red;
            }
            else
            {
                texts[i].color = Color.white;
            }
        }
    }






}
