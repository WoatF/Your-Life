using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	Transform player;
	Renderer renderer;
	SpriteRenderer spriteRenderer;
	MapNew map;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		renderer = GetComponent<Renderer>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		map = GameObject.FindObjectOfType<MapNew>();
		transform.position = new Vector2(map.size.x*map.disTile.x/2 ,map.size.y*map.disTile.y/2);
		spriteRenderer.size = new Vector2(map.size.x*map.disTile.x/2 ,map.size.y*map.disTile.y/2);

	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.mainTextureOffset = new Vector2(Time.time , transform.position.y)/50;
		if(DayNight.instance != null)
		if(DayNight.instance.bright.ToString() =="Day")
		{
			spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
		}
		else 
		{
			spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
		}
	}
}
