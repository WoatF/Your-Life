using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapNew : MonoBehaviour {

	[System.Serializable]
	public class Source
	{	
		public string Name;

		[Range(0,100)]
		public int percent;
		public int repeat;
		[Header("Image")]
		public Transform[] sr_Dust;
		public Transform[] sr_Winter;
		public Transform[] sr_Sand;
		public bool canReplace;
		public bool useBase;
		public string layer;
		[Range(0,0.7f)]
		public float Range;
		[Range(0,100)]
		public float noise;
		
		
	}
	[System.Serializable]
	public class DataGround
	{
		public string name;
		public string layer;
		public Vector2 position;
		public DataGround(string name,string layer, Vector2 pos)
		{
			this.name = name;
			this.layer = layer;
			this.position = pos;
		}
	}
	List<DataGround> data = new List<DataGround>();
	public Source[] sources;
	[SerializeField]
	private Tilemap tilemap;
	[SerializeField]
	TileBase tileDust,tileWinter,tileSand;
	public Vector2Int size;
	public Vector2 disTile = new Vector2(1,1);
	[SerializeField]
	private int numberOfSeason;
	[SerializeField]
	private bool useRandomSeed;
	[SerializeField]
	private string seed;
	[Range(0,100)]
	public int randomFillPercent;
	[SerializeField]
	private Transform allground,allObject;
	int[,] map;
	int[,] sortMap;
	System.Random random;
	private Dictionary<string,Transform> DtrTile = new Dictionary<string, Transform>();
	void Start ()
    {
		LoadTile(PlayerPrefs.GetString("seed"));

		LoadDictionary();

        GeneratorMap();
        
        // tile.Add(sources[0].sr_Dust[0].name.ToString(),sources[0].sr_Dust[0].gameObject);
        // Instantiate(tile[sources[0].sr_Dust[0].name.ToString()],Vector2.left,Quaternion.identity);
        // Debug.Log(tile);
    }

    private void LoadDictionary()
    {
        for (int a = 0; a < sources.Length; a++)
        {
            for (int dust = 0; dust < sources[a].sr_Dust.Length; dust++)
            {
                if (!DtrTile.ContainsKey(sources[a].sr_Dust[dust].name))
                    DtrTile.Add(sources[a].sr_Dust[dust].name, sources[a].sr_Dust[dust]);
            }
            for (int dust = 0; dust < sources[a].sr_Sand.Length; dust++)
            {
                if (!DtrTile.ContainsKey(sources[a].sr_Sand[dust].name))
                    DtrTile.Add(sources[a].sr_Sand[dust].name, sources[a].sr_Sand[dust]);
            }
            for (int dust = 0; dust < sources[a].sr_Winter.Length; dust++)
            {
                if (!DtrTile.ContainsKey(sources[a].sr_Winter[dust].name))
                    DtrTile.Add(sources[a].sr_Winter[dust].name, sources[a].sr_Winter[dust]);
            }
        }
		Debug.Log(DtrTile.Count);
    }

    void GeneratorMap()
	{
		map = new int[size.x,size.y];
		
		RandomFillMap();

		for (int i = 0; i < 5; i ++) {

            SmoothMap();

        }
		ProcessMap();
		if(PlayerPrefs.GetString("DataGround","").Length<=0)
		CreateTerria();
		else
		{
			string path = PlayerPrefs.GetString("DataGround");
			DataGround[] data = JsonHelper.FromJson<DataGround>(path);
			LoadObj(data);
			GameObject.FindObjectOfType<SaveGame>().loadObj();
		}
		
		Draw();
		//DrawMap();
	}
	void Draw()
	{
		for(int x = 0; x < size.x; x++)
		{
			for(int y = 0; y < size.y; y++)
			{
				if(map[x,y] != 1)
				switch (map[x,y])
				{
					case 0:
					tilemap.SetTile(new Vector3Int(x,y,0),tileDust);
					break;
					case 2:
					tilemap.SetTile(new Vector3Int(x,y,0),tileWinter);
					break;
					case 3:
					tilemap.SetTile(new Vector3Int(x,y,0),tileSand);
					break;
				}
			}
		}
	}
	void RandomFillMap() 
	{	
        if (useRandomSeed && seed == "") {
            //seed = Time.time.ToString();
			seed = Random.Range(0,10000).ToString();
        }

      		 System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < size.x; x ++) {
            for (int y = 0; y < size.y ; y ++) {     
               
                    map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
					
            }
        }
    }
 

	void SmoothMap() {
        for (int x = 0; x < size.x; x ++) {
            for (int y = 0; y < size.y; y ++) {
                int neighbourWallTiles = GetSurroundingWallCount(x,y);

                if (neighbourWallTiles > 4)
                    map[x,y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x,y] = 0;

            }
        }
    }
	int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
                if (neighbourX >= 0 && neighbourX < size.x && neighbourY >= 0 && neighbourY < size.y) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += map[neighbourX,neighbourY];
                    }
                }
                else {
                    wallCount ++;
                }
            }
        }

        return wallCount;
    }
	void CreateTerria()
	{	
		sortMap = new int[size.x,size.y];

		//Debug.Log(sortMap[1,1]);

		for(int a = 0; a < sources.Length; a++)

			for(int x = 0; x < size.x ; x++)
			{
				for(int y = 0; y < size.y; y++)
				{
					if(map[x,y] != 1)
					{
						int randomFillSeason = (Random.Range(0,1000)< sources[a].percent) ? 1:0;
						
						if(randomFillSeason == 1)
						{	
							int limited = sources[a].repeat;
							if(x>limited && y>limited){

							for(int rpx = x-limited; rpx < x; rpx++)
							{
								for(int rpy = y-limited; rpy < y; rpy++)
								{	
									int randomSc = Random.Range(0,100) < sources[a].noise ? 1:0;
									
									if(randomSc == 1)
			
									if(sources[a].canReplace == true ){
										
											
											Vector3 pos = new Vector3( disTile.x*rpx , rpy* disTile.y , 0)+transform.position;
											
											Transform go = null;
											
										switch(map[rpx,rpy])
										{
											case 0:
											if(sources[a].sr_Dust.Length != 0)
											go = sources[a].sr_Dust[Random.Range(0,sources[a].sr_Dust.Length)];
											break;
											
											case 2:
											if(sources[a].sr_Winter.Length != 0)
											go = sources[a].sr_Winter[Random.Range(0,sources[a].sr_Winter.Length)];
											break;

											case 3:
											if(sources[a].sr_Sand.Length != 0)
											go = sources[a].sr_Sand[Random.Range(0,sources[a].sr_Sand.Length)];
											break;
										}	
										if(go != null)
										{
											Transform tile =  Instantiate(go,pos,Quaternion.identity);
											
											SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();				
										
											sr.sortingOrder = -(int)(tile.position.y*100);

											tile.SetParent(allObject);
											data.Add(new DataGround(go.name,sources[a].layer,pos));	
										}
										
											
											
										
									}else {
										if(sortMap[rpx,rpy]== 0 && map[rpx,rpy]!=1)
										{
											
											Vector3 pos = new Vector3( disTile.x*rpx + Random.Range(-sources[a].Range,sources[a].Range), rpy* disTile.y + Random.Range(-0.2f,0.2f) , 0)+transform.position;
										
											Transform go = null;
											
										switch(map[rpx,rpy])
										{
											case 0:
											if(sources[a].sr_Dust.Length != 0)
											go = sources[a].sr_Dust[Random.Range(0,sources[a].sr_Dust.Length)];
											break;
											
											case 2:
											if(sources[a].sr_Winter.Length != 0)
											go = sources[a].sr_Winter[Random.Range(0,sources[a].sr_Winter.Length)];
											break;

											case 3:
											if(sources[a].sr_Sand.Length != 0)
											go = sources[a].sr_Sand[Random.Range(0,sources[a].sr_Sand.Length)];
											break;
										}	
											if(go!= null){
																													
												Transform tile =  Instantiate(go,pos,Quaternion.identity);

												SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();

												sr.sortingOrder = -(int)(tile.position.y*100);

												sr.sortingLayerName = sources[a].layer;

												tile.SetParent(allObject);												
												
												sortMap[rpx,rpy]= 1;
												
												map[rpx,rpy] = sources[a].useBase ? 1:map[rpx,rpy];
											
											}
											
											
											
										}
									}
									
								}
							}
						}
					
							
						}
					}
				}
			}
	}
	int getSeason(int SeasonCount)
	{	int x= 1;
		while (x==1)
		{			
			x = random.Next(0,SeasonCount);			
		}
		Debug.Log(x) ;
		return x;
	}
	void ProcessMap() {
		// List<List<Coord>> wallRegions = GetRegions (1);
		// int wallThresholdSize = 50;

		// foreach (List<Coord> wallRegion in wallRegions) {
		// 	if (wallRegion.Count < wallThresholdSize) {
		// 		foreach (Coord tile in wallRegion) {
		// 			map[tile.tileX,tile.tileY] = 0;
		// 		}
		// 	}
		// }

		List<List<Coord>> roomRegions = GetRegions (0);
		random = new System.Random(seed.GetHashCode());
		foreach (List<Coord> roomRegion in roomRegions) {
				int rdSeason = getSeason(numberOfSeason+1);
				foreach (Coord tile in roomRegion) {
					map[tile.tileX,tile.tileY] = rdSeason;
				}
			
		}
	}

	List<List<Coord>> GetRegions(int tileType) {
		List<List<Coord>> regions = new List<List<Coord>> ();
		int[,] mapFlags = new int[size.x,size.y];

		for (int x = 0; x < size.x; x ++) {
			for (int y = 0; y < size.y; y ++) {
				if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
					List<Coord> newRegion = GetRegionTiles(x,y);
					regions.Add(newRegion);

					foreach (Coord tile in newRegion) {
						mapFlags[tile.tileX, tile.tileY] = 1;
					}
				}
			}
		}

		return regions;
	}

	List<Coord> GetRegionTiles(int startX, int startY) {
		List<Coord> tiles = new List<Coord> ();
		int[,] mapFlags = new int[size.x,size.y];
		int tileType = map [startX, startY];

		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (new Coord (startX, startY));
		mapFlags [startX, startY] = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();
			tiles.Add(tile);

			for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
				for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
					if (IsInMapRange(x,y) && (y == tile.tileY || x == tile.tileX)) {
						if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
							mapFlags[x,y] = 1;
							queue.Enqueue(new Coord(x,y));
						}
					}
				}
			}
		}

		return tiles;
	}

	bool IsInMapRange(int x, int y) {
		return x >= 0 && x < size.x && y >= 0 && y < size.y;
	}
	struct Coord {
		public int tileX;
		public int tileY;

		public Coord(int x, int y) {
			tileX = x;
			tileY = y;
		}
	}
	public void SaveTile(string seed)
	{
		PlayerPrefs.SetString("seed",seed);
	}
	void LoadTile(string seed)
	{
		if(this.seed == "")
		this.seed = seed;
	}
	public void saveObj()
	{
		for(int x = 0; x < allObject.childCount; x++)
		{
			SpriteRenderer sr = allObject.GetChild(x).GetComponent<SpriteRenderer>();
			string layer = sr.sortingLayerName;
			Vector2 pos = sr.transform.position;
			string name = sr.gameObject.name.Remove(sr.gameObject.name.Length-7);
			data.Add(new DataGround(name,layer,pos));
		}
		
		string jsonData = JsonHelper.ToJson<DataGround>(data.ToArray());
		PlayerPrefs.SetString("DataGround",jsonData);
		SaveTile(this.seed);
		
	}
	public void LoadObj(DataGround[] datas)
	{
		for(int x = 0; x< datas.Length;x++)
		{
			Transform tile = DtrTile[datas[x].name.ToString()];
			Vector2 pos = datas[x].position;
			string layer = datas[x].layer;
			Transform tiles = Instantiate(tile,pos,Quaternion.identity);
			SpriteRenderer sr = tiles.GetComponent<SpriteRenderer>();
			sr.sortingLayerName = layer;
			sr.sortingOrder = -(int)(pos.y*100);
			tiles.SetParent(allObject);	
		}
	}
	// void OnDrawGizmos() {
    //     if (map != null) {
	// 		int sao= 0;
    //         for (int x = 0; x < size.x; x ++) {
    //             for (int y = 0; y < size.y; y ++) {
    //                 if(y-1 >= size.y/2-x+1&& x+size.x/2 >= y && x<=y+size.x/2&& y-size.x/2<size.x-x+1)
	// 				{
	// 					Vector3 pos = new Vector3(x,y+sao,0);
						
    //                 Gizmos.DrawCube(pos,Vector3.one);
	// 				}
                    
    //             }
    //         }
    //     }
    // }
}
