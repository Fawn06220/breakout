using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgzik : MonoBehaviour
{
    private GameObject am;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();
        audioManager.Play("bg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
