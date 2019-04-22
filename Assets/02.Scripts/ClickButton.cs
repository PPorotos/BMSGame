using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickButton : MonoBehaviour
{
    //public float notePositionY;
    public int noteNumber;

    public List<GameObject> notePosition;

    public int count = 0;
    public int combo = 0;

    public float positionX;
    public float positionY;

    public GameObject perfectPrefab;
    public GameObject greatPrefab;
    public GameObject greenEffectParticle;
    public GameObject blueEffectParticle;
    GameObject greenEffect;
    GameObject blueEffect;
    GameObject perfect;
    GameObject great;

    GameManager gameManager;

    private void Awake()
    {
        notePosition = new List<GameObject>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
       positionX = gameObject.transform.position.x;
       positionY = gameObject.transform.position.y;
    }

    public void PerfectEffect()
    {
        perfect = Instantiate(perfectPrefab, new Vector3(positionX, positionY + 1f, 0), Quaternion.identity);
        perfect.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Destroy(perfect, 1f);
        greenEffect = Instantiate(greenEffectParticle, new Vector3(positionX, positionY, 0), Quaternion.identity);
        Destroy(greenEffect, 1f);
    }
    public void GreatEffect()
    {
        blueEffect = Instantiate(blueEffectParticle, new Vector3(positionX, positionY, 0), Quaternion.identity);
        Destroy(blueEffect, 1f);
    }

    public void ButtonClick()
    {
        float distance = gameObject.transform.position.y - notePosition[count].transform.position.y;

        if (distance <= 0.5f && distance >= -0.8f)
        {
            PerfectEffect();
            Destroy(notePosition[count]);
            count +=1;
            gameManager.combo += 1;
            gameManager.score += 100;
        }
        else if (distance <= 0.5f && distance >= -1.3f)
        {
            GreatEffect();
            Destroy(notePosition[count]);
            count +=1;
            gameManager.combo += 1;
            gameManager.score += 50;
        }
        else if (distance > 0.5f)
        {
            Destroy(notePosition[count]);
            count +=1;
            gameManager.combo = 0;
            gameManager.score += 5;
        }
    }

    public void MissCombo()
    {
        gameManager.combo = 0;
    }
}
