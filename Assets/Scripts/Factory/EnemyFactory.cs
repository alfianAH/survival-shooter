using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField] private GameObject[] enemyPrefab;
    
    public GameObject FactoryMethod(int tag, Vector3 position)
    {
        GameObject enemy = Instantiate(enemyPrefab[tag], position, Quaternion.identity);
        return enemy;
    }
}
