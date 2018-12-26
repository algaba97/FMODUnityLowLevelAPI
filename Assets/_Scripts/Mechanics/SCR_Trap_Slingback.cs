using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlasticMoose.Minecart 
{
	public class SCR_Trap_Slingback : MonoBehaviour 
	{
		public float slowdownSpeed;
		public float startAccelerationAt;
		public float flingbackSpeed;
		public bool shouldReverse;
		public SkinnedMeshRenderer gumMesh;
		public float gumDum;
		public bool retract; 
		public float elapsedTime;
		void Update()
		{
			if (shouldReverse) 
			{

				move.Acceleration = 0; 
				move.CurrentSpeed = move.CurrentSpeed - Time.deltaTime * slowdownSpeed;
				gumDum += gumDum * Time.deltaTime;
				gumMesh.SetBlendShapeWeight (0, gumDum);
				if (move.CurrentSpeed <= 0) 
				{
					retract = true;
					move.CurrentSpeed = move.CurrentSpeed - Time.deltaTime * slowdownSpeed * flingbackSpeed;
				}
			}

			if (retract && gumDum > 0) {
				gumDum -= gumDum * Time.deltaTime * 5;
				gumMesh.SetBlendShapeWeight (0, gumDum);
				if (gumDum < 1) {
					retract = false;
					gumDum = 30;
				}
			}


			if (move.CurrentSpeed <= startAccelerationAt)
			{
				move.Acceleration = 7.5f;
				shouldReverse = false;
			}
		}

		public Movement move;
		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Cart" && move.CurrentSpeed > 9)
			shouldReverse = true;
		}
	}
}