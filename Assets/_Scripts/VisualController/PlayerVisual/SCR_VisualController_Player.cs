using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_VisualController_Player : MonoBehaviour
{
	private Animator playerAnimControll;

	//Combine
	private bool combineLeft = false;
	private bool combineRight = false;

	private void Start ()
	{
		playerAnimControll = GetComponent<Animator> ();



		//EVENTS

		//Grabbing
		EventManager.instance.StartListening ("Visual_Player_GrabLeft", GrabLeft);
		EventManager.instance.StartListening ("Visual_Player_GrabRight", GrabRight);

		EventManager.instance.StartListening ("Visual_Player_FailGrabLeft", FailGrabLeft);
		EventManager.instance.StartListening ("Visual_Player_FailGrabRight", FailGrabRight);

		//Throwing 
		EventManager.instance.StartListening ("Visual_Player_ThrowLeft", ThrowLeft);
		EventManager.instance.StartListening ("Visual_Player_ThrowRight", ThrowRight);


		//Combining
		EventManager.instance.StartListening ("Visual_Player_CombineLeft", CombineLeft);
		EventManager.instance.StartListening ("Visual_Player_CombineRight", CombineRight);
		EventManager.instance.StartListening ("Visual_Player_StopCombineLeft", StopCombineLeft);
		EventManager.instance.StartListening ("Visual_Player_StopCombineRight", StopCombineRight);
		EventManager.instance.StartListening ("Visual_Player_Combine", Combine);
	}

	private void OnDestroy ()
	{
		if (EventManager.instance != null)
		{
			//Grab
			EventManager.instance.StopListening ("Visual_Player_GrabLeft", GrabLeft);
			EventManager.instance.StopListening ("Visual_Player_GrabRight", GrabRight);

			EventManager.instance.StopListening ("Visual_Player_FailGrabLeft", FailGrabLeft);
			EventManager.instance.StopListening ("Visual_Player_FailGrabRight", FailGrabRight);

			//Throwing 
			EventManager.instance.StopListening ("Visual_Player_ThrowLeft", ThrowLeft);
			EventManager.instance.StopListening ("Visual_Player_ThrowRight", ThrowRight);

			//Combine
			EventManager.instance.StopListening ("Visual_Player_CombineLeft", CombineLeft);
			EventManager.instance.StopListening ("Visual_Player_CombineRight", CombineRight);
			EventManager.instance.StopListening ("Visual_Player_StopCombineLeft", StopCombineLeft);
			EventManager.instance.StopListening ("Visual_Player_StopCombineRight", StopCombineRight);
			EventManager.instance.StopListening ("Visual_Player_Combine", Combine);
		}
	}

	private void Update ()
	{
		if (combineLeft)
			playerAnimControll.SetBool ("Combine_LeftHand", true);
		else
			playerAnimControll.SetBool ("Combine_LeftHand", false);

		if (combineRight)
			playerAnimControll.SetBool ("Combine_RightHand", true);
		else
			playerAnimControll.SetBool ("Combine_RightHand", false);


	}

	//Grab
	private void GrabLeft ()
	{
		StartCoroutine (AnimGrabLeft ());
	}

	private void GrabRight ()
	{
		StartCoroutine (AnimGrabRight ());
	}

	private void FailGrabRight ()
	{
		StartCoroutine (AnimFailGrabRight ());
	}

	private void FailGrabLeft ()
	{
		StartCoroutine (AnimFailGrabLeft ());
	}

	//Throw
	private void ThrowLeft ()
	{StartCoroutine (AnimThrowLeft ());
		if(GetComponent<AudioSource> () != null)
		GetComponent<AudioSource> ().Play ();
		
	}

	private void ThrowRight ()
	{StartCoroutine (AnimThrowRight ());
		if(GetComponent<AudioSource> () != null)
		GetComponent<AudioSource> ().Play ();
		
	}

	//Combine
	private void CombineLeft ()
	{
		combineLeft = true;
	}

	private void CombineRight ()
	{
		combineRight = true;
	}

	private void Combine ()
	{
		StartCoroutine (AnimCombine ());
	}

	private void StopCombineRight ()
	{
		combineRight = false;
	}

	private void StopCombineLeft ()
	{
		combineLeft = false;
	}

	//Grab
	IEnumerator AnimGrabLeft ()
	{
		playerAnimControll.SetBool ("Grab_LeftHand", true);
		yield return new WaitForSeconds (0.1f);
		playerAnimControll.SetBool ("Grab_LeftHand", false);
	}

	IEnumerator AnimGrabRight ()
	{
		playerAnimControll.SetBool ("Grab_RightHand", true);
		yield return new WaitForSeconds (0.1f);
		playerAnimControll.SetBool ("Grab_RightHand", false);
	}

	IEnumerator AnimFailGrabLeft ()
	{
		playerAnimControll.SetBool ("HandFailGrab_Left", true);
		yield return new WaitForSeconds (1.2f);
		playerAnimControll.SetBool ("HandFailGrab_Left", false);
	}

	IEnumerator AnimFailGrabRight ()
	{
		playerAnimControll.SetBool ("HandFailGrab_Right", true);
		yield return new WaitForSeconds (1.2f);
		playerAnimControll.SetBool ("HandFailGrab_Right", false);
	}

	//Throw
	IEnumerator AnimThrowLeft ()
	{
		playerAnimControll.SetBool ("Throw_LeftHand", true);
		yield return new WaitForSeconds (0.1f);
		playerAnimControll.SetBool ("Throw_LeftHand", false);
	}

	IEnumerator AnimThrowRight ()
	{
		playerAnimControll.SetBool ("Throw_RightHand", true);
		yield return new WaitForSeconds (0.1f);
		playerAnimControll.SetBool ("Throw_RightHand", false);
	}

	//Combine
	IEnumerator AnimCombine ()
	{
		playerAnimControll.SetBool ("Combine_Both", true);
		yield return new WaitForSeconds (0.1f);
		playerAnimControll.SetBool ("Combine_Both", false);
	}
}
