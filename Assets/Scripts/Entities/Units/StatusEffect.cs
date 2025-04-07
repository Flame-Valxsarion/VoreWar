using OdinSerializer;


class StatusEffect
{
    [OdinSerialize]
    internal StatusEffectType Type;
    [OdinSerialize]
    internal float Strength;
    [OdinSerialize]
    internal int Duration;
    [OdinSerialize]
    internal Unit Applicator;

    public StatusEffect(StatusEffectType type, float strength, int duration, Unit applicator = null)
    {
        Type = type;
        Strength = strength;
        Duration = duration;
        Applicator = applicator;
    }

}

