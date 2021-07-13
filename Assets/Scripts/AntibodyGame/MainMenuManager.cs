using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cVirus;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _clickButton;

    [SerializeField]
    private AudioClip _buttonPointerEnter;

    public void OnClickStartButton()
    {
        //_cVirusAnimator.enabled = true;
        _cVirus.SetActive(true);
        _audioSource.PlayOneShot(_clickButton, 1f);
        StartCoroutine(ChangeScene());
    }

    public void OnPointerEnterButton()
    {
        _audioSource.PlayOneShot(_buttonPointerEnter, 1.5f);
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("AntibodyGame");
    }
}
