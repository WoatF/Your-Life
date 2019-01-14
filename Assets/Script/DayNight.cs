using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour {
	public enum Bright{ Day,Night}
	public Bright bright;
	public SpriteRenderer[] sr;
	Color[] srColor = new Color[3];
	public Transform clock,sClock;
	public Sprite sDay,sNight;
	public Text dayText;
	public int day;
	public float speedChangeDay;
	bool changingDay;
	public static DayNight instance;
	
	void Awake()
	{	
		instance = this;
		srColor[0] = sr[0].color;
		srColor[1] = sr[1].color;
		srColor[2] = sr[2].color;
		sr[0].color = Color.clear;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		dayText.text = day.ToString();
		sClock.Rotate(new Vector3(0,0,1)*Time.deltaTime);
		
		if(sClock.rotation.z > 0)
		{
			bright = Bright.Day;

			sr[0].color = Color.Lerp(sr[0].color, Color.clear, speedChangeDay*Time.deltaTime);
			sr[1].color = Color.Lerp(sr[1].color, Color.clear, speedChangeDay*Time.deltaTime);
			sr[2].color = Color.Lerp(sr[2].color, Color.clear, speedChangeDay*Time.deltaTime);

		}
		else
		{
			bright = Bright.Night;

			sr[0].color = Color.Lerp(sr[0]. color,srColor[0], speedChangeDay*Time.deltaTime);
			sr[1].color = Color.Lerp(sr[1]. color,srColor[1], speedChangeDay*Time.deltaTime);
			sr[2].color = Color.Lerp(sr[2]. color,srColor[2], speedChangeDay*Time.deltaTime);
		}
		switch(bright)
		{
			case Bright.Day:
			clock.GetComponent<Image>().sprite = sDay;
			transform.GetChild(0).GetComponent<SpriteMask>().enabled = false;
			break;
			case Bright.Night:
			transform.GetChild(0).GetComponent<SpriteMask>().enabled = true;
			clock.GetComponent<Image>().sprite = sNight;
			break;
		}
	}
}
