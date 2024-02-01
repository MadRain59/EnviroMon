using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteractionTrigger : MonoBehaviour
{
    public NoteUIController noteUIController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tetapkan rujukan NoteUIController kepada instans ini
            noteUIController = other.GetComponent<NoteUIController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set semula rujukan NoteUIController apabila pemain keluar dari zon pemicu
            noteUIController = null;
        }
    }

    void Update()
    {
        // Periksa input pemain untuk mengalihkan nota apabila berada dalam zon pemicu
        if (noteUIController != null && Input.GetKeyDown(KeyCode.E))
        {
            noteUIController.ToggleNote();
        }
    }
}
