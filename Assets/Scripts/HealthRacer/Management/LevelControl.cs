using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CartContentControl),typeof(GameManagerHR))]
public class LevelControl : MonoBehaviour
{
    [Header("Oyuncu Değişkenleri")]
    [Tooltip("Oyuncu nesnesi")]
    public GameObject player;
    [Tooltip("Oyuncu hareket Scripti")]
    public PlayerControls playerMovement;
    [Tooltip("Oyuncunun tüm sepetindeki Food nesnelerini içeren Liste(Food)")]
    public List<Food> playerCart;
    [Tooltip("Oyuncu hareket edebilir mi?")]
    public bool canPlayerMove = false;

    [Space]
    [Tooltip("Oyuncu sepetini kontrol eden script")]
    public CartContentControl _cartContentControl;


    [Space]
    [Header("UI nesneleri")]
    [Tooltip("Kalan zamanın belirtildiği Text nesnesi(Text)")]
    public Text RTText;
    [Tooltip("Oyunu durdurup başlatma butonunun Image komponenti")]
    public Image pauseButton;
    [Tooltip("Oyun durdu mu?")]
    public bool gamePaused = true;
    [Tooltip("Oyunu durdurup başlatma butonunun üzerideki resim(Sprite)")]
    public Sprite pauseSprite, resumeSprite;
    [Tooltip("Oyun sesini kapatıp açma butonunun Image komponenti")]
    public Image muteButton;
    [Tooltip("Oyun sesini kapatıp açma butonunun üzerideki resim(Sprite)")]
    public Sprite muteSprite, unmuteSprite;

    [Space]
    [Header("Bölüm bitiş nesneleri")]
    [Tooltip("Bölümün çıkış noktasını belirten nesne(GameObject)")]
    public GameObject exitArea;
    [Tooltip("Bölümün çıkış noktasını belirten nesnenin Materyali(Material)")]
    public Material exitAreaMat;
    [Tooltip("Oyuncu bölümün çıkış nokasında mı?")]
    bool tryingToLeave = false;
    [Tooltip("Bölümün Çıkış noktasının alabileceği renk")]
    Color successColor, underBPColor, overACPColor;
    [Tooltip("Geçerli sepette bulunan nesnelerin toplam AburCubur Puanı")]
    public float ACP;
    [Tooltip("Geçerli sepette bulunan nesnelerin toplam Besin Puanı")]
    public float BP;
    [Tooltip("Bölümün geçilmesi için toplam süre")]
    [Range(20, 180)]
    public float levelTime = 100;
    [Tooltip("Bölümün geçilmesi için kalan süre")]
    [Range(20, 180)]
    public float remainingTime = 100;

    [Header("Menu Değişkenleri")]
    [Tooltip("Pause Menüsü")]
    public GameObject Menu, MenuBackground, cartPreviewUI;
    [Tooltip("Oyun Sonu menüsü")]
    public GameObject successMenu, failedMenu, ACPFulledMenu, veryHealtyMenu;
    [Tooltip("Bölüm bitti mi?")]
    bool levelEnded = false;
    [SerializeField] private Text CountDownText;
    [SerializeField] private GameObject roof;
    [SerializeField] [Range(20, 500)] private float roofHeight = 50;
    [SerializeField] [Range(.01f, 50)] private float roofSpeed = 10;
    Vector3[] roofPositions = new Vector3[2];
    bool roofLifting = false;

    [Header("Tutorial Değişkenleri")]
    [Tooltip("Tutorial adımlarının tutulduğu parent nesne")]
    public GameObject tutorialMain;
    [Tooltip("Tutorial adımları")]
    public GameObject[] tutorialMenus = new GameObject[5];
    [Tooltip("Şuanki Tutorial adımı")]
    public int tutorialIndex = 0;

    void Start()
    {
        roofPositions[0] = roof.transform.position;
        roofPositions[1] = roof.transform.position + Vector3.up * roofHeight + Vector3.forward * 50;
        _cartContentControl = GetComponent<CartContentControl>();
        ACP = _cartContentControl.totalACP;
        BP = _cartContentControl.totalBP;
        playerMovement = player.GetComponent<PlayerControls>();
        playerCart = playerMovement.cartContent;
        exitAreaMat = exitArea.GetComponent<Renderer>().material;
        successColor = new Color(0, 1, 0, .5f);
        underBPColor = new Color(1, 0, 0, .5f);
        overACPColor = new Color(1, 0, 1, .5f);
        RTText.text = remainingTime.ToString();
    }

    private void FixedUpdate()
    {
        if (canPlayerMove)
            remainingTime -= Time.deltaTime;
        ACP = _cartContentControl.totalACP;
        BP = _cartContentControl.totalBP;
        playerCart = playerMovement.cartContent;
        playerMovement.canPlayerMove = canPlayerMove;

        if (!canPlayerMove)
            return;

        if (remainingTime <= 0)
        {
            // bölüm başarısız
            Failed(false);
        }

        if (Vector3.Distance(player.transform.position, exitArea.transform.position) < 10)
        {
            // çıkış noktasında
            tryingToLeave = true;
        }
        else
        {
            tryingToLeave = false;
        }


        if (ACP < 100 && BP > 100)
        {
            exitAreaMat.color = successColor;
            if (tryingToLeave)
                if (ACP < 25)
                    Success(true);
                else
                    Success(false);
            // bölüm bitebilir ve başarılı olur
        }
        if (ACP < 100 && BP < 100)
        {
            exitAreaMat.color = underBPColor;
            // bölüm bitemez(henüz)
        }
        if (ACP > 100)
        {
            // exitAreaMat.color = overACPColor;
            // if (tryingToLeave)
            Failed(true);
            // bölüm başarısız 
        }
        RTText.text = ((int)remainingTime).ToString();
    }

    public void PauseResume()
    {
        if (!levelEnded)
            if (tutorialIndex > 4)
                if (gamePaused)
                {
                    canPlayerMove = true;
                    pauseButton.sprite = pauseSprite;
                    gamePaused = false;
                    Menu.SetActive(false);
                    MenuBackground.SetActive(false);
                    cartPreviewUI.SetActive(true);
                    countDownControl();
                    roofController(true);
                }
                else
                {
                    canPlayerMove = false;
                    pauseButton.sprite = resumeSprite;
                    gamePaused = true;
                    Menu.SetActive(true);
                    MenuBackground.SetActive(true);
                    cartPreviewUI.SetActive(false);
                    roofController(false);
                }
            else
            {
                return;
            }
    }

    public void muteUnmute()
    {
        if (GameManagerHR.isGameMuted)
        {
            muteButton.sprite = muteSprite;
            GameManagerHR.isGameMuted = false;
        }
        else
        {

            muteButton.sprite = unmuteSprite;
            GameManagerHR.isGameMuted = true;

        }
    }

    public void Failed(bool ACPFulled)
    {
        roofController(false);
        if (ACPFulled)
        {
            canPlayerMove = false;
            gamePaused = true;
            MenuBackground.SetActive(true);
            ACPFulledMenu.SetActive(true);
            levelEnded = true;
        }
        else
        {
            canPlayerMove = false;
            gamePaused = true;
            MenuBackground.SetActive(true);
            failedMenu.SetActive(true);
            levelEnded = true;
        }
    }
    public void Success(bool veryHealty)
    {
        roofController(false);
        if (veryHealty)
        {
            canPlayerMove = false;
            gamePaused = true;
            MenuBackground.SetActive(true);
            veryHealtyMenu.SetActive(true);
            levelEnded = true;
        }
        else
        {
            canPlayerMove = false;
            gamePaused = true;
            MenuBackground.SetActive(true);
            successMenu.SetActive(true);
            levelEnded = true;
        }
    }
    public void Replay()
    {
        remainingTime = levelTime;
        RTText.text = levelTime.ToString();
        player.GetComponent<PlayerControls>().cartContent = new List<Food>();
        player.transform.position = new Vector3(0, -0.61f, 0); // başlangıç pozisyonu

        Menu.SetActive(false);
        ACPFulledMenu.SetActive(false);
        failedMenu.SetActive(false);
        veryHealtyMenu.SetActive(false);
        successMenu.SetActive(false);
        canPlayerMove = false;
        gamePaused = true;
        MenuBackground.SetActive(false);
        levelEnded = false;

        GetComponent<GameManagerHR>().reCreateFoods();

        countDownControl();
        roofController(true);
    }
    public void proceedTutorial()
    {
        tutorialMenus[tutorialIndex].SetActive(false);
        if (tutorialIndex < 4)
        {
            tutorialMenus[++tutorialIndex].SetActive(true);
        }
        else
        {
            ++tutorialIndex;
            canPlayerMove = true;
            gamePaused = false;
            tutorialMain.SetActive(false);
            MenuBackground.SetActive(false);
            cartPreviewUI.SetActive(true);
            countDownControl();
            roofController(true);
        }

    }

    public void reOpenTutorial()
    {
        if (gamePaused) return;
        if (tutorialIndex <= 4) return;
        tutorialIndex = 0;
        canPlayerMove = false;
        gamePaused = true;
        tutorialMain.SetActive(true);
        tutorialMenus[0].SetActive(true);
        MenuBackground.SetActive(true);
        cartPreviewUI.SetActive(false);
        roofController(false);
    }

    public void countDownControl()
    {
        int seconds = 0;
        if (levelTime - remainingTime > 3) seconds = 0;
        if (levelTime - remainingTime <= 3) seconds = 3;
        StartCoroutine(countDown(seconds));
        StopCoroutine(countDown(seconds));
    }

    IEnumerator countDown(int seconds)
    {
        canPlayerMove = false;
        gamePaused = true;
        cartPreviewUI.SetActive(false);

        CountDownText.gameObject.SetActive(true);
        int sayac = seconds;
        CountDownText.text = "Hazır!";
        while (sayac >= 0)
        {
            yield return new WaitForSecondsRealtime(1);
            CountDownText.text = sayac.ToString();
            sayac--;
        }
        CountDownText.text = "Başla!";
        yield return new WaitForSecondsRealtime(1);
        CountDownText.gameObject.SetActive(false);

        canPlayerMove = true;
        gamePaused = false;
        cartPreviewUI.SetActive(true);
    }

    public void roofController(bool lifting)
    {
        if (!roofLifting)
        {
            StartCoroutine(liftRoof(lifting));
            StopCoroutine(liftRoof(lifting));
        }
    }
    IEnumerator liftRoof(bool lifting)
    {
        Vector3 current = lifting ? roofPositions[0] : roofPositions[1];
        Vector3 target = lifting ? roofPositions[1] : roofPositions[0];


        float lerp = 0;
        if (Vector3.Distance(target, roof.transform.position) > 1)
            while (lerp < 1)
            {
                roofLifting = true;
                roof.transform.position = Vector3.Lerp(current, target, lerp);

                lerp += Time.deltaTime * roofSpeed;
                yield return null;
            }
        roofLifting = false;
    }



}
