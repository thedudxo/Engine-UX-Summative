﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pickup : MonoBehaviour {

    private GameObject mainCamera;
    public GameObject carriedObject;
    private float pickupDist = 3;
    public GameObject carrier;
    public float smooth;
    public bool carrying = false;

	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
	}


	void FixedUpdate () {
		if (carrying) {
            Carry(carriedObject);
            CheckDrop();
        } else {
            Pickup();
        }
	}

    void Carry(GameObject o) {
        o.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(o.transform.position, carrier.transform.position, Time.deltaTime * smooth));
    }

    void CheckDrop() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Drop();
        }
    }

    public void Drop(){
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carrier.GetComponent<SphereCollider>().enabled = false;
    }

    void Pickup() {
        if (Input.GetKeyDown(KeyCode.E) && !this.GetComponent<Player_Clone>().cloning) {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Grabbable g = hit.collider.GetComponent<Grabbable>();
                if(g != null && hit.distance <= pickupDist) {
                    carrying = true;
                    g.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                    g.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    carriedObject = g.gameObject;
                    carriedObject.transform.rotation = Quaternion.identity;
                    carrier.GetComponent<SphereCollider>().enabled = true;
                }
            }
        }
    }

}