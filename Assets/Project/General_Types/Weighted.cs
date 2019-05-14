﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour {

    public bool overButton = false;
    public bool destroyed = false;

    public void Gravity() {
        if(PlayerManager.player_Clone.clonedObject != gameObject && PlayerManager.player_Pickup.carriedObject != gameObject) {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void Update() {
        if (overButton && PlayerManager.player_Pickup.carriedObject != gameObject) {
            Hover(gameObject);
        }
    }
}