using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuild : MonoBehaviour {
	public GameObject front;
	Sprite sprite;
	void Start () {

		Sprite spriteFront = front.GetComponent<SpriteRenderer>().sprite;

		sprite = GetComponent<SpriteRenderer>().sprite;

		Debug.Log(sprite.border +" " + sprite.pivot +" "+ spriteFront.bounds.size.y/2);

		transform.position = front.transform.position + new Vector3(( spriteFront.pivot.y - spriteFront.border.w + sprite.pivot.y - sprite.border.y)/sprite.pixelsPerUnit,0,0);
	}
	
	void Update () {
		
	}
}
