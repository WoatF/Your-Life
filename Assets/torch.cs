using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch : MonoBehaviour {

	Transform inside;
	public float size=1,speed=2;
	void Start () {
		inside = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		inside.localScale = new Vector2(Mathf.Abs(Mathf.Sin(Time.time*speed))*0.2f+size,Mathf.Abs(Mathf.Sin(Time.time*speed))*0.2f+size);
		
	}
}
