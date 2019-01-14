using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEditMap : MonoBehaviour {
	[System.Serializable]
	public class infoGround
	{
		public int layer;
		public Vector3 pos;
		public int number;
	}
	public Transform[] tiles;
	private Vector2 disTile = new Vector2(1.561f,0.718f);
	public Vector2 sizeMap;
	public bool save;
	private List<int> rdMap;
	[SerializeField]
	private Transform allground;
	void Start () {

		rdMap = new List<int>();
		
	}

	
	public void load()
	{
		// foreach(Ground gr in SaveGame.dataGround)
		// 		{
		// 			GameObject tileLoaded = (GameObject)Instantiate(tileAsset,gr.pos,Quaternion.identity);

		// 			SpriteRenderer sr =  tileLoaded.GetComponent<SpriteRenderer>();

		// 			sr.sprite = gr.sr;

		// 			sr.sortingOrder = gr.layer;
		// 		}
	}
}
