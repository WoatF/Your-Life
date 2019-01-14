using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler,IPointerDownHandler{
    public int slot;
    public void OnDrop(PointerEventData eventData)
    {

        if(transform.childCount < slot && ItemHandle.ObjBeDrag!= null)
        ItemHandle.ObjBeDrag.transform.SetParent(transform);
        Debug.Log("drop");
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        if(transform.childCount < 1)
        {
            GameObject.FindObjectOfType<WeaponSystem>().GetComponent<WeaponSystem>().UseWeapon(null);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
