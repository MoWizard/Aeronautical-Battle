using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Game Manager
    private GameManager m_GameManager; 

    // Player Audio
    public AudioSource ExplosionAudio;
    public AudioSource PlayerShootingAudio;
    public AudioClip ExplosionClip;
    public AudioClip PlayerShootingClip;
    public AudioClip LlamaClip;

    // Enemy Audio
    public AudioSource SmallExplosionAudio;
    public AudioSource EnemyShootingAudio;
    public AudioClip SmallExplosionClip;
    public AudioClip EnemyShootingClip;

    // Bullet Audio
    public AudioSource BulletExplosionAudio;
    public AudioClip BulletExplosionClip;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = GetComponent<GameManager>();

        // Player
        ExplosionAudio = gameObject.AddComponent<AudioSource>();
        ExplosionAudio.clip = ExplosionClip;
        PlayerShootingAudio = gameObject.AddComponent<AudioSource>();
        PlayerShootingAudio.clip = PlayerShootingClip;

        // Enemy
        SmallExplosionAudio = gameObject.AddComponent<AudioSource>();
        SmallExplosionAudio.clip = SmallExplosionClip;
        EnemyShootingAudio = gameObject.AddComponent<AudioSource>();
        EnemyShootingAudio.clip = EnemyShootingClip;

        // Bullet
        BulletExplosionAudio = gameObject.AddComponent<AudioSource>();
        BulletExplosionAudio.clip = BulletExplosionClip;
    }

    private void Update()
    {
        if (m_GameManager.LlamaTime == true)
        {
            PlayerShootingAudio.clip = LlamaClip;
        }
        else
        {
            PlayerShootingAudio.clip = PlayerShootingClip;
        }
    }
}
