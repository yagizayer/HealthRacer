using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicClass : MonoBehaviour
{

    public AudioSource _audioSource;
    private GameObject[] other;
    private bool NotFirst = false;
    private void Awake()
    {

        other = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
    }
    public void PauseMusic()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        _audioSource.Pause();
    }
    public void UnpouseMusic()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        _audioSource.UnPause();
    }
}