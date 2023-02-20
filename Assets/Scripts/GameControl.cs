using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject pig;
    public GameObject ammo;
    int m_pigCount;
    int m_ammoCount;
    int m_score;
    UI m_ui;
    float m_timePig;
    float m_timeAmmo;
    bool m_isOperation;
    // Start is called before the first frame update
    void Start()
    {
        m_timePig = 0;
        m_pigCount = 0;
        m_ui = FindObjectOfType<UI>();
        m_ui.SetScoreText("Score: " + m_score);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_pigCount == 0 && m_timePig <= 0)
        {
            m_pigCount = 1;

            SpawnPig();
        }
        if (m_ammoCount==0 && m_timeAmmo <=0)
        {
            m_ammoCount = 1;

            SpawnAmmo();
            m_isOperation = false;
        }
        {
        }
        m_timePig -= Time.deltaTime;
        m_timeAmmo -= Time.deltaTime;
    }

    public void SpawnPig()
    {
        float randXpos = Random.Range(4f, 7.8f);

        Vector2 spawnPos = new Vector2(randXpos, 2f);

        if (pig)
        {
            Instantiate(pig, spawnPos, Quaternion.identity);
        }
    }

    public void SpawnAmmo()
    {
        Vector2 spawnPos = new Vector2(-4.22f, -1.81f);

        if (ammo)
        {
            Instantiate(ammo, spawnPos, Quaternion.identity);
        }
    }

    public void Setscore(int value)
    {
        m_score = value;
    }

    public int GetScore()
    {
        return m_score;
    }

    public void ScoreIncrement()
    {
        m_score++;
        m_ui.SetScoreText("Score: " + m_score);
    }

    public void setPigDestroy()
    {
        m_pigCount = 0;
    }

    public void setAmmoDestroy()
    {
        m_ammoCount = 0;
       
    }

    public void setTimePig()
    {
        m_timePig = 4;
    }

    public void setTimeAmmo()
    {
        m_timeAmmo = 5;
        
    }

    public void isOperation()
    {
        m_isOperation = true;
    }

    public bool getOperation()
    {
        return m_isOperation;
    }
}
