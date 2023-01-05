using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpBricks : MonoBehaviour
{   
    [SerializeField]
    private int hp = 1;
    private GameObject am;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("AudioManager");
        audioManager = am.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hp > 1)
        {
            audioManager.Play("brick");
            hp--;
        }
        else
        {
            audioManager.Play("xplode");
            Destroy(gameObject);
        }  
    }
}
