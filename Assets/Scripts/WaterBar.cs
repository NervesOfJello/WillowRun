using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBar : MonoBehaviour {

    [SerializeField]
    private Ent ent;

    private float BarRatio = 1;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        BarRatio = ent.WaterMeterLevelAsPercentage;
        transform.localScale = new Vector3(BarRatio, 1, 1);
	}
}
