using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour {

	LineRenderer line ;
	Movement movement;
	void Start()
	{

		 line = transform.parent.GetComponent<LineRenderer>();

		 movement = GameObject.FindObjectOfType<Movement>();

	}
	public void SetRotation () {
		
		float angle = AngleBetweenVector2(line.GetPosition(0),line.GetPosition(1));

		if(movement.facingRight)
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle	));
		else
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+180));
		
	}
	void Update()
	{	
			transform.position = line.GetPosition(1);
	
			GetComponent<SpriteRenderer>().enabled = movement.useHook;
		
	}
	 private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
 {
         Vector2 diference = vec2 - vec1;
         float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
         return Vector2.Angle(Vector2.right, diference) * sign;
     }
	

}
