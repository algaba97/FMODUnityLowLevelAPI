using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManipulator : MonoBehaviour {

	private SkinnedMeshRenderer rock;
	[Range(0f, 100f)]
	public float targetValue1, targetValue2, targetValue3, targetValue4, targetValue5, targetValue6, targetValue7, targetValue8, targetValue9;



	// Use this for initialization
	void Start () 
	{
		rock = transform.GetChild (0).GetComponent<SkinnedMeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rock.SetBlendShapeWeight (0, targetValue1);
		rock.SetBlendShapeWeight (1, targetValue2);
		rock.SetBlendShapeWeight (2, targetValue3);
		rock.SetBlendShapeWeight (3, targetValue4);
		rock.SetBlendShapeWeight (4, targetValue5);
		rock.SetBlendShapeWeight (5, targetValue6);
		rock.SetBlendShapeWeight (6, targetValue7);
		rock.SetBlendShapeWeight (7, targetValue8);
		rock.SetBlendShapeWeight (8, targetValue9);
	}
}
