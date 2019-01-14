using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
	SpriteRenderer sr;
	BoxCollider2D box;
	public GameObject crate;
	Transform buildParent;
	Vector2 divBuild;
	void Awake () {
		sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
		box = GetComponent<BoxCollider2D>();
		buildParent = GameObject.Find("Build").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void updatePreview(Sprite sprite)
	{	
		Debug.Log("UPdatePreview!!!!!!!!!!!!");
		if(GameManager.instance.building == true &&ItemHandle.PickItem.canBuild == true && sprite != null)
		{	
			sr.enabled = true;
			transform.GetChild(0).localPosition = new Vector2(-(sprite.pivot.x- sprite.border.z)/sprite.pixelsPerUnit,0);
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
			updateCollider(ItemHandle.PickItem);
		}
		else
		{
			clearPreview();
		}	
		
		
		
	}
	public void Building()
	{
		GameObject prebuild = (GameObject) Instantiate(crate,transform.GetChild(0).position,Quaternion.identity);
		
		SpriteRenderer srBuild = prebuild.GetComponent<SpriteRenderer>();
		srBuild.sprite = sr.sprite;
		BoxCollider2D colBuild = prebuild.GetComponent<BoxCollider2D>();
		colBuild.offset = new Vector2((srBuild.sprite.pivot.x- srBuild.sprite.border.z)/srBuild.sprite.pixelsPerUnit + box.offset.x,box.offset.y);
		colBuild.size = box.size;
		prebuild.GetComponent<Graves>().item = new Item[1]{ItemHandle.PickItem};
		srBuild.sortingOrder = -(int)(prebuild.transform.position.y*100);
		prebuild.transform.SetParent(buildParent);
		prebuild.transform.rotation = Quaternion.Euler(0,0,-90);
		
	}
	public void clearPreview()
	{
		sr.enabled = false;
		box.enabled = false;
		sr.sprite = null;
	}
	public void updateCollider(Item item)
	{	
		if(GameManager.instance.building == true)
		box.enabled = true;
		Sprite sr = item.sprite;
		box.offset = new Vector2(-(sr.pivot.x*2 -sr.border.x-sr.border.z)/2/sr.pixelsPerUnit,0);
		box.size = new Vector2(sr.pivot.x*2 -sr.border.x-sr.border.z,(sr.pivot.x - sr.border.w)*2)/sr.pixelsPerUnit;
	}
	Vector2 PosBuild(GameObject built,Sprite sprite)
	{
		Vector2 pos = Vector2.zero;
		Sprite spriteFront = built.GetComponent<SpriteRenderer>().sprite;
		if(Mathf.Abs(divBuild.x) > Mathf.Abs(divBuild.y))
		{
			if(divBuild.x > 0 )
			{
				pos = built.transform.position + new Vector3(( spriteFront.pivot.y - spriteFront.border.w + sprite.pivot.y - sprite.border.y)/sprite.pixelsPerUnit * 2,0,0);
			}
			else
			{
				pos = built.transform.position + new Vector3(-(( spriteFront.pivot.y - spriteFront.border.w + sprite.pivot.y - sprite.border.y)/sprite.pixelsPerUnit * 2),0,0);
			}
		}
		else
		{
			if(divBuild.y > 0)
			{
				pos = built.transform.position + new Vector3(0,(spriteFront.pivot.x*2 - spriteFront.border.x +spriteFront.border.z + sprite.border.z)/sprite.pixelsPerUnit,0);
			}
			if(divBuild.y < 0)
			{
				pos = built.transform.position + new Vector3(0,-(spriteFront.pivot.x*2 - spriteFront.border.x +spriteFront.border.z + sprite.border.z)/sprite.pixelsPerUnit,0);
			}
		}

		Debug.Log(sprite.border +" " + sprite.pivot +" "+ spriteFront.bounds.size.y/2);
		
		 
		
		return pos;
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Build")
		{	
			float allX = box.size.x+col.GetComponent<BoxCollider2D>().size.x;
			float distanceX = Mathf.Abs(transform.position.x - col.transform.position.x);
			Debug.Log("x: "+ distanceX/allX*100);
			float allY = box.size.y+col.GetComponent<BoxCollider2D>().size.y;
			float distanceY = Mathf.Abs(transform.position.y - col.transform.position.y);
			Debug.Log("y: "+ distanceY/allY*100);
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.tag =="Build")
		{
			if(ItemHandle.PickItem != null && GameManager.instance.building == true)
			transform.GetChild(0).position = PosBuild(col.gameObject,ItemHandle.PickItem.sprite);
			float sizeY = col.GetComponent<BoxCollider2D>().size.y;
			float sizeX = col.GetComponent<BoxCollider2D>().size.x;
			float allX = box.size.y+sizeY;
			float distanceX = /*Mathf.Abs*/(transform.position.x - col.transform.position.x);
			//Debug.Log("x: "+ distanceX/allX*100);
			float allY = box.size.x+sizeX;
			float distanceY = /*Mathf.Abs*/((transform.position.y+box.offset.x) - (col.transform.position.y-col.GetComponent<BoxCollider2D>().offset.x-sizeX*2));
			//Debug.Log("y: "+ distanceY/allY*100);
			test.testUI.x = distanceX/allX*100;
			test.testUI.y = distanceY/allY*100 +100;
			divBuild.x= distanceX/allX*100;
			divBuild.y= distanceY/allY*100 +100;
		}
		
			
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Build")
		{	if(GameManager.instance.building == true)
			if(sr.sprite != null )
			updatePreview(sr.sprite);
			else
			transform.GetChild(0).localPosition = Vector2.zero;
		}
	}
}
