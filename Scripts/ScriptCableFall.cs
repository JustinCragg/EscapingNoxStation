using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class ScriptCableFall : MonoBehaviour {

    Transform player;
    Animator anim;
    Transform lightTransform;
    Light pointLight;
    Transform ps;

    float timer = 0;

    AudioSource audioSource;

    public AudioClip audioExplosion;

    /******************************************************************************************************
    * Called once at the start of the scene
    *******************************************************************************************************/
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        ps = transform.Find("ParticleSystem");
        lightTransform = transform.Find("PointLight");
        pointLight = lightTransform.GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }


    /******************************************************************************************************
    * Called once every frame
    *******************************************************************************************************/
    void Update() {
        if (timer == 0) {
            if (player && anim.enabled == false && Vector3.Distance(player.position, transform.position) < 6) {

                anim.enabled = true;

                ps.gameObject.SetActive(true);

                lightTransform.gameObject.SetActive(true);

                timer += Time.deltaTime;

                audioSource.clip = audioExplosion;
                audioSource.loop = false;
                audioSource.Play();
            }
        }
        else {

            timer += Time.deltaTime;

            if (timer > 0.8f) {
                lightTransform.gameObject.SetActive(false);
                enabled = false;
            }
            else {
                pointLight.intensity = Random.Range(0.2f, 0.7f);
            }
        }
    }
}

