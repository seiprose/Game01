using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseBow : MonoBehaviour
{
    [SerializeField] private Rigidbody[] Arrows;
    [SerializeField] GameObject Bow;
    public Rigidbody arrowSet;

    Arrow arrow;
    public bool setBow = false;
    public bool readyShoot = false;
    public float arrowPower = 0;
    public float arrowFullPower;
    public int i = 0;
    public Image crossHair;
    public Image chargingBar;
    public Text currentArrow;
    public bool useBow = false;

    void Start()
    {
        Bow.SetActive(false);
        arrow = GetComponent<Arrow>();
        crossHair.gameObject.SetActive(false);
        chargingBar.gameObject.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            useBow = !useBow;
        }
        if(useBow)
        {
            Bow.SetActive(true);
            crossHair.gameObject.SetActive(setBow);
            currentArrow.text = Arrows[i].transform.name;
            ArrowSelect();
            if(Input.GetButtonDown("Fire2"))
            {
                setBow = !setBow;
            }
            if(setBow == true)
            {
                Shoot();
            }
        }
        else
        {
            Bow.SetActive(false);
            crossHair.gameObject.SetActive(false);
            chargingBar.gameObject.SetActive(false);
            return;
        }
    }

    void ArrowSelect()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            i++;
            if(i >= Arrows.Length + 1)
            {
                i = Arrows.Length + 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            i--;
            if(i<=0)
            {
                i = 0;
            }
        }
    }

    void Shoot()
    {
        if(Input.GetButton("Fire1"))
        {
            arrowSet = Arrows[i];
            arrowFullPower = Arrows[i].gameObject.GetComponent<Arrow>().fullCharge;
            chargingBar.gameObject.SetActive(true);
            chargingBar.fillAmount = arrowPower / arrowFullPower;

            arrowPower += Time.deltaTime * 5f;
            if(arrowPower == arrowFullPower)
            {
                arrowPower = arrowFullPower;
            }
        }
        if(Input.GetButtonUp("Fire1"))
        {
            float y = transform.localEulerAngles.y;
            Vector3 angle = new Vector3(0, -y, 0);

            Rigidbody newArrow = (Rigidbody)Instantiate(arrowSet, Camera.main.transform.position, Camera.main.transform.rotation);
            newArrow.AddForce(Camera.main.transform.forward * arrowPower, ForceMode.Impulse);            
            arrowPower = 0;
            chargingBar.gameObject.SetActive(false);
        }
    }


}
