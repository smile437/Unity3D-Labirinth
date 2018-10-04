using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{ 
	private List<UserData> container;

	public Vector2 scrollPosition = Vector2.zero;

	void Awake () 
	{
		this.container = LevelController.Users = DataProvider.Deserializer ();
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0 , 160,30), "Back"))
		{
			SceneManager.LoadScene("MainMenu");
		}

		GUI.BeginGroup (new Rect (Screen.width / 2 - 125, Screen.height / 2 - 160, 260, 300));

		GUI.Box (new Rect (0, 0, 260, 300), "Leaderboard");

		var y = 20;
		scrollPosition = GUI.BeginScrollView(new Rect (10, 20, 250, 280), scrollPosition, new Rect (0, 10, 250, 300));

		for (int i = 0; i < this.container.Count; i++) 
		{
			GUI.Label (new Rect (10, y, 220, 30), this.container [i].Name + " " + this.container [i].Score + " " + this.container [i].ExitReason + " " + this.container [i].LaunchDate + " " + this.container [i].PlayedTime);
			y += 30;
		}

		GUI.EndScrollView();

		GUI.EndGroup ();
	}
}
