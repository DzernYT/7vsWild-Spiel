using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class RecipeofButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public CraftingRecipe recipe;
    private GameObject infoText;
    void Start()
    {
        infoText = GameObject.FindGameObjectWithTag("InfoText");
        infoText.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        infoText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
