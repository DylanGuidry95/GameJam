using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Actions : MonoBehaviour
{
	void Update()
	{
		if(TimeKeeper.globalScale == 0)
		{
			GetComponent<Button>().interactable = true;
		}
	}

	public void EnableDisable(Button other)
	{
		other.interactable = !other.interactable;
	}

	public void EnableDisable(GameObject other)
	{
		other.SetActive(!other.activeSelf);
	}

	public void DisableSelf()
	{
		gameObject.SetActive(false);
	}

	public void SetTimeScale(float a_scale)
	{
		TimeKeeper.SetTimeScale(a_scale);
	}

	public void ToggleTime()
	{
		if(TimeKeeper.globalScale == 0)
		{
			TimeKeeper.SetTimeScale(1);
		}
		else
		{
			TimeKeeper.SetTimeScale(0);
		}
	}

	public void ChangeScene(string a_scene)
	{
		Application.LoadLevel(a_scene);
	}

	public void ExitApplication()
	{
		Application.Quit();
	}
}