using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public GameObject effect;
    GameControl m_gc;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        m_gc = FindObjectOfType<GameControl>();
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ammo"))
        {
            //Instantiate(effect, transform.position, Quaternion.Euler(-90, 0, 0));
            m_gc.setTimePig();
            m_gc.setPigDestroy();
            m_gc.ScoreIncrement();
            ani.SetBool("isEx", true);
            Destroy(gameObject, 0.2f);
        }
    }

}
