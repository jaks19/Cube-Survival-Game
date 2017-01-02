using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Statics : MonoBehaviour
{
	// Designed to modularize 'Level reload Process'
	public static IEnumerator ReloadLevel(){
		yield return new WaitForSeconds (2.0f);
		SceneManager.LoadScene(PlayerPrefs.GetInt("current"));
	}

	// Designed to modularize 'Score Calculation Process'
	/* Algorithm:
		6 half stars make up 3 stars
		Half star for each token collected and 
	 	3 half stars if finish within 5/12 of time or 2 half stars if finish after 5/12 and within 8/12 else 1 half star 
	 	A 'half star' is 0.5 pts 
	 */
	public static float CalculateScore(Dictionary<string, bool> tokensCollected, float leftTime, float initialTime){
		float score = 0.0f;
		foreach (string token in tokensCollected.Keys) {
			if (tokensCollected [token]) {
				score += 0.5f;
			}
		}
		float fractionTime = (initialTime - leftTime) / initialTime;
		if (fractionTime <= 5f/12f) {
			score += 1.5f;
		} else if (fractionTime <= 9f/12.0f) {
			score += 1.0f;
		} else {
			score += 0.5f;
		} 
		return score;
	}
}

