using System;
using System.Collections.Generic;
using UnityEngine;

class Vipers : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Vipers1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Vipers2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Vipers3;
    readonly Sprite[] Sprites4 = SpriteDictionary.Vipers4;

    readonly float xOffset = -7.5f; //12 pixels * 5/8

    bool oversize = false;

    public Vipers()
    {
        BodySizes = 0;
        HairStyles = 0;
        EyeTypes = 4;
        SpecialAccessoryCount = 16; // hood
        AccessoryColors = 0;
        HairColors = 0;
        MouthTypes = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        BodyAccentTypes1 = 7; // extra pattern
        TailTypes = 2; // scale pattern on/off

        WeightGainDisabled = true;
        GentleAnimation = true;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(3, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // hood
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.ExtraColor1)); // extra pattern
        BodyAccent2 = new SpriteExtraInfo(12, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // second stomach
        BodyAccent3 = new SpriteExtraInfo(11, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // default tail
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // arms
        BodyAccent5 = new SpriteExtraInfo(10, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // slit outside
        BodyAccent6 = new SpriteExtraInfo(10, BodyAccentSprite6, WhiteColored); // slit inside
        BodyAccent7 = new SpriteExtraInfo(3, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // middle tail
        BodyAccent8 = new SpriteExtraInfo(3, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor)); // middle tail B
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(6, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(6, EyesSecondarySprite, WhiteColored);
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(9, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(8, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(16, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(15, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(14, BallsSprite, WhiteColored);

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new ViperArmour1TypeFull(),
            new ViperArmour1TypeNoGloves(),
            new ViperArmour1TypeNoCap(),
            new ViperArmour1TypeBare(),
            new ViperArmour2TypeFull(),
            new ViperArmour2TypeNoGloves(),
            new ViperArmour2TypeNoCap(),
            new ViperArmour2TypeBare(),
            new ViperArmour3TypeFull(),
            new ViperArmour3TypeNoGloves(),
            new ViperArmour3TypeNoCap(),
            new ViperArmour3TypeBare(),
            new ViperArmour4TypeFull(),
            new ViperArmour4TypeNoGloves(),
            new ViperArmour4TypeNoCap(),
            new ViperArmour4TypeBare(),
            new ViperRuler1TypeFull(),
            new ViperRuler1TypeNoGloves(),
            new ViperRuler1TypeNoCap(),
            new ViperRuler1TypeBare(),
            new ViperRuler2TypeFull(),
            new ViperRuler2TypeNoGloves(),
            new ViperRuler2TypeNoCap(),
            new ViperRuler2TypeBare(),
        };

        AllowedWaistTypes = new List<MainClothing>()
        {
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();

        AvoidedMainClothingTypes = 0;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);

        unit.ExtraColor1 = unit.SkinColor;

        unit.TailType = 0;
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (!Config.LamiaUseTailAsSecondBelly)
        {
            if (actor.GetCombinedStomachSize(15) > 14)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -8 * .625f);
                AddOffset(Dick, 0, -8 * .625f);
                AddOffset(BodyAccent5, 0, -8 * .625f);
                AddOffset(BodyAccent6, 0, -8 * .625f);
            }
            else if (actor.GetCombinedStomachSize(15) > 12)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -6 * .625f);
                AddOffset(Dick, 0, -6 * .625f);
                AddOffset(BodyAccent5, 0, -6 * .625f);
                AddOffset(BodyAccent6, 0, -6 * .625f);
            }
            else if (actor.GetCombinedStomachSize(15) > 10)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -4 * .625f);
                AddOffset(Dick, 0, -4 * .625f);
                AddOffset(BodyAccent5, 0, -4 * .625f);
                AddOffset(BodyAccent6, 0, -4 * .625f);
            }
            else if (actor.GetCombinedStomachSize(15) > 8)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -2 * .625f);
                AddOffset(Dick, 0, -2 * .625f);
                AddOffset(BodyAccent5, 0, -2 * .625f);
                AddOffset(BodyAccent6, 0, -2 * .625f);
            }
            else if (actor.GetCombinedStomachSize(15) > 6)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -1 * .625f);
                AddOffset(Dick, 0, -1 * .625f);
                AddOffset(BodyAccent5, 0, -1 * .625f);
                AddOffset(BodyAccent6, 0, -1 * .625f);
            }
            else
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, 0);
            }
        }
        else
        {
            if (actor.GetStomachSize(15) > 14)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -8 * .625f);
                AddOffset(Dick, 0, -8 * .625f);
                AddOffset(BodyAccent5, 0, -8 * .625f);
                AddOffset(BodyAccent6, 0, -8 * .625f);
            }
            else if (actor.GetStomachSize(15) > 12)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -6 * .625f);
                AddOffset(Dick, 0, -6 * .625f);
                AddOffset(BodyAccent5, 0, -6 * .625f);
                AddOffset(BodyAccent6, 0, -6 * .625f);
            }
            else if (actor.GetStomachSize(15) > 10)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -4 * .625f);
                AddOffset(Dick, 0, -4 * .625f);
                AddOffset(BodyAccent5, 0, -4 * .625f);
                AddOffset(BodyAccent6, 0, -4 * .625f);
            }
            else if (actor.GetStomachSize(15) > 8)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -2 * .625f);
                AddOffset(Dick, 0, -2 * .625f);
                AddOffset(BodyAccent5, 0, -2 * .625f);
                AddOffset(BodyAccent6, 0, -2 * .625f);
            }
            else if (actor.GetStomachSize(15) > 6)
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, -1 * .625f);
                AddOffset(Dick, 0, -1 * .625f);
                AddOffset(BodyAccent5, 0, -1 * .625f);
                AddOffset(BodyAccent6, 0, -1 * .625f);
            }
            else
            {
                AddOffset(Belly, xOffset, 0);
                AddOffset(Breasts, xOffset, 0);
                AddOffset(SecondaryBreasts, xOffset, 0);
                AddOffset(Balls, xOffset, 0);
            }
        }
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.TailType == 0)
        {
            if (actor.Unit.HasBreasts)
                return Sprites[0];
            return Sprites[1];
        }
        else
        {
            if (actor.Unit.HasBreasts)
                return Sprites4[0];
            return Sprites4[1];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.TailType == 0)
        {
            if (actor.IsEating || actor.IsAttacking)
                return Sprites[3];
            return Sprites[2];
        }
        else
        {
            if (actor.IsEating || actor.IsAttacking)
                return Sprites4[3];
            return Sprites4[2];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // hood
    {
        if (actor.Unit.TailType == 0)
        {
            return Sprites[21 + actor.Unit.SpecialAccessoryType];
        }
        else
        {
            return Sprites4[5 + actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[6 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodyAccentType1)]; // extra pattern

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // second stomach = tail
    {
        if (actor.Unit.Predator == false)
            return null;
        int size2;
        if (Config.LamiaUseTailAsSecondBelly && (actor.PredatorComponent.Stomach2ndFullness > 0 || actor.PredatorComponent.TailFullness > 0))
            size2 = Math.Min(actor.GetStomach2Size(19, 0.9f) + actor.GetTailSize(19, 0.9f), 19);
        else if (actor.PredatorComponent.TailFullness > 0)
            size2 = actor.GetTailSize(19, 0.9f);
        else
            return null;

        if (actor.Unit.TailType == 0)
        {
            if (size2 == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.tail))
            {
                return Sprites3[1];
            }
            else if (size2 == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.tail))
            {
                return Sprites3[0];
            }
            else
            {
                return Sprites2[80 + size2];
            }
        }
        else
        {
            if (size2 == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.tail))
            {
                return Sprites4[47];
            }
            else if (size2 == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.tail))
            {
                return Sprites4[46];
            }
            else
            {
                return Sprites4[48 + size2];
            }
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // default tail
    {
        if (actor.Unit.TailType == 0)
        {
            return Sprites[37];
        }
        else
        {
            return Sprites4[21];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[38 + (actor.IsAttacking ? 1 : 0)]; // arms

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // slit outside
    {
        if (Config.HideCocks) return null;

        if (Config.HideViperSlits) return null;


        if (actor.Unit.HasDick == false)
        {
            if (actor.IsUnbirthing)
                return Sprites[49];
            return Sprites[48];
        }
        else
        {
            if (actor.IsErect() || actor.IsCockVoring)
                return Sprites[52];
            return Sprites[51];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // slit inside
    {
        if (Config.HideCocks) return null;

        if (Config.HideViperSlits) return null;

        if (actor.Unit.HasDick == false)
        {
            if (actor.IsUnbirthing)
                return Sprites[50];
            return null;
        }
        else
        {
            if (actor.IsErect() || actor.IsCockVoring)
                return Sprites[53];
            return null;
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // middle tail
    {
        if (actor.Unit.TailType == 0)
        {
            if (actor.GetStomachSize(15) == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                return Sprites[99];
            else if (actor.GetStomachSize(15) == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                return Sprites[98];
            else if (actor.GetStomachSize(15) >= 14)
                return Sprites[97];
            else if (actor.GetStomachSize(15) >= 12)
                return Sprites[96];
            return null;
        }
        else
        {
            if (actor.GetStomachSize(15) == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                return Sprites4[27];
            else if (actor.GetStomachSize(15) == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                return Sprites4[26];
            else if (actor.GetStomachSize(15) >= 14)
                return Sprites4[25];
            else if (actor.GetStomachSize(15) >= 12)
                return Sprites4[24];
            return null;
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // middle tail B
    {
        if (actor.Unit.Predator == false)
            return null;
        int size2;
        if (Config.LamiaUseTailAsSecondBelly && (actor.PredatorComponent.Stomach2ndFullness > 0 || actor.PredatorComponent.TailFullness > 0))
            size2 = Math.Min(actor.GetStomach2Size(19, 0.9f) + actor.GetTailSize(19, 0.9f), 19);
        else if (actor.PredatorComponent.TailFullness > 0)
            size2 = actor.GetTailSize(19, 0.9f);
        else
            return null;

        if (actor.Unit.TailType == 0)
        {
            if (size2 >= 19)
                return Sprites4[70];
            else if (size2 >= 18)
                return Sprites4[69];
            else if (size2 >= 15)
                return Sprites4[68];
            return null;
        }
        else
        {
            if (size2 >= 19)
                return Sprites4[73];
            else if (size2 >= 18)
                return Sprites4[72];
            else if (size2 >= 15)
                return Sprites4[71];
            return null;
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[5];
        else if (actor.IsEating)
            return Sprites[4];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[44 + actor.Unit.EyeType];

    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => Sprites[40 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!Config.LamiaUseTailAsSecondBelly)
        {
            if (actor.HasBelly)
            {
                int size0 = actor.GetCombinedStomachSize(15);

                if (actor.Unit.TailType == 0)
                {
                    if (size0 == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        return Sprites[95];
                    }
                    else if (size0 == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        return Sprites[94];
                    }
                    else
                    {
                        return Sprites[78 + size0];
                    }
                }
                else
                {
                    if (size0 == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        return Sprites4[45];
                    }
                    else if (size0 == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        return Sprites4[44];
                    }
                    else
                    {
                        return Sprites4[28 + size0];
                    }
                }
            }
            else
            {
                return null;
            }
        }
        if (actor.HasBelly)
        {
            int size = actor.GetStomachSize(15);

            if (actor.Unit.TailType == 0)
            {
                if (size == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    return Sprites[95];
                }
                else if (size == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    return Sprites[94];
                }
                else
                {
                    return Sprites[78 + size];
                }
            }
            else
            {
                if (size == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    return Sprites4[45];
                }
                else if (size == 15 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    return Sprites4[44];
                }
                else
                {
                    return Sprites4[28 + size];
                }
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return Sprites[70 + actor.GetWeaponSprite()];
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
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(28 * 28, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites2[27];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
            {
                return Sprites2[26];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 24)
            {
                return Sprites2[25];
            }

            if (leftSize > 24)
                leftSize = 24;

            return Sprites2[0 + leftSize];
        }
        else
        {
            return Sprites2[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(28 * 28, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites2[55];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
            {
                return Sprites2[54];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 24)
            {
                return Sprites2[53];
            }

            if (rightSize > 24)
                rightSize = 24;

            return Sprites2[28 + rightSize];
        }
        else
        {
            return Sprites2[28 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsCockVoring)
        {
            return Sprites[62 + actor.Unit.DickSize];
        }
        else if (actor.IsErect())
        {
            return Sprites[54 + actor.Unit.DickSize];
        }
        else
            return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        int ballsize = actor.GetBallSize(20, 1.5f);

        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(20, 1.5f) == 20)
        {
            return Sprites2[79];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(20, 1.2f) == 20)
        {
            return Sprites2[78];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(20, 1.35f) == 20)
        {
            return Sprites2[77];
        }
        else if (actor.PredatorComponent?.BallsFullness > 0)
        {
            return Sprites2[56 + ballsize];
        }
        else
        {
            return null;
        }
    }


    class ViperArmour1TypeFull : MainClothing
    {
        public ViperArmour1TypeFull()
        {
            DiscardSprite = SpriteDictionary.Vipers4[22];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1422;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[10];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[2 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[11];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];
            clothing5.GetSprite = (s) => SpriteDictionary.Vipers3[17];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[13 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[15 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour1TypeNoGloves : MainClothing
    {
        public ViperArmour1TypeNoGloves()
        {
            DiscardSprite = SpriteDictionary.Vipers4[22];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1422;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[10];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[2 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[11];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[17];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour1TypeNoCap : MainClothing
    {
        public ViperArmour1TypeNoCap()
        {
            DiscardSprite = SpriteDictionary.Vipers4[22];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            Type = 1422;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[10];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[2 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[11];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[13 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[15 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour1TypeBare : MainClothing
    {
        public ViperArmour1TypeBare()
        {
            DiscardSprite = SpriteDictionary.Vipers4[22];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            Type = 1422;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[10];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[2 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[11];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour2TypeFull : MainClothing
    {
        public ViperArmour2TypeFull()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[18];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[20 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[19];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];
            clothing5.GetSprite = (s) => SpriteDictionary.Vipers3[30];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[28 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[15 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour2TypeNoGloves : MainClothing
    {
        public ViperArmour2TypeNoGloves()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[18];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[20 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[19];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[30];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour2TypeNoCap : MainClothing
    {
        public ViperArmour2TypeNoCap()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[18];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[20 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[19];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[28 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[15 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour2TypeBare : MainClothing
    {
        public ViperArmour2TypeBare()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[18];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[20 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[19];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour3TypeFull : MainClothing
    {
        public ViperArmour3TypeFull()
        {
            DiscardSprite = SpriteDictionary.Vipers4[23];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1423;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[40];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[31 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[41];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];
            clothing5.GetSprite = (s) => SpriteDictionary.Vipers3[39];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[42 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[15 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour3TypeNoGloves : MainClothing
    {
        public ViperArmour3TypeNoGloves()
        {
            DiscardSprite = SpriteDictionary.Vipers4[23];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1423;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[40];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[31 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[41];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[39];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour3TypeNoCap : MainClothing
    {
        public ViperArmour3TypeNoCap()
        {
            DiscardSprite = SpriteDictionary.Vipers4[23];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            Type = 1423;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[40];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[31 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[41];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[42 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[15 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour3TypeBare : MainClothing
    {
        public ViperArmour3TypeBare()
        {
            DiscardSprite = SpriteDictionary.Vipers4[23];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            Type = 1423;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[40];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[31 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[41];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[12];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler1TypeFull : MainClothing
    {
        public ViperRuler1TypeFull()
        {
            DiscardSprite = SpriteDictionary.Vipers3[98];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1498;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[52];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[44 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[53];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[57];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[55 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler1TypeNoGloves : MainClothing
    {
        public ViperRuler1TypeNoGloves()
        {
            DiscardSprite = SpriteDictionary.Vipers3[98];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1498;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[52];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[44 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[53];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[57];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler1TypeNoCap : MainClothing
    {
        public ViperRuler1TypeNoCap()
        {
            DiscardSprite = SpriteDictionary.Vipers3[98];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1498;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[52];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[44 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[53];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[55 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler1TypeBare : MainClothing
    {
        public ViperRuler1TypeBare()
        {
            DiscardSprite = SpriteDictionary.Vipers3[98];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            Type = 1498;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[52];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[44 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[53];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour4TypeFull : MainClothing
    {
        public ViperArmour4TypeFull()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing6 = new SpriteExtraInfo(7, null, null);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[68];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[78];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[60 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[70 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[69];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[79];
            }

            clothing5.GetSprite = (s) => SpriteDictionary.Vipers3[58];
            clothing6.GetSprite = (s) => SpriteDictionary.Vipers3[59];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[80 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[82 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour4TypeNoGloves : MainClothing
    {
        public ViperArmour4TypeNoGloves()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[68];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[78];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[60 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[70 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[69];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[79];
            }

            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[58];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[59];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour4TypeNoCap : MainClothing
    {
        public ViperArmour4TypeNoCap()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[68];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[78];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[60 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[70 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[69];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[79];
            }

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[80 + (attacking ? 1 : 0)];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[82 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperArmour4TypeBare : MainClothing
    {
        public ViperArmour4TypeBare()
        {
            DiscardSprite = SpriteDictionary.Vipers3[97];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            Type = 1497;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[68];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[78];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[60 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[70 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[69];
                clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[79];
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler2TypeFull : MainClothing
    {
        public ViperRuler2TypeFull()
        {
            DiscardSprite = SpriteDictionary.Vipers3[99];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1499;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[92];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[84 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[93];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];
            clothing4.GetSprite = (s) => SpriteDictionary.Vipers3[96];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[94 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler2TypeNoGloves : MainClothing
    {
        public ViperRuler2TypeNoGloves()
        {
            DiscardSprite = SpriteDictionary.Vipers3[99];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1499;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[92];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[84 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[93];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[96];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler2TypeNoCap : MainClothing
    {
        public ViperRuler2TypeNoCap()
        {
            DiscardSprite = SpriteDictionary.Vipers3[99];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1499;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[92];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[84 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[93];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];

            bool attacking = actor.IsAttacking;
            clothing3.GetSprite = (s) => SpriteDictionary.Vipers3[94 + (attacking ? 1 : 0)];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }


    class ViperRuler2TypeBare : MainClothing
    {
        public ViperRuler2TypeBare()
        {
            DiscardSprite = SpriteDictionary.Vipers3[99];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            Type = 1499;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Vipers.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[92];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[84 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vipers3[93];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vipers3[54];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }



}
