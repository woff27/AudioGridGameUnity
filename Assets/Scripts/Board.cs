using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	private int boardW = 7; //used for initial board width 
	private int boardH = 7; //used for initial board height 
	private float bX; //position in x of grid blocks
	private float bY; //posiiton in y of grid blocks
	private int i; //for loop grid creation, width
	private int v; //for loop board creation, height
	public GameObject grid; //Prefab (set in inspector)
	public Material sSquare; //Green material for starting square
	public Material BlankGrid; // blank one
	public int startLimit; //How many times you can click to create a start block
	private int startCount; //Current amount of start blocks
	public Vector3[,] gridArray = new Vector3[7,7]; //2D array for newgrid positions
	public GameObject[,] gridList = new GameObject[7,7]; //2d array for gameobjects themselves
	public GameObject[,] lightList = new GameObject[7,7];
	public Component[,] scriptList = new Component[7,7]; //2d array for the scripts in each cell
	
	public Material GridChild;

	private GameObject newgrid; //instantiated prefab
	private GameObject SpotL; // Light
	public GameObject SpotLight;
	private GameObject SLight;
	private int Count; //for tag iteration
	private int time = 0;

	private Color lerpColor = Color.Lerp (Color.cyan, Color.green, 1f);
	
	public void MouseDown()
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
							//hit.collider.renderer.material = sSquare;
							hit.transform.tag = "sSquare";
							StateScript SS = (StateScript) hit.transform.GetComponent("StateScript");
							SS.Cell.up = SS.Cell.down = SS.Cell.left = SS.Cell.right = true;
							
							startCount++; 
						}
					}
				}
			}
		}
	}

	public void BoardCreate()
	{
		//variable setting
		Count = 1;
		startCount = 0;
		bX=-3.0f;
		bY=-1.5f;
		//Board creation for loop
		for(v=0; v < boardH; v++)
		{
			for(i=0; i < boardW; i++)
			{
				//moves block over
				bX+=1.5f;
				//creating prefab in scene, tags for later ID purposes
				newgrid = (GameObject)Instantiate(grid, new Vector3 (bX,bY,0), Quaternion.identity);
				//newgrid.transform.tag = Count.ToString();
				newgrid.transform.localScale = new Vector3(1,1,0.5f);
				//Arrays for positions and GameObjects
				gridList[i,v] = newgrid;
				StateScript SS = (StateScript)gridList[i,v].transform.GetComponent("StateScript");

				foreach(Transform child in gridList[i,v].transform)
				{
					lightList[i,v] = child.gameObject;
				}



				Count++;
			}
			//moves block up, reset x position of board creation
			bY+=1.5f;
			bX=-3.0f;
		}

	}

	public void tickCells()
	{
		//State switching
		for(int x = 0; x<7; x++)
		{
			for(int y = 0; y<7; y++)
			{
				StateScript SS = (StateScript)gridList[x,y].transform.GetComponent<StateScript>();
				SS.Cell.oldup = SS.Cell.up;
				SS.Cell.olddown = SS.Cell.down;
				SS.Cell.oldleft = SS.Cell.left;
				SS.Cell.oldright = SS.Cell.right;

//				if(SS.Cell.up && SS.Cell.down && SS.Cell.left && SS.Cell.right == false)
//				{
//					gridList[x,y].collider.renderer.material = BlankGrid;
//				}

				if(SS.Cell.up || SS.Cell.down || SS.Cell.left || SS.Cell.right == true)
				{
					gridList[x,y].collider.renderer.material = GridChild;
					lightList[x,y].SetActive(true);
				}

				if(gridList[x,y].transform.tag == "sSquare")
				{
					if(gridList[x,y].collider.renderer.material != sSquare)
					{
						SS.Cell.up = SS.Cell.down = SS.Cell.left = SS.Cell.right = true;
					}
					gridList[x,y].collider.renderer.material = sSquare;
					lightList[x,y].SetActive(true);
					lightList[x,y].light.color = lerpColor;

				}

				SS.Cell.up = SS.Cell.down = SS.Cell.left = SS.Cell.right = false;
			}
		}

		//Check other cells
		for(int x = 0; x<7; x++)
		{
			for(int y = 0; y<7; y++)
			{
				gridList[x,y].GetComponent<StateScript>().Surround(x,y);
			}
		}
	}
	
	public void TimedCellCheck()
	{
		time++;
		if(time>30)
		{
			for(int x = 0; x<7; x++)
			{
				for(int y = 0; y<7; y++)
				{
					if(gridList[x,y].collider.renderer.material != sSquare)
					{
						gridList[x,y].collider.renderer.material = BlankGrid;
						lightList[x,y].SetActive(false);
					}
				}
			}

			time = 0;
			tickCells();
		}
	}

	public void Flashing()
	{
		for(int x = 0; x<7; x++)
		{
			for(int y = 0; y<7; y++)
			{
				lightList[x,y].light.intensity = Random.Range(0,10);
			}
		}
	}

	void Start ()
	{
		BoardCreate();
	}

	void Update () 
	{
		MouseDown();
		TimedCellCheck();
		Flashing();
	}
}
