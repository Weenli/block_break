using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class Shape : MonoBehaviour {
	public bool can_rotate = true;
	public Vector3 QueueOffset;
	public void Move(Vector3 moveDirection)
    {
		transform.position += moveDirection; 
    }
	public void MoveLeft()
    {
		Move(new Vector3(-1, 0, 0));
    }
	
	public void MoveRight()
    {
		Move(new Vector3(1, 0, 0));
    }
	public void MoveDown()
    {
		//await Task.Delay(280 * 10);
		Move(new Vector3(0, -1, 0));
    }
	public void MoveUp()
    {
		Move(new Vector3(0, 1, 0));
    }
	public void RotateRight()
    {
		if (can_rotate)
		{
			transform.Rotate(0, 0, -90);
		}
    }
	public void RotateLeft()
	{
		if (can_rotate)
		{
			transform.Rotate(0, 0, 90);
		}
	}
	public void RotateClockwise(bool clockwise)
	{
		if (clockwise)
		{
			RotateRight();
		}
		else
		{
			RotateLeft();
		}
	}
	void Start()
    {

	}
	void Update()
    {

    }
}
