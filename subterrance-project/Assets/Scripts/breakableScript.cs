using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableScript : MonoBehaviour {

    public int hithealth = 3;
    public bool isWaterBlock = false;
    private int myhits = 0;
    private Material myMat;
    public GameObject myDeathPart;

    private ParticleSystem waterfallPart;
    private Vector3 patScale;
    private UnityEngine.ParticleSystem.MinMaxCurve patEmiss;

    // Use this for initialization
    void Start () {
        myMat = gameObject.GetComponent<Renderer>().material;

        if (isWaterBlock)
        {
            waterfallPart = GameObject.Find("WaterfallPat").GetComponent<ParticleSystem>();
            ParticleSystem.ShapeModule newShape = waterfallPart.shape;
            patScale = newShape.scale;
            newShape.scale = new Vector3(1f, newShape.scale.y, newShape.scale.z);

            ParticleSystem.EmissionModule newEmiss = waterfallPart.emission;
            patEmiss = newEmiss.rateOverTime;
            newEmiss.rateOverTime = 50f;
        }

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
                //ParticleSystem.MainModule newMain = waterfallPart.main;
                //newMain.startColor = Color.blue;
                ParticleSystem.ShapeModule newShape = waterfallPart.shape;
                newShape.scale = patScale;

                ParticleSystem.EmissionModule newEmiss = waterfallPart.emission;
                newEmiss.rateOverTime = patEmiss;
            }

            GameObject myPart = Instantiate(myDeathPart, transform.position, transform.rotation);
            Destroy(myPart, 3f);
            Destroy(gameObject);
        }
    }
}
