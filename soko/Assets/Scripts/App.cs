using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Helps manage performance.
public class App : MonoBehaviour {

	void Start ()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}
