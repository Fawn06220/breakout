using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatorBrick : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lst_prefabs;
    [SerializeField]
    private List<GameObject> lst_bonus;

    private float x = -7.5f;
    private float y = .5f;
    private float z = 9.5f;


    // Start is called before the first frame update
    void Start()
    {
        for (var j = 0; j < 10; j++)
        {
            for (var i = 0; i < 8; i++)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {   
                    var alea = Random.Range(0, lst_prefabs.Count);
                    Instantiate(lst_prefabs[alea], new Vector3(x + i, y, z - j), Quaternion.identity);
                    Instantiate(lst_prefabs[alea], new Vector3(-x - i, y, z - j), Quaternion.identity);               

                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
