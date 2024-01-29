using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPop : MonoBehaviour
{
    private bool interactable = false;
    public GameObject infoSheet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = false;
            Infooff();
        }
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            if (infoSheet.activeSelf)
            {
                Infooff();
            }
            else
            {
                Infoon();
            }
        }
    }

    void Infoon()
    {
        infoSheet.SetActive(true);
    }

    void Infooff()
    {
        infoSheet.SetActive(false);
    }
}
