using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CraftSlot : MonoBehaviour,IDropHandler {
	public int slot;

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount < slot && ItemHandle.ObjBeDrag!= null)
        {
            ItemHandle.ObjBeDrag.transform.SetParent(transform);
            //CraftItem.instance.PreviewItemSlot();
        }
        
    }

}
