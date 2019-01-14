using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveGame : MonoBehaviour {
	[SerializeField]
	private string namePath = "NewPath";
	public List<Item> ownItem = new List<Item>();
	[System.Serializable]
	public struct itemData
	{
		public int Count;
		public int getRank;
		public itemData(int Count,int getRank)
		{
			this.Count = Count;
			this.getRank = getRank;
		}
	}
	[System.Serializable]
	public class DataPlayer
	{
		public int heal;
		public Vector2 position;
	}
	public class DataGround
	{
		int sortInLayer;
		string layer;
		string name;
	}
	public List<itemData> itemDatas = new List<itemData>();
	public List<Recipe> ownRecipe = new List<Recipe>();
	bool haveItem;
	void Awake()
	{
		
	}
	void Start()
	{

	}
	public void _SaveGame()
	{
		saveDataPlayer();
		saveItem();
	}
	public void saveDataPlayer()
	{		
		heal heal = GameObject.FindGameObjectWithTag("Player").GetComponent<heal>();
		DataPlayer data = new DataPlayer();
		data.heal = heal.Health;
		data.position = heal.transform.position;
		string DataJson = JsonUtility.ToJson(data);
		PlayerPrefs.SetString("DataPlayer",DataJson);

	}
	public void LoadDataPlayer(DataPlayer data)
	{
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		Player.transform.position = data.position;
		Player.GetComponent<heal>().Health = data.heal;
	}
	public void saveItem()
	{	

		this.ownRecipe = inventory.instance.ownRecipes;
		
		this.ownItem = inventory.instance.GetItems();
		itemDatas = inventory.instance.GetDatas();
		Debug.Log(itemDatas.Count);
		if(ownItem.Count >0)
		{
			string data = JsonHelper.ToJson(itemDatas.ToArray());
			
			string saveItems = JsonHelper.ToJson(this.ownItem.ToArray());

			Debug.Log(saveItems);
			Debug.Log(data);
			PlayerPrefs.SetString("DataItem",data);
			
			PlayerPrefs.SetString("item",saveItems);
			Debug.Log("length: "+PlayerPrefs.GetString("item","").Length);
		}else
		{
			PlayerPrefs.SetString("item","");
			Debug.Log("length: "+PlayerPrefs.GetString("item","").Length);
		}
		
		if(ownRecipe.Count >0)
		{
			string Recipes = JsonHelper.ToJson(this.ownRecipe.ToArray());

			PlayerPrefs.SetString("Recipe",Recipes);

			Debug.Log(Recipes);	
		}
		else
		{
			PlayerPrefs.SetString("Recipe","");
		}
	
	}
	public void loadObj()
	{
		
		if(PlayerPrefs.GetString("item").Length>0)
		{
			Item[] ArrayItem = JsonHelper.FromJson<Item>(PlayerPrefs.GetString("item"));
			this.ownItem = new List<Item>(ArrayItem);
			itemData[] datas = JsonHelper.FromJson<itemData>(PlayerPrefs.GetString("DataItem"));
			itemDatas = new List<itemData>(datas);
			inventory.instance.load(ownItem,itemDatas.ToArray());
		}
		
		if(PlayerPrefs.GetString("Recipe").Length >0)
		{
			Recipe[] ArrayRecipe = JsonHelper.FromJson<Recipe>(PlayerPrefs.GetString("Recipe"));
			inventory.instance.ownRecipes = new List<Recipe>(ArrayRecipe);
		}
		
		
		LoadDataPlayer(JsonUtility.FromJson<DataPlayer>(PlayerPrefs.GetString("DataPlayer")));
		
	}

	void loadObj(GameObject go)
	{
		Instantiate(go,Vector3.zero,Quaternion.identity);
	}
	public void deleteFile()
	{
		PlayerPrefs.DeleteAll();
	
	}
}
