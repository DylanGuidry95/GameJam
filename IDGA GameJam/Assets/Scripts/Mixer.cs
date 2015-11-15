using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Mixer : MonoBehaviour 
{
	public List<AudioSource>Tracks = new List<AudioSource>();
	public Text trackName;
	public int CurrentTrack = 0;

	[SerializeField]
	List<AudioSource>MenuSetList = new List<AudioSource>();
	[SerializeField]
	List<AudioSource>GameSetList = new List<AudioSource>();
	[SerializeField]
	List<AudioSource>ActiveSetList = new List<AudioSource>();

	public string MixerName;
	public Slider Volume;

	// Use this for initialization
	void Start () 
	{
		MixerName = this.tag;
		Tracks.AddRange(GetComponentsInChildren<AudioSource>());
		Messenger.AddListener<string>("GameStateChange", MusicSelection);
	}

	public void SkipTrack()
	{
		if(CurrentTrack != Tracks.Count - 1)
		{
			Tracks[CurrentTrack].Stop();
			CurrentTrack++;
			Play();
		}
	}

	void MusicSelection(string gamestate)
	{
		switch(gamestate)
		{
		case "MainMenu":
			CurrentTrack = 0;
			ActiveSetList.Clear();
			ActiveSetList.AddRange(MenuSetList);
			break;
		case "GamePlay":
			CurrentTrack = 0;
			ActiveSetList.Clear();
			ActiveSetList.AddRange(GameSetList);
			break;
		case "EndGame":
			CurrentTrack = 0;
			ActiveSetList.Clear();
			ActiveSetList.AddRange(MenuSetList);
			break;
		}
	}

	public void PrevTrack()
	{
		if(CurrentTrack != 0)
		{
			ActiveSetList[CurrentTrack].Stop();
			CurrentTrack--;
			Play();
		}
	}

	public void VolumeControl()
	{
		ActiveSetList[CurrentTrack].volume = Volume.value;
	}

	public void Play()
	{
		ActiveSetList[CurrentTrack].Play();
		VolumeControl();
		trackName.text = ActiveSetList[CurrentTrack].clip.name;
	}

	public void Pause()
	{
		ActiveSetList[CurrentTrack].Pause();
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
