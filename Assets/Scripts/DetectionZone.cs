using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    //creates a unity event for detecting colliders at the start of the game
    public UnityEvent noColliderRemain;

    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D coll;


    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if (detectedColliders.Count <= 0)
        {
            noColliderRemain.Invoke();
        }
    }
}
