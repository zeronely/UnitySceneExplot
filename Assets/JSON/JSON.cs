using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class JSON : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
#if UNITY_EDITOR
	  string filepath = Application.dataPath +"/StreamingAssets"+"/json.txt";
#elif UNITY_IPHONE
	  string filepath = Application.dataPath +"/Raw"+"/json.txt";
#endif	
	   
		StreamReader sr  = File.OpenText(filepath);
		string  strLine = sr.ReadToEnd();
	   JsonData jd = JsonMapper.ToObject(strLine);
	   JsonData gameObjectArray = jd["GameObjects"];
		int i,j,k;
		for (i = 0; i < gameObjectArray.Count; i++)
		{
	 	   JsonData senseArray = gameObjectArray[i]["scenes"];
		   for (j = 0; j < senseArray.Count; j++)
	   	   {
				string sceneName = (string)senseArray[j]["name"];
				if(!sceneName.Equals("Assets/StarTrooper.unity"))
				{
					continue;
				}
				JsonData gameObjects = senseArray[j]["gameObject"];
				
				for (k = 0; k < gameObjects.Count; k++)
				{
					string objectName = (string)gameObjects[k]["name"];
					string asset = "Prefab/" + objectName;
					Vector3 pos = Vector3.zero;
					Vector3 rot = Vector3.zero;
					Vector3 sca = Vector3.zero;
					
					
					JsonData position = gameObjects[k]["position"];
					JsonData rotation = gameObjects[k]["rotation"];
					JsonData scale = gameObjects[k]["scale"];
					
					pos.x = float.Parse((string)position[0]["x"]);
					pos.y = float.Parse((string)position[0]["y"]);
					pos.z = float.Parse((string)position[0]["z"]);

					rot.x = float.Parse((string)rotation[0]["x"]);
					rot.y = float.Parse((string)rotation[0]["y"]);
					rot.z = float.Parse((string)rotation[0]["z"]);
				
					sca.x = float.Parse((string)scale[0]["x"]);
					sca.y = float.Parse((string)scale[0]["y"]);
					sca.z = float.Parse((string)scale[0]["z"]);
					
					GameObject ob = (GameObject)Instantiate(Resources.Load(asset),pos,Quaternion.Euler(rot));
					ob.transform.localScale = sca;
					
				}
				
		   }
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,200,200),"JSON WORLD"))
		{
			Application.LoadLevel("XMLScene");
		}
		
	}

}
