using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject bossObj;
    public List<Collider2D> detectedCollider = new List<Collider2D>();
    private Collider2D col;

    public GameObject bossHealth;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        detectedCollider.Add(other);
        if (detectedCollider.Count > 0)
        {
            bossHealth.gameObject.SetActive(true);
            bossObj.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        detectedCollider.Remove(other);
    }
}
