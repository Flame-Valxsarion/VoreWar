using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSprite : MonoBehaviour
{
    public TextMeshProUGUI HitPercentage;
    public TextMeshProUGUI DamageIndicator;
    public TextMeshProUGUI LevelText;

    public Slider HealthBar;
    public RectTransform HealthBarOrange;

    float timeUntilHealthBarReset = -1;

    public SpriteRenderer FlexibleSquare;
    internal bool BlueColored;

    float remainingTimeForDamage;
    float startingTimeForDamage = .75f;
    Color startingColor;

    CompleteSprite _CompleteSprite;
    Animator animator;
    Animator belly2Animator;
    Animator ballsAnimator;
    Animator boobsAnimator;
    Animator SecondBoobsAnimator;

    public readonly int SfxSourcesCount = 5;
    public readonly float pitchMin = 0.92f;
    public readonly float pitchMax = 1.08f;

    public AudioSource[] SfxSources;
    public AudioSource LoopSource;

    public Transform GraphicsFolder;
    public Transform OtherFolder;

    int lastHealth = 0;

    internal CompleteSprite CompleteSprite { get => _CompleteSprite; set => _CompleteSprite = value; }

    public void Awake()
    {
        SfxSources = new AudioSource[5];

        for (var i = 0; i < SfxSourcesCount; i++)
        {
            SfxSources[i] = gameObject.AddComponent<AudioSource>();
            SfxSources[i].pitch = pitchMin + ((pitchMax - pitchMin) / SfxSourcesCount) * i;
        }
        LoopSource = gameObject.AddComponent<AudioSource>();
    }

    public void UpdateHealthBar(Actor_Unit unit)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        HealthBarOrange.gameObject.SetActive(false);
        float healthFraction = unit.Unit.HealthPct;
        if (healthFraction > .99f || healthFraction <= 0)
        {
            HealthBar.gameObject.SetActive(false);
        }
        else
        {
            HealthBar.gameObject.SetActive(true);
            HealthBar.value = healthFraction;
        }
        lastHealth = unit.Unit.Health;
    }

    public void ShowDamagedHealthBar(Actor_Unit unit, int damage)
    {
        timeUntilHealthBarReset = .1f;
        HealthBarOrange.gameObject.SetActive(true);
        float orangeLevel = 0.5f + (Mathf.Abs(Time.time % 1 - .5f) - 0.25f);
        HealthBarOrange.GetComponent<Image>().color = new Color(1, orangeLevel, 0);
        float baseHealthFraction = unit.Unit.HealthPct;
        float newhealthFraction = (float)(unit.Unit.Health - damage) / unit.Unit.MaxHealth;
        HealthBarOrange.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 150 * baseHealthFraction);
        HealthBar.gameObject.SetActive(true);
        HealthBar.value = newhealthFraction;
    }

    public void HitPercentagesDisplayed(bool displayed)
    {
        HitPercentage.gameObject.SetActive(displayed);
    }

    public void DisplayHitPercentage(float chance, Color color, int damage = 0)
    {
        HitPercentagesDisplayed(true);
        HitPercentage.text = (Mathf.Round(chance * 100)).ToString() + "%";
        if (damage > 0) HitPercentage.text += "\n-" + damage;
        HitPercentage.faceColor = color;
    }

    public void DisplayResist()
    {
        DamageIndicator.faceColor = Color.red;
        DamageIndicator.text = "Resist";
        FinishDisplayedTextSetup();
    }

    public void DisplayCharm()
    {
        DamageIndicator.faceColor = Color.magenta;
        DamageIndicator.text = "Charmed!";
        FinishDisplayedTextSetup();
    }

    public void DisplayHypno()
    {
        DamageIndicator.faceColor = Color.green;
        DamageIndicator.text = "Hypnotized!";
        FinishDisplayedTextSetup();
    }

    public void DisplayDazzle()
    {
        DamageIndicator.faceColor = Color.red;
        DamageIndicator.text = "Dazzled!";
        FinishDisplayedTextSetup();
    }

    public void DisplaySummoned()
    {
        DamageIndicator.faceColor = Color.white;
        DamageIndicator.text = "Summoned!";
        FinishDisplayedTextSetup();
    }

    public void DisplaySeduce()
    {
        DamageIndicator.faceColor = Color.magenta;
        DamageIndicator.text = "Seduced!";
        FinishDisplayedTextSetup();
    }

    public void DisplayDistract()
    {
        DamageIndicator.faceColor = Color.magenta;
        DamageIndicator.text = "Distracted!";
        FinishDisplayedTextSetup();
    }

    public void DisplayEscape()
    {
        DamageIndicator.faceColor = Color.blue;
        DamageIndicator.text = "Escaped";
        FinishDisplayedTextSetup();
    }
    public void DisplayCrit()
    {
        DamageIndicator.faceColor = Color.red;
        DamageIndicator.text = "CRITICAL!";
        FinishDisplayedTextSetup();
    }
    public void DisplayGraze()
    {
        DamageIndicator.faceColor = Color.green;
        DamageIndicator.text = "Graze";
        FinishDisplayedTextSetup();
    }

    public void DisplayDamage(int damage, bool spellDamage = false, bool expGain = false)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        if (Config.DamageNumbers == false && damage != 0)
            return;
        if (damage > 0)
        {
            if (expGain)
            {
                DamageIndicator.faceColor = Color.yellow;
                DamageIndicator.text = $"+{damage}";
            }
            else
            {
            DamageIndicator.faceColor = Color.red;
            DamageIndicator.text = $"-{damage}";
            }
        }
        else if (damage < 0)
        {
            DamageIndicator.faceColor = Color.blue;
            DamageIndicator.text = $"+{Mathf.Abs(damage)}";
        }
        else
        {
            DamageIndicator.faceColor = Color.red;
            if (spellDamage)
                DamageIndicator.text = "No Effect";
            else
                DamageIndicator.text = "Miss";
        }
        FinishDisplayedTextSetup();
    }
    void FinishDisplayedTextSetup()
    {
        remainingTimeForDamage = startingTimeForDamage;
        startingColor = DamageIndicator.faceColor;
        DamageIndicator.gameObject.SetActive(true);
    }

    void UpdateDisplayDamage()
    {
        remainingTimeForDamage -= Time.deltaTime;
        if (remainingTimeForDamage < 0)
            DamageIndicator.gameObject.SetActive(false);
        else
        {
            DamageIndicator.faceColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1.5f * remainingTimeForDamage / startingTimeForDamage);
            DamageIndicator.outlineColor = new Color(0, 0, 0, 1.5f * remainingTimeForDamage / startingTimeForDamage);
        }
    }

    public void UpdateSprites(Actor_Unit actor, bool activeTurn)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        Vector3 goalScale = new Vector3(1, 1, 1);

        goalScale *= actor.Unit.GetScale();

        if (lastHealth != actor.Unit.Health)
            UpdateHealthBar(actor);
     

        if (goalScale.x > GraphicsFolder.localScale.x)
        {
            float newScale = Mathf.Min(GraphicsFolder.localScale.x + .03f, goalScale.x);
            GraphicsFolder.localScale = new Vector3(newScale, newScale, newScale);
        }
        else if (goalScale.x < GraphicsFolder.localScale.x)
        {
            float newScale = Mathf.Max(GraphicsFolder.localScale.x - .03f, goalScale.x);
            GraphicsFolder.localScale = new Vector3(newScale, newScale, newScale);
        }

        if (timeUntilHealthBarReset > 0)
        {
            timeUntilHealthBarReset -= Time.deltaTime;
            if (timeUntilHealthBarReset <= 0)
                UpdateHealthBar(actor);
        }

        UpdateFlexibleSquare(actor, activeTurn);

        if (remainingTimeForDamage >= 0)
            UpdateDisplayDamage();

        if (CompleteSprite == null)
        {
            CreateCompleteSprite(actor);
        }

        CompleteSprite.SetActor(actor);
        CompleteSprite.UpdateSprite();
        UpdateLevelText(actor);
        ApplyTinting(actor);

    }

    private void ApplyTinting(Actor_Unit actor)
    {
        if (actor.Visible && actor.Targetable == false)
        {
            ApplyDeadEffect();
        }
        else if (actor.Surrendered || actor.DamagedColors)
        {
            float tint = .4f;
            if (actor.DamagedColors)
                tint = .8f;
            CompleteSprite.RedifySprite(tint);
            if (actor.Surrendered)
            {
            var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.FadeInFadeOut).GetComponent<FadeInFadeOut>();
            obj.transform.SetPositionAndRotation(new Vector3(actor.Position.x + .15f, actor.Position.y + .15f, 0), new Quaternion());
            obj.transform.localScale = new Vector3(2, 2, 1);
            obj.SpriteRenderer.sprite = State.GameManager.SpriteDictionary.SpellIcons[7];
            obj.HoldTime = 0;
            obj.FadeInTime = 0;
            obj.FadeOutTime = 0;
            }
        }
        else if (actor.Unit.GetStatusEffect(StatusEffectType.Petrify) != null)
        {
            CompleteSprite.DarkenSprites();
        }
        else if (actor.Unit.GetStatusEffect(StatusEffectType.Frozen) != null)
        {
            CompleteSprite.ApplyDeadEffect();
        }
    }

    private void UpdateLevelText(Actor_Unit actor)
    {
        LevelText.gameObject.SetActive(Config.ShowLevelText);
        if (Config.ShowLevelText)
            LevelText.text = $"Level: {actor.Unit.Level}";
    }

    private void CreateCompleteSprite(Actor_Unit actor)
    {
        if (Config.AnimatedBellies)
        {
            CompleteSprite = new CompleteSprite(State.GameManager.SpriteRendererPrefab, State.GameManager.SpriteRenderAnimatedPrefab, GraphicsFolder);
            animator = CompleteSprite.GetSpriteOfType(SpriteType.Belly)?.GameObject.GetComponentInParent<Animator>();
            if (animator != null)
            {
                var raceData = Races.GetRace(actor.Unit);
                if (raceData.GentleAnimation)
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                else
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                animator.enabled = true;
            }
            
            belly2Animator = CompleteSprite.GetSpriteOfType(SpriteType.SecondaryBelly)?.GameObject.GetComponentInParent<Animator>();
            if (belly2Animator != null)
            {
                var raceData = Races.GetRace(actor.Unit);
                if (raceData.GentleAnimation)
                    belly2Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                else
                    belly2Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                belly2Animator.enabled = true;
            }

            ballsAnimator = CompleteSprite.GetSpriteOfType(SpriteType.Balls)?.GameObject.GetComponentInParent<Animator>();
            if (ballsAnimator != null)
            {
                var raceData = Races.GetRace(actor.Unit);
                if (raceData.GentleAnimation)
                    ballsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                else
                    ballsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                ballsAnimator.enabled = true;
            }

            boobsAnimator = CompleteSprite.GetSpriteOfType(SpriteType.Breasts)?.GameObject.GetComponentInParent<Animator>();
            if (boobsAnimator != null)
            {
                var raceData = Races.GetRace(actor.Unit);
                if (raceData.GentleAnimation)
                    boobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                else
                    boobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                boobsAnimator.enabled = true;
            }

            SecondBoobsAnimator = CompleteSprite.GetSpriteOfType(SpriteType.SecondaryBreasts)?.GameObject.GetComponentInParent<Animator>();
            if (SecondBoobsAnimator != null)
            {
                var raceData = Races.GetRace(actor.Unit);
                if (raceData.GentleAnimation)
                    SecondBoobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                else
                    SecondBoobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                SecondBoobsAnimator.enabled = true;
            }

        }
        else
            CompleteSprite = new CompleteSprite(State.GameManager.SpriteRendererPrefab, null, GraphicsFolder);
    }

    private void UpdateFlexibleSquare(Actor_Unit actor, bool activeTurn)
    {
        float alpha = 1;
        if (Config.AllianceSquaresDarkness == 2)
            alpha = (activeTurn && actor.Movement > 0) ? .9f : .5f;
        else if (Config.AllianceSquaresDarkness == 1)
            alpha = (activeTurn && actor.Movement > 0) ? .5f : .2f;
        else
            alpha = 0;

        if (actor.Unit.FixedSide != actor.Unit.Side && TacticalUtilities.PlayerCanSeeTrueSide(actor.Unit))
        {
        if (BlueColored)
        {
                {
                    if (Config.AllianceSquaresDarkness == 3)
                    {
                        if (activeTurn && actor.Movement > 0)
                            FlexibleSquare.color = new Color(1f, 0.6f, 0, 1);
                        else
                            FlexibleSquare.color = new Color(0.75f, 0.40f, 0, 1);
                    }
                    else
                        FlexibleSquare.color = new Color(0.8f, 0.5f, 0f, alpha);
                }
            }
            else
            {
                if (Config.AllianceSquaresDarkness == 3)
                {
                    if (activeTurn && actor.Movement > 0)
                        FlexibleSquare.color = new Color(0.65f, 0, 0.9f, 1);
                    else
                        FlexibleSquare.color = new Color(0.35f, 0, 0.40f, 1);
                }
                else
                    FlexibleSquare.color = new Color(0.45f, 0f, 0.75f, alpha);
            }
        }
        else if (BlueColored)
        {
            if (Config.AltFriendlyColor)
            {
                if (Config.AllianceSquaresDarkness == 3)
                {
                    if (activeTurn && actor.Movement > 0)
                        FlexibleSquare.color = new Color(0, 1, 0, 1);
                    else
                        FlexibleSquare.color = new Color(0, 0.4f, 0, 1);
                }
                else
                    FlexibleSquare.color = new Color(0f, 0.8f, 0, alpha);
            }
            else
            {
                if (Config.AllianceSquaresDarkness == 3)
                {
                    if (activeTurn && actor.Movement > 0)
                        FlexibleSquare.color = new Color(0, 0, 1, 1);
                    else
                        FlexibleSquare.color = new Color(0, 0, 0.4f, 1);
                }
                else
                    FlexibleSquare.color = new Color(0f, 0f, 0.8f, alpha);
            }
        }
        else
        {
            if (Config.AllianceSquaresDarkness == 3)
            {
                if (activeTurn && actor.Movement > 0)
                    FlexibleSquare.color = new Color(1, 0, 0, 1);
                else
                    FlexibleSquare.color = new Color(.4f, 0, 0, 1);
            }
            else
                FlexibleSquare.color = new Color(0.8f, 0f, 0, alpha);
        }
    }

    public void AnimateBalls(float odds)
    {
        if (ballsAnimator == null) return;
        if (Random.value > odds) return;
        if (!ballsAnimator.GetCurrentAnimatorStateInfo(0).IsName("none")) return;
        int ran = Random.Range(0, 3); // 0 up to 3
        if (ran == 0) ballsAnimator.SetTrigger("wriggle");
        if (ran == 1) ballsAnimator.SetTrigger("wriggle2");
        if (ran == 2) ballsAnimator.SetTrigger("wriggle3");
    }

    public void AnimateBelly(float odds)
    {
        if (animator == null) return;
        if (Random.value > odds) return;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("none")) return;
        int ran = Random.Range(0, 4); // 0 up to 3
        if (ran == 1) animator.SetTrigger("wriggle");
        if (ran == 2) animator.SetTrigger("wriggle2");
        if (ran == 3) animator.SetTrigger("wriggle3");
    }
    public void AnimateSecondBelly(float odds)
    {
        if (belly2Animator == null) return;
        if (Random.value > odds) return;
        if (!belly2Animator.GetCurrentAnimatorStateInfo(0).IsName("none")) return;
        int ran = Random.Range(0, 4); // 0 up to 3
        if (ran == 1) belly2Animator.SetTrigger("wriggle");
        if (ran == 2) belly2Animator.SetTrigger("wriggle2");
        if (ran == 3) belly2Animator.SetTrigger("wriggle3");
    }

    public void AnimateBoobs(float odds)
    {
        if (boobsAnimator == null) return;
        if (Random.value > odds) return;
        if (!boobsAnimator.GetCurrentAnimatorStateInfo(0).IsName("none")) return;
        int ran = Random.Range(0, 3); // 0 up to 3
        if (ran == 1) boobsAnimator.SetTrigger("wriggle");
        if (ran == 2) boobsAnimator.SetTrigger("wriggle2");
        if (ran == 3) boobsAnimator.SetTrigger("wriggle3");
    }

    public void AnimateSecondBoobs(float odds)
    {
        if (SecondBoobsAnimator == null) return;
        if (Random.value > odds) return;
        if (!SecondBoobsAnimator.GetCurrentAnimatorStateInfo(0).IsName("none")) return;
        int ran = Random.Range(0, 3); // 0 up to 3
        if (ran == 1) SecondBoobsAnimator.SetTrigger("wriggle");
        if (ran == 2) SecondBoobsAnimator.SetTrigger("wriggle2");
        if (ran == 3) SecondBoobsAnimator.SetTrigger("wriggle3");
    }

    public void AnimateBellyEnter()
    {
        if (animator == null) return;
        animator.SetTrigger("enter");
    }

    void ApplyDeadEffect()
    {

        LevelText.gameObject.SetActive(false);
        FlexibleSquare.gameObject.SetActive(false);
        HealthBar.gameObject.SetActive(false);
        CompleteSprite.ApplyDeadEffect();
    }
}
