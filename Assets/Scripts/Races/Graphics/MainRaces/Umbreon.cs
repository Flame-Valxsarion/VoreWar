using System;
using System.Collections.Generic;
using UnityEngine;

class Umbreon : DefaultRaceData
{
//    bool BreastBlocked = false; // Handled in MainClothing.cs
//    bool CockBlocked = false; // Handled in MainClothing.cs
    bool slotBA1 = false;
    bool slotBA2 = false;
    bool slotBA3 = false;

    public Umbreon()
    {
        GentleAnimation = true;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonSkin);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonSkin);
        BodySizes = 0;
        BodyAccentTypes1 = 28; // Shirt Decal
        BodyAccentTypes2 = 2; // Rust
        EyeTypes = 9;
        HairStyles = 0;
        MouthTypes = 0;
        HairColors = 0;
        FurCapable = true; // Used to determine handedness

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(4, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); // rings
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); // head rings
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); // Dick rings
        BodyAccent3 = new SpriteExtraInfo(21, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); // Left Breast Ring color
        BodyAccent4 = new SpriteExtraInfo(21, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); // Right Breast Ring color
        BodyAccent5 = new SpriteExtraInfo(19, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); // Belly ring color
        BodyAccent6 = new SpriteExtraInfo(0, BodyAccentSprite6, WhiteColored); // Body Armor check 1
        BodyAccent7 = new SpriteExtraInfo(0, BodyAccentSprite7, WhiteColored); // Body Armor check 2
        BodyAccent8 = new SpriteExtraInfo(0, BodyAccentSprite8, WhiteColored); // Body Armor check 3
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored);        
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(4, EyesSecondarySprite, WhiteColored);
        Belly = new SpriteExtraInfo(18, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonExt, s.Unit.SkinColor));
        Beard = new SpriteExtraInfo(4, BeardSprite, WhiteColored); //nose
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null; //new SpriteExtraInfo(3, BodySizeSprite, null, FurryBellyColor);
        Breasts = new SpriteExtraInfo(20, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonExt, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(20, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonExt, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor));
        Hair = null;
        Hair2 = null;
        Hair3 = null;

        AvoidedMainClothingTypes = 0;

        //RestrictedClothingTypes = 0;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonClothes);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonArmor);
        //ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonArmor);
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
            MainAccessories.SantaHat,
        };
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new TShirt()
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            new Pants1(),
            new Pants2(),
            new Pants3(),
            new Pants4(),
            new Pants5(),
            new Pants6(),
            new Pants7(),
            new Pants8(),
            new Pants9(),
        };

        ExtraMainClothing1Types = new List<MainClothing>() //Special clothing
        {
            new Armor1(),
            new Armor2(),
            new LeaderArmor1(),
            new LeaderArmor2(),
        };
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.ClothingColor = State.Rand.Next(clothingColors);
        unit.ExtraColor1 = State.Rand.Next(ExtraColors1);
        //unit.ExtraColor2 = State.Rand.Next(ExtraColors2);
        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingExtraType1 = State.Rand.Next(2) + 3;
        }
        else
        {
            unit.ClothingExtraType1 = State.Rand.Next(2) + 1;
        }
    }

    internal override int BreastSizes => 8;
    internal override int DickSizes => 7;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int sprite = actor.IsAttacking ? (actor.Unit.Furry ? 2 : 1) : 0;

        return State.GameManager.SpriteDictionary.Umbreon[sprite];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        int sprite = actor.IsAttacking ? (actor.Unit.Furry ? 6 : 5) : 4;

        return State.GameManager.SpriteDictionary.Umbreon[sprite];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Umbreon[7];

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));

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

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .50f)
            {
                Dick.layer = 21;
                return State.GameManager.SpriteDictionary.Umbreon2[35 + (actor.Unit.DickSize * 2)];
            }
            else
            {
                Dick.layer = 14;
                return State.GameManager.SpriteDictionary.Umbreon2[36 + (actor.Unit.DickSize * 2)];
            }
        }

        return null;
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false /*|| CockBlocked*/)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .50f)
            {
                BodyAccent2.layer = 22;
                return State.GameManager.SpriteDictionary.Umbreon2[49 + (actor.Unit.DickSize * 2)];
            }
            else
            {
                BodyAccent2.layer = 15;
                return State.GameManager.SpriteDictionary.Umbreon2[50 + (actor.Unit.DickSize * 2)];
            }
        }
        return null;
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Left Breast Ring color
    {
        if (actor.Unit.HasBreasts == false /*|| BreastBlocked*/)
            return null;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return State.GameManager.SpriteDictionary.Umbreon3[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return State.GameManager.SpriteDictionary.Umbreon3[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return State.GameManager.SpriteDictionary.Umbreon3[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return State.GameManager.SpriteDictionary.Umbreon3[0 + leftSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.Umbreon3[0 + actor.Unit.BreastSize];
        }
    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Right Breast Ring color
    {
        if (actor.Unit.HasBreasts == false /*|| BreastBlocked*/)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return State.GameManager.SpriteDictionary.Umbreon3[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return State.GameManager.SpriteDictionary.Umbreon3[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return State.GameManager.SpriteDictionary.Umbreon3[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return State.GameManager.SpriteDictionary.Umbreon3[32 + rightSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.Umbreon3[32 + actor.Unit.BreastSize];
        }
    }
    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Belly ring color
    {
        if (actor.HasBelly)
        {
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(BodyAccent5, 0, -33 * .625f);
                return null;
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(BodyAccent5, 0, -33 * .625f);
                return null;
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(BodyAccent5, 0, -33 * .625f);
                return null;
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(BodyAccent5, 0, -33 * .625f);
                return null;
            }
            switch (size)
            {
                case 26:
                    AddOffset(BodyAccent5, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(BodyAccent5, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(BodyAccent5, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(BodyAccent5, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(BodyAccent5, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(BodyAccent5, 0, -32 * .625f);
                    break;
            }

            return State.GameManager.SpriteDictionary.Umbreon3[70 + size];
        }
        else
        {
            return null;
        }
    }

//Check if the unit has BodyArmor for the armor sprites
    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        Accessory acc1 = null;
        slotBA1 = false;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 1)
            return null;
        if (actor.Unit.Items[0] is Accessory)
        {
            acc1 = (Accessory)actor.Unit.Items[0];
            if (acc1 == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
                slotBA1 = true;
        }
        return null;
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        Accessory acc2 = null;
        slotBA2 = false;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 2)
            return null;
        if (actor.Unit.Items[1] is Accessory)
        {
            acc2 = (Accessory)actor.Unit.Items[1];
            if (acc2 == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
                slotBA2 = true;
        }
        return null;
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        Accessory acc3 = null;
        slotBA3 = false;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 3)
            return null;
        if (actor.Unit.HasTrait(Traits.Resourceful))
            if (actor.Unit.Items[2] is Accessory)
        {
            acc3 = (Accessory)actor.Unit.Items[2];
            if (acc3 == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
                slotBA3 = true;
        }
        return null;
    }
//Check if the unit has BodyArmor for the armor sprites

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int sprite = 3;
        return State.GameManager.SpriteDictionary.Umbreon[sprite];

    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.GetWeaponSprite() >=4)
            {
                return State.GameManager.SpriteDictionary.Umbreon[106 + actor.GetWeaponSprite()];
            }
            if (actor.Unit.Furry)
            {
                return State.GameManager.SpriteDictionary.Umbreon[106 + actor.GetWeaponSprite()];
            }
            else
            {
                return State.GameManager.SpriteDictionary.Umbreon[102 + actor.GetWeaponSprite()];
            }
        }
        else return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Umbreon[11 + actor.Unit.EyeType * 2];
    }
    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Umbreon[11 + (actor.Unit.EyeType * 2) + 1];
    }

    protected override Sprite BeardSprite(Actor_Unit actor) //nose
    {
        return State.GameManager.SpriteDictionary.Umbreon[10];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {

        return State.GameManager.SpriteDictionary.Umbreon[8 + (actor.IsEating ? 1 : 0)];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
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

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        //if (actor.Unit.Furry && Config.FurryGenitals)
        //{
        //    int size = actor.Unit.DickSize;
        //    int offset = (int)((actor.PredatorComponent?.BallsFullness ?? 0) * 3);
        //    if (offset > 0)
        //        return State.GameManager.SpriteDictionary.FurryDicks[Math.Min(12 + offset, 23)];
        //    return State.GameManager.SpriteDictionary.FurryDicks[size];
        //}

        int baseSize = actor.Unit.DickSize;
        int ballOffset = actor.GetBallSize(31, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 31)
        {
            AddOffset(Balls, 0, -25 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[30];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 30)
        {
            AddOffset(Balls, 0, -25 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[29];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 29)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[28];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 28)
        {
            AddOffset(Balls, 0, -21 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[27];
        }
        int combined = Math.Min(baseSize + ballOffset, 26);
        if (combined == 26)
            AddOffset(Balls, 0, -16 * .625f);
        else if (combined == 24 || combined == 25)
            AddOffset(Balls, 0, -12 * .625f);
        else if (combined >= 23 && combined <= 24)
            AddOffset(Balls, 0, -8 * .625f);
        else if (combined == 22)
            AddOffset(Balls, 0, -6 * .625f);
        if (ballOffset > 0)
        {
            return State.GameManager.SpriteDictionary.Umbreon2[combined];
        }

        return State.GameManager.SpriteDictionary.Umbreon2[baseSize];
    }

    class TShirt : MainClothing
    {
        public TShirt()
        {
            blocksBreasts = true;
            blocksDick = false;
            Type = 86002;
            clothing1 = new SpriteExtraInfo(16, null, null); //Shirt
            clothing2 = new SpriteExtraInfo(17, null, null); //Decal
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spr = 31;
            int decal_base = 46;
            if (actor.Unit.HasBreasts)
            {
                spr = 34;
                decal_base = 74;
            }

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[spr];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[decal_base + actor.Unit.BodyAccentType1];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants1 : MainClothing
    {
        public Pants1()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[37];
            base.Configure(sprite, actor);
        }
    }

    class Pants2 : MainClothing
    {
        public Pants2()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86038;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[38];
            base.Configure(sprite, actor);
        }
    }

    class Pants3 : MainClothing
    {
        public Pants3()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86039;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[39];
            base.Configure(sprite, actor);
        }
    }

    class Pants4 : MainClothing
    {
        public Pants4()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[40];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants5 : MainClothing
    {
        public Pants5()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[41];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants6 : MainClothing
    {
        public Pants6()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[42];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants7 : MainClothing
    {
        public Pants7()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[43];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants8 : MainClothing
    {
        public Pants8()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[44];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants9 : MainClothing
    {
        public Pants9()
        {
            blocksDick = true;
            coversBreasts = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(5, null, null); //Pants
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[45];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonClothes, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Armor1 : MainClothing
    {
        public Armor1()
        {
            blocksBreasts = false;
            coversBreasts = false;
            blocksDick = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(21, null, null); //Armor
            clothing3 = new SpriteExtraInfo(6, null, null); //LegArmor
            clothing4 = new SpriteExtraInfo(23, null, WhiteColored); //'Uncolored part'
            clothing5 = new SpriteExtraInfo(24, null, null); //Rust
            clothing6 = new SpriteExtraInfo(7, null, null); //LegRust
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Umbreon.slotBA1 == true || Races.Umbreon.slotBA2 == true || Races.Umbreon.slotBA3 == true)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 116 : 115) : 114];
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[127];
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 130 : 129) : 128];
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 143 : 142) : 141];
                clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[153];
            }
            else
            {
                clothing1.GetSprite = (s) => null;
                clothing3.GetSprite = (s) => null;
                clothing4.GetSprite = (s) => null;
                clothing5.GetSprite = (s) => null;
                clothing6.GetSprite = (s) => null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            if (actor.Unit.BodyAccentType2 == 0)
            {
                clothing5.GetSprite = null;
                clothing6.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class Armor2 : MainClothing
    {
        public Armor2()
        {
            blocksBreasts = false;
            coversBreasts = false;
            blocksDick = false;
            Type = 86037;
            clothing1 = new SpriteExtraInfo(21, null, null); //Armor
            clothing3 = new SpriteExtraInfo(6, null, null); //LegArmor
            clothing4 = new SpriteExtraInfo(23, null, WhiteColored); //'Uncolored part'
            clothing5 = new SpriteExtraInfo(24, null, null); //Rust
            clothing6 = new SpriteExtraInfo(7, null, null); //LegRust
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Umbreon.slotBA1 == true || Races.Umbreon.slotBA2 == true || Races.Umbreon.slotBA3 == true)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 119 : 118) : 117];
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[127];
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 133 : 132) : 131];
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 146 : 145) : 144];
                clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[153];
            }
            else
            {
                clothing1.GetSprite = (s) => null;
                clothing3.GetSprite = (s) => null;
                clothing4.GetSprite = (s) => null;
                clothing5.GetSprite = (s) => null;
                clothing6.GetSprite = (s) => null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            if (actor.Unit.BodyAccentType2 == 0)
            {
                clothing5.GetSprite = null;
                clothing6.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class LeaderArmor1 : MainClothing
    {
        public LeaderArmor1()
        {
            Type = 86040;
            coversBreasts = false;
            blocksDick = false;
            leaderOnly = true;
            clothing1 = new SpriteExtraInfo(21, null, null); //Armor
            clothing3 = new SpriteExtraInfo(6, null, null); //LegArmor
            clothing4 = new SpriteExtraInfo(23, null, WhiteColored); //'Uncolored part'
            clothing5 = new SpriteExtraInfo(24, null, null); //Rust
            clothing6 = new SpriteExtraInfo(7, null, null); //LegRust
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Umbreon.slotBA1 == true || Races.Umbreon.slotBA2 == true || Races.Umbreon.slotBA3 == true)
            {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 122 : 121) : 120];
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[127];
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 136 : 135) : 134];
            clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 149 : 148) : 147];
            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[153];
            }
            else
            {
            clothing1.GetSprite = (s) => null;
            clothing3.GetSprite = (s) => null;
            clothing4.GetSprite = (s) => null;
            clothing5.GetSprite = (s) => null;
            clothing6.GetSprite = (s) => null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            if (actor.Unit.BodyAccentType2 == 0)
            {
                clothing5.GetSprite = null;
                clothing6.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class LeaderArmor2 : MainClothing
    {
        public LeaderArmor2()
        {
            blocksBreasts = true;
            blocksDick = false;
            leaderOnly = true;
            Type = 86040;
            clothing1 = new SpriteExtraInfo(21, null, null); //Armor
            clothing2 = new SpriteExtraInfo(22, null, null); //Emblem
            clothing3 = new SpriteExtraInfo(6, null, null); //LegArmor
            clothing4 = new SpriteExtraInfo(24, null, WhiteColored); //'Uncolored part'
            clothing5 = new SpriteExtraInfo(25, null, null); //Rust
            clothing6 = new SpriteExtraInfo(7, null, null); //LegRust
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Umbreon.slotBA1 == true || Races.Umbreon.slotBA2 == true || Races.Umbreon.slotBA3 == true)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 125 : 124) : 123];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[126];
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[127];
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 139 : 138) : 137];
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[actor.IsAttacking ? (actor.Unit.Furry ? 152 : 151) : 150];
                clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.Umbreon[153];
            }
            else
            {
                clothing1.GetSprite = (s) => null;
                clothing2.GetSprite = (s) => null;
                clothing3.GetSprite = (s) => null;
                clothing4.GetSprite = (s) => null;
                clothing5.GetSprite = (s) => null;
                clothing6.GetSprite = (s) => null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, actor.Unit.AccessoryColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonArmor, actor.Unit.ExtraColor1);
            if (actor.Unit.BodyAccentType2 == 0)
            {
                clothing5.GetSprite = null;
                clothing6.GetSprite = null;
            }
            if (actor.PredatorComponent?.LeftBreastFullness > 0 || actor.PredatorComponent?.RightBreastFullness > 0)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing5.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }
}

