using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
	public List<GameObject> roads;
	public Transform lastRoad;
	public Transform newRoad;
    // Start is called before the first frame update
	IEnumerator Start()
    {
		while (true) {
			newRoad = Instantiate (roads [Random.Range(0,6)], lastRoad.position, lastRoad.rotation 	* Quaternion.Euler(180f*Random.Range(0,2), 0f, 0f)).GetComponent<Transform> ();
			lastRoad = newRoad.GetChild(0).Find("RoadEnd");
			yield return new WaitForSeconds (3f);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
