using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawTable : MonoBehaviour {
    struct pickPoint
    {
    public int max{get;set;}
    public int min{get;set;}

    public pickPoint(int mx, int mn)
    {
      max = mx;
      min = mn;
    }
    }
    List<pickPoint> points = new List<pickPoint>();
    public Transform baseOfPoint;

    void Start () 
    {

	  }
	
	
    public void CreationRecipe()
    {
      clearPoint();
      loadPoint();
      int count = 0;
      int min = getMin();
      int max = getMax();
      foreach(UnlockRecipe recipe in inventory.AllUnlockRecipes)
      {
        bool brokenLoop =false;
        for(int mn = -min; mn < 25; mn++)
        {
         if(mn < min || mn > max)
         {
           foreach(pickPoint point in points)
        {
            for(int x =0; x<recipe.points.Length; x++)
            {
              if( point.max+mn == recipe.points[x].max && point.min+mn == recipe.points[x].min
              || point.max+mn == recipe.points[x].min && point.min+mn == recipe.points[x].max)
              {
                count++;
              }
            }
            if(count == recipe.points.Length && count == points.Count)
            {
                foreach(Recipe recipes in recipe.recipe)
                inventory.instance.ownRecipes.Add(recipes);            
                
                inventory.instance.Minus(1,ItemHandle.PickItemObj);

                brokenLoop = true;
                
                destroy();
                
                
            }
          }
         }
          if(brokenLoop == true)
          {
            break;
          }
        }
        
      }
    }
    int getMin()
    {
      int min = 0;
      for(int x = 0; x < points.Count; x++)
      {
        if(min < points[x].min)
        {
          min = points[x].min;
        }
      }
      
      return min;
    }
    int getMax()
    {
      int max = 0;
      for(int x = 0; x < points.Count; x++)
      {
        if(max < points[x].max)
        {
          max = points[x].max;
        }
      }
      
      return max;
    }
    public void loadPoint()
    {
      for(int x = 0; x< baseOfPoint.childCount; x++)
      {
        draw dr = baseOfPoint.GetChild(x).GetComponent<draw>();
        if(dr.targetSerial>0)
        {
          if(dr.targetSerial > dr.serial)
           {
               addPoint(dr.targetSerial,dr.serial);
           }
           else
           {
               addPoint( dr.serial, dr.targetSerial);
           }
        }
        
      }
    }
  public void addPoint(int max, int min)
  {
    
    points.Add(new pickPoint(max,min));
    Debug.Log("have points: "+ points.Count);
  }
  public void clearPoint()
  {
    points.Clear();
  }
  public void destroy()
  {
    Destroy(gameObject);
  }
}
