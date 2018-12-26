using UnityEngine;
using XInputDotNetPure; // Required in C#

public class vibration: MonoBehaviour
{
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	bool bat = false;

	//timer
	float time = 0.0f;


	// Use this for initialization
	void Start()

	{
		vibrate(0.5f);
		// No need to initialize anything for the plugin
	}

	void FixedUpdate()
	{
		time -= Time.deltaTime;
		if (time > 0.0f) {
			if(!bat)
			GamePad.SetVibration (playerIndex, 1, 1);
			else
				GamePad.SetVibration (playerIndex, 0.5f, 0.5f);
		} else {
			GamePad.SetVibration (playerIndex, 0, 0);
			bat = false;
		}

	}

	public void vibrate(float timme) {
		time = timme;
	}
	public void lowvibrate(float timme) {
		time = timme;
		bat = true;
	}
	// Update is called once per frame
	void Update()
	{
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected ans use it
		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}

		prevState = state;
		state = GamePad.GetState(playerIndex);

	}

	void OnGUI()
	{

	}
}
