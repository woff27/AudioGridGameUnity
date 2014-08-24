using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	public int boardW; //used for initial board width (in Inspector)
	public int boardH; //used for initial board height (in Inspector)
	private float bX; //position in x of grid blocks
	private float bY; //posiiton in y of grid blocks
	private int i; //for loop grid creation, width
	private int v; //for loop board creation, height
	public GameObject grid; //Prefab (set in inspector)
	public Material sSquare; //Green material for starting square
	public int startLimit; //How many times you can click to create a start block
	private int startCount; //Current amount of start blocks
	public Vector3[,] gridArray = new Vector3[5,5]; //2D array for newgrid positions
	public GameObject[,] gridList = new GameObject[5,5]; //2d array for gameobjects themselves
	public GameObject newgrid; //instantiated prefab
	private int Count; //for tag iteration
	
	void Awake () 
	{
		//variable setting
		Count = 1;
		startCount = 0;
		bX=0;
		bY=0;
		//Board creation for loop
		for(v=0; v<=boardH-1; v++)
		{
			for(i=0; i<=boardW-1; i++)
			{
				//moves block over
				bX+=1.5f;
				//creating prefab in scene, tags for later ID purposes
				newgrid = (GameObject)Instantiate(grid, new Vector3 (bX,bY,0), Quaternion.identity);
				newgrid.transform.tag = Count.ToString();
				newgrid.transform.localScale = new Vector3(1,1,0.5f);
				//Arrays for positions and GameObjects
				gridArray[i,v] = new Vector3(bX,bY,0);
				gridList[i,v] = newgrid;
				//count for tags and Debug logs to check Arrays
				Count++;
				//Debug.Log(gridList[i,v]);
				//Debug.Log(gridArray[i,v]);
			}
		//moves block up, reset x position of board creation
		bY+=1.5f;
		bX=0;
		}
	}

	void Start ()
	{
		//nothing yet
	}

	void Update () 
	{
		//check if too many start blocks are on
		if(startCount < startLimit)
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				//raycast to grid blocks
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit))
				{
					if(hit.collider.name == "BackgroundCube(Clone)") 
					{
						if(hit.transform.tag != "sSquare")
						{
							//change to green material for start square
							hit.collider.renderer.material = sSquare;
							hit.transform.tag = "sSquare";
							startCount++; 
						}
					}
				}
			}
		}
		//Checking for square and changing other squares
//		for (int x = 0; x < 5; x++)
//		{
//			for (int y = 0; y < 5; y++)
//			{
//				if(gridList[x,y].transform.tag == "sSquare")
//				{
//					if(x<4)
//					{
//						gridList[x+2,y].collider.renderer.material = sSquare;
//					}
//					if(y<4)
//					{
//						gridList[x,(y+2)].collider.renderer.material = sSquare;
//					}
//					if(x>0)
//					{
//						gridList[(x-2),y].collider.renderer.material = sSquare;
//					}
//					if(y>0)
//					{
//						gridList[x,(y-2)].collider.renderer.material = sSquare;
//					}
//				}
//			}
//		}
	}
}
