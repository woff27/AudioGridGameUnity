using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	private GameObject gridCurrent;
	public GameObject[,] gridList; 
	public Material sSquare; 
	private int x;
	private int y;
	
	void Start ()
	{
		Board gC = (Board)GameObject.Find("Controller").GetComponent("Board");
		gridList = gC.gridList;
	}

}
