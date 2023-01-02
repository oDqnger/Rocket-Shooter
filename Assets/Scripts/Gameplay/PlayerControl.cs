using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class PlayerControl : MonoBehaviour
{   
    public float speed;
    private string ROCKTAG = "Rocks";
    private string COINTAG = "Coin";

    [SerializeField] GameObject Bullet;

    public float bulletSpeed;

    public static int health = 3;

    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private GameObject heartSymbol;
    public static int points = 0;
    public static TextMeshProUGUI pointsText;

    public static bool isNextWave = false;

    public static int target = 20;

    Animator shakeAnimation;

    [SerializeField] AudioSource laserShoot, explodeAudio, hitAudioOne, hitAudioTwo;

    [SerializeField] ParticleSystem rocketExplode;
    [SerializeField] AudioSource coinSound;

    public static int coins = 0;

    [SerializeField] TextMeshProUGUI coinText;

    public static bool shouldExplode = true;

    [SerializeField] Animator FadeIn, ScreenSlide;
    [SerializeField] TextMeshProUGUI hitPoints;

    public static bool PlayHitPoint = false;

    public GameObject DeathScreen;

    public static int BombsCollected = 0;
    public TextMeshProUGUI Score, Coins, Bombs, _rockCollected;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        pointsText = GameObject.Find("Canvas/Points").GetComponent<TextMeshProUGUI>();
        shakeAnimation = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    IEnumerator PlayShakeAnimation() {
        shakeAnimation.SetBool("isShake", true);
        yield return new WaitForSeconds(0.2f);
        shakeAnimation.SetBool("isShake", false);
    }

    IEnumerator PlayUltraAnimation(float shakeTime) {
        shakeAnimation.SetBool("isUltraShake", true);
        yield return new WaitForSeconds(shakeTime);
        shakeAnimation.SetBool("isUltraShake", false);
    }

    IEnumerator HitAnimation() {
        hitPoints.color = Color.green;
        hitPoints.text = "+1";
        FadeIn.SetBool("FadeIn", true);
        yield return new WaitForSeconds(0.6f);
        FadeIn.SetBool("FadeIn", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            var instantiatedObj = Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y + 1.5f, 0f), transform.rotation);
            instantiatedObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f) * bulletSpeed; 
            laserShoot.Play();
        }
        
        pointsText.text = points.ToString();

        if (points >= target) {
            isNextWave = true;
            target = target * 2;
        }

        if (BulletScript.isPlaying) {
            StartCoroutine(PlayShakeAnimation());
            explodeAudio.Play();
            BulletScript.isPlaying = false;
        }

        if (SpawnCoinScript.isCollected) {
            coinSound.Play();
            coins++;
            coinText.text = $"Coins: {coins.ToString()}";
            SpawnCoinScript.isCollected = false;
        }

        if (PlayHitPoint) {
            StartCoroutine(HitAnimation());
            PlayHitPoint = false;
        }
        
    }
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(0f, -1f, 0f) * speed * Time.deltaTime);
            Debug.Log(isNextWave);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate(new Vector3(0f, 1f, 0f) * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Translate(new Vector3(1f, 0f, 0f) * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Translate(new Vector3(-1f, 0f, 0f) * speed * Time.deltaTime);
        }
    }

    void OnDeathScreen() {
        DeathScreen.SetActive(true);
        ScreenSlide.SetBool("IsSlide", true);
        Coins.text = coins.ToString();
        Bombs.text = BombsCollected.ToString();
        _rockCollected.text = points.ToString();
        Score.text = $"Total Score: {((coins + BombsCollected + points) / 2)}"; // all of this code right here, is made from unity
    }
    IEnumerator OnHit(Collision2D collision2d) {
        if (health <= 1) {
            health-=1;
            StartCoroutine(PlayUltraAnimation(1f));
            healthText.text = health.ToString();
            hitAudioTwo.Play();
            rocketExplode.Play();
            rocketExplode.transform.position = transform.position;
            Destroy(gameObject);
            Destroy(collision2d.gameObject);
            OnDeathScreen();
        } else {
            health -= 1;
            StartCoroutine(PlayUltraAnimation(0.5f));
            Destroy(collision2d.gameObject);
            healthText.text = health.ToString();
            hitAudioOne.Play();
            GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.2238786f, 1f);
            heartSymbol.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.2238786f, 1f);
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = Color.white;
            heartSymbol.GetComponent<SpriteRenderer>().color = Color.white;

            hitPoints.color = Color.red;
            hitPoints.text = "-1";
            FadeIn.SetBool("FadeIn", true);
            yield return new WaitForSeconds(0.6f);
            FadeIn.SetBool("FadeIn", false);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == ROCKTAG) {
            StartCoroutine(OnHit(col));
        } else if (col.gameObject.tag == COINTAG) {
            Destroy(col.gameObject); // over here, all of this code is basically unity's stuff. its not even c#'s code
        }
    }
}
