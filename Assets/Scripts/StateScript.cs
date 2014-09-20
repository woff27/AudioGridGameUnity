using UnityEngine;
using System.Collections;

public class StateScript : MonoBehaviour {

	public Material GridChild;
	public Material BlankGrid;
	public Material sSquare;

	public State Cell = new State();// Cell will be call for State ie Cell.down etc.

	public int x;
	public int y;

	public void Surround(int x, int y)
	{
		Board gC = (Board)GameObject.Find("Controller").GetComponent("Board");
		GameObject[,] gridList = gC.gridList;

		if(y>0)
		{
			StateScript SSup = (StateScript)gridList[x,y-1].GetComponent("StateScript");
			if(SSup.Cell.oldup == true)
			{
				//this.gameObject.collider.renderer.material = GridChild;
				Cell.up = true;
			}
		}
		if(y<6)
		{
			StateScript SSdown = (StateScript)gridList[x,y+1].GetComponent("StateScript");
			if(SSdown.Cell.olddown == true)
			{
				//this.gameObject.collider.renderer.material = GridChild;
				Cell.down = true;
			}
		}
		if(x<6)
		{
			StateScript SSleft = (StateScript)gridList[x+1,y].GetComponent("StateScript");
			if(SSleft.Cell.oldleft == true)
			{
				//this.gameObject.collider.renderer.material = GridChild;
				Cell.left = true;
			}
		}
		if(x>0)
		{
			StateScript SSright = (StateScript)gridList[x-1,y].GetComponent("StateScript");
			if(SSright.Cell.oldright == true)
			{
				//this.gameObject.collider.renderer.material = GridChild;
				Cell.right = true;
			}
		}
	}

	//State of on or off bool for each Cell 
	public class State
	{
		public bool up;
		public bool down;
		public bool left;
		public bool right;

		public bool oldup;
		public bool olddown;
		public bool oldleft;
		public bool oldright;

		public State()
		{
			up = false;
			down = false;
			left = false;
			right = false;

			oldup = false;
			olddown = false;
			oldleft = false;
			oldright = false;
		}
	}	
}
