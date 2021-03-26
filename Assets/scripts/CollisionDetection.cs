using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
	public GameObject intersectingRoad;
	public bool colliding = false;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnCollisionEnter(Collision col){
		
		if (Mathf.Round(this.transform.parent.position.x) != Mathf.Round(col.transform.Find ("RoadEnd").position.x) && Mathf.Round(this.transform.parent.position.z) != Mathf.Round(col.transform.Find ("RoadEnd").position.z) && Mathf.Round(this.transform.Find("RoadEnd").position.x) != Mathf.Round(col.transform.parent.position.x) && Mathf.Round(this.transform.Find("RoadEnd").position.z) != Mathf.Round(col.transform.parent.position.z)) {
			//Destroy (col.gameObject);
			colliding = true;
			intersectingRoad = col.gameObject;
			Debug.Log (colliding);
			Debug.Log ("Colliding With: " + intersectingRoad.name);
			//Debug.Log (this.transform.parent.name + "Begin Position: " + this.transform.parent.position + " against " + col.gameObject.name + "End Position: " + col.transform.Find("RoadEnd").position + "\r\n" + this.transform.parent.name + "End Position: " + this.transform.Find("RoadEnd").position + " against " + col.gameObject.name + "Begin Position: " + col.transform.parent.position);
		}

	}
		
}