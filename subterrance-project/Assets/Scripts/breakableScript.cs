using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableScript : MonoBehaviour {

    public int hithealth = 3;
    public bool isWaterBlock = false;
    private int myhits = 0;
    private Material myMat;

    private ParticleSystem waterfallPart;

    // Use this for initialization
    void Start () {
        myMat = gameObject.GetComponent<Renderer>().material;

        if (isWaterBlock) waterfallPart = GameObject.Find("Waterfall").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void breakReg()
    {
        myhits++;
        if(myhits < hithealth)
        {
            myMat.SetFloat("_break_hitVal", (float)myhits/hithealth);
        }
        else
        {
            if (isWaterBlock)
            {
                ParticleSystem.MainModule newMain = waterfallPart.main;
                newMain.startColor = Color.blue;
            }

            Destroy(gameObject);
        }
    }
}
