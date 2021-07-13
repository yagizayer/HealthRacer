using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManagerHR : MonoBehaviour
{
    [Header("Player Info")]
    [Tooltip("Oyuncu Nesnesi")]
    public GameObject _player;
    [Tooltip("Oyuncunun NavMeshAgent Komponenti")]
    [SerializeField]
    NavMeshAgent _playerAgent;
    [Tooltip("Oyuncunun kontrol scripti")]
    [SerializeField]
    PlayerControls _PlayerControls;



    [Header("Food Generation")]
    [SerializeField]
    [Tooltip("Oluşturulacak Food nesnesi sayısı")]
    int foodCount = 42;
    [Tooltip("Oyun alanında Food nesnelerinin yerşeleceği yerleri belirleyen değişkenler")]
    public float isleWitdh = 10, foodSeparation = 13, foodRotationSpeed = 1, foodWaveHeight = 10;
    [SerializeField]
    [Tooltip("Resources/FoodPickups konumundaki ScriptableObject'lerin tamamı")]
    Object[] foods;
    [Tooltip("Oyun alanında Food nesnelerinin görüleneceği örnek nesne(GameObject)")]
    public GameObject displayPrefab;
    [SerializeField]
    [Tooltip("Oyun alanında Food nesnelerinin görüleneceği nesneler listesi(GameObject)")]
    List<GameObject> foodDisplays = new List<GameObject>();
    [SerializeField]
    [Tooltip("Oyun alanında Food nesnelerinin Parent nesnesi)")]
    GameObject foodsWrap;


    [Header("Audio Controls")]
    [Tooltip("Hareket etme sesleri")]
    public AudioSource[] audioSources = new AudioSource[3];
    [Tooltip("Çevre sesleri")]
    public AudioSource environmentSound;
    [Tooltip("Oyun Sesi kapalı mı?")]
    public static bool isGameMuted = false;

    [Header("Velocity Variables")]
    [SerializeField]
    [Range(.1f, 3)]
    [Tooltip("Kalkış olayının gerçekleştiğini belirten minimum ivme")]
    float startingAcceleration = 1.5f;
    [SerializeField]
    [Range(-3f, -.1f)]
    [Tooltip("Duruş olayının gerçekleştiğini belirten minimum ivme")]
    float stoppingAcceleration = -.5f;
    [Tooltip("Bir önceki frame'e ait hız")]
    float lastSpeed = 0;


    private void Start()
    {
        if (_player == null) _player = GameObject.FindWithTag("Player");
        if (_PlayerControls == null) _PlayerControls = _player.GetComponent<PlayerControls>();
        if (_playerAgent == null) _playerAgent = _player.GetComponent<NavMeshAgent>();

        foodsWrap = GameObject.Find("FoodsWrap");
        foodDisplays = generateDisplays(foodCount);
        foodDisplays = getAllFoods(foodDisplays);
        positionAllFoods(foodsWrap, foodDisplays);

        audioSources = _PlayerControls.audioSources;
    }

    public void reCreateFoods(){
        foodDisplays = new List<GameObject>();
        foodsWrap.transform.Clear();
        
        foodDisplays = generateDisplays(foodCount);
        foodDisplays = getAllFoods(foodDisplays);
        positionAllFoods(foodsWrap, foodDisplays);
    }

    List<GameObject> generateDisplays(int numberOfDisplays)
    {
        List<GameObject> result = new List<GameObject>();
        for (int displayNo = 0; displayNo < numberOfDisplays; displayNo++)
        {
            GameObject tempDisplay = Instantiate(displayPrefab);
            tempDisplay.transform.parent = foodsWrap.transform;
            result.Add(tempDisplay);
        }
        return result;
    }

    List<GameObject> getAllFoods(List<GameObject> displays)
    {
        List<GameObject> result = displays;

        foods = Resources.LoadAll("FoodPickups", typeof(Food));
        for (int foodNo = 0; foodNo < foodCount; foodNo++)
        {
            displays[foodNo].GetComponentInChildren<FoodDisplay>().food = (Food)foods[foodNo];
            displays[foodNo].AddComponent<FoodBaseIndicator>();
            displays[foodNo].tag = "FoodObject";
            displays[foodNo].name = foods[foodNo].name;
        }
        return result;
    }

    void positionAllFoods(GameObject parent, List<GameObject> displays)
    {
        int tempCount = 0;
        for (int rowCount = 0; rowCount < 9; rowCount++)
        {
            if (rowCount == 4) continue;
            for (int columnCount = 0; columnCount < 7; columnCount++)
            {
                if (columnCount == 3) continue;
                if (tempCount < foods.Length)
                    displays[tempCount].transform.position = new Vector3((rowCount + 1) * isleWitdh, 0, (columnCount + 1) * foodSeparation) + parent.transform.position;
                tempCount++;
            }
        }
    }

    void rotateFoods()
    {
        foreach (Transform food in foodsWrap.transform)
        {
            foreach (Transform item in food)
            {
                if (item.tag == "GFX")
                {
                    item.Rotate(Vector3.up, 1 * foodRotationSpeed, Space.Self);
                    item.position += Mathf.Sin(Time.time * Mathf.PI) * (1 / foodWaveHeight) * Vector3.up;
                }
            }
        }
    }

    float calculateAcceleration()
    {
        return _playerAgent.velocity.magnitude - lastSpeed;
    }

    void playAudio(float acceleration)
    {


        if (isGameMuted)
        {
            environmentSound.Stop();
            return;
        }
        if (!isGameMuted && !environmentSound.isPlaying) environmentSound.Play();


        AudioSource playingAudio = new AudioSource();
        foreach (AudioSource audio in audioSources)
        {
            if (audio.isPlaying)
            {
                playingAudio = audio;
                break;
            }
        }
        // no audio
        if (acceleration > startingAcceleration)
        {
            // speeding
            if (!audioSources[0].isPlaying)
            {
                audioSources[0].Play();
            }
        }
        else if (acceleration < stoppingAcceleration)
        {
            // slowing
            if (playingAudio && !audioSources[2].Equals(playingAudio))
                playingAudio.Stop();
            if (!audioSources[2].isPlaying)
            {
                audioSources[2].Play();
            }
        }
        else if (_playerAgent.velocity.magnitude > 0)
        {
            // moving
            if (!audioSources[1].isPlaying)
            {
                audioSources[1].Play();
            }
        }
        else
        {
            // standing
            foreach (AudioSource audio in audioSources)
            {
                audio.Stop();
            }
        }
    }
    private void FixedUpdate()
    {
        rotateFoods();

        playAudio(calculateAcceleration());
        lastSpeed = _playerAgent.velocity.magnitude;
    }




}
