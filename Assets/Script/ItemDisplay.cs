using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDisplay : MonoBehaviour{
	public Item item;
	public int count;
	Text text;
	void Start()
	{	if(count <1)
		SetupItem();
	}
    public void SetupItem (int count = 1,Item item = null) {
		if(item != null)
		this.item = item;
		text = transform.Find("Text").GetComponent<Text>();
		GetComponent<Image>().sprite = this.item.sprite;
		this.count = count;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(count > 1)
		text.text = count.ToString();
		else
		text.text = "";
	}
}
