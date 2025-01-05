using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to handle scene transitions and constant data throughout all the scenes
public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance;
    public AudioSource MusicAudioSource, SFXAudioSource;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
