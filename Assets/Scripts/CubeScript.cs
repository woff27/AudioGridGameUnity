using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	private GameObject gridCurrent;
	public GameObject[,] gridList; 
	public Material sSquare; 
	private GameObject greenSpot; //instantiated spotlight for start block
	public GameObject Spotlight; //spawns on each grid block (in Inspector)
	private int x;
	private int y;
	private int SpotCount = 0;
	private float SpotIntensity;

	void Start ()
	{
		Board gC = (Board)GameObject.Find("Controller").GetComponent("Board");
		gridList = gC.gridList;
	}

	void Update ()
	{
		for(x=0; x<=4; x++)
		{
			for(y=0; y<=4; y++)
			{
				if (gridList[x,y] == this.gameObject)
				{
					if(this.gameObject.GetComponentInChildren<Light>())
					{
						this.gameObject.GetComponentInChildren<Light>().intensity = Random.Range(-10f,10f);
					}

					if(this.gameObject.transform.tag == "sSquare")
					{
						if(SpotCount < 1)
						{
							greenSpot = (GameObject) Instantiate(Spotlight, this.gameObject.collider.transform.position + new Vector3(0,0,-4), Quaternion.identity);
							greenSpot.transform.parent = this.gameObject.transform;

							if(x<4)
							{
								gridList[x+1,y].collider.renderer.material = sSquare;
								greenSpot = (GameObject) Instantiate(Spotlight, gridList[x+1,y].collider.transform.position + new Vector3(0,0,-4), Quaternion.identity);
								greenSpot.transform.parent = gridList[x+1,y].transform;
							}
							if(y<4)
							{
								gridList[x,(y+1)].collider.renderer.material = sSquare;
								greenSpot = (GameObject) Instantiate(Spotlight, gridList[x,(y+1)].collider.transform.position + new Vector3(0,0,-4), Quaternion.identity);
								greenSpot.transform.parent = gridList[x,(y+1)].transform;
							}
							if(x>0)
							{
								gridList[(x-1),y].collider.renderer.material = sSquare;
								greenSpot = (GameObject) Instantiate(Spotlight, gridList[(x-1),y].collider.transform.position + new Vector3(0,0,-4), Quaternion.identity);
								greenSpot.transform.parent = gridList[(x-1),y].transform;
							}
							if(y>0)
							{
								gridList[x,(y-1)].collider.renderer.material = sSquare;
								greenSpot = (GameObject) Instantiate(Spotlight, gridList[x,(y-1)].collider.transform.position + new Vector3(0,0,-4), Quaternion.identity);
								greenSpot.transform.parent = gridList[x,(y-1)].transform;
							}
							SpotCount++;
						}
					}
				}
			}
		}
	}
}
