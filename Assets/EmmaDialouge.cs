using UnityEngine;

public class EmmaDialogue : MonoBehaviour
{
    public GameObject hitDialougeEmma;
    public GameObject dialogueEmma;
    Damageable damageable;

    private void Start()
    {
        damageable = GetComponent<Damageable>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            DialogueOn();
            Invoke("DeactivateDialogue", 1f);
        }
    }

    private void Update()
    {
        if (damageable.IsHit)
        {
            HitDialogeuOn();
            Invoke("DeactivateHitDialogue", 1f);
        }
    }
    private void DialogueOn()
    {
        dialogueEmma.SetActive(true);
    }

    private void DeactivateDialogue()
    {
        dialogueEmma.SetActive(false);
    }

    private void HitDialogeuOn()
    {
        hitDialougeEmma.SetActive(true);
    }

    private void DeactivateHitDialogue()
    {
        hitDialougeEmma.SetActive(false);
    }


}
