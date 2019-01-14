using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOfGround : MonoBehaviour {

	public float heal;
	public Sprite[] AniGrow;
	public Sprite[] AniDeath;
	public float frame;
	[HideInInspector]
	public SpriteRenderer sr;
	bool die;
	public void Start () {

		sr = GetComponent<SpriteRenderer>();

		if(AniGrow.Length >0)

		StartCoroutine(UpdateAnimation(AniGrow,false));

	}
	IEnumerator UpdateAnimation(Sprite[] sprites,bool death)
	{
		for(int x = 0; x < sprites.Length; x ++)
		{
			sr.sprite = sprites[x];
			
			yield return new WaitForSeconds(frame);
			
		}
		if(death == true) Destroy(gameObject);		
	}

	
	
	// Update is called once per frame
	void Update () {

		if(heal <= 0 && die == false )
		{
			if(AniDeath.Length != 0)
			StartCoroutine(UpdateAnimation(AniDeath,true));
			else 
			Destroy(gameObject);
			die = true;
			Reward();
		}
	}
	protected virtual void Reward()
	{

	}
}
