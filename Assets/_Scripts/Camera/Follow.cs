using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose;

namespace PlasticMoose.Camera {
	public class Follow : MonoBehaviour {
		public Minecart.Movement target;

		private float currentSpeed = 1f;
		
		void Update() {
			if(target == null)
				return;
			transform.position = target.transform.position;
			transform.rotation = target.transform.rotation;
		}
	}
}