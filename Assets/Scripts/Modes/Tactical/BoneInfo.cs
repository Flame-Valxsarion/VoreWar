using OdinSerializer;
using UnityEngine;

class BoneInfo
{
    [OdinSerialize]
    public BoneTypes boneTypes = BoneTypes.GenericBonePile;
    [OdinSerialize]
    public string name = "";
    [OdinSerialize]
    public int accessoryColor = -1;

    public BoneInfo(BoneTypes boneTypes, string name = "", int accessoryColor = -1)
    {
        this.name = name;
        this.boneTypes = boneTypes;
        this.accessoryColor = accessoryColor;
    }

    public Vector3 GetBoneScalingForScat()
    {
        Vector3 rtn = new Vector3(1f, 1f);
        switch (boneTypes)
        {
            case BoneTypes.GenericBonePile:
            case BoneTypes.SlimePile:
            case BoneTypes.CrypterBonePile:
            case BoneTypes.DisposedCondom:
            case BoneTypes.CumPuddle:
            case BoneTypes.HoneyPuddle:
            case BoneTypes.UrinePuddle:
                rtn = new Vector3(0.65f, 0.65f);
                break;
            case BoneTypes.CrypterSkull:
            case BoneTypes.HumanoidSkull:
            case BoneTypes.LizardSkull:
            case BoneTypes.Imp2EyeSkull:
            case BoneTypes.Imp1EyeSkull:
            case BoneTypes.Imp3EyeSkull:
            case BoneTypes.SeliciaSkull:
                rtn = new Vector3(0.65f, 0.65f);
                break;
            case BoneTypes.FurryBones:
            case BoneTypes.FurryRabbitBones:
            case BoneTypes.HarrysGooPile:
                rtn = new Vector3(1f, 1f);
                break;
            case BoneTypes.Kangaroo:
                rtn = new Vector3(0.85f, 0.85f);
                break;
            case BoneTypes.Alligator:
                rtn = new Vector3(1f, 1f);
                break;
            case BoneTypes.Wyvern:
            case BoneTypes.Compy:
            case BoneTypes.Shark:
            case BoneTypes.DarkSwallower:
            case BoneTypes.Cake:
            case BoneTypes.WyvernBonesWithoutHead:
                rtn = new Vector3(0.5f, 0.5f);
                break;
            case BoneTypes.VisionSkull:
                rtn = new Vector3(0.8f, 0.8f);
                break;
            default:
                // Hide unknown bone type
                rtn = new Vector3(0f, 0f);
                break;
        }
        return rtn;
    }

    public Vector3 GetBoneOffsetForScat()
    {
        Vector3 rtn;
        switch (this.boneTypes)
        {
            case BoneTypes.GenericBonePile:
                rtn = new Vector3(0, 0.01f);
                break;
            case BoneTypes.SlimePile:
            case BoneTypes.CrypterBonePile:
            case BoneTypes.DisposedCondom:
            case BoneTypes.CumPuddle:
                rtn = new Vector3(0, 0);
                break;
            case BoneTypes.CrypterSkull:
            case BoneTypes.HumanoidSkull:
            case BoneTypes.LizardSkull:
            case BoneTypes.Imp2EyeSkull:
            case BoneTypes.Imp1EyeSkull:
            case BoneTypes.Imp3EyeSkull:
                rtn = new Vector3(Random.Range(-.07f, .07f), -.02f);
                break;
            case BoneTypes.SeliciaSkull:
                rtn = new Vector3(Random.Range(-.07f, .07f), Random.Range(0, .03f));
                break;
            case BoneTypes.FurryBones:
            case BoneTypes.FurryRabbitBones:
            case BoneTypes.HarrysGooPile:
                rtn = new Vector3(0, 0.02f);
                break;
            case BoneTypes.Kangaroo:
                rtn = new Vector3(0, 0.02f);
                break;
            case BoneTypes.Alligator:
            case BoneTypes.Wyvern:
                rtn = new Vector3(0, 0);
                break;
            case BoneTypes.Compy:
                rtn = new Vector3(0, 0.03f);
                break;
            case BoneTypes.Shark:
                rtn = new Vector3(0 - 0.01f, -0.01f);
                break;
            case BoneTypes.DarkSwallower:
                rtn = new Vector3(0, 0.05f);
                break;
            case BoneTypes.Cake:
                rtn = new Vector3(Random.Range(-.11f, .11f), 0.1f);
                break;
            case BoneTypes.WyvernBonesWithoutHead:
                rtn = new Vector3(0, .05f);
                break;
            case BoneTypes.VisionSkull:
                rtn = new Vector3(Random.Range(-.07f, .07f), Random.Range(-0.13f, -0.15f));
                break;
            default:
                rtn = new Vector3(0, 0);
                break;
        }
        return rtn;
    }

    public Vector3 GetBonePosForScat(Vector3 orgin)
    {
        Vector3 offset = GetBoneOffsetForScat();
        return new Vector3(orgin.x + offset.x, orgin.y + offset.y);
    }
}
