using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
	public List<GameObject> roads;
	public Transform lastRoad;
	public Transform newRoad;
	public float spawnDelay = 1f;
    // Start is called before the first frame update
	IEnumerator Start()
    {
		while (true) {
			newRoad = Instantiate (roads [Random.Range(0,5)], lastRoad.position, lastRoad.rotation 	* Quaternion.Euler(180f*Random.Range(0,2), 0f, 0f)).GetComponent<Transform> ();
			lastRoad = newRoad.GetChild(0).Find("RoadEnd");
			yield return new WaitForSeconds (spawnDelay);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
