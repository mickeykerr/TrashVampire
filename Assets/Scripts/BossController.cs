using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public delegate void BossSummonAbility();
    public event BossSummonAbility OnBossSummonAbility;

    public float SummonCooldown = 5;
    public Slider Slider;
    public float MaxHealth = 100;
    public float BossHealthSliderSpeed = 5;
    public float BossInvincibilityTimer = 0.5f;

    public GameObject WinTouchObject;

    public UnityEvent OnBossDeath;

    [HideInInspector]
    public float _currentHealth { get; private set; }

    private bool _invincible = false;
    private bool _isDead { get; set; } = false;
    private bool _paused { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;
        StartCoroutine(SummonAbility());
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = Mathf.Lerp(Slider.value, (_currentHealth / MaxHealth), Time.deltaTime*BossHealthSliderSpeed);
    }

    public void SetHealth(float health)
    {
        _currentHealth = health;

        if (_currentHealth <= 0)
        {
            _isDead = true;
            SelfDeathEvents();
        }
    }

    public void Damage(float damage)
    {
        if (_invincible) return;
        _invincible = true;
        Debug.Log("Boss damaged! ");
        SetHealth(_currentHealth - damage);
        StartCoroutine(IFrameCounter());
        StartCoroutine(flashColor(Color.red, 0.15f));
    }

    private IEnumerator SummonAbility()
    {
        while (!_isDead)
        {
            if (!_paused) OnBossSummonAbility?.Invoke();
            yield return new WaitForSeconds(SummonCooldown);
        }
    }

    public IEnumerator flashColor(Color whatColor, float durationInSeconds)
    {
        if (TryGetComponent(out SpriteRenderer render))
        {
            Color original = Color.white;
            render.color = whatColor;
            yield return new WaitForSeconds(durationInSeconds);
            render.color = original;
        }
    }

    private void SelfDeathEvents()
    {
        EnemyEvents self = GetComponent<EnemyEvents>();

        //stop ability to move
        self.movementStatus = false;

        //disable hit
        self.DamageCollider.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(self.DamageCollider);

        //he dead as hell
        self.isDead = true;
        self.ded.Invoke();
        OnBossDeath?.Invoke();

        //hide enemy
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        GetComponent<PolygonCollider2D>().enabled = false;

        WinTouchObject.SetActive(true);
        Time.timeScale = 0.5f;
        StartCoroutine(SlowOnKill());
        //destroy gameObject
        Destroy(this.gameObject, 1f);
    }
    
    private IEnumerator IFrameCounter()
    {
        yield return new WaitForSecondsRealtime(BossInvincibilityTimer);
        _invincible = false;
    }

    private IEnumerator SlowOnKill()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
    }
}
