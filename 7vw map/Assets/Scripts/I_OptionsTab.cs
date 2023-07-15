using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class I_OptionsTab : MonoBehaviour, IPointerClickHandler, IPointerDownHandler //,IPointerUpHandler
{
    public GameObject optionsTab;

    public Color rightClickColor = Color.gray;
    public float rightClickColorDuration = 0.1f;

    private Button button;
    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            optionsTab.SetActive(false);
        }
    }

    public void showOptions()
    {
        optionsTab.SetActive(true);
    }

    public void eat (Item item)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            showOptions();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
           // StartCoroutine(FadeToRightClickColor());
        }
    }
}
