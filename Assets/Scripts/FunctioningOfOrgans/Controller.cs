using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    [Header ("Text")]
    [SerializeField] private TMP_Text _organNameText;
    [SerializeField] private TMP_Text _organInfoText;
    [SerializeField] private TextWrite _textWrite;

    [Header ("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _popSound;
    [SerializeField] private AudioClip _chalkSound;

    private void Start()
    {
        Instance = this;
    }
    public void GiveInfo(string organName, TextAsset textAsset)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.PlayOneShot(_popSound);
        _audioSource.PlayOneShot(_chalkSound);
        _textWrite.timer = 0;
        _textWrite.characterIndex = 0;
        _organNameText.text = organName;
        _textWrite.textToWrite = textAsset;
    }
}
