using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // tambahkan baris ini untuk menggunakan TextMeshPro

public class NoteUIController : MonoBehaviour
{
    public GameObject noteUI; // Rujukan kepada elemen UI TextMeshPro atau keseluruhan UI nota
    public TextMeshProUGUI noteText; // Rujukan kepada elemen UI TextMeshPro untuk memaparkan kandungan nota

    private bool isNoteOpen = false;

    void Start()
    {
        TutupNota();
    }

    void Update()
    {
        // Periksa input pemain untuk mengalihkan nota
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleNote();
        }
    }

    void BukaNota()
    {
        // Tetapkan kandungan nota di sini (anda boleh muatkan dari fail atau tetapkan di dalam skrip)
        noteText.text = "Ini adalah kandungan nota. Tekan 'E' untuk menutup.";

        // Paparkan UI nota
        noteUI.SetActive(true);

        isNoteOpen = true;
    }

    void TutupNota()
    {
        // Sembunyikan UI nota
        noteUI.SetActive(false);

        isNoteOpen = false;
    }

    public void ToggleNote()
    {
        // Fungsi ini untuk memanggil antara BukaNota dan TutupNota berdasarkan status nota
        if (isNoteOpen)
        {
            TutupNota();
        }
        else
        {
            BukaNota();
        }
    }
}
