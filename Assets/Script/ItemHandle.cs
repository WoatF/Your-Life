using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemHandle : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler {
    public static Item PickItem;
    public static ItemDisplay PickItemObj;
    Transform startParent;
    public static GameObject ObjBeDrag;
    CanvasGroup canvasGroup;

    void Start ()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}
    public void OnBeginDrag(PointerEventData eventData)
    {
      Debug.Log("drag");

      startParent = transform.parent;
      
      transform.SetParent(InventoryUI.setParent); 
      
      ObjBeDrag = gameObject;
      
      canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(Input.touchCount > 0 )
      {
        Touch touch = Input.GetTouch(0);

         pos = touch.position;
      }	
        pos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
       ObjBeDrag = null;

        canvasGroup.blocksRaycasts = true;

        if(transform.parent == InventoryUI.setParent)
        {
          transform.SetParent(startParent);
        }
        
        
    }

  float lastClick = 0f;
  float interval = 0.4f;
    public void OnPointerDown(PointerEventData eventData)
    { 	
      int count = eventData.clickCount;
       Debug.Log(count);
        if(InventoryUI.canUse == true){
          PickItem = GetComponent<ItemDisplay>().item;
          if(PickItem!= null)
          {
            GameObject.FindObjectOfType<WeaponSystem>().GetComponent<WeaponSystem>().UseWeapon(PickItem);

            PickItemObj = GetComponent<ItemDisplay>();
            
            GameObject.FindObjectOfType<Build>().updatePreview(PickItem.sprite);
          }
          
          
        }   
         if ((lastClick+interval)>Time.time)
          {
            Debug.Log(count);
                  Transform getParent = inventory.instance.FullInventory;
                  if(transform.IsChildOf(getParent))
                  {
                    ItemDisplay Dp = GetComponent<ItemDisplay>();
                    
                    if(Dp.count >= 2)
                    {
                      GameObject cloneItem = (GameObject) Instantiate(inventory.instance.ItemUI,getParent);

                      
                      ItemDisplay iD = cloneItem.GetComponent<ItemDisplay>();
                      
                      iD.item = GetComponent<ItemDisplay>().item;

                      iD.SetupItem();
                      if(Dp.count % 2 == 0)
                    {                     
                      Dp.count /=2;

                      iD.count = Dp.count;                      
                    }
                    else
                    {
                      Dp.count /=2;                     
                      iD.count = Dp.count;
                      Dp.count +=1;
                    }
                     
                    }
                    

                  }
          }//is a double click
      else
          { 

          }//is a single click
      lastClick = Time.time;  

     
    }


	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrop(PointerEventData eventData)
    { 
      if(ObjBeDrag != gameObject && ObjBeDrag != null)
      {
        ItemDisplay underObj = GetComponent<ItemDisplay>();
      
        ItemDisplay onObj = ObjBeDrag.GetComponent<ItemDisplay>();
        if(underObj.item.Name == onObj.item.Name )
        {
          
          if(underObj.item.canDuplicate == true )
          {
            bool isfull = (underObj.count + onObj.count) > underObj.item.maxAmount;

            if(isfull == false)
            {
              underObj.count += onObj.count;

              Destroy(ObjBeDrag);
            }
            else
            {
              onObj.count  = underObj.count + onObj.count - underObj.item.maxAmount;
                                                  
              underObj.count = underObj.item.maxAmount;           
              
            } 
                
                
      }
      
              
            }
      }
     
        
    }
}
