using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlPersonaje : MonoBehaviour {

	Rigidbody2D rgb;
	Animator anim;
	public float maxVel = 5f;
	bool haciaDerecha = true;
	public Slider slider;
	public Text txt;
	public Text txtscore;
	public int energy = 100;
	public int score = 0;
	public bool enGolpe = false;
	private float proxGolpe = 0.0f;
	public float golpeRate = 0.5f;
	public int damageBala = 20;
	public int saludPotion = 20;
	public bool viva = true;
	public Text  fin;
	public Text relicTxt;
	public bool relicObtained = false;


	AudioSource aSource;
	public float audioVolume = 0.03f;
	public AudioClip salto;
	public AudioClip ataque;
	public AudioClip golpeada;
	public AudioClip derrotada;
	public AudioClip curada;
	public AudioClip reliquia;
	public AudioClip win;

	public GameObject retroalimentacionEnergiaPrefab;
	Transform retroalimentacionSpawnPoint = null;

	ControlEscena ctrEscena;
	
	public bool jumping = false;
	public float yJumpForce = 350;
	Vector2 jumpForce;
	public bool isOnTheFloor = true;


	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		rgb.freezeRotation = true;
		aSource = GetComponent<AudioSource> ();
		fin.enabled = false;
		relicTxt.enabled = false;
		aSource.volume = audioVolume;
	
		retroalimentacionSpawnPoint = GameObject.Find ("spawnPoint").transform;
		ctrEscena = GameObject.Find ("Fondo").GetComponent<ControlEscena> ();
	
	}
	
	void Update (){
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Muriendo") && viva) {
			if (energy <= 0) { 
				energy = 0;
				anim.SetTrigger ("morir");
				aSource.PlayOneShot(derrotada);
				viva = false;
				ctrEscena.RegistrarFin();
				fin.enabled = true;
				Invoke ("GameOver", 2);


			}
		} else {
			return;
		}

		slider.value = energy;
		txt.text = energy.ToString ();
		txtscore.text = "Score: "+ score.ToString ();

		//para daño por colision de cuerpo
		//aColisionado = false;

		if (Mathf.Abs (Input.GetAxis ("Fire1")) > 0.01f && viva && Time.time > proxGolpe) {
			if (enGolpe == false) { 
				enGolpe = true;
				anim.SetTrigger ("atacar");
				aSource.PlayOneShot(ataque);


				proxGolpe = Time.time + golpeRate;


				//anim.StopPlayback();

				//anim.Play("Melee");

			}

		} else {
			enGolpe = false;

		}

	}

	/*

	private void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.name.Equals ("goblin archer") && enGolpe == true) {
			ControlEnemigo ctr = other.gameObject.GetComponent<ControlEnemigo>();

			if(ctr != null) ctr.RecibirAtaque();
			Debug.Log("enter");

			
		}
		
	}

	private void OnTriggerStay2D(Collider2D other){
		
		if (other.gameObject.name.Equals ("goblin archer") && enGolpe == true) {
			ControlEnemigo ctr = other.gameObject.GetComponent<ControlEnemigo>();
			
			if(ctr != null) ctr.RecibirAtaque();
			Debug.Log("stay");
			
			
		}
		
	}

*/


	void FixedUpdate () {

		float v = Input.GetAxis ("Horizontal");
		Vector2 vel = new Vector2 (0, rgb.velocity.y);

		if (viva) {
			v *= maxVel;
			vel.x = v;
			rgb.velocity = vel;

		}

		if(enGolpe == false && viva == true){
			if (vel.x != 0) {
				//anim.StopPlayback();
				anim.SetBool("moviendose", true);
				anim.SetTrigger ("caminar");
				//anim.Play("Caminando");
				
			} else {

				if(enGolpe == false){
					//anim.StopPlayback();
					anim.SetBool("moviendose", false);
					anim.SetTrigger ("detenerse");
					//anim.Play("Idle_girl");

					//Debug.Log ("esta idle");


				}
			}

		}



		isOnTheFloor = rgb.velocity.y == 0;
		if (Input.GetAxis ("Jump") > 0.01f && viva) {
			if (!jumping && isOnTheFloor) {
				jumping = true;
				jumpForce.x = 0f;
				jumpForce.y = yJumpForce;
				rgb.AddForce (jumpForce);
				aSource.PlayOneShot(salto);

			}

		} else {
			jumping = false;

		}



		//anim.SetFloat ("speed", vel.x);
		if (viva) {

			if (haciaDerecha && v < 0) {
				haciaDerecha = false;
				Flip ();
			} else if (!haciaDerecha && v > 0) {
				haciaDerecha = true;
				Flip ();
			}
		}

	
	}

	void Flip(){
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;

	}

	public void RecibirBala(){
		energy -= damageBala;
		int numRetro = -damageBala;
		aSource.PlayOneShot(golpeada);


		InstanciarRetroalimentacionEnergia (numRetro);
	}

	public void Curar(){
		energy += saludPotion;
		aSource.PlayOneShot (curada);
		InstanciarRetroalimentacionEnergia (saludPotion);
		if (energy >= 100) {
			energy = 100;
		}

		score += 100;
	}

	public void PuntuarReliquia(){
		score += 1000;
		aSource.PlayOneShot (reliquia);
		ctrEscena.RegistrarFin ();
		relicObtained = true;
		relicTxt.enabled = true;

		aSource.PlayOneShot (win);
		Invoke ("GameOver", 5);
	}

	private void InstanciarRetroalimentacionEnergia(int incremento) {
		GameObject retroalimentcionGO = null;
		if (retroalimentacionSpawnPoint!=null)
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
		else
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, transform.position, transform.rotation);
		
		retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
	}

	private void GameOver(){
		Application.LoadLevel ("scena01");

	}

	public void RecibirOtroDmg(){
		energy -= damageBala;
		int numRetro = -damageBala;
		aSource.PlayOneShot(golpeada);
		InstanciarRetroalimentacionEnergia (numRetro);

		var p = transform.position;
		if (haciaDerecha) {
			p.x += -2;
			transform.position = p;
		} else {
			p.x += 2;
			transform.position = p;
		}




	}
	
}
