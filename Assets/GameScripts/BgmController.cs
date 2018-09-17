using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmController : MonoBehaviour
{

	//audiosource
	public AudioSource[] audioSources;

	//GameController
	GameObject gc;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		audioSources = gameObject.GetComponents<AudioSource>();      
	}

    public void GameClear()
	{
		audioSources[0].Stop();
        audioSources[1].Play();
	}

	public void GameOver()
	{
        audioSources[0].Stop();
        audioSources[2].Play();
	}
    
	public void Reset()
	{
		audioSources[1].Stop();
		audioSources[2].Stop();
		audioSources[0].Play();
	}
}