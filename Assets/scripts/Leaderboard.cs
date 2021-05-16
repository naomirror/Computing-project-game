using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Leaderboard : MonoBehaviour
{
	public Transform scoreList;
	public Transform scoreEntry;
	public Scores scores;
	float templateHeight = 60f;
	List<LeaderboardEntry> leaderboardEntryList;
	List<Transform> leaderboardTransformList;
	void Awake(){
		scores = GameObject.Find ("Tocus").GetComponent<Scores> ();
		leaderboardEntryList = new List<LeaderboardEntry> () {
			new LeaderboardEntry{ score = 35, name = "rub" },
			new LeaderboardEntry{ score = 29, name = "wei" },
			new LeaderboardEntry{ score = 25, name = "bla" },
			new LeaderboardEntry{ score = 19, name = "yan" },
			new LeaderboardEntry{ score = 15, name = "jau" },
			new LeaderboardEntry{ score = 10, name = "nor" },
			new LeaderboardEntry{ score = 5, name = "pyr" },
			new LeaderboardEntry{ score = 3, name = "ren" },
			new LeaderboardEntry{score = scores.collected, name = "YOU"}
		};
		//string jsonString = PlayerPrefs.GetString ("leaderboardTable");
		//Highscores highscores = JsonUtility.FromJson<Highscores> (jsonString);
	
		for (int i = 0; i < leaderboardEntryList.Count; i++) {
			for (int j = i + 1; j < leaderboardEntryList.Count; j++) {
				if (leaderboardEntryList [j].score > leaderboardEntryList [i].score) {
					LeaderboardEntry tmp = leaderboardEntryList [i];
					leaderboardEntryList [i] = leaderboardEntryList [j];
					leaderboardEntryList [j] = tmp;
				}
			}
		}
		leaderboardTransformList = new List<Transform> ();
			
			foreach(LeaderboardEntry l in leaderboardEntryList){
			CreateLeaderboardEntryTransform (l, scoreList, leaderboardTransformList);
			}
		/*
		Highscores highscores = new Highscores{leaderboardEntryList = leaderboardEntryList};
		string json = JsonUtility.ToJson (highscores);
		PlayerPrefs.SetString("leaderboardTable", json);
			PlayerPrefs.Save();
		Debug.Log(PlayerPrefs.GetString("leaderboardTable"));
*/


	}


	void CreateLeaderboardEntryTransform(LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList){
		Transform scoreEntryTransform = Instantiate(scoreEntry, scoreList);
		RectTransform scoreEntryRect = scoreEntryTransform.GetComponent<RectTransform>();
		scoreEntryRect.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
		int rank = transformList.Count+1;
		scoreEntryTransform.Find ("positionText").GetComponent<TMP_Text> ().text = rank.ToString();
		scoreEntryTransform.Find ("collectibleText").GetComponent<TMP_Text> ().text = leaderboardEntry.score.ToString();
	scoreEntryTransform.Find("nameText").GetComponent<TMP_Text>().text = leaderboardEntry.name;
		transformList.Add (scoreEntryTransform);
	}
	class LeaderboardEntry{
		public int score;
		public string name = "User";
	}
}
