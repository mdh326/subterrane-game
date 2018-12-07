using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalGlowScript : MonoBehaviour {

    public float pulseTime = 4f;
    private Material myMat;
    private Light myLight;
    private float myTimer = 0;
    private float sineTime = 0;
    private float glowFactor;

	// Use this for initialization
	void Start () {
        myMat = gameObject.GetComponent<Renderer>().material;
        myLight = gameObject.GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        myTimer += Time.deltaTime;

        if (myTimer > pulseTime / 2){
            sineTime += Time.deltaTime;
        } else if (myTimer < pulseTime / 2){
            sineTime -= Time.deltaTime;
        }

        if(sineTime > 1f){
            sineTime = 1f;
        } else if (sineTime < 0f){
            sineTime = 0f;
        }

        glowFactor = 0.1f + 0.9f * ();
	}
}
