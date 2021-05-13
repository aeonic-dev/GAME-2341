using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 spawnLocation;
    public AudioSource audioSource;
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
