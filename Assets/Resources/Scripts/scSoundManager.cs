using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scSoundManager : MonoBehaviour
{
    public  AudioClip acError, acMuerte, acFire, acWin;
    private AudioSource audioSource;
    void Start()
    {
        acError = Resources.Load<AudioClip>("Audios/Error or failed");
        acMuerte = Resources.Load<AudioClip>("Audios/qubodupImpactMeat02");
        acFire = Resources.Load<AudioClip>("Audios/rifle");
        acWin = Resources.Load<AudioClip>("Audios/Won!");
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "fire":
                StartCoroutine(IEPlaySound(acFire, 0.3f));
                break;
            case "death":
                StartCoroutine(IEPlaySound(acMuerte));
                break;
            case "lose":
                StartCoroutine(IEPlaySound(acError));
                break;
            case "win":
                StartCoroutine(IEPlaySound(acWin));
                break;
            default:
                break;
        }
    }

    IEnumerator IEPlaySound(AudioClip audio, float volumen = 1f)
    {
        GameObject soundGO = new GameObject("Sonido");
        AudioSource audSRC = soundGO.AddComponent<AudioSource>();
        audSRC.volume = volumen;
        audSRC.PlayOneShot(audio);
        yield return new WaitForSeconds(5f);
        Destroy(soundGO);
    }
}
