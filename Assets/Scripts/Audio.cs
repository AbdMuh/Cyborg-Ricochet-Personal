using UnityEngine;

public class Audio : MonoBehaviour
{

    
    private static Audio instance;

    private void Awake()
    {
        // Check if there's already an instance of AudioManager
        if (instance == null)
        {
            // If not, set this as the instance and don't destroy it on load
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    [Header("-----Audio Sound-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio SFX-----")]
    public AudioClip Background;
    public AudioClip Booster;
    public AudioClip Bounce;
    public AudioClip ButtonClick;
    public AudioClip IconClick;
    public AudioClip BulletSound;
    public AudioClip SwordAttack;
    public AudioClip LaserHit;
    public AudioClip BladeHit;
    public AudioClip EnemyDeath;


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
