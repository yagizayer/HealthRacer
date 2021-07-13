using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMP_Text = TMPro.TMP_Text;
using UnityEngine.SceneManagement;

public class AntibodyGameManager : MonoBehaviour
{
    public bool isGameRunning = false;
    public float gameSpeedMultiplier;

    #region Start Panel

    [SerializeField]
    private GameObject _startPanel;

    [SerializeField]
    private GameObject[] _antibodyButtons = new GameObject[4];

    public void StartGameButton()
    {
        _startPanel.SetActive(false);

        foreach (GameObject button in _antibodyButtons)
        {
            button.SetActive(true);
        }

        isGameRunning = true;
    }

    #endregion

    #region Finish Panel

    [SerializeField]
    private GameObject _finishPanel;

    [SerializeField]
    private TMP_Text _finishScoreText;

    public void RestartButton()
    {
        SceneManager.LoadScene("AntibodyGame");
    }

    #endregion

    #region Audio

    private AudioSource _audioSource;
    private AudioSource _musicSource;

    [SerializeField]
    private AudioClip _protected;

    [SerializeField]
    private AudioClip _buttonClick;

    [SerializeField]
    private AudioClip _hit;

    [SerializeField]
    private AudioClip _youWin;

    [SerializeField]
    private AudioClip _brokenHeart;

    [SerializeField]
    private AudioClip _buttonPointerEnter;

    public void PlayAudio(int audio)
    {
        if (_isAudioOn)
        {
            switch (audio)
            {
                case Constants.ProtectedSound:
                    _audioSource.PlayOneShot(_protected, 2f);
                    break;
                case Constants.ButtonClickSound:
                    _audioSource.PlayOneShot(_buttonClick, 1f);
                    break;
                case Constants.HitSound:
                    _audioSource.PlayOneShot(_hit, 2f);
                    break;
                case Constants.YouWinSound:
                    _audioSource.PlayOneShot(_youWin, 1f);
                    break;
                case Constants.BrokenHeart:
                    _audioSource.PlayOneShot(_brokenHeart, 3f);
                    break;
                case Constants.ButtonPointerEnter:
                    _audioSource.PlayOneShot(_buttonPointerEnter, 1.5f);
                    break;
            }
        }
    }

    #endregion

    #region Score System

    private int _score;

    [SerializeField]
    private TMP_Text _scoreText;

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
        PlayAudio(Constants.ProtectedSound);
    }
    #endregion

    #region Game Functions

    private void GameOver()
    {
        isGameRunning = false;
        StartCoroutine(ActiveFinishPanel());
    }

    private IEnumerator ActiveFinishPanel()
    {
        yield return new WaitForSeconds(1.4f);
        _finishPanel.SetActive(true);
        _finishScoreText.text = string.Format("Skor: {0}", _score.ToString());
        _audioSource.Stop();
        PlayAudio(Constants.YouWinSound);
    }

    #endregion

    #region Antibody

    private bool _canSpawnAntibody = true;

    [SerializeField]
    private GameObject[] _antibodies = new GameObject[4];

    private Vector2 _antibodySpawnPos = new Vector2(7.5f, 0);

    private VirusType _selectedAntibodyType;
    public void SetAntibodyType(int type)
    {
        _selectedAntibodyType = (VirusType)type;
        if (_canSpawnAntibody)
        {
            SpawnAntibody();
            _canSpawnAntibody = false;
            StartCoroutine(CooldownToSpawn());
        }

    }

    public void SpawnAntibody()
    {
        GameObject antibody = Instantiate(_antibodies[(int)_selectedAntibodyType], _antibodySpawnPos, Quaternion.identity);
    }

    private IEnumerator CooldownToSpawn()
    {
        yield return new WaitForSeconds(1.5f);
        _canSpawnAntibody = true;
    }

    #endregion

    #region Spawning Virus

    [SerializeField]
    private GameObject[] _viruses = new GameObject[4];

    [SerializeField]
    private float _spawnTime;

    private float _tempSpawnTime;

    private Vector2 _virusSpawnPos = new Vector2(-12, 0);

    private void SpawnVirus()
    {
        int randomIndex = Random.Range(0, 4);

        GameObject newVirus = Instantiate(_viruses[randomIndex], _virusSpawnPos, Quaternion.identity);
    }

    private void CheckTimeToSpawnVirus()
    {
        if (_tempSpawnTime > 0)
        {
            _tempSpawnTime -= Time.deltaTime;
        }
        else
        {
            _tempSpawnTime = _spawnTime;
            SpawnVirus();
        }
    }

    #endregion

    #region Health System

    private int _health = 4;

    [SerializeField]
    private Image[] _hearts = new Image[4];

    public void HitPlayer()
    {
        _health -= 1;
        _hearts[_health].gameObject.SetActive(false);

        gameSpeedMultiplier -= gameSpeedMultiplier / 7;
        if (gameSpeedMultiplier < 0.5f) { gameSpeedMultiplier = 0.5f; }

        PlayAudio(Constants.HitSound);

        StartCoroutine(PlayHeartBrokeSound());

        if (_health <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator PlayHeartBrokeSound()
    {
        yield return new WaitForSeconds(0.4f);
        PlayAudio(Constants.BrokenHeart);
    }

    #endregion

    #region Spawning Red Blood Cell

    [SerializeField]
    private GameObject _bloodCell;

    [SerializeField]
    private float _bloodCellSpawnTime;

    private float _tempBloodCellSpawnTime;

    [SerializeField]
    private float _xStartPos;

    [SerializeField]
    private float _minYStartPos;

    [SerializeField]
    private float _maxYStartPos;

    private void SpawnBloodCell()
    {
        float randomPos = Random.Range(_minYStartPos, _maxYStartPos);
        Vector2 spawnPos = new Vector2(_xStartPos, randomPos);

        Instantiate(_bloodCell, spawnPos, Quaternion.identity);
    }

    private void CheckTimeToSpawnBloodCell()
    {
        if (_tempBloodCellSpawnTime > 0)
        {
            _tempBloodCellSpawnTime -= Time.deltaTime;
        }
        else
        {
            _tempBloodCellSpawnTime = _bloodCellSpawnTime;
            SpawnBloodCell();
        }
    }

    #endregion

    #region ESC MENU

    [SerializeField]
    private GameObject _escMenu;

    [SerializeField]
    private Sprite _musicOn;

    [SerializeField]
    private Sprite _musicOff;

    [SerializeField]
    private Sprite _audioOn;

    [SerializeField]
    private Sprite _audioOff;

    [SerializeField]
    private Image _musicImage;

    [SerializeField]
    private Image _audioImage;

    private bool _isAudioOn = true;
    private bool _isMusicOn = true;

    private void CheckInputESCMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_escMenu.activeSelf)
            {
                _escMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                _escMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ESCMenuOpenButton()
    {
        _escMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ESCResumeButton()
    {
        _escMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ESCRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AntibodyGame");
    }

    public void ESCReturnMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AntibodyMenu");
    }

    public void ESCMusicButton()
    {
        if (_isMusicOn)
        {
            _isMusicOn = false;
            _musicSource.Stop();
            _musicImage.sprite = _musicOff;
        }
        else
        {
            _isMusicOn = true;
            _musicSource.Play();
            _musicImage.sprite = _musicOn;
        }
    }

    public void ESCAudioButton()
    {
        if (_isAudioOn)
        {
            _isAudioOn = false;
            _audioImage.sprite = _audioOff;
        }
        else
        {
            _isAudioOn = true;
            _audioImage.sprite = _audioOn;
        }
    }

    #endregion

    private void Start()
    {
        _audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        _musicSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        _tempSpawnTime = _spawnTime;
        _tempBloodCellSpawnTime = _bloodCellSpawnTime;
        //isGameRunning = true;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            CheckTimeToSpawnVirus();
            CheckTimeToSpawnBloodCell();
            CheckInputESCMenu();
        }
    }

    private void FixedUpdate()
    {
        if (isGameRunning)
        {
            gameSpeedMultiplier += Time.deltaTime * 0.015f;
            //Debug.Log(gameSpeedMultiplier);
        }
    }
}
