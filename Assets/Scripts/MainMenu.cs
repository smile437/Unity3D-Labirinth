using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	string currName = "";
	
	void OnGUI()
	{
		GUI.BeginGroup (new Rect (Screen.width / 2 - 125, Screen.height / 2 - 160, 250, 300));

		GUI.Box (new Rect (0, 0, 250, 300), "Dangerous maze");

		this.currName = GUI.TextField (new Rect (45, 70, 160, 30),this.currName, 21);

		if(GUI.Button(new Rect(45, 110 , 160,30), "Play"))
		{
			if(this.name.Length>0)
			{
				LevelController.CurrentName = this.currName;
				SceneManager.LoadScene ("Game");
			}
		}

		if(GUI.Button(new Rect(45, 150, 160,30), "Leaderboard"))
		{
			SceneManager.LoadScene ("Leaderboard");
		}

		if(GUI.Button(new Rect(45, 190, 160,30), "Exit"))
		{
			Application.Quit ();
		}

		GUI.EndGroup ();
	}
}
