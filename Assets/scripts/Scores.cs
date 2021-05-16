using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Scores : MonoBehaviour
{
	public GameObject HUD;
	public GameObject leaderboard;
	public Text speedText, collectiblesText, averageSpeedText, timeText;
	public float averageSpeed;
	public int collected = 0, minutes, seconds;
	public CarMovement cm;
	public float time;
    // Start is called before the first frame update
    void Start()
    {
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			time = 300f;
		} else {
			time = Time.timeSinceLevelLoad;
		}
		cm = this.gameObject.GetComponent<CarMovement> ();
    }

	int averageCount = 1;
	float speed = 0;
    // Update is called once per frame
    void Update()
    {
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			time = time - Time.deltaTime;
		} else {
			time = Time.timeSinceLevelLoad;
		}
		minutes = Mathf.FloorToInt (time / 60);
		seconds = Mathf.FloorToInt (time % 60);
		if (seconds == 30 || seconds == 60) {
			averageCount = 1;
			speed = Mathf.Round (cm.carVelocity);
		}
		averageCount += 1;
		speed += Mathf.Round (cm.carVelocity);
		averageSpeed = Mathf.Round(speed / averageCount);

		averageSpeedText.text = "Average Speed: " + averageSpeed;
		speedText.text = "Speed: " + Mathf.Round(cm.carVelocity);
		collectiblesText.text = "Collectibles: " + collected;
		timeText.text = "Time: " + minutes + " mins " + seconds + " seconds";

		if (SceneManager.GetActiveScene ().buildIndex == 2 && minutes == 0 && seconds == 0) {
			Time.timeScale = 0;
			HUD.SetActive (false);
			leaderboard.SetActive (true);
		}
    }

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Collectible(Clone)") {
			collected += 1;
			Destroy (col.gameObject);
		}
	}
}
