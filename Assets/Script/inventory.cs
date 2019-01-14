using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour {
    
    #region Singleton
    public static inventory instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    public Item normalAttack;
	public GameObject ItemPrefab,ItemUI;
    public Transform inventoryUi;
    public Transform FullInventory;
    public List<Item> ownItem;
    public List<GameObject> ownObj;
    public List<Recipe> ownRecipes;
    public static List<UnlockRecipe> AllUnlockRecipes;
    int InventorySlot;
    
	void Start ()
    {   
        
        LoadUnlockRp();
        
        InventorySlot = FullInventory.GetComponent<ItemDrop>().slot;
      
    }
    public void load(List<Item> item,SaveGame.itemData[] data)
    {
        Transform parent = null;
        for(int x = 0; x < item.Count; x++)
        {
            if(data[x].getRank <7)
            {
                parent = inventoryUi.GetChild(x);
            }
            else
            {
                parent = FullInventory;
            }
            GameObject go =(GameObject) Instantiate(ItemUI,parent);
            
            go.GetComponent<ItemDisplay>().SetupItem(data[x].Count,item[x]);

            Debug.Log(x);
        }
    }
    private void LoadUnlockRp()
    {
        AllUnlockRecipes = new List<UnlockRecipe>();
        foreach(UnlockRecipe ul in (UnlockRecipe[])Resources.LoadAll<UnlockRecipe>("Unlock Recipes"))
        {
            AllUnlockRecipes.Add(ul);
        }
    }
	void Update () {
        
		
        
	}
    public void Minus(int amount,ItemDisplay display)
    {
        if(display.count >= amount)
        {
            display.count -= amount;
            if(display.count <= 0)
            {
                Destroy(display.gameObject);
            }
        }
    }
   public List<Item> GetItems()
   {
       List<Item> items = new List<Item>();
        for(int x = 0; x< FullInventory.childCount+7; x++)
        {
            if(x <7)
            {
                if(inventoryUi.GetChild(x).childCount>0)
                {
                    Item item = inventoryUi.GetChild(x).GetChild(0).GetComponent<ItemDisplay>().item;
                    items.Add(item);
                }
            }
            else if(x >= 7)
            {
                Item item = FullInventory.GetChild(x-7).GetComponent<ItemDisplay>().item;
                items.Add(item);
            }
        }
       return items;
   }
    public List<SaveGame.itemData> GetDatas()
    {
        List<SaveGame.itemData> data = new List<SaveGame.itemData>();

        for(int x = 0; x< FullInventory.childCount+7; x++)
        {
            if(x < 7)
            {
                if(inventoryUi.GetChild(x).childCount>0)
                {
                    int count = inventoryUi.GetChild(x).GetChild(0).GetComponent<ItemDisplay>().count;
                    data.Add(new SaveGame.itemData(count,x));
                }
            }
            else if(x >= 7)
            {
                int count = FullInventory.GetChild(x-7).GetComponent<ItemDisplay>().count;
                data.Add(new SaveGame.itemData(count,x));
            }
            
        }
        Debug.Log(data);
        return data;
    }
    public void AddItem(Item item,GameObject go)
    {   
        int count = hadItem(item);
        
        Debug.Log(count);

        Transform canUse = null;

        for(int x = 0;x < InventorySlot+7;x++)
        {
            if(x < 7)
            {
                if(inventoryUi.GetChild(x).childCount <= 0 )
                {
                    canUse = inventoryUi.GetChild(x);
                    break;
                }
            }
            else
            {
                canUse = FullInventory;
                break;
            }
        }
            

        if(count < 0 )
            {   
                
                
                GameObject ItemPre = (GameObject) Instantiate(ItemUI,canUse);
                    
                ItemDisplay iD = ItemPre.GetComponent<ItemDisplay>();

                iD.item = item;

                if(iD.item!= null)
            
                iD.SetupItem();

                ownObj.Add(ItemPre);
            }
            else if( count >= 7)
            {   
                
                canUse.GetChild(count-7).GetComponent<ItemDisplay>().count +=1;
                Debug.Log("slot: "+count);

                
            }
            else if(count >=0 && count < 7)
            {
                canUse = inventoryUi;
                canUse.GetChild(count).GetChild(0).GetComponent<ItemDisplay>().count +=1;
            }
            Destroy(go);

            ownItem.Add(item);
            
            
    }
      
    int hadItem(Item item)
    {   
        
        int number = -1;
        if(item.canDuplicate == true)
        {                             
           for(int x = 0; x < FullInventory.childCount+7; x ++)
            {   
                if(x < 7)
                {
                    if(inventoryUi.GetChild(x).childCount != 0 )
                    {
                       ItemDisplay id = inventoryUi.GetChild(x).GetChild(0).GetComponent<ItemDisplay>();
                        if(item.Name == id.item.Name)
                        {
                            if(id.count == id.item.maxAmount)
                            {

                            number = -1;
                            
                            }
                                
                            else {
                                number = x; 
                                break;
                                }       
                        
                        } 
                    }
                    
                }else
                {
                    ItemDisplay iD = FullInventory.GetChild(x-7).GetComponent<ItemDisplay>();
                    if(item.Name == iD.item.Name )
                    {  
                        if(iD.count == iD.item.maxAmount)
                        {
                        number = -1;
                        
                        }
                            
                        else {
                            number = x; 
                            break;
                            }       
                    
                    }
                }
             
        }
        
        
        
        }
        
        return number;
    }
    public Item GetItem(GameObject go)
    {   
        Item item = null;
        if(go != null)
        item = go.GetComponent<ItemPrefab>().item;

        return item;
    }
	
    
}
