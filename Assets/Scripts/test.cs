using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ani.SetBool("isEx", true);
            //ani.SetBool("notEx", false);
            //Destroy(gameObject);

        }

        //if (Input.GetButton("Fire2"))
        //{
        //    ani.SetBool("notEx", true);
        //    ani.SetBool("isEx", false);

        //}
    }
}
