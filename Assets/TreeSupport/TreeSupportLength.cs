using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSupportLength : MonoBehaviour {

	public SkinnedMeshRenderer leftSkin;
	public SkinnedMeshRenderer rightSkin;

	[Range(0f, 100f)]
	public float Leftlength, Rightlength;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		leftSkin.SetBlendShapeWeight (0, Leftlength);
		rightSkin.SetBlendShapeWeight (0, Rightlength);
	}
}
