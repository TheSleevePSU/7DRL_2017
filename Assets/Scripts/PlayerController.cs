using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Vector3 targetPosition;
	public Vector3 startPosition;
	private float startTime;
	private float journeyLength;

	public float speed = 1.0f;
	public float minDistance = 0.05f;
	
	// Use this for initialization
	void Start () {
		targetPosition = this.transform.position;
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateControl();
		UpdateMovement();
	}

	void UpdateControl()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow)
		||  Input.GetKeyDown(KeyCode.DownArrow)
		||  Input.GetKeyDown(KeyCode.LeftArrow)
		||  Input.GetKeyDown(KeyCode.RightArrow)
		   )
		{
			startPosition = transform.position;
			startTime = Time.time;
			
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				targetPosition = transform.position + new Vector3(0, 1, 0);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				targetPosition = transform.position + new Vector3(0, -1, 0);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				targetPosition = transform.position + new Vector3(-1, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				targetPosition = transform.position + new Vector3(1, 0, 0);
			}

			journeyLength = Vector3.Distance(targetPosition, startPosition);
		}
	}

	void UpdateMovement()
	{
		if (Vector3.Distance(startPosition, targetPosition) <= minDistance)
		{
			transform.position = targetPosition;
			startPosition = targetPosition;
		}
		else
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startPosition, targetPosition, fracJourney);
		}
	}
}
