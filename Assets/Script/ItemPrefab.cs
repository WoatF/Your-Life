using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour {

	public Item item;

	void Start()
	{
		StartCoroutine(Shake(transform,10,0.3f));

		if(item != null)
		SetupItem();
	}
	public void SetupItem()
	{
		SpriteRenderer srg =  gameObject.GetComponent<SpriteRenderer>();

		srg.sprite = item.sprite;
		
		srg.sortingOrder = -(int)(transform.position.y*100);
		
	}
	IEnumerator Shake(Transform obj,float range, float time)
	{	
		Vector3 pos = obj.position;
		while(time >0)
		{	
			if(obj!= null)
			obj.position = pos + new Vector3(0,Mathf.Sin(Time.time* range)*time);

			time -= Time.deltaTime;
			yield return null;
		}
	}
	
}
