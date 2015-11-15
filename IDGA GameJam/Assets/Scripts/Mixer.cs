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

	private string state;

	void Awake()
	{
		Tracks.AddRange(GetComponentsInChildren<AudioSource>());
		if(Tracks.Count == 0)
		{
			gameObject.SetActive(false);
			this.enabled = false;
		}
		Messenger.AddListener<string>("GameStateChange", MusicSelection);
	}

	// Use this for initialization
	void Start () 
	{
		MixerName = this.tag;
		ActiveSetList.Clear();
		ActiveSetList = MenuSetList;
		state = "mainmenu";
		Play();
		LoadFormat();
	}

	public void SkipTrack()
	{
		if(CurrentTrack != Tracks.Count - 1)
		{
			Debug.Log(CurrentTrack);
			Tracks[CurrentTrack].Stop();
			CurrentTrack++;
			Play();
		}
	}

	public void AudioQue(string msg)
	{
		Messenger.Broadcast<string>("GameStateChange", msg.ToLower());
	}

	public void SaveFormat()
	{
		PlayerPrefs.DeleteKey(MixerName + state);
		PlayerPrefs.SetFloat(MixerName + state, Volume.value);
		PlayerPrefs.Save();
	}

	public void LoadFormat()
	{
		if(PlayerPrefs.HasKey(MixerName + state))
		{
			Volume.value = PlayerPrefs.GetFloat(MixerName + state);
		}
	}

	void MusicSelection(string gamestate)
	{
		switch(gamestate)
		{
		case "mainmenu":
			state = gamestate;
			if(ActiveSetList.Count >= 0)
				ActiveSetList[CurrentTrack].Stop();
			CurrentTrack = 0;
			ActiveSetList = MenuSetList;
			Play();
			LoadFormat();
			break;
		case "gameplay":
			state = gamestate;
			if(ActiveSetList.Count >= 0)
				ActiveSetList[CurrentTrack].Stop();
			CurrentTrack = 0;
			ActiveSetList = GameSetList;
			Play();
			LoadFormat();
			break;
		case "endgame":
			state = gamestate;
			if(ActiveSetList.Count >= 0)
				ActiveSetList[CurrentTrack].Stop();
			CurrentTrack = 0;
			ActiveSetList = MenuSetList;
			Play();
			LoadFormat();
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
