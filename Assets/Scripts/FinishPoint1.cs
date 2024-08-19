using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] GameObject LevelComplete;
   private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelComplete.SetActive(true);
            //Debug.Log("Level Completed!");
        }
    }
}
