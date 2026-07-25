using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    internal bool SoundEnabled = true;

    private readonly int sourceCount = 9;
    private AudioSource[] efxSources;
    private int sourceIndex = 0;

    private readonly float lowPitchRange = 0.92f;
    private readonly float highPitchRange = 1.08f;

    private float voreVolume = 1f;
    private float combatVolume = 1f;
    private float passiveVoreSoundVolume = 1f;

    private float sfxFloor = 0.7f;
    private float loopFloor = 0f;

    private AudioClip[] Swings;
    private AudioClip[] ArrowHits;
    private AudioClip[] MeleeHits;
    private AudioClip[] ArmorHits;
    private AudioClip[] Burps;
    private AudioClip[] Farts;

    private readonly Dictionary<PreyLocation, AudioClip[]> Swallows = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> FailedSwallows = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> Digests = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> DigestLoops = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> Absorbs = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> AbsorbLoops = new Dictionary<PreyLocation, AudioClip[]>();

    private readonly Dictionary<string, AudioClip[]> SpellCasts = new Dictionary<string, AudioClip[]>();
    private readonly Dictionary<string, AudioClip[]> SpellHits = new Dictionary<string, AudioClip[]>();

    private readonly Dictionary<string, AudioClip[]> MiscSounds = new Dictionary<string, AudioClip[]>();

    internal void PlaySwing(Actor_Unit actor) => RandomizeSfx(Swings, actor, combatVolume);
    internal void PlayArrowHit(Actor_Unit actor) => RandomizeSfx(ArrowHits, actor, combatVolume);
    internal void PlayMeleeHit(Actor_Unit actor) => RandomizeSfx(MeleeHits, actor, combatVolume);
    internal void PlayArmorHit(Actor_Unit actor) => RandomizeSfx(ArmorHits, actor, combatVolume);

    internal void PlayBurp(Actor_Unit actor) => RandomizeSfx(Burps, actor, voreVolume);
    internal void PlayFart(Actor_Unit actor) => RandomizeSfx(Farts, actor, voreVolume);

    internal void PlaySwallow(PreyLocation location, Actor_Unit actor) => RandomizeSfx(Swallows[location], actor, voreVolume);
    internal void PlayFailedSwallow(PreyLocation location, Actor_Unit actor) => RandomizeSfx(FailedSwallows[location], actor, voreVolume);

    internal void PlayDigest(PreyLocation location, Actor_Unit actor) => RandomizeSfx(Digests[location], actor, voreVolume);
    internal void PlayDigestLoop(PreyLocation location, Actor_Unit actor) => RandomizeLoop(DigestLoops[location], actor, passiveVoreSoundVolume);

    internal void PlayAbsorb(PreyLocation location, Actor_Unit actor) => RandomizeSfx(Absorbs[location], actor, voreVolume);
    internal void PlayAbsorbLoop(PreyLocation location, Actor_Unit actor) => RandomizeLoop(AbsorbLoops[location], actor, passiveVoreSoundVolume);

    internal void PlaySpellCast(Spell spell, Actor_Unit actor) => RandomizeSfx(SpellCasts[spell.Id], actor, combatVolume);

    // todo locate the spell correctly
    internal void PlaySpellHit(Spell spell, Vector2 location) => RandomizeSfxGlobal(SpellHits[spell.Id], location, combatVolume);

    private void PopulateVoreClips(Dictionary<PreyLocation, AudioClip[]> dict, string name)
    {
        char sep = Path.DirectorySeparatorChar;
        dict[PreyLocation.stomach] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}oral");
        dict[PreyLocation.stomach2] = dict[PreyLocation.stomach];
        dict[PreyLocation.anal] = dict[PreyLocation.stomach];
        dict[PreyLocation.tail] = dict[PreyLocation.stomach];
        dict[PreyLocation.balls] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}cock");
        dict[PreyLocation.breasts] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}breast");
        dict[PreyLocation.leftBreast] = dict[PreyLocation.breasts];
        dict[PreyLocation.rightBreast] = dict[PreyLocation.breasts];
        dict[PreyLocation.womb] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}unbirth");
        dict[PreyLocation.bladder] = dict[PreyLocation.womb];
    }

    private void PopulateClips(ref AudioClip[] array, string name)
    {
        char sep = Path.DirectorySeparatorChar;
        array = Resources.LoadAll<AudioClip>($"audio{sep}{name}");
    }

    private void InitSources()
    {
        efxSources = new AudioSource[sourceCount];

        for (int x = 0; x < sourceCount; x++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.pitch = (highPitchRange - lowPitchRange) * x / sourceCount + lowPitchRange;
            efxSources[x] = source;
        }
    }

    public void SetVolume(float combatVolume, float voreVolume, float passiveVoreSoundVolume)
    {
        this.voreVolume = voreVolume;
        this.combatVolume = combatVolume;
        this.passiveVoreSoundVolume = passiveVoreSoundVolume;

        //foreach(var source in efxSources)
        //{
        //    source.volume = volume;
        //}
    }

    private void Awake()
    {
        char sep = Path.DirectorySeparatorChar;

        PopulateClips(ref Swings, $"combat{sep}swings");
        PopulateClips(ref ArrowHits, $"combat{sep}arrow-hits");
        PopulateClips(ref MeleeHits, $"combat{sep}melee-hits");
        PopulateClips(ref ArmorHits, $"combat{sep}armor-hits");

        PopulateClips(ref Burps, $"vore{sep}burps");
        PopulateClips(ref Farts, $"vore{sep}farts");

        PopulateVoreClips(Swallows, "swallow");
        PopulateVoreClips(FailedSwallows, "fail");
        PopulateVoreClips(Digests, "digest");
        PopulateVoreClips(DigestLoops, "digest-loop");
        PopulateVoreClips(Absorbs, "absorb");
        PopulateVoreClips(AbsorbLoops, "absorb-loop");

        foreach (var spell in SpellList.SpellDict.Values)
        {
            string id = spell.Id;

            SpellCasts[id] = Resources.LoadAll<AudioClip>($"audio{sep}spell{sep}{id}{sep}cast");
            SpellHits[id] = Resources.LoadAll<AudioClip>($"audio{sep}spell{sep}{id}{sep}hit");
        }
        MiscSounds["unbound"] = Resources.LoadAll<AudioClip>($"audio{sep}spell{sep}unbound");

        InitSources();
    }

    private void PlayLoop(AudioClip clip, AudioSource source)
    {
        // Debug.Log(clip);
        source.clip = clip;
        source.Play();
    }

    private void PlaySfx(AudioClip clip, AudioSource source, float volume)
    {
        source.PlayOneShot(clip, volume); //Allows sounds to play over each other without using multiple Audio Sources
    }

    float PositionSound(AudioSource source, Vector2 position, float minVolume)
    {
        Camera camera = State.GameManager.Camera;

        float deltaX = camera.transform.position.x - position.x;
        float deltaY = camera.transform.position.y - position.y;
        float height = camera.orthographicSize / 4;

        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY + height * height);

        float volume = 1.2f / (deltaX * deltaX + deltaY * deltaY + height * height * height * height);

        // todo: do volume properly oops
        // I don't like this formula very much

        float pan = -Mathf.Atan(deltaX / distance / 5) / Mathf.PI * 2;

        source.panStereo = pan;

        // since we might need to pass the volume along, we just return it

        return Mathf.Clamp(volume, minVolume, 1f);
    }

    // Plays a sound that isn't attached to a unit
    // If the location is null, the sound has no pan or volume adjustment
    // Otherwise, it sounds like it came from that location on the tactical map

    void RandomizeSfxGlobal(AudioClip[] clips, Vector2? location, float volume)
    {
        if (SoundEnabled == false)
            return;
        if (clips == null || clips.Length == 0)
            return;
        if (State.GameManager.TacticalMode.TacticalSoundBlocked())
            return;

        AudioSource source = efxSources[sourceIndex];

        sourceIndex = (sourceIndex + 1) % sourceCount;

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        if (location != null)
        {
            volume *= PositionSound(source, (Vector2)location, 0.7f);
        }
        else
        {
            source.panStereo = 0;
        }

        PlaySfx(clip, source, volume);
    }

    void RandomizeLoop(AudioClip[] clips, Actor_Unit actor, float volume)
    {
        if (SoundEnabled == false)
            return;
        if (clips == null || clips.Length == 0)
            return;
        if (actor == null)
        {
            return;
        }

        if (State.GameManager.TacticalMode.TacticalSoundBlocked())
            return;

        AudioSource source = actor.UnitSprite.LoopSource;

        volume *= PositionSound(source, actor.UnitSprite.transform.position, loopFloor);

        source.volume = volume;

        // Don't interrupt an existing source

        if (source.isPlaying)
            return;

        source.pitch = Random.Range(lowPitchRange, highPitchRange);

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        PlayLoop(clip, source);
    }

    void RandomizeSfx(AudioClip[] clips, Actor_Unit actor, float volume)
    {
        if (SoundEnabled == false)
            return;
        if (clips == null || clips.Length == 0)
            return;

        if (actor == null || actor.UnitSprite == null)
        {
            RandomizeSfxGlobal(clips, null, volume);
            return;
        }
        if (State.GameManager.TacticalMode.TacticalSoundBlocked())
            return;

        AudioSource source = actor.UnitSprite.SfxSources[Random.Range(0, actor.UnitSprite.SfxSourcesCount)];

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        volume *= PositionSound(source, actor.UnitSprite.transform.position, sfxFloor);

        if (actor.UnitSprite.isActiveAndEnabled) //Keeps a Unity warning from popping up
            PlaySfx(clip, source, volume);
    }

    internal void PlayMisc(string name, Actor_Unit actor)
    {
        var volume = combatVolume;

        AudioClip[] clips = MiscSounds[name];

        if (SoundEnabled == false)
            return;
        if (clips == null || clips.Length == 0)
            return;

        if (actor == null || actor.UnitSprite == null)
        {
            RandomizeSfxGlobal(clips, null, volume);
            return;
        }
        if (State.GameManager.TacticalMode.TacticalSoundBlocked())
            return;

        AudioSource source = actor.UnitSprite.SfxSources[Random.Range(0, actor.UnitSprite.SfxSourcesCount)];

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        volume *= PositionSound(source, actor.UnitSprite.transform.position, sfxFloor);

        if (actor.UnitSprite.isActiveAndEnabled) //Keeps a Unity warning from popping up
            PlaySfx(clip, source, volume);
    }
}
