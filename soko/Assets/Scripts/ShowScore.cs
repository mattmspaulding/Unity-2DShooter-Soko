using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowScore : MonoBehaviour {

	Text text_;

	void Start()
	{
		text_ = GetComponent<Text>();
		
		text_.text = "" + ScoreManager.score;
		
		EnemyHealth.totalScore = 0;
	}
}
