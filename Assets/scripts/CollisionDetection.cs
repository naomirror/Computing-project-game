using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnCollisionEnter(Collision col){

		if (this.transform.parent.position != col.transform.Find ("RoadEnd").position && this.transform.Find("RoadEnd").position != col.transform.parent.position  &&this.transform.parent.rotation != col.transform.Find ("RoadEnd").rotation && this.transform.Find("RoadEnd").rotation != col.transform.parent.rotation) {
			Debug.Log (col.gameObject.name);
		}

	}
		
}