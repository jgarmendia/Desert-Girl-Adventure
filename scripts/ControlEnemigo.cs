using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlEnemigo : MonoBehaviour {

	public float vel = -1f;
	Rigidbody2D rgb;
	Animator anim;
	public Slider slider;
	public Text txt;
	public int energy = 100;
	public GameObject bulletprototype;
	public int damageAtaque = 20;
	public bool vivo = true;
	public float delayMuerte = 2;

	public GameObject retroalimentacionEnergiaPrefab;
	Transform retroalimentacionSpawnPoint = null;

	AudioSource aSource;
	public float audioVolume = 0.03f;
	public AudioClip disparo;
	public AudioClip muerto;
	public AudioClip golpeado;



	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		aSource = GetComponent<AudioSource> ();
		aSource.volume = audioVolume;


	
	}

	void Update (){
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Muriendo")) {
			if (energy <= 0) { 
				energy = 0;
				anim.SetTrigger ("morir");
				anim.SetFloat("speed", 0);
				if(vivo) {
					aSource.PlayOneShot(muerto);
				}
				vivo = false;
				vel = 0;
				Destroy(gameObject, delayMuerte);

			}
		} else {
			return;
		}

		slider.value = energy;
		txt.text = energy.ToString ();
	}

	void FixedUpdate () {
		Vector2 v = new Vector2 (vel, 0);
		rgb.velocity = v;

		if (vivo) {

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("goblin_walk") && Random.value < 1f / (60f * 3f)) {
				anim.SetTrigger ("attack");
			} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Apuntando")) {
				if (Random.value < 1f / 3f) {
					anim.SetTrigger ("disparar");
				} else {
					anim.SetTrigger ("caminar");
				}
			}

		}
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!other.gameObject.name.Equals ("Girl") || !other.gameObject.name.Equals ("Arma") && vivo) {

			Flip ();
		}
	}

	void Flip(){
		vel *= -1;
		// girar
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;

	}

	public void Disparar(){
		anim.SetTrigger ("attack");

	}

	public void EmitirBalaGob(){
		if (vivo) {

			GameObject bulletCopy = Instantiate (bulletprototype);
			bulletCopy.transform.position = new Vector3 (transform.position.x, transform.position.y, -1f);
			bulletCopy.GetComponent<ControlBalaGob> ().direction = new Vector3 (transform.localScale.x, 0, 0);
			aSource.PlayOneShot (disparo);
		}
	}

	public void RecibirAtaque(){
		energy -= damageAtaque;
		aSource.PlayOneShot (golpeado);
		int numRetro = - damageAtaque;
		InstanciarRetroalimentacionEnergia (numRetro);
	}

	private void InstanciarRetroalimentacionEnergia(int incremento) {
		GameObject retroalimentcionGO = null;
		if (retroalimentacionSpawnPoint!=null)
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
		else
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, transform.position, transform.rotation);
		
		retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
	}





}
