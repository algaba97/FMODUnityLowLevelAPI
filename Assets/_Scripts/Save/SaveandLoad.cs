using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Retrieved from https://answers.unity.com/questions/1300019/how-do-you-save-write-and-load-from-a-file.html
//adapted and improved by Jorge Algaba
[System.Serializable]
public class GameData
{
	public int level;
	public int music;
	public List<float> levels;


	public GameData(int levelInt,int musicInt, List<float> levelsInt)
	{
		level = levelInt;
		music = musicInt;
		levels = new List<float> (levelsInt);
	
	}
}
public class SaveandLoad : MonoBehaviour {


		int currentLevel = 3;
	    int music = 1;
	List<float> levelsaux; 
		

		void Start()
	{
			//SaveFile();
			//LoadFile();
		}

		public void SaveFile()
		{
		Debug.Log ("Save");
		currentLevel = GameObject.Find ("GameManager").GetComponent<GameManager> ().level;
		music = GameObject.Find ("GameManager").GetComponent<GameManager> ().music;
		levelsaux = new List<float>(GameObject.Find ("GameManager").GetComponent<GameManager> ().timelevels);


			string destination = Application.persistentDataPath + "/save.dat";
			FileStream file;

		if (File.Exists (destination))
			file = File.OpenWrite (destination);
		else {
			file = File.Create (destination);
			levelsaux.Add (99.9f);
			levelsaux.Add (99.9f);
			levelsaux.Add (99.9f);
			levelsaux.Add (99.9f);
			levelsaux.Add (99.9f);

		}

		    GameData data = new GameData(currentLevel,music,levelsaux);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, data);
			file.Close();
		}

		public void LoadFile()
		{
			string destination = Application.persistentDataPath + "/save.dat";
		Debug.Log (destination);
			FileStream file;

		if (File.Exists (destination)) {
			Debug.Log ("Loading");
			file = File.OpenRead (destination);
			BinaryFormatter bf = new BinaryFormatter();
			GameData data = (GameData) bf.Deserialize(file);
			file.Close();
			Debug.Log (" fake code to be able to go to level 2 wituout doing level 1 dont delete me until im not needed");
			currentLevel= data.level;
			music = data.music;
			GameObject.Find ("GameManager").GetComponent<GameManager> ().level = 2;
			GameObject.Find ("GameManager").GetComponent<GameManager> ().music = data.music;
			GameObject.Find ("GameManager").GetComponent<GameManager> ().timelevels = new List<float> (data.levels);

			Debug.Log(data.level);
		 }
			else
			{
				Debug.Log("y0, File not found, creating one new ");
			SaveFile ();
				return;
			}


		}

}
