using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItem : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<Item> items = new List<Item>();
    // Start is called before the first frame update

    public static ShowItem instance;

    private Item ItemBefore = null;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showItem(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].image == item.image)
            {
                Debug.Log("show item");
                if(ItemBefore == null)
                {
                    ItemBefore = item;
                }
                else
                {
                    hideItem(ItemBefore);
                    ItemBefore = item;
                }
                gameObjects[i].SetActive(true);
                return;
            }
            else
            {
                hideAll();
            }
        }
        
    }

    public void hideItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
            {
                gameObjects[i].SetActive(false);
                return;
            }
        }

    }

    public void hideAll()
    {
        for (int i = 0; i < items.Count; i++)
        {
          
           gameObjects[i].SetActive(false);
      
            
        }
    }
}
