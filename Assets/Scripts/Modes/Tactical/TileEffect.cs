using OdinSerializer;

public class TileEffect
{
    [OdinSerialize]
    internal int RemainingDuration;
    [OdinSerialize]
    internal double Strength;
    [OdinSerialize]
    internal TileEffectType Type;

    public TileEffect(int remainingDuration, double strength, TileEffectType type)
    {
        RemainingDuration = remainingDuration;
        Strength = strength;
        Type = type;
    }
    
    public override string ToString()
    {
        string retval = "TileEffect(";
        retval += RemainingDuration.ToString();
        retval += ", ";
        retval += Strength.ToString("F2");
        retval += ", ";
        retval += Type.ToString();
        retval += ")";
        return retval;
    }
}
