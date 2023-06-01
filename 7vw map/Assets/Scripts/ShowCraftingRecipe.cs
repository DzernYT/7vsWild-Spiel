using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShowCraftingRecipe : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CraftingRecipe recipeScript;
    public GameObject infoText;
    private int[] quantities;
    private Item[] items;
    private ShowInfoText showInfoText;
    Vector3 mousePosition = new Vector3(0, 0, 0);

    private bool once = true;

    private void Start()
    {
        showInfoText = GameObject.FindGameObjectWithTag("RecipeManager").GetComponent<ShowInfoText>();
        recipeScript = GetComponent<RecipeofButton>().recipe;

        items = recipeScript.inputItems;
        quantities = GetComponent<RecipeofButton>().recipe.craftQuantity;


    }
    private void Update()
    {
       
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted

        infoText.SetActive(true);
        if (once)
        {
            mousePosition = Input.mousePosition;
            infoText.transform.position = transform.position + new Vector3(-90, -20, 0);
        }
        


        showInfoText.addStuff(items, quantities, recipeScript);
    }
    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        showInfoText.changeColor(recipeScript);
        Debug.Log("Cheeeecckkk");
    }
        public void OnPointerExit(PointerEventData eventData)
    {
        once = true;
        infoText.SetActive(false);
    }
}
