using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public GameObject player;
    Transform oldPlayerPos;
    public GameObject turret;
    public float speed = 10;

    public AudioSource audioSource;
    public AudioClip soundEnemyShot;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        turret = GameObject.FindGameObjectWithTag("Turret");
        oldPlayerPos = player.transform;

        audioSource = GetComponent<AudioSource>();
        soundEnemyShot = (AudioClip)Resources.Load("Sounds/enemy_shotLaser");
        audioSource.PlayOneShot(soundEnemyShot);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(oldPlayerPos.transform);
        transform.position += transform.forward * speed * Time.deltaTime; //Moves towards player
        //Debug.Log(oldPlayerPos.transform.position);
    }
}
