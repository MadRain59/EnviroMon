using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;

    private void Awake()
    {
        //FindObjectOfType<Canvas>() used to find the object of type <Canvas> latched onto the game object with the attached script component
        gameCanvas = FindObjectOfType<Canvas>();

    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        //takes the world position of the character and creates the text when character hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //calls the TMP_Text into the script
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        //turns the int value of damageReceived into a string of text
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        //takes the world position of the character and creates the text when character hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //calls the TMP_Text into the script
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        //turns the int value of damageReceived into a string of text
        tmpText.text = healthRestored.ToString();
    }
}
