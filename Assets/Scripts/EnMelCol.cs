using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnMelCol : MonoBehaviour
{
    EnCtrlr en;

    private void Awake()
    {
        en = FindObjectOfType<EnCtrlr>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlyrCtrlr>().Dmgd(en.atkDmg);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EnCtrlr>().Dmgd(en.atkDmg);
        }
    }
}
