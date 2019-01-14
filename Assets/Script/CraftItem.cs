using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem: MonoBehaviour {
	#region Singleton

	public static CraftItem instance;
	private void Awake()
	{
		instance = this;
	}

	#endregion
	public GameObject prefabItem;
	public List<Recipe> OwnRecipes;
	Item slot1;
	Item slot2;
	public Transform slot_1;
	public Transform slot_2;
	Transform resultSlot;
	public Transform ChangeButton;
	public Image PreviewItemSlot;
	int NumberOfItem;
	Sprite originSr;
	public Recipe[] SelectedItem = new Recipe[0];
	
	void Start () {
		resultSlot = transform;
		originSr = PreviewItemSlot.sprite;
		this.OwnRecipes = inventory.instance.ownRecipes;
	}
	
	// Update is called once per frame
	void Update () {
			if(slot_1.childCount > 0)
		{
			slot1 = slot_1.GetChild(0).GetComponent<ItemDisplay>().item;			
		}
		else
		slot1 = null;
		if(slot_2.childCount > 0)
			slot2 = slot_2.GetChild(0).GetComponent<ItemDisplay>().item;
		else
		slot2 = null;
		if(slot1 == null && slot2 == null)
		clearResult();
		else
		UpdatePreviewItem();
		if(SelectedItem != null)
		
		{
	
		bool changeUp =  NumberOfItem < SelectedItem.Length - 1;	
		ChangeButton.GetChild(0).gameObject.SetActive(changeUp);
		
		bool changDown = NumberOfItem > 0;
		ChangeButton.GetChild(1).gameObject.SetActive(changDown);
		}
		else
		{
			ChangeButton.GetChild(1).gameObject.SetActive(false);
			ChangeButton.GetChild(0).gameObject.SetActive(false);
		}
	}
	public void ChangeValue(bool Up)
	{	
		
		
			if(Up == true)	
		{
			if(NumberOfItem < SelectedItem.Length-1)
			NumberOfItem ++;
			
		}
		else
		{	
			if(NumberOfItem >0)
			NumberOfItem --;
		}
			
		
		
	}
	public void crafting()
	{
		//foreach(Recipe recipe in OwnRecipes)
		//if((recipe.input01 == slot1 && recipe.input02 == slot2) ||
			//(recipe.input01 == slot2 && recipe.input02 == slot1))
		//{	
			if(SelectedItem != null)
			{
				if(slot_1.childCount > 0)
				{
					ItemDisplay id1 = slot_1.GetChild(0).GetComponent<ItemDisplay>();
					id1.count --;
					if(id1.count <=0)
					{
						Destroy(slot_1.GetChild(0).gameObject);
					}
					
				}
				
				if(slot_2.childCount > 0)
				{
					ItemDisplay id2 = slot_2.GetChild(0).GetComponent<ItemDisplay>();
					id2.count --;
					if(id2.count <=0)
					{
						Destroy(slot_2.GetChild(0).gameObject);
					}
				}
				UpdatePreviewItem();

				if(SelectedItem != null && SelectedItem[NumberOfItem]!= null)
				{	
					int count = SelectedItem[NumberOfItem].count;
					
					if(resultSlot.childCount >0)
				{	
					
					ItemDisplay hadItem = resultSlot.GetChild(0).GetComponent<ItemDisplay>();
					if(SelectedItem[NumberOfItem].result == hadItem.item && hadItem.count + count <= hadItem.item.maxAmount)
					{
						hadItem.count +=  count;
					}
				}else
				{
					
					GameObject InsItem = (GameObject) Instantiate(prefabItem,transform);
					ItemDisplay iD = InsItem.GetComponent<ItemDisplay>();
					iD.item = SelectedItem[NumberOfItem].result;
					iD.SetupItem();					
					iD.count = count;
				
				}
																					
			}
		}
					
			
		//}
		
	}
	public void clearResult()
	{
		PreviewItemSlot.sprite = originSr;
		//SelectedItem = new Item[0];
		NumberOfItem = 0;
	}
	Recipe[] itemsResult()
	{
		List<Recipe> items = new List<Recipe>();
		foreach(Recipe recipe in OwnRecipes)
		if((recipe.input01 == slot1 && recipe.input02 == slot2) ||
			(recipe.input01 == slot2 && recipe.input02 == slot1))
			{								
					items.Add(recipe);				
			}
			if(items.Count == 0)
			return null;

			return items.ToArray();
	}
	public void UpdatePreviewItem()
	{
		// foreach(Recipe recipe in OwnRecipes)
		// if((recipe.input01 == slot1 && recipe.input02 == slot2) ||
		// 	(recipe.input01 == slot2 && recipe.input02 == slot1))
		// {				
		// 		SelectedItem = recipe.result;
		// 		PreviewItemSlot.sprite = recipe.result[number].sprite;
		
		// }
		SelectedItem = itemsResult();
		if(SelectedItem != null && SelectedItem[NumberOfItem]!= null)
		{
			PreviewItemSlot.sprite = SelectedItem[NumberOfItem].result.sprite;
		}
		else
			PreviewItemSlot.sprite = originSr;
	}
}
