using UnityEngine;

public class BossKnightEnter : MonoBehaviour
{
    [SerializeField] private BossKnight bossHK;
    [SerializeField] private BossDoorColor bossDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Room Enter!");
        bossHK.BossSpawn();
        bossDoor.gameObject.SetActive(true);
        
        bossDoor.Close();
        
        Destroy(gameObject);
    }
}
