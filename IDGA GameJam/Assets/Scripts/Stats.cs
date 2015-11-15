using UnityEngine;
using System.Collections;
using System;

public class Stats : MonoBehaviour 
{
	FSM<CSTATES> _fsm;

	enum CSTATES
	{
		e_Init,
		e_Idle,
		e_Growing,
		e_Shrinking,
		e_Dead,
		e_Count
	}

	[SerializeField]
	float minEnergy;
	[SerializeField]
	public float currentEnergy;
	public float maxEnergy;

	public float growthTimer; 

	void Awake()
	{
		_fsm = new FSM<CSTATES>();
		AddStates();
		AddTransitions();
		Instance = this;
	}

	void AddStates()
	{
		foreach(int i in Enum.GetValues(typeof(CSTATES)))
		{
			if((CSTATES)i != CSTATES.e_Count)
			{
				_fsm.AddState((CSTATES)i);
			}
		}
	}

	void AddTransitions()
	{
		_fsm.AddTransition(CSTATES.e_Init, CSTATES.e_Idle);
		_fsm.AddTransition(CSTATES.e_Idle, CSTATES.e_Shrinking);
		_fsm.AddTransition(CSTATES.e_Shrinking, CSTATES.e_Growing);
		_fsm.AddTransition(CSTATES.e_Growing, CSTATES.e_Shrinking);
		_fsm.AddTransition(CSTATES.e_Shrinking, CSTATES.e_Dead);
		_fsm.AddTransition(CSTATES.e_Growing, CSTATES.e_Dead);
	}

	// Use this for initialization
	void Start () 
	{
		_fsm.Transition(_fsm.state, CSTATES.e_Idle);
		_fsm.Transition(_fsm.state, CSTATES.e_Shrinking);
		minEnergy = 0;
		currentEnergy = maxEnergy / 2;
		if(FindObjectOfType<GrowthGage>().gameObject == true)
		{
			GameObject meter = FindObjectOfType<GrowthGage>().gameObject;
			meter.GetComponent<GrowthGage>().energy = currentEnergy;
		}
		StartCoroutine(Shrinking());
	}

	IEnumerator Growing(float mass)
	{
		growthTimer += mass;
		while(growthTimer * Time.deltaTime > 0)
		{
			currentEnergy += Time.deltaTime;
			if(currentEnergy >= maxEnergy)
			{
				_fsm.Transition(_fsm.state, CSTATES.e_Dead);
			}
			growthTimer -= Time.deltaTime * 1;
			yield return null;
		}
		growthTimer = 0;
		_fsm.Transition(_fsm.state, CSTATES.e_Shrinking);
		StartCoroutine(Shrinking());
		StopCoroutine(Growing(mass));
	}

	IEnumerator Shrinking()
	{
		while(_fsm.state == CSTATES.e_Shrinking)
		{
			if(TimeKeeper.globalScale > 0)
			{
				currentEnergy -= Time.deltaTime;
				if(currentEnergy <= minEnergy && tag == "Player")
				{
					_fsm.Transition(_fsm.state, CSTATES.e_Dead);
				}
			}
			yield return null;
		}
		StopCoroutine(Shrinking());
	}

	void OnCollisionEnter(Collision a)
	{
		Debug.Log(a.gameObject.name);
		StopCoroutine(Shrinking());
		_fsm.Transition(_fsm.state, CSTATES.e_Growing);
		if(a.gameObject.GetComponent<BaseStats>() == true)
		{
			StartCoroutine(Growing(a.gameObject.GetComponent<BaseStats>().mass));
			Destroy(a.gameObject);
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if(TimeKeeper.globalScale > 0)
		{
			transform.localScale = new Vector3(currentEnergy, currentEnergy, currentEnergy);
			if(FindObjectOfType<GrowthGage>())
			{
			GameObject meter = FindObjectOfType<GrowthGage>().gameObject;
			meter.GetComponent<GrowthGage>().energy = currentEnergy;
			}
			if(_fsm.state == CSTATES.e_Dead)
			{
				StopAllCoroutines();
				Destroy(gameObject);
			}

			TimeKeeper.SetTimeScale(currentEnergy / maxEnergy * 2);
		}

	}

	private Stats Instance;
	public  Stats _Instance
	{
		get
		{
			return Instance;
		}
	}
}
