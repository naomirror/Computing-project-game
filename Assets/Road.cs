using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
	public Transform extraRoadBegin;
	public Transform roadBegin;
	public Transform road15Begin;
	public Transform road45Begin;
	public Transform roadUTurnBegin;
	public Transform roadEnd;
	public Transform road15End;
	public Transform road45End;
	public Transform roadUTurnEnd;
	public GameObject road;
	public Transform lastRoadEnd;
    // Start is called before the first frame update
    void Start()
    {
		extraRoadBegin = GameObject.Find ("Road15Anchor (1)").GetComponent<Transform>();
		roadBegin = this.transform;
		road15Begin = GameObject.Find ("Road15Anchor").GetComponent<Transform> ();
		road15End = road15Begin.transform.Find ("road15").Find("RoadEnd");
		road45Begin = GameObject.Find ("Road45Anchor").GetComponent<Transform> ();
		road45End = road45Begin.transform.Find ("road 45").Find("RoadEnd");
		roadUTurnBegin = GameObject.Find ("RoadUTurnAnchor").GetComponent<Transform> ();
		roadUTurnEnd = roadUTurnBegin.transform.Find ("road u turn").Find ("RoadEnd");
		roadEnd = this.transform.Find ("road 30").Find("RoadEnd");
		road = GameObject.Find ("road");
		lastRoadEnd = road.GetComponent<Transform>().Find ("RoadEnd");

	
    }

    // Update is called once per frame
    void Update()
    {
		roadBegin.position = lastRoadEnd.position;
		roadBegin.rotation = lastRoadEnd.rotation;
		road15Begin.position = roadEnd.position;
		road15Begin.rotation = roadEnd.rotation;
		road45Begin.position = road15End.position;
		road45Begin.rotation = road15End.rotation;
		roadUTurnBegin.position = road45End.position;
		roadUTurnBegin.rotation = road45End.rotation;
		extraRoadBegin.position = roadUTurnEnd.position;
		extraRoadBegin.rotation = roadUTurnEnd.rotation;











    }
}
