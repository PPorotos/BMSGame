using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObj : MonoBehaviour
{
    public int speed;
    public bool isStart = false;

    public int channel;
    public float noteTime;

    public float destroyPositionY;
    public float destroyDelayTime;

    public int noteBlockNum = 0;
    public bool missCheck = false;

    ClickButton button;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bottom")
        {
            gameObject.GetComponent<Renderer>().material.color = Color.black;
            //gameObject.GetComponentInParent<Renderer>().material.color = Color.green;

            //Transform particleObject = (Transform)Instantiate(Resources.Load("Particle/Effect1", typeof(Transform)), new Vector3(transform.position.x, -29f, -1), Quaternion.identity);
            switch (noteBlockNum)
            {
                case 1:
                    button = GameObject.Find("Button1").GetComponent<ClickButton>();
                    button.count += 1;
                    button.MissCombo();
                    break;
                case 2:
                    button = GameObject.Find("Button2").GetComponent<ClickButton>();
                    button.count += 1;
                    button.MissCombo();
                    break;
                case 3: 
                    button = GameObject.Find("Button3").GetComponent<ClickButton>();
                    button.count += 1;
                    button.MissCombo();
                    break;
                case 4:
                    button = GameObject.Find("Button4").GetComponent<ClickButton>();
                    button.count += 1;
                    button.MissCombo();
                    break;
                case 5:
                    button = GameObject.Find("Button5").GetComponent<ClickButton>();
                    button.count += 1;
                    button.MissCombo();
                    break;
            }
            Destroy(gameObject);
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        if (isStart == true)
        {
            StartCoroutine(move());
        }
    }

    IEnumerator move()
    {

        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (transform.position.y > destroyPositionY)
        {
            transform.Translate(Vector3.down * speed * Time.smoothDeltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

        yield return null;
    }

    public void setPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

}
