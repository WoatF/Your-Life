using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour {

	SpriteRenderer sprite;
	public bool autoPos = true;
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		
		sprite.sprite = transform.parent.GetComponent<SpriteRenderer>().sprite;

		sprite.sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder;

		if(autoPos== true)
		transform.localPosition = new Vector3(-0.1f,0.1f);

		transform.rotation = Quaternion.Euler(-47,0,222);
	}
	
	// Update is called once per frame
	
}
