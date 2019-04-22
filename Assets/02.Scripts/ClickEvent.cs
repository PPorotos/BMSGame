using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{

    IPointerClickHandler clickHandler;
    private PointerEventData pointerEventData;
    RaycastHit hit;

    private void Awake()
    {
        pointerEventData = new PointerEventData(EventSystem.current);

    }
    private void Update()
    {
        for(int i =0; i<Input.touchCount; ++i)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if(Physics.Raycast(ray, out hit))
                {
                    clickHandler = hit.collider.gameObject.GetComponent<IPointerClickHandler>();
                    clickHandler.OnPointerClick(pointerEventData);
                }
            }
        }
    }
}
