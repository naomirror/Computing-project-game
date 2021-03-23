using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
	public Transform roadBeginning;
	public Transform roadEnd;
    // Start is called before the first frame update
    void Start()
    {
		roadBeginning = this.GetComponent<Transform> ();
		roadEnd = roadBeginning.GetChild(0).Find("RoadEnd");
    }

    // Update is called once per frame
    void Update()
    {


    }

	public void setPosition(Vector3 position, Quaternion rotation){
		roadBeginning.position = position;
		roadBeginning.rotation = rotation;
	}
}
