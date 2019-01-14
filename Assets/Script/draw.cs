using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class draw : MonoBehaviour,IDragHandler,IPointerDownHandler,IDropHandler,IEndDragHandler {	
	Vector3 pos;
	public bool chase;
	public static GameObject target,nextTarget;
	LineRenderer line;
    public int serial,targetSerial;
    DrawTable table;
    void Start () {
		line = GetComponent<LineRenderer>();
        table = transform.parent.parent.parent.GetComponent<DrawTable>();
	}

    public void OnDrag(PointerEventData eventData)
    {
        if(Input.touchCount > 0 )
      {
        Touch touch = Input.GetTouch(0);

         pos = touch.position;
      }	
	  if(Input.GetMouseButton(0))

        pos = Input.mousePosition;
		      
        line.SetPosition(0,Vector3.one);

		line.SetPosition(1,(((Vector2)Camera.main.ScreenToWorldPoint(pos)-(Vector2)transform.position)*100)+(((Vector2)Camera.main.ScreenToWorldPoint(pos)-(Vector2)transform.position)*35));
		//Debug.Log("Drag");
    }
	

    public void OnPointerDown(PointerEventData eventData)
    {
        nextTarget = null;
        target = gameObject;
        Debug.Log("choose! "+target);
        targetSerial = 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
       if(target != gameObject)
       {
           nextTarget = gameObject;
           Debug.Log(nextTarget);
           
       }
       if(nextTarget == null )
       {
           line.SetPosition(1,Vector3.zero);
       }else
       {
           
           target.GetComponent<draw>().targetSerial = serial;

           target.GetComponent<LineRenderer>().SetPosition(1,(Vector2)( transform.position - target.transform.position )*100 + (Vector2)(transform.position - target.transform.position)*35 );
       }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(nextTarget == null )
       {
           line.SetPosition(1,Vector3.zero);
       }
    }
}
