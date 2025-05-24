using Mirror;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 angleLimits;
    [SerializeField] private float forceMult;
    
    [SerializeField] private GameObject[] itemPrefabs;

    private void SpawnItem(int index) {
        float angle = Random.Range(angleLimits.x, angleLimits.y);
        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        
        GameObject newItem = Instantiate(itemPrefabs[index], transform.position, Quaternion.identity);
        NetworkServer.Spawn(newItem);
        Rigidbody2D rb = newItem.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * forceMult, ForceMode2D.Impulse);
    }

    
    public void BtnSpawnItem(int item) { 
        SpawnItem(item);
    }
}
