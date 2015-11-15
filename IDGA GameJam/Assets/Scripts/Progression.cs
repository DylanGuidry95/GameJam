using UnityEngine;
using System.Collections;

public class Progression : MonoBehaviour 
{
	public enum eSTAGES
	{
		Star,
		Sun,
		Planet1,
		Planet2,
		Moon,
		Life,
		BlackHole,
		BurnOut,
		Count
	}


	public float RoundTimer;
	public float GameTime;
	GameObject player;
	public float timeMod;
	public eSTAGES cStage;
	private int stage;
	// Use this for initialization
	void Start () 
	{	stage = (int)cStage;
		player = GameObject.FindWithTag("Player");
		StageTracker();
	}

	void TimeProgression()
	{
		GameTime -= Time.deltaTime * 1;

		if(GameTime <= 0)
		{
			stage++;
			cStage = (eSTAGES)stage;
			StageTracker();
		}
	}

	void ProgressState(eSTAGES j)
	{
		cStage = j;
		StageTracker();

	}

	void StageTracker()
	{
		switch(cStage)
		{
		case eSTAGES.Star:
			RoundTimer = 60 * 5;
			GameTime = RoundTimer;
			GetRadialSpawner(stage);
			break;
		case eSTAGES.Sun:
			RoundTimer = 60 * 5;
			GameTime = RoundTimer;
			GetRadialSpawner(stage);
			break;
		case eSTAGES.Planet1:
			RoundTimer = 60 * 5;
			GameTime = RoundTimer;
			GetRadialSpawner(stage);
			break;
		case eSTAGES.Planet2:
			RoundTimer = 60 * 5;
			GameTime = RoundTimer;
			break;
		case eSTAGES.Moon:
			RoundTimer = 60 * 5;
			GameTime = RoundTimer;
			GetRadialSpawner(stage);
			break;
		case eSTAGES.Life:
			RoundTimer = 60 * 3;
			GameTime = RoundTimer;
			GetRadialSpawner(stage);
			break;
		case eSTAGES.BlackHole:
			Lose ("blackhole");
			break;
		default:
			break;
		}
	}

	void GetRadialSpawner(int n)
	{
		if(player.GetComponent<RadialSpawner>())
			player.GetComponent<RadialSpawner>().SelectUnit(n);
	}

	void Lose(string msg)
	{
		if(msg == "blackhole")//For when blowing up
		{
			///Dylan Put stuff here
		}
		else //For when burning out
		{
			///Dylan put stuff here
		}
	}

	// Update is called once per frame
	void Update () 
	{
		TimeProgression();
		if(player.GetComponent<Stats>().currentEnergy <= 0)
			ProgressState(eSTAGES.BlackHole);
		else if(player.GetComponent<Stats>().currentEnergy >= 100)
			ProgressState(eSTAGES.BurnOut);
	}
}
