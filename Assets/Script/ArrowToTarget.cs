using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToTarget : MonoBehaviour {
	GameObject target;
	SpriteRenderer sr;
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		target = Movement.target;
		if(target != null)
		{
			
		transform.position = new Vector2(target.transform.position.x,target.transform.position.y + Mathf.Sin(Time.time*3f)*0.05f+0.15f);
		
			sr.enabled = true;
		}
		else {
			sr.enabled = false;
		}
		

	}

}
