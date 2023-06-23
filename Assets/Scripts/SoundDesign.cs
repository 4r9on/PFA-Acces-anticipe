using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDesign : MonoBehaviour
{
    public float PhaseOfSound;
    public List<AudioClip> clipList1;
    public List<AudioClip> clipList2;
    public List<AudioClip> clipList3;

    private void Start()
    {
        PhaseOfSound = 1;
    }
}
