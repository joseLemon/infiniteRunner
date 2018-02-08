using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsGenerator : MonoBehaviour {

	public Transform player;
	public GameObject[] availableObjects;
	public int maxNumObjectsInScene = 10;
	// para que el objeto sea creado antes de entrar a la camara
	public float minimumDistanceObjectSpawn = 13;

	private List<GameObject> objects = new List<GameObject>();
	private float screenWidthPoints = 0.0f;
	private float screenHeightPoints = 0.0f;

	// Use this for initialization
	void Start () {
		screenHeightPoints = Camera.main.orthographicSize * 2.0f;
		screenWidthPoints = Camera.main.aspect * screenHeightPoints;
	}
	
	// Update is called once per frame
	void Update () {
		GenerateObjectsIfRequired ();
	}

	void GenerateObjectsIfRequired () {
		float playerX = player.position.x;
		// para que el objeto se remueva fuera de la pantalla, no en la orilla
		float removeObjectX = playerX - screenWidthPoints * 0.75f;
		float furthestObjectX = minimumDistanceObjectSpawn;

		List<GameObject> objectsToRemove = new List<GameObject> ();

		foreach (GameObject obj in objects) {
			float objPosX = obj.transform.position.x;
			float objWidth = obj.transform.Find ("Outline").lossyScale.x;

			furthestObjectX = Mathf.Max (furthestObjectX, objPosX + objWidth * 0.5f);

			if (objPosX < removeObjectX)
				objectsToRemove.Add (obj);
		}

		foreach (GameObject obj in objectsToRemove) {
			objects.Remove (obj);
			Destroy (obj);
		}

		int currentItemsNum = objects.Count;
		if (currentItemsNum < maxNumObjectsInScene) {
			AddObject (furthestObjectX);
		}
	}

	void AddObject(float lastObjectX) {
		int randomIndex = Random.Range (0, availableObjects.Length);

		GameObject obj = Instantiate(availableObjects[randomIndex]);

		float objWidth = obj.transform.Find ("Outline").lossyScale.x;
		float objHeight = obj.transform.Find ("Outline").lossyScale.y;

		float objectPositionX = lastObjectX + Random.Range (3 * objWidth, 4 * objWidth);
		float objectPositionY = Random.Range (-screenHeightPoints * 0.5f + objHeight, screenHeightPoints * 0.5f - objHeight);

		obj.transform.position = new Vector3 (objectPositionX, objectPositionY, 0.0f);

		objects.Add (obj);
	}
}
