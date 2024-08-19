using UnityEngine;

public class FinishPoint2 : MonoBehaviour
{
    [SerializeField] GameObject LevelComplete;



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelComplete.SetActive(true);
            
        }
    }

}
