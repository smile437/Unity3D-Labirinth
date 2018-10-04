using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour 
{
	private float x, z;
	private const int Rows = 10;
	private const int Columns = 10;
	private const float cellWidth = 6;
	private const float cellHeight = 6;	
	private static float timer = 0;
	private static BaseLevelGenerator mazeGenerator;
	private static bool isGameStoped = false;
	private MazeCell cell;
	private float timeToGenCoin = 0f;

	public static List<UserData> Users = new List<UserData>();
	public static int CoinsCount = 0;
	public static string exitReason = "";
	public static string CurrentName = "";
	public static int CoinsOnLevel = 0;

	public GameObject GoalPrefab;
	public GameObject Floor;
	public GameObject Wall;
	public GameObject [] Enemies = new GameObject[3];


	public static void KillPlayer()
	{
		isGameStoped = true;
		SaveData ();
		SceneManager.LoadScene ("MainMenu");
	}

	public static void CoinPicked()
	{
		if (CoinsCount > 20) 
		{
			(GameObject.Find ("Zombie").GetComponent (typeof(ZombieControll)) as ZombieControll).Multiply+=0.05f;
			(GameObject.Find ("Zombie1").GetComponent (typeof(ZombieControll)) as ZombieControll).Multiply+=0.05f;
			(GameObject.Find ("Mummy").GetComponent (typeof(MummyControl)) as MummyControl).Multiply+=0.05f;
		}
	}

	private void CreateNewCoin()
	{
		if (CoinsOnLevel < 10) 
		{
			this.x = UnityEngine.Random.Range(0,Columns) * cellWidth;
			this.z = UnityEngine.Random.Range(0,Rows) * cellHeight;
			this.cell = mazeGenerator.GetMazeCell ((int)x / 6, (int)z / 6);
			GameObject coin = Instantiate (GoalPrefab, new Vector3 (this.x, 0.1f, this.z), Quaternion.Euler (90, 0, 0)) as GameObject;
			coin.transform.parent = transform;
			CoinsOnLevel++;
		}
			
	}

	private void Awake()
	{
		Users = DataProvider.Deserializer ();
	}

	private void Start () 
	{
		Enemies [1].SetActive(false);
		Enemies [2].SetActive(false);

		mazeGenerator = new MazeGen (Rows, Columns);
		mazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++)
		{
			for(int column = 0; column < Columns; column++)
			{
				this.x = column * cellWidth;
				this.z = row * cellHeight;
				this.cell = mazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				if(this.cell.WallRight)
				{
					tmp = Instantiate(Wall,new Vector3(x+cellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;
					tmp.transform.parent = transform;
				}

				if(this.cell.WallFront)
				{
					tmp = Instantiate(Wall,new Vector3(x,0,z+cellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;
					tmp.transform.parent = transform;
				}

				if(this.cell.WallLeft)
				{
					tmp = Instantiate(Wall,new Vector3(x-cellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;
					tmp.transform.parent = transform;
				}

				if(this.cell.WallBack)
				{
					tmp = Instantiate(Wall,new Vector3(x,0,z-cellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
	}

	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			isGameStoped = !isGameStoped;
			if (Time.timeScale == 1) 
			{
				Time.timeScale = 0;
			} 
			else
			{
				Time.timeScale = 1;
			}
		}

		if (!isGameStoped) 
		{
			timer += Time.deltaTime;
		}

		if (CoinsCount == 5) 
		{
			Enemies [1].SetActive (true);
		}
		if (CoinsCount == 10) 
		{
			Enemies [2].SetActive (true);
		}

		if (CoinsOnLevel < 10) 
		{
			this.timeToGenCoin += Time.deltaTime;
			if(this.timeToGenCoin >= 5.0f)
			{
				this.CreateNewCoin ();
				this.timeToGenCoin = 0f;
			}
		}
	}

	private void OnGUI()
	{
		if(isGameStoped)
		{
			GUI.BeginGroup (new Rect (Screen.width / 2 - 125, Screen.height / 2 - 160, 200, 250));

			GUI.Box (new Rect (0, 0, 200, 250), "Pause");


			if(GUI.Button(new Rect(20, 110 , 160,30), "Exit"))
			{
				exitReason = "Exit";
				SaveData ();
				SceneManager.LoadScene ("MainMenu");
			}

			GUI.EndGroup ();
		}
	}

	private static void SaveData()
	{
		var dataToSave = new UsersContainer ();
		dataToSave.Users = Users;
		dataToSave.Users.Add(new UserData () 
		{
			Score = CoinsCount,
			Name = CurrentName,
			ExitReason = exitReason,
			LaunchDate = DateTime.Now.ToString("dd/MM/yyyy"),
			PlayedTime = timer.ToString(),
		});

		DataProvider.Serializer (dataToSave);
	}
}
