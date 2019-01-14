using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graves : HealOfGround {
	public Item[] item;
	[Range(0,100)]
	public float percent=100;
	new void Start()
	{
		base.Start();
	}
	protected override void Reward()
	{	for(int x = 0; x < item.Length; x++)
			if(Random.Range(0f,100f) < percent)
				if(item.Length != 0)
				{	
					Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f,0.5f),transform.position.y+ Random.Range(-0.5f,0.5f));
					GameObject go = (GameObject)Instantiate(GameObject.FindObjectOfType<inventory>().ItemPrefab,pos,Quaternion.identity);
					
					int rdi = Random.Range(0,item.Length);					
					
					ItemPrefab ip =  go.GetComponent<ItemPrefab>();	
					
					ip.item = item[rdi];

					ip.SetupItem();
				
				}
		
	}
}
