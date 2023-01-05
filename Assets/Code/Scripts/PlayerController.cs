using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // chemin de la classe stockant les informations en rapport avec les mouvements 

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float PlayerSpeed = 20f;


   // [SerializeField] 
    private Rigidbody rb; //Rigidbody indique au script son utilisation (type) rb est le nom de la variable contenant les informations du rigidbody
   // [SerializeField] 
    private float movementX; // deplacement fleche horizontal
    

    // Start is called before the first frame update comme les informations
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*declaration
    * InputValue = type  / movementValue = nom de la variable utilise par la fonction qui va stocke les deplacement fleches droite gauche haut bas 
    * donc deplacement sur x horizontal et y vertical / vector 2 
    */
    void OnMove(InputValue movementValue)
    {
        //Fonctions
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        
    }
       

    //FixedUpdate pour tout ce qui est calcul physique
    private void FixedUpdate()
    {
        // declaration environnement 

        rb.velocity = new Vector3(movementX, 0.0f, 0.0f) * PlayerSpeed;

    }

} 
