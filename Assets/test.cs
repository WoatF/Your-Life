using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

	public static test testUI;
	public float x{set;get;}
	public float y{set;get;}
	public Text textX;
	public Text textY;
	void Awake()
	{
		testUI = this;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		textX.text = x+"";
		textY.text = ""+y;
	}
}
