using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME;

namespace PlasticMoose.Minecart {
	public class Movement : MonoBehaviour {
		public Path_Comp CurrentPath;
		public float DistanceOnPath = 0f;

		[Header("Speeds")]
		[Range(0f, 50f)]
		public float MinSpeed = 3f;
		[Range(0f, 50f)]
		public float MaxSpeed = 50f;
		public float Acceleration = 1f;
		public float CurrentSpeed = 5f;
		[SerializeField]
		private float currentAcceleration = 5f;

		[Header("Wheels")]
		public Wheel[] Wheels;
		public float WheelRadius = 0.28f;
		public float WheelSlip = .5f;
		public bool IsBreaking = false;

		private void Update() {
			//A path is needed in order to follow a path.
			if(CurrentPath == null)
				return;
			
			//Speed this game tick
			float step = CurrentSpeed * Time.deltaTime;

			//Wheel rotations based on that speed
			float rotationInRadians = step / WheelRadius;
			float rotationInDegrees = rotationInRadians * Mathf.Rad2Deg;

			//This should probably be moved to the Wheel class so we don't have this for loop in the main loop
			for(int i = 0; i < Wheels.Length; i++) {
				bool backWheel = Wheels[i].WheelDirection == Wheel.Direction.Back;
				if(backWheel && !IsBreaking || !backWheel)
					Wheels[i].transform.Rotate(rotationInDegrees, 0f, 0f);
			}
			
			//Find the point on the path we are now based on the speed
			DistanceOnPath = Mathf.Clamp(DistanceOnPathNextStep(step), 0, CurrentPath.TotalDistance);
			//Simple check to see if we are looping around the current path
			if(CurrentPath.TotalDistance == DistanceOnPath && CurrentPath.isCircuit)
				DistanceOnPath = 0f;

			//Extract the point we are on
			Path_Point CurrentPoint = CurrentPath.GetPathPoint(DistanceOnPath);
			Path_Point NextPoint = CurrentPath.GetPathPoint(DistanceOnPathNextStep(step));

			//Find the world position of the path point we found
			Vector3 CurrentPosition = CurrentPath.transform.TransformPoint(CurrentPoint.point);
			//Find the direction we are heading based on the old and the new point
			Vector3 Direction = (CurrentPath.transform.TransformPoint(NextPoint.point) - CurrentPosition).normalized;

			//Math to calculate acceleration. It checks if you are currently under the "Minimum speed" and is breaking. If you're not breaking slowly add acceleration again
			float currentMinSpeed = CurrentSpeed < MinSpeed ? CurrentSpeed + (IsBreaking ? 0f : Acceleration * Time.deltaTime) : MinSpeed;
			currentAcceleration = Remap(Direction.y, -.75f, .75f, Acceleration, -Acceleration);
			CurrentSpeed = Mathf.Clamp(CurrentSpeed + (currentAcceleration * Time.deltaTime), currentMinSpeed, MaxSpeed);
			
			//If we are breaking slow down...
			if(IsBreaking) {
				CurrentSpeed = Mathf.Clamp(CurrentSpeed - (WheelSlip * Remap(currentAcceleration, -Acceleration, Acceleration, Acceleration, Acceleration * .1f)), 0f, MaxSpeed);
			}

			//Move the minecart to the new position on the path
			transform.position = CurrentPosition;
			//Rotate the minecart in the direction we just went
			transform.rotation = Quaternion.LookRotation(CurrentPoint.forward, CurrentPoint.up);

			//Checks to see if we have reached the end of the current path, if we have jump to the next one.
			if(!CurrentPath.isCircuit && CurrentPath.NextPath != null) {
				if(DistanceOnPath > CurrentPath.TotalDistance - (step / 2f)) {
					CurrentPath = CurrentPath.NextPath;
					DistanceOnPath = 0f;
				}
			}
		}

		//Remaps a value from inbetween a range to a new range. Example: .5 from between 0 and 1 to 0 to 10 will return 5
		private float Remap(float value, float from1, float to1, float from2, float to2) {
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

		//Figure out where we might be next game tick. This is an estimate based on the current speed and the current framerate.
		private float DistanceOnPathNextStep(float step) {
			if(!CurrentPath.isCircuit)
				return Mathf.Clamp(DistanceOnPath + step, 0f, CurrentPath.TotalDistance);
			return (DistanceOnPath + step) > CurrentPath.TotalDistance ? DistanceOnPath + step - CurrentPath.TotalDistance : DistanceOnPath + step;
		}
	}
}
