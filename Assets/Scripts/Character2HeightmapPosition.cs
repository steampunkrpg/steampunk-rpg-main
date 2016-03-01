using UnityEngine;
using System.Collections;

public class Character2HeightmapPosition : MonoBehaviour {
	
	private GameObject[] Unit;
	private GameObject[] Enemy;

	// Use this for initialization
	void Start () {
		Unit = GameObject.FindGameObjectsWithTag("Unit");
		Enemy = GameObject.FindGameObjectsWithTag("Enemy");
		MoveParticles ();
	}

	void FixedUpdate () {
		GameObject camera = GameObject.Find ("Main Camera");
		CameraBounds bounds = camera.GetComponentInChildren<CameraBounds> ();
		Vector3 posC = camera.transform.position;
		posC.y = Terrain.activeTerrain.SampleHeight (camera.transform.position) + bounds.offset - bounds.zoom;
		camera.transform.position = posC;

		foreach (GameObject o in Unit) {
			if (o != null) {
				Vector3 pos = o.transform.position;
				pos.y = Terrain.activeTerrain.SampleHeight (o.transform.position) + (o.transform.lossyScale.y / 2f);
				o.transform.position = pos;
			}
		}

		foreach (GameObject o in Enemy) {
			if (o != null) {
				Vector3 pos = o.transform.position;
				pos.y = Terrain.activeTerrain.SampleHeight (o.transform.position) + (o.transform.lossyScale.y / 2f);
				o.transform.position = pos;
			}
		}
	}

	private void MoveParticles() {
		GameObject HexGrid = GameObject.Find ("HexGrid10x10P");
		foreach (Transform tile in HexGrid.transform) {
			foreach (Transform particle in tile) {
				Vector3 pos = particle.transform.position;
				pos.y = Terrain.activeTerrain.SampleHeight (particle.transform.position);
				particle.transform.position = pos;
			}
		}
	}
}
