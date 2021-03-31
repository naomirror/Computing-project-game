using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
	public List<GameObject> roads;
	public GameObject tree;
	public GameObject collectible;
	public GameObject roadBlock;
	public Transform lastRoad;
	public Transform newRoad;
	public float spawnDelay = 1f;
    // Start is called before the first frame update
	IEnumerator Start()
    {
		int x = 5;
		Renderer r;
		while (true) {
			/*newRoad = Instantiate (roads [0], lastRoad.position, lastRoad.rotation * Quaternion.Euler (180f * Random.Range (0, 2), 0f, 15f)).GetComponent<Transform> ();
			lastRoad = newRoad.GetChild(0).Find("RoadEnd");
			lastRoad.rotation = lastRoad.rotation * Quaternion.Euler (0f, 0f, -15f);
			yield return new WaitForSeconds (spawnDelay);
*/
			newRoad = Instantiate (roads [Random.Range(0,x)], lastRoad.position, lastRoad.rotation 	* Quaternion.Euler(180f*Random.Range(0,2), 0f, 0f)).GetComponent<Transform> ();
			lastRoad = newRoad.GetChild(0).Find("RoadEnd");
			if (newRoad.GetChild(0).name == "road u turn") {
				x = 4;
			} else {
				x = 5;
			}
			yield return new WaitForSeconds (0.02f);
			CollisionDetection roadCollisionScript = newRoad.GetChild(0).GetComponent<CollisionDetection> ();
			if (roadCollisionScript.colliding == true) {
				//newRoad.gameObject.active = false;
				if (newRoad.GetChild (0).name == "road") {
					newRoad.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
				} else {
					newRoad.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = false;
				}
				while (roadCollisionScript.colliding == true) {
					bool wait = false;
					foreach (GameObject road in roadCollisionScript.intersectingRoad) {
						if (road.tag != "FinishedContact") {
							wait = true;
						}
					}
					if (wait == false) {
						foreach (GameObject road in roadCollisionScript.intersectingRoad) {
							Destroy (road.transform.parent.gameObject);
						}
						roadCollisionScript.colliding = false;
						if (newRoad.GetChild (0).name == "road") {
							newRoad.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
						} else {
							newRoad.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = true;
						}
					}
					yield return new WaitForSeconds (3f);
				}
			}
			if (newRoad.GetChild (0).name == "road") {
				 r = newRoad.GetChild (0).GetChild (0).GetComponent<Renderer> ();
			} else {
				r = newRoad.GetChild (0).GetComponent<Renderer> ();
			}

			float randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
			float randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);

			RaycastHit hit;
			if (Physics.Raycast(new Vector3(randomX, r.bounds.max.y + 5f, randomZ), -Vector3.up, out hit)) {
				//GameObject treeSpawn =Instantiate (tree, new Vector3 (randomX, r.bounds.max.y, randomZ), Quaternion.Euler(-90f,0f,0f));
				//treeSpawn.transform.parent = newRoad.GetChild (0).transform;
			} 
			yield return new WaitForSeconds (spawnDelay);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
