using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class TaurHumanHalf : BlankSlate
{
    //readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.CentaurParts;
    internal bool oversize = false;
    public TaurHumanHalf()
    {
        EyeTypes = 6;
        HairStyles = 36;
        MouthTypes = 12;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        GentleAnimation = true;
        WeightGainDisabled = true;
        BodyAccentTypes1 = 6; // eyebrows

        ExtendedBreastSprites = true;

        Head = new SpriteExtraInfo(12, HeadSprite, null, (s) => FurryColor(s)); // human part
        Eyes = new SpriteExtraInfo(14, EyesSprite, WhiteColored); // human part
        SecondaryEyes = new SpriteExtraInfo(13, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor)); // human part
        Mouth = new SpriteExtraInfo(13, MouthSprite, WhiteColored); // human part
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // human part
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // human part
        Hair3 = new SpriteExtraInfo(15, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows human part
        Body = new SpriteExtraInfo(11, BodySprite, null, (s) => FurryColor(s));// Human Body
        Weapon = new SpriteExtraInfo(11, WeaponSprite, WhiteColored); // human part
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColor(s)); // human part
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColor(s)); // human part
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColor(s));// human part
    }
    internal override int BreastSizes => 8;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.EyeColor = State.Rand.Next(EyeColors);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 1 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 0);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }

        if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 3 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 3 * .625f, -4 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 2)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 2 * .625f);
                }
                else
                {
                    AddOffset(Weapon, -2 * .625f, 3 * .625f);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
            }
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[4];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[1];
                }
            }
            else
            {
                return State.GameManager.SpriteDictionary.HumansBodySprites2[7];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[5];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[2];
                }
            }
            else
            {
                return State.GameManager.SpriteDictionary.HumansBodySprites2[8];
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[3];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[0];
                }
            }
            else
            {
                return State.GameManager.SpriteDictionary.HumansBodySprites2[6];
            }

        }
    }
    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else
            return State.GameManager.SpriteDictionary.HumansBodySprites3[108 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.HumansBodySprites2[71 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.HumansBodySprites2[72 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.HumansBodySprites3[120 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites2[69];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites3[24 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return null;
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites3[25 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return State.GameManager.SpriteDictionary.TaurTorso[3 + (actor.Unit.HasBreasts ? 0 : 4)];
            return State.GameManager.SpriteDictionary.TaurTorso[0 + (actor.Unit.HasBreasts ? 0 : 4)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 1:
                return State.GameManager.SpriteDictionary.TaurTorso[3 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 2:
                return State.GameManager.SpriteDictionary.TaurTorso[1 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 3:
                return State.GameManager.SpriteDictionary.TaurTorso[3 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 4:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 5:
                return State.GameManager.SpriteDictionary.TaurTorso[1 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 6:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 7:
                return State.GameManager.SpriteDictionary.TaurTorso[1 + (actor.Unit.HasBreasts ? 0 : 4)];
            default:
                return State.GameManager.SpriteDictionary.TaurTorso[0 + (actor.Unit.HasBreasts ? 0 : 4)];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.GetExclusiveStomachSize(24) < 1) return null;
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[102];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return State.GameManager.SpriteDictionary.HumansVoreSprites[70 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return State.GameManager.SpriteDictionary.HumansVoreSprites[0 + leftSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return State.GameManager.SpriteDictionary.HumansVoreSprites[32 + rightSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
        }
    }
    internal static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }

    internal void OffsetAllHumanParts(Actor_Unit actor, int x, int y)
    {
        // Offset all human parts
        float humanPartXoffset = x * .625f;
        float humanPartYoffset = y * .625f;
        AddOffset(Head, humanPartXoffset, humanPartYoffset);
        AddOffset(Eyes, humanPartXoffset, humanPartYoffset);
        AddOffset(SecondaryEyes, humanPartXoffset, humanPartYoffset);
        AddOffset(Mouth, humanPartXoffset, humanPartYoffset);
        AddOffset(Hair, humanPartXoffset, humanPartYoffset);
        AddOffset(Hair2, humanPartXoffset, humanPartYoffset);
        AddOffset(Hair3, humanPartXoffset, humanPartYoffset);
        AddOffset(Body, humanPartXoffset, humanPartYoffset);
        AddOffset(Weapon, humanPartXoffset, humanPartYoffset);
        AddOffset(Breasts, humanPartXoffset, humanPartYoffset);
        AddOffset(SecondaryBreasts, humanPartXoffset, humanPartYoffset);
        AddOffset(Belly, humanPartXoffset, humanPartYoffset);
    }
}