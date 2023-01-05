using UnityEngine;

public class BalleController : MonoBehaviour
{
    public float force;
    public float coeff_rebond;
    private bool in_game;
    private Vector3 lastVelocity;
    private Vector3 vec;
    private GameObject sol;
    private GameObject am;
    private float limite_z;
    private Vector3 pos_init;
    private Rigidbody rigidBalle;
    private TrailRenderer trailBalle;
    private AudioManager audioManager;
    [SerializeField]
    private float init_force = 10f;
    [SerializeField]
    private float X_min = 10f;
    [SerializeField]
    private float Z_min = 10f;
    // GameObject 
    public GameObject playerObject;

    // Start is called before the first frame update
    void Awake()
    {
        in_game = false;
        vec = new Vector3(Random.Range(0.0f, 1.0f), 0, Random.Range(0.5f, 1.0f));
        sol = GameObject.Find("Sol");
        am = GameObject.Find("AudioManager");
        //On recupere la valeur minimum de z
        limite_z = sol.GetComponent<Renderer>().bounds.min.z;
        rigidBalle = GetComponent<Rigidbody>();
        trailBalle = GetComponent<TrailRenderer>();
        audioManager = am.GetComponent<AudioManager>();
    }

    void Start()
    {
        //On recupere la position initiale de la balle
        pos_init = transform.position;
    }

    void OnPress()
    {
        if (in_game == false)
        {
            rigidBalle.velocity = vec * force;
            in_game = true;
            trailBalle.enabled = true;
            audioManager.Play("start");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
        //Trick pour WebGL (simmer.io)
        if (Input.GetKey(KeyCode.Space))
        {
            OnPress();
        }

        if (!in_game && playerObject != null)
        {
            // get and use the player position 
            pos_init.x = playerObject.transform.position.x;
            pos_init.z = playerObject.transform.position.z+.9f;

            // apply player X position to the ball 
            transform.position = pos_init;
        }
    }
    void FixedUpdate()
    {
        lastVelocity = rigidBalle.velocity;
        rigidBalle.velocity = rigidBalle.velocity.normalized * force;
        var pos_balle_z = transform.position.z;
        if (pos_balle_z < limite_z)
        {
            in_game = false;
            transform.position = pos_init;
            rigidBalle.velocity = Vector3.zero;
            rigidBalle.angularVelocity = Vector3.zero;
            rigidBalle.Sleep();
            vec = new Vector3(Random.Range(0.0f, 1.0f), 0, Random.Range(0.5f, 1.0f));
            trailBalle.enabled = false;
            audioManager.Play("go");
            force = init_force;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            force = force + coeff_rebond;
            rigidBalle.velocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
        }
        if (collision.gameObject.tag == "Bord") 
        {
            rigidBalle.velocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
            audioManager.Play("hit");
            float angle = Vector3.Angle(rigidBalle.velocity, collision.contacts[0].normal);
            //Debug.Log(angle);
            if (Mathf.Abs(rigidBalle.velocity.x) < X_min || Mathf.Abs(rigidBalle.velocity.z) < X_min )
            {
                rigidBalle.velocity = new Vector3((rigidBalle.velocity.x < 0) ? -X_min : X_min, rigidBalle.velocity.y, (rigidBalle.velocity.z < 0) ? -Z_min : Z_min);
                //Debug.Log("FORCED!!!");
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            rigidBalle.velocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
            audioManager.Play("hit");
        }
    }
}
