using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoadManager : MonoBehaviour
{
	public Scores score;
	public List<GameObject> roads, roadSpawn, roadPick;
	public GameObject tree;
	public GameObject collectible;
	public GameObject roadBlock;
	public Transform lastRoad;
	public Transform newRoad;
	public float spawnDelay = 1f;
	int roadToSpawnIncrement = 0;
	bool first = true;
	CollisionDetection roadCollisionScript;
	Renderer r;
	// Start is called before the first frame update
	IEnumerator Start()
	{
		score = GameObject.Find ("Tocus").GetComponent<Scores> ();
		setRoadProbabilities (8);
		addRoadSegmentsToSpawn ();
		roadCollisionScript = lastRoad.parent.GetComponent<CollisionDetection>();
		while (true) {
			if (first == true && (score.seconds == 30 || score.seconds == 59)) {
				setRoadProbabilities (score.averageSpeed);
				addRoadSegmentsToSpawn ();
				first = false;
				Debug.Log ("Populating at:" + score.minutes + ":" + score.seconds);
			}
			if (score.seconds != 30 && score.seconds != 59) {
				first = true;
			}
			if (roadToSpawnIncrement <= roadSpawn.Count - 1) {
					newRoad = Instantiate (roadSpawn [roadToSpawnIncrement], lastRoad.position, lastRoad.rotation * Quaternion.Euler (180f * Random.Range (0, 2), 0f, 0f)).GetComponent<Transform> ();
					lastRoad = newRoad.GetChild (0).Find ("RoadEnd");
					roadToSpawnIncrement += 1;
					Debug.Log ("InstantiatingRoad");
					if (newRoad.GetChild (0).name == "road") {
						r = newRoad.GetChild (0).GetChild (0).GetComponent<Renderer> ();
					} else {
						r = newRoad.GetChild (0).GetComponent<Renderer> ();
					}
				bool hasHit = false;
				float randomX;
				float randomZ;
				RaycastHit hit;
				while (!hasHit){
					randomX = Random.Range (r.bounds.min.x, r.bounds.max.x);
					randomZ = Random.Range (r.bounds.min.z, r.bounds.max.z);

					if (Physics.Raycast (new Vector3 (randomX, r.bounds.max.y + 5f, randomZ), -Vector3.up, out hit)) {
						GameObject collectibleSpawn = Instantiate (collectible, new Vector3 (randomX, r.bounds.max.y + 1f, randomZ), Quaternion.identity);
						//collectibleSpawn.transform.parent = newRoad.GetChild (0).transform;
						//GameObject treeSpawn =Instantiate (tree, new Vector3 (randomX, r.bounds.max.y, randomZ), Quaternion.Euler(-90f,0f,0f));
						//treeSpawn.transform.parent = newRoad.GetChild (0).transform;
						hasHit = true;
					} 
		}
				if (Random.Range (0, 100) < treeProbability) {
					bool hasHitTree = false;
					while (!hasHitTree) {
						randomX = Random.Range (r.bounds.min.x, r.bounds.max.x);
						randomZ = Random.Range (r.bounds.min.z, r.bounds.max.z);

		
						if (Physics.Raycast (new Vector3 (randomX, r.bounds.max.y + 5f, randomZ), -Vector3.up, out hit)) {
							GameObject treeSpawn = Instantiate (tree, new Vector3 (randomX, r.bounds.max.y, randomZ), Quaternion.Euler (-90f, 0f, 0f));
							treeSpawn.transform.parent = newRoad.GetChild (0).transform;
							hasHitTree = true;
						} 
					}
				}
					Debug.Log ("Increment: " + roadToSpawnIncrement);
					roadCollisionScript = newRoad.GetChild (0).GetComponent<CollisionDetection> ();
				yield return new WaitForSeconds (0.02f);
				if (roadCollisionScript.colliding == true) {
					//newRoad.gameObject.active = false;
					Debug.Log ("COLLISION DETECTED");
					if (newRoad.GetChild (0).name == "road") {
						newRoad.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
					} else {
						newRoad.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = false;
					}
					while (roadCollisionScript.colliding == true) {
						Debug.Log ("Waiting on collision to finish at" + score.minutes + ":" + score.seconds);
						bool wait = false;
						foreach (GameObject road in roadCollisionScript.intersectingRoad) {
							if (road.tag != "FinishedContact") {
								wait = true;
							}
						}
						if (wait == false) {
							Debug.Log ("Destroying roads");
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
						if (first == true && (score.seconds == 30 || score.seconds == 59)) {
							setRoadProbabilities (score.averageSpeed);
							addRoadSegmentsToSpawn ();
							first = false;
							Debug.Log ("Populating at:" + score.minutes + ":" + score.seconds);
						}
						if (score.seconds != 30 && score.seconds != 59) {
							first = true;
						}
						yield return new WaitForSeconds (1f);
					}
				}
			}
			yield return new WaitForSeconds (0.5f);
		} 

		/*
		int x = 5;
		Renderer r;
		while (true) {
			//newRoad = Instantiate (roads [0], lastRoad.position, lastRoad.rotation * Quaternion.Euler (180f * Random.Range (0, 2), 0f, 15f)).GetComponent<Transform> ();
			//lastRoad = newRoad.GetChild(0).Find("RoadEnd");
			//lastRoad.rotation = lastRoad.rotation * Quaternion.Euler (0f, 0f, -15f);
			//yield return new WaitForSeconds (spawnDelay);

			newRoad = Instantiate (roads [Random.Range(0,x)], lastRoad.position, lastRoad.rotation 	* Quaternion.Euler(180f*Random.Range(0,2), 0f, 0f)).GetComponent<Transform> ();
			lastRoad = newRoad.GetChild(0).Find("RoadEnd");
			Debug.Log (newRoad.GetChild (0).name + " coordinates: " + newRoad.GetChild (0).Find ("RoadEnd").transform.eulerAngles);
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
				GameObject collectibleSpawn =Instantiate (collectible, new Vector3 (randomX, r.bounds.max.y + 1f, randomZ), Quaternion.identity);
				//collectibleSpawn.transform.parent = newRoad.GetChild (0).transform;
				//GameObject treeSpawn =Instantiate (tree, new Vector3 (randomX, r.bounds.max.y, randomZ), Quaternion.Euler(-90f,0f,0f));
				//treeSpawn.transform.parent = newRoad.GetChild (0).transform;
			} 
			yield return new WaitForSeconds (spawnDelay);
		}
	*/
	}

	int a = 0, b = 0, c = 0, d = 0, e = 0, treeProbability;
	void setRoadProbabilities(float averageSpeed){
		roadPick.Clear ();
		if (averageSpeed <= 8) {
			a = 50;
			b = 50;
			c = 0;
			d = 0;
			e = 0;
			treeProbability = 0;
		} else if (averageSpeed > 8 && averageSpeed < 10) {
			a = 34;
			b = 33;
			c = 33;
			d = 0;
			e = 0;
			treeProbability = 5;
		}
		else if (averageSpeed > 10 && averageSpeed < 12) {
			a = 20;
			b = 30;
			c = 30;
			d = 20;
			e = 0;
			treeProbability = 10;
		}
		else if (averageSpeed > 12 && averageSpeed < 14) {
			a = 20;
			b = 20;
			c = 30;
			d = 15;
			e = 15;
			treeProbability = 15;
		}
		else if (averageSpeed > 14 && averageSpeed < 16) {
			a = 15;
			b = 20;
			c = 25;
			d = 20;
			e = 20;
			treeProbability = 20;
		}
		else if (averageSpeed > 16 && averageSpeed < 18) {
			a = 10;
			b = 15;
			c = 25;
			d = 25;
			e = 25;
			treeProbability = 25;
		}
		else if (averageSpeed > 18 && averageSpeed < 20) {
			a = 10;
			b = 10;
			c = 20;
			d = 30;
			e = 30;
			treeProbability = 30;
		}
		else if (averageSpeed > 20) {
			a = 0;
			b = 10;
			c = 30;
			d = 30;
			e = 30;
			treeProbability = 35;
		}

		addToRoadPick (a, 0);
		addToRoadPick (b, 1);
		addToRoadPick (c, 2);
		addToRoadPick (d, 3);
		addToRoadPick (e, 4);
	}
	void addToRoadPick(int amount, int roadType){
		for (int i = 0; i < amount; i++) {
			roadPick.Add (roads [roadType]);
		}
	}

	void addRoadSegmentsToSpawn(){
		for (int i = 0; i < 10; i++) {
			if (roadSpawn.Count > 0 && roadSpawn [roadSpawn.Count - 1].transform.GetChild (0).name == "road u turn") {
				roadSpawn.Add (roadPick [Random.Range (0, 100 - e)]);
			} else {
				roadSpawn.Add (roadPick [Random.Range (0, 100)]);
			}
		}
		Debug.Log (roadSpawn.Count);
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (0.5f);
	}
	// Update is called once per frame
	void Update()
	{
			
	}
}
