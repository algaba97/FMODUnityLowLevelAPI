using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlasticMoose.Minecart {
	public class Wheel : MonoBehaviour {
		public enum Side {
			Left, Right
		}
		public enum Direction {
			Front, Back
		}
		[Header("Settings")]
		public Side WheelSide = Side.Left;
		public Direction WheelDirection = Direction.Front;

		[Header("Assets")]
		public GameObject SparksParticles;
		public AudioClip BreakSound, RunningSound;
		public Movement MinecartParent;

		private Coroutine playingSpark;

		public void Start() {
			/*
			 * No parent minecart linked, searching the parent gameobjects for the Minecart.Movement class.
			 * It is needed to figure out if the minecart is breaking and if it is moving
			 */
			if(MinecartParent == null) {
				MinecartParent = transform.GetComponentInParent<Movement>();
			}
		}

		//This Coroutine plays the sparks for this wheel for a certain duration in seconds.
		private IEnumerator playSparkParticle(float duration) {
			SparksParticles.SetActive(true);
			yield return new WaitForSeconds(duration);
			SparksParticles.SetActive(false);
			playingSpark = null;
		}

		void Update() {
			if(MinecartParent == null)
				return;

			if(SparksParticles != null) {
				if(MinecartParent.CurrentSpeed <= 0.01f) {
					
					//Stop playing any sparks particles if the minecart is stopped
					if(playingSpark != null) {
						StopCoroutine(playingSpark);
					}
					SparksParticles.SetActive(false);
				} else if(MinecartParent.CurrentSpeed > 0f && MinecartParent.IsBreaking) {

					//Stop any playing particles and activate them because the minecart is breaking down.
					if(playingSpark != null) {
						StopCoroutine(playingSpark);
					}
					SparksParticles.SetActive(true);
				} else if(playingSpark == null) {

					//No particles should be playing. Disable them if they are for some reason
					SparksParticles.SetActive(false);
					float randomSpark = Random.Range(0, 300);
					//1 in 300 chance (every tick) of the spark to randomly start playing for a certain duration.
					if(randomSpark == 0) {
						float randomSparkDuration = Random.Range(.1f, .75f);
						playingSpark = StartCoroutine(playSparkParticle(randomSparkDuration));
					}
				}
			}
		}
	}
}
