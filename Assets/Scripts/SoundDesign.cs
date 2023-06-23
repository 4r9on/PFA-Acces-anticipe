using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDesign : MonoBehaviour
{
    public float PhaseOfSound;
    public List<AudioClip> clipList1;
    public List<AudioClip> clipList2;
    public List<AudioClip> clipList3;
    public AudioSource audioSource;
    public float Timing;

    private void Start()
    {
        PhaseOfSound = 1;
        if(GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
            InvokeRepeating("SoundIsLooping", 0.1f, audioSource.clip.length + Timing);
        }
    }

    public void SoundIsLooping()
    {
        Debug.Log(clipList1.Count);
        audioSource.clip = clipList1[Random.Range(0, clipList1.Count)];
        audioSource.Play();
    }
}
