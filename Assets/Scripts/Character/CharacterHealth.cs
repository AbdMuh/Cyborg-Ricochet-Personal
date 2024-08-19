
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using CandyCoded.HapticFeedback;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    public float _health;
    public HealthBar HealthBar;

    private void Start()
    {
        Time.timeScale = 1f;
        _health = 100f;
        HealthBar.SetMaxHealth(_health);
    }

    private void FixedUpdate()
    {
        if (_health <= 0)
        {
            Time.timeScale = 0f;
            GameOver.SetActive(true);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HapticFeedback.HeavyFeedback();
            _health -= 10f;
            HealthBar.SetHealth(_health);
        }
    }
    public void LaserDamage()
    {
        HapticFeedback.HeavyFeedback();
        _health -= 100f * Time.deltaTime;
        HealthBar.SetHealth(_health);
    }

    public void BladeDamage()
    {
        HapticFeedback.HeavyFeedback();
        _health -= 2f;
        HealthBar.SetHealth(_health);
    }

}
