using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerator : MonoBehaviour {

	public Transform player;
	public GameObject[] availableRooms;

	private List<GameObject> currentRooms = new List<GameObject>();
	private float screenWidthPoints = 0;

	// Use this for initialization
	void Start () {
		float screenHeight = Camera.main.orthographicSize * 2.0f;
		screenWidthPoints = Camera.main.aspect * screenHeight;
	}
	
	// Update is called once per frame
	void Update () {
		GenerateRoomIfRequired ();
	}

	void GenerateRoomIfRequired() {
		List<GameObject> roomsToRemove = new List<GameObject> ();

		bool addRooms = true;

		float playerPosX = player.position.x;

		float removeRoomX = playerPosX - screenWidthPoints;
		float addRoomX = playerPosX + screenWidthPoints;

		float furthestRoomEndX = -availableRooms[0].transform.Find("Ceiling").lossyScale.x;

		foreach (GameObject room in currentRooms) {
			float roomWidth = room.transform.Find("Ceiling").lossyScale.x;
			float roomStartX = room.transform.position.x - roomWidth * 0.5f;
			float roomEndX = room.transform.position.x + roomWidth * 0.5f;

			if (roomStartX > addRoomX)
				addRooms = false;

			if (roomEndX < removeRoomX)
				roomsToRemove.Add (room);

			furthestRoomEndX = Mathf.Max (furthestRoomEndX, roomEndX);
		}

		foreach (GameObject roomToRemove in roomsToRemove) {
			currentRooms.Remove (roomToRemove);
			Destroy(roomToRemove);
		}

		if (addRooms) {
			addRoom (furthestRoomEndX);
		}
	}

	void addRoom(float furthestRoomEndX){
		int randomRoomIndex = Random.Range (0, availableRooms.Length);

		GameObject room = Instantiate (availableRooms [randomRoomIndex]);

		float roomWidth = room.transform.Find("Ceiling").lossyScale.x;
		float roomCenter = furthestRoomEndX + roomWidth * 0.5f;
		room.transform.position = new Vector3 (roomCenter, 0, 0);

		currentRooms.Add (room);
	}
}
