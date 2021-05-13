using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject player;
    public float xOffset;
    public float yOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 npos = (-player.transform.position/6-transform.position)/10;
        npos.x += xOffset;
        npos.y += yOffset;
        transform.position += npos;
    }
}
