using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour {

	public Renderer[] layers;
	public float[] speed;

	public bool activeScroll = true;

	private float offset = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (activeScroll) {
			for (int i = 0; i < layers.Length; i++) {
				if (speed [i] != 0) {
					offset = Time.time;
					float offsetElement = offset * speed[i];
					layers [i].material.mainTextureOffset = new Vector2 (offsetElement, 0);
				}
			}
		}
	}
}
