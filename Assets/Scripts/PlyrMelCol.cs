using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrMelCol : MonoBehaviour
{
    PlyrCtrlr plyr;

    private void Awake()
    {
        plyr = FindObjectOfType<PlyrCtrlr>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnCtrlr>().Dmgd(plyr.atkDmg);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnCtrlr>().Dmgd(plyr.atkDmg);
        }
    }
}
