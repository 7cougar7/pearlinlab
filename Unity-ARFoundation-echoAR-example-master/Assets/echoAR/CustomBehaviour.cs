/**************************************************************************
* Copyright (C) echoAR, Inc. 2018-2020.                                   *
* echoAR, Inc. proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at           *
* https://www.echoar.xyz/terms, or another agreement                      *
* between echoAR, Inc. and you, your company or other organization.       *
***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using UnityEngine;


public class CustomBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Entry entry;
    string ECHOAR_API_KEY = "cool-fire-1148";


    /// <summary>
    /// EXAMPLE BEHAVIOUR
    /// Queries the database and names the object based on the result.
    /// </summary>

    // Use this for initialization
    void Start()
    {
        // Add RemoteTransformations script to object and set its entry
        this.gameObject.AddComponent<RemoteTransformations>().entry = entry;

        // Qurey additional data to get the name
        string value = "";
        if (entry.getAdditionalData() != null && entry.getAdditionalData().TryGetValue("name", out value))
        {
            // Set name
            this.gameObject.name = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            string direction = "";
            if (entry.getAdditionalData().TryGetValue("direction", out direction)){
                string newDirection = direction == "right" ? "left" : "right"; 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://console.echoAR.xyz/post?key={0}&entry={1}&data={2}&value={3}", ECHOAR_API_KEY, entry.getId(), "direction", newDirection));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
        // Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        // RaycastHit hit;
        // if(Physics.Raycast(ray, out hit)) {
        //     if(hit.collider != null)
        //     {
        //         Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0);
        //         hit.collider.GetComponent<MeshRenderer>().material.color = newColor;
        //     }
        }
      
    }
}