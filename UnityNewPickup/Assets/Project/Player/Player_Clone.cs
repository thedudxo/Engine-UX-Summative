﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Clone : MonoBehaviour {

    private GameObject targetObject;        // The real object the player wants to clone
    private GameObject clonedObject;        // The current cloned object visible in the world
    public GameObject targetPosition;       // Position to clone new objects at (in front of player)
    public Material cloneMaterial;
    GameObject mainCamera;
    bool cloned = false;

    void Start ()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
	

    public void reset()
    {
        targetObject = null;
        Destroy(clonedObject);
        cloned = false;
    }

	void Update () {

        if (Input.GetMouseButtonDown(1))    //right click
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray cloneRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if(Physics.Raycast(cloneRay, out hit))
            {
                GameObject lookingAt = hit.transform.gameObject;

                if (lookingAt.GetComponent<Cloneable>() != null)
                {
                    targetObject = lookingAt;
                    Debug.Log("Object copied to clipboard");
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && targetObject != null && cloned == false) {
            Player_Pickup.Instance.cloning = true;
            clonedObject = Instantiate(targetObject, mainCamera.transform.position + mainCamera.transform.forward * Player_Pickup.Instance.distance, Quaternion.identity);
            clonedObject.name = "Clone";
            clonedObject.GetComponent<Cloneable>().isClone = true;
            Player_Pickup.Instance.carrying = true;
            clonedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Player_Pickup.Instance.carriedObject = clonedObject;
            cloned = true;

            clonedObject.GetComponent<MeshRenderer>().material = cloneMaterial;
        }

        if (Input.GetMouseButtonDown(0) && targetObject != null && cloned == true) {
            if (targetObject.name == "Clone") {
                targetObject = null;
                Debug.Log("No clone");
            }
            Player_Pickup.Instance.cloning = true;
            clonedObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * Player_Pickup.Instance.distance;
            clonedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Player_Pickup.Instance.carriedObject = clonedObject;
            Player_Pickup.Instance.carrying = true;
        }

    }
}
/*
        if (Input.GetMouseButton(0) && targetObject != null) //left click
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(clonedObject);
                clonedObject = Instantiate(targetObject);
                clonedObject.SetActive(false);
                if (Button_Behaviour.Instance.weight >= 1) {
                    Button_Behaviour.Instance.ButtonCheck();
                }
            }
            

            clonedObject.transform.position = targetPosition.transform.position;
            clonedObject.transform.rotation = targetPosition.transform.rotation;
            clonedObject.GetComponent<Rigidbody>().isKinematic = true;
            clonedObject.GetComponent<Rigidbody>().isKinematic = false; //Resets the "velocity" caused by "falling"
            clonedObject.SetActive(true);

            clonedObject.GetComponent<MeshRenderer>().material = cloneMaterial;
            
        }*/
