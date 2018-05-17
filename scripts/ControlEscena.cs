using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class ControlEscena : MonoBehaviour {
	float version = 0.01f;
	int numEnemigosDerrotados = 0;
	int numReliquias = 0;
	AudioSource aSource;
	public float audioVolume = 0.03f;
	public AudioClip bgMusic;

	// Use this for initialization
	void Start () {
		RegistrarInicio ();
		aSource = GetComponent<AudioSource> ();
		aSource.playOnAwake = true; 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RegistrarInicio(){
		UnityAnalytics.CustomEvent("gameStart", new Dictionary<string, object>
		                           {
			//{ "potions", totalPotions },
			//{ "coins", totalCoins },

		});

	}

	public void RegistrarReliquias(){
		numReliquias++;
	}

	public void RegistrarEnemigosDerrotados(){
		numEnemigosDerrotados++;
	}

	public void RegistrarFin(){
		float secsJuego = Time.time;
		//int vidaGirl = GameObject.Find ("Girl").GetComponent<ControlPersonaje> ().energy;
		//int vidaGoblin = GameObject.Find ("goblin archer").GetComponent<ControlEnemigo> ().energy;
		aSource.Stop ();
		UnityAnalytics.CustomEvent("gameOver", new Dictionary<string, object>
		                           {
			{ "time", secsJuego },
			{ "numReliquias", numReliquias },
			{ "numEnemigosDerrotados", numEnemigosDerrotados },
			{ "version", version }
			
		});

	}


}
