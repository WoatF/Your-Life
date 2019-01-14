using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour{
	public int layer;
	public Vector2 pos;
	public int number;
	public int objinGround;
	SpriteRenderer sr;
	void Start(){
		sr = GetComponent<SpriteRenderer>();
		
		sr.sortingOrder = -(int)(transform.position.y*10);
	}

}
