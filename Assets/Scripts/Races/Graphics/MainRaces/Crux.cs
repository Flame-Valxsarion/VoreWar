using CruxClothing;
using System.Collections.Generic;
using UnityEngine;


class Crux : DefaultRaceData
{
    RaceFrameList frameListDrool = new RaceFrameList(new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, new float[9] { .8f, .6f, .5f, .4f, .4f, .4f, .4f, .4f, .4f });
    RaceFrameList frameListWet = new RaceFrameList(new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new float[10] { .4f, .8f, .8f, .7f, .6f, .5f, .4f, .4f, .4f, .4f });

    public Crux()
    {
        ExtraColors1 = 7; // Main colour
        ExtraColors2 = 7; // Secondary colour
        ExtraColors3 = 8; // Flesh colour
        ExtraColors4 = ColorMap.SlimeColorCount;
        EyeColors = ColorMap.EyeColorCount;

        SkinColors = 0;
        HairColors = 0;
        AccessoryColors = 0;
        MouthTypes = 0;

        WeightGainDisabled = true;

        clothingColors = ColorMap.ClothingColorCount;

        GentleAnimation = false;

        Hair3 = new SpriteExtraInfo(0, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Tail.
        // 0 doubles as the layer for the lower necklace layer.
        // Secondary shirt layer is 1.
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Main body.
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // A coconut.
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Leg stripes.
        Balls = new SpriteExtraInfo(4, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Two coconuts.
        Eyes = new SpriteExtraInfo(5, EyesSprite, (s) => ColorMap.GetEyeColor(s.Unit.EyeColor)); // The whole expression.
        // Dick under belly/vulva layer is 6.
        Hair2 = new SpriteExtraInfo(7, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Ears.
        BreastShadow = new SpriteExtraInfo(8, BreastsShadowSprite, WhiteColored); // Wetness animation.
        // Pants are layer 9.
        BodyAccent = new SpriteExtraInfo(9, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Arms.
        BodyAccent3 = new SpriteExtraInfo(10, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Arm stripes.
        Belly = new SpriteExtraInfo(12, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Belly.
        Breasts = new SpriteExtraInfo(13, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Two melons.
        // Necklace upper layer is 13.
        // Shirt layer 14.
        Beard = new SpriteExtraInfo(15, BeardSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Neck/Cheek fur.
        BodyAccent5 = new SpriteExtraInfo(16, BodyAccentSprite5, WhiteColored); // Drool animation.
        Dick = new SpriteExtraInfo(17, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // A cucumber.
        SecondaryEyes = new SpriteExtraInfo(18, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Hands.
        Mouth = new SpriteExtraInfo(19, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Open mouth.
        BodyAccent4 = new SpriteExtraInfo(20, BodyAccentSprite4, WhiteColored); // Teeth.
        BodySize = new SpriteExtraInfo(21, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Hand stripes.
        Hair = new SpriteExtraInfo(22, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, (s.Unit.ExtraColor1 * 56) + (s.Unit.ExtraColor2 * 8) + s.Unit.ExtraColor3)); // Hair.
        BodyAccessory = new SpriteExtraInfo(23, AccessorySprite, WhiteColored); // First item.
        SecondaryAccessory = new SpriteExtraInfo(24, SecondaryAccessorySprite, WhiteColored); // Second item.
        Weapon = new SpriteExtraInfo(25, WeaponSprite, WhiteColored); // A weapon.        

        BodySizes = 4;
        HairStyles = 14;

        HeadTypes = 4;
        TailTypes = 16;
        FurTypes = 11;
        EarTypes = 17;
        BodyAccentTypes1 = 2; // Used for checking if breasts have a visible areola or not.
        BodyAccentTypes2 = 7; // Leg Stripe patterns.
        BodyAccentTypes3 = 5; // Arm Stripe patterns.
        BallsSizes = 3;
        VulvaTypes = 3;
        BasicMeleeWeaponTypes = 2;
        AdvancedMeleeWeaponTypes = 2;
        BasicRangedWeaponTypes = 2;
        AdvancedRangedWeaponTypes = 2;

        BackWeapon = null;

        AvoidedMainClothingTypes = 2;
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            CruxClothingTypes.TShirt,
            CruxClothingTypes.NetShirt,
            CruxClothingTypes.RaggedBra,
            CruxClothingTypes.LabCoat,
            CruxClothingTypes.Rags,
            CruxClothingTypes.SlaveCollar

        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            CruxClothingTypes.CruxJeans,
            CruxClothingTypes.Boxers1,
            CruxClothingTypes.Boxers2,
            CruxClothingTypes.FannyBag,
            CruxClothingTypes.BeltBags
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
        };
        AllowedClothingAccessoryTypes = new List<ClothingAccessory>()
        {
            CruxClothingTypes.NecklaceGold,
            CruxClothingTypes.NecklaceCrux
        };
    }

    internal override int DickSizes => 9;
    internal override int BreastSizes => 7;

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        { new AnimationController.FrameList(), // Drool controller. Index 0.
          new AnimationController.FrameList()}; // Wetness controller. Index 1.
    }

    // These check which types of a body and head the crux has, then return the appropriate sprite.
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return SpriteDictionary.Crux[actor.Unit.BodySize];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // First item spriting.
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 1)
            return null;
        if (actor.Unit.Items[0] is Accessory)
            acc = (Accessory)actor.Unit.Items[0];
        if (acc == null)
            return null;
        BodyAccessory.layer = 23;

        if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
        {
            if (actor.Unit.EarType >= 7)
                return SpriteDictionary.Crux[374];
            return SpriteDictionary.Crux[373];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
        {
            BodyAccessory.layer = 21;
            return SpriteDictionary.Crux[375];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
        {
            BodyAccessory.layer = 11;
            return SpriteDictionary.Crux[386];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gloves))
        {
            if (actor.Unit.HasWeapon == false || actor.Surrendered)
                return SpriteDictionary.Crux[376];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Crux[377];
                case 1:
                    return SpriteDictionary.Crux[379];
                case 2:
                    return SpriteDictionary.Crux[377];
                case 3:
                    return SpriteDictionary.Crux[379];
                case 4:
                    return SpriteDictionary.Crux[377];
                case 5:
                    return SpriteDictionary.Crux[380];
                case 6:
                    return SpriteDictionary.Crux[378];
                case 7:
                    return SpriteDictionary.Crux[380];
                default:
                    return SpriteDictionary.Crux[376];
            }
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
        {
            if (actor.Unit.HasWeapon == false || actor.Surrendered)
                return SpriteDictionary.Crux[381];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Crux[382];
                case 1:
                    return SpriteDictionary.Crux[384];
                case 2:
                    return SpriteDictionary.Crux[382];
                case 3:
                    return SpriteDictionary.Crux[384];
                case 4:
                    return SpriteDictionary.Crux[382];
                case 5:
                    return SpriteDictionary.Crux[385];
                case 6:
                    return SpriteDictionary.Crux[383];
                case 7:
                    return SpriteDictionary.Crux[385];
                default:
                    return SpriteDictionary.Crux[381];
            }
        }
        return null;
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 2)
            return null;
        if (actor.Unit.Items[1] is Accessory)
            acc = (Accessory)actor.Unit.Items[1];
        if (acc == null)
            return null;
        BodyAccessory.layer = 24;

        if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
        {
            if (actor.Unit.EarType >= 7)
                return SpriteDictionary.Crux[374];
            return SpriteDictionary.Crux[373];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
        {
            BodyAccessory.layer = 21;
            return SpriteDictionary.Crux[375];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
        {
            BodyAccessory.layer = 11;
            return SpriteDictionary.Crux[386];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gloves))
        {
            if (actor.Unit.HasWeapon == false || actor.Surrendered)
                return SpriteDictionary.Crux[376];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Crux[377];
                case 1:
                    return SpriteDictionary.Crux[379];
                case 2:
                    return SpriteDictionary.Crux[377];
                case 3:
                    return SpriteDictionary.Crux[379];
                case 4:
                    return SpriteDictionary.Crux[377];
                case 5:
                    return SpriteDictionary.Crux[380];
                case 6:
                    return SpriteDictionary.Crux[378];
                case 7:
                    return SpriteDictionary.Crux[380];
                default:
                    return SpriteDictionary.Crux[376];
            }
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
        {
            if (actor.Unit.HasWeapon == false || actor.Surrendered)
                return SpriteDictionary.Crux[381];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Crux[382];
                case 1:
                    return SpriteDictionary.Crux[384];
                case 2:
                    return SpriteDictionary.Crux[382];
                case 3:
                    return SpriteDictionary.Crux[384];
                case 4:
                    return SpriteDictionary.Crux[382];
                case 5:
                    return SpriteDictionary.Crux[385];
                case 6:
                    return SpriteDictionary.Crux[383];
                case 7:
                    return SpriteDictionary.Crux[385];
                default:
                    return SpriteDictionary.Crux[381];
            }
        }
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.Crux[6 + actor.Unit.HeadType];

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Checks if the crux has a thin body and if so applies thin arms, otherwise applies thick arms, then checks the correct position for them.
    {
        if (actor.Unit.BodySize <= 1)
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                    return SpriteDictionary.Crux[351];
                else return SpriteDictionary.Crux[4];
            }
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Crux[351];
                case 1:
                    return SpriteDictionary.Crux[355];
                case 2:
                    return SpriteDictionary.Crux[351];
                case 3:
                    return SpriteDictionary.Crux[355];
                case 4:
                    return SpriteDictionary.Crux[351];
                case 5:
                    return SpriteDictionary.Crux[357];
                case 6:
                    return SpriteDictionary.Crux[353];
                case 7:
                    return SpriteDictionary.Crux[357];
                default:
                    return null;
            }
        }
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
                return SpriteDictionary.Crux[352];
            else return SpriteDictionary.Crux[5];
        }
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Crux[352];
            case 1:
                return SpriteDictionary.Crux[356];
            case 2:
                return SpriteDictionary.Crux[352];
            case 3:
                return SpriteDictionary.Crux[356];
            case 4:
                return SpriteDictionary.Crux[352];
            case 5:
                return SpriteDictionary.Crux[358];
            case 6:
                return SpriteDictionary.Crux[354];
            case 7:
                return SpriteDictionary.Crux[358];
            default:
                return null;
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize <= 1)
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                    return SpriteDictionary.Crux[401];
                else return null;
            }
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Crux[401];
                case 1:
                    return SpriteDictionary.Crux[405];
                case 2:
                    return SpriteDictionary.Crux[401];
                case 3:
                    return SpriteDictionary.Crux[405];
                case 4:
                    return SpriteDictionary.Crux[401];
                case 5:
                    return SpriteDictionary.Crux[407];
                case 6:
                    return SpriteDictionary.Crux[403];
                case 7:
                    return SpriteDictionary.Crux[407];
                default:
                    return null;
            }
        }
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
                return SpriteDictionary.Crux[402];
            else return null;
        }
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Crux[402];
            case 1:
                return SpriteDictionary.Crux[406];
            case 2:
                return SpriteDictionary.Crux[402];
            case 3:
                return SpriteDictionary.Crux[406];
            case 4:
                return SpriteDictionary.Crux[402];
            case 5:
                return SpriteDictionary.Crux[408];
            case 6:
                return SpriteDictionary.Crux[404];
            case 7:
                return SpriteDictionary.Crux[408];
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Leg stripes.
    {
        if (actor.Unit.BodyAccentType2 == 0) return null;
        return SpriteDictionary.Crux[271 + actor.Unit.BodyAccentType2];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Arm stripes.
    {
        switch (actor.Unit.BodyAccentType3)
        {
            case 0: return null;
            case 1:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[278];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[278];
                        case 1:
                            return SpriteDictionary.Crux[283];
                        case 2:
                            return SpriteDictionary.Crux[278];
                        case 3:
                            return SpriteDictionary.Crux[283];
                        case 4:
                            return SpriteDictionary.Crux[278];
                        case 5:
                            return SpriteDictionary.Crux[284];
                        case 6:
                            return SpriteDictionary.Crux[278];
                        case 7:
                            return SpriteDictionary.Crux[284];
                        default:
                            return SpriteDictionary.Crux[278];
                    }
                }
            case 2:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[279];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[279];
                        case 1:
                            return SpriteDictionary.Crux[285];
                        case 2:
                            return SpriteDictionary.Crux[279];
                        case 3:
                            return SpriteDictionary.Crux[285];
                        case 4:
                            return SpriteDictionary.Crux[279];
                        case 5:
                            return SpriteDictionary.Crux[286];
                        case 6:
                            return SpriteDictionary.Crux[378];
                        case 7:
                            return SpriteDictionary.Crux[286];
                        default:
                            return SpriteDictionary.Crux[279];
                    }
                }
            case 3:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[280];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[282];
                        case 1:
                            return SpriteDictionary.Crux[287];
                        case 2:
                            return SpriteDictionary.Crux[282];
                        case 3:
                            return SpriteDictionary.Crux[287];
                        case 4:
                            return SpriteDictionary.Crux[282];
                        case 5:
                            return SpriteDictionary.Crux[288];
                        case 6:
                            return SpriteDictionary.Crux[282];
                        case 7:
                            return SpriteDictionary.Crux[288];
                        default:
                            return SpriteDictionary.Crux[280];
                    }
                }
            case 4:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[281];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[281];
                        case 1:
                            return SpriteDictionary.Crux[289];
                        case 2:
                            return SpriteDictionary.Crux[281];
                        case 3:
                            return SpriteDictionary.Crux[289];
                        case 4:
                            return SpriteDictionary.Crux[281];
                        case 5:
                            return SpriteDictionary.Crux[290];
                        case 6:
                            return SpriteDictionary.Crux[281];
                        case 7:
                            return SpriteDictionary.Crux[290];
                        default:
                            return SpriteDictionary.Crux[281];
                    }
                }
            default: return null;
        }
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) // Hand stripes.
    {
        switch (actor.Unit.BodyAccentType3)
        {
            case 0: return null;
            case 1:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[409];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[409];
                        case 1:
                            return SpriteDictionary.Crux[414];
                        case 2:
                            return SpriteDictionary.Crux[409];
                        case 3:
                            return SpriteDictionary.Crux[414];
                        case 4:
                            return SpriteDictionary.Crux[409];
                        case 5:
                            return SpriteDictionary.Crux[415];
                        case 6:
                            return SpriteDictionary.Crux[409];
                        case 7:
                            return SpriteDictionary.Crux[284];
                        default:
                            return SpriteDictionary.Crux[409];
                    }
                }
            case 2:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[410];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[410];
                        case 1:
                            return SpriteDictionary.Crux[416];
                        case 2:
                            return SpriteDictionary.Crux[410];
                        case 3:
                            return SpriteDictionary.Crux[416];
                        case 4:
                            return SpriteDictionary.Crux[410];
                        case 5:
                            return SpriteDictionary.Crux[417];
                        case 6:
                            return SpriteDictionary.Crux[410];
                        case 7:
                            return SpriteDictionary.Crux[419];
                        default:
                            return SpriteDictionary.Crux[410];
                    }
                }
            case 3:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[411];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[413];
                        case 1:
                            return SpriteDictionary.Crux[418];
                        case 2:
                            return SpriteDictionary.Crux[413];
                        case 3:
                            return SpriteDictionary.Crux[418];
                        case 4:
                            return SpriteDictionary.Crux[413];
                        case 5:
                            return SpriteDictionary.Crux[419];
                        case 6:
                            return SpriteDictionary.Crux[413];
                        case 7:
                            return SpriteDictionary.Crux[419];
                        default:
                            return SpriteDictionary.Crux[411];
                    }
                }
            case 4:
                {
                    if (actor.Unit.HasWeapon == false || actor.Surrendered)
                        return SpriteDictionary.Crux[412];
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            return SpriteDictionary.Crux[412];
                        case 1:
                            return SpriteDictionary.Crux[420];
                        case 2:
                            return SpriteDictionary.Crux[412];
                        case 3:
                            return SpriteDictionary.Crux[420];
                        case 4:
                            return SpriteDictionary.Crux[412];
                        case 5:
                            return SpriteDictionary.Crux[421];
                        case 6:
                            return SpriteDictionary.Crux[412];
                        case 7:
                            return SpriteDictionary.Crux[421];
                        default:
                            return SpriteDictionary.Crux[412];
                    }
                }
            default: return null;
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.PredatorComponent?.VisibleFullness > 0)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.PredatorComponent.VisibleFullness > 3)
                {
                    if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2) return SpriteDictionary.Crux[194];
                    else return SpriteDictionary.Crux[219];
                }
            }
            if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2) return SpriteDictionary.Crux[170 + actor.GetStomachSize(23)];
            else return SpriteDictionary.Crux[195 + actor.GetStomachSize(23)];
        }
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Crux[30 + actor.Unit.EyeType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Crux[76 + actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return SpriteDictionary.Crux[13 + actor.Unit.EarType];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return SpriteDictionary.Crux[60 + actor.Unit.TailType];
    }

    protected override Sprite BeardSprite(Actor_Unit actor)
    {
        if (actor.Unit.FurType == 0) return null;
        return SpriteDictionary.Crux[49 + actor.Unit.FurType];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
        {
            if (actor.PredatorComponent.BallsFullness > 3)
            {
                if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2) return SpriteDictionary.Crux[245];
                else return SpriteDictionary.Crux[271];
            }

        }

        if (actor.PredatorComponent?.BallsFullness > 0)
            switch (actor.Unit.BodySize)
            {
                case 0: return SpriteDictionary.Crux[231 + actor.GetBallSize(13)];
                case 1: return SpriteDictionary.Crux[257 + actor.GetBallSize(13)];
                case 2: return SpriteDictionary.Crux[231 + actor.GetBallSize(13)];
                case 3: return SpriteDictionary.Crux[257 + actor.GetBallSize(13)];
            }
        switch (actor.Unit.BodySize)
        {
            case 0: return SpriteDictionary.Crux[220 + actor.Unit.BallsSize + actor.Unit.DickSize];
            case 1: return SpriteDictionary.Crux[246 + actor.Unit.BallsSize + actor.Unit.DickSize];
            case 2: return SpriteDictionary.Crux[220 + actor.Unit.BallsSize + actor.Unit.DickSize];
            case 3: return SpriteDictionary.Crux[246 + actor.Unit.BallsSize + actor.Unit.DickSize];
        }
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
        {
            if (actor.Unit.HeadType == 0 || actor.Unit.HeadType == 2)
                return SpriteDictionary.Crux[10];
            return SpriteDictionary.Crux[11];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Crux[12];
        return null;
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.BreastSize >= 0)
        {
            if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2)
            {
                if (actor.Unit.BodyAccentType1 == 0) return SpriteDictionary.Crux[90 + actor.Unit.BreastSize];
                return SpriteDictionary.Crux[97 + actor.Unit.BreastSize];
            }
            if (actor.Unit.BodyAccentType1 == 0) return SpriteDictionary.Crux[104 + actor.Unit.BreastSize];
            return SpriteDictionary.Crux[111 + actor.Unit.BreastSize];
        }
        return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.BasicMeleeWeaponType == 2)
            if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 1)
                Weapon.GetColor = (s) => ColorMap.GetSlimeColor(s.Unit.ExtraColor4);
            else Weapon.GetColor = WhiteColored;
        else Weapon.GetColor = WhiteColored;

        if (actor.Unit.HasWeapon == false || actor.Surrendered)
            return null;
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Crux[359 + actor.Unit.BasicMeleeWeaponType];
            case 1:
                return SpriteDictionary.Crux[362 + actor.Unit.BasicMeleeWeaponType];
            case 2:
                return SpriteDictionary.Crux[365 + actor.Unit.AdvancedMeleeWeaponType];
            case 3:
                return SpriteDictionary.Crux[367 + actor.Unit.AdvancedMeleeWeaponType];
            case 4:
                if (actor.Unit.BasicRangedWeaponType == 1) return SpriteDictionary.Crux[422];
                else return SpriteDictionary.Crux[369];
            case 5:
                if (actor.Unit.BasicRangedWeaponType == 1) return SpriteDictionary.Crux[424];
                else return SpriteDictionary.Crux[371];
            case 6:
                if (actor.Unit.AdvancedRangedWeaponType == 1) return SpriteDictionary.Crux[423];
                else return SpriteDictionary.Crux[370];
            case 7:
                if (actor.Unit.AdvancedRangedWeaponType == 1) return SpriteDictionary.Crux[425];
                else return SpriteDictionary.Crux[372];
            default:
                return null;
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Handles both dicks and vulvas, giving priority to dicks. They are in front, after all.
    {
        if (actor.Unit.DickSize >= 0) // Place a dick. If this is true, no checks for vulva will be made.
        {
            if (actor.IsCockVoring)
            {
                if (actor.GetStomachSize(24) <= 6)
                {
                    Dick.layer = 17;
                    return SpriteDictionary.Crux[168];
                }
                Dick.layer = 6;
                return SpriteDictionary.Crux[169];
            }

            if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2)
            { // For the primary colour belly bodies.
                if (actor.IsErect())
                {
                    if (actor.GetStomachSize(24) <= 6)
                    {
                        Dick.layer = 17;
                        switch (actor.Unit.DickSize)
                        {
                            case 0: return SpriteDictionary.Crux[127];
                            case 1: return SpriteDictionary.Crux[128];
                            case 2: return SpriteDictionary.Crux[129];
                            case 3: return SpriteDictionary.Crux[131];
                            case 4: return SpriteDictionary.Crux[132];
                            case 5: return SpriteDictionary.Crux[133];
                            case 6: return SpriteDictionary.Crux[135];
                            case 7: return SpriteDictionary.Crux[136];
                            case 8: return SpriteDictionary.Crux[137];
                        }
                    }
                    Dick.layer = 6;
                    return SpriteDictionary.Crux[150 + actor.Unit.DickSize];
                }

                Dick.layer = 6;
                switch (actor.Unit.DickSize)
                {
                    case 0: return SpriteDictionary.Crux[126];
                    case 1: return SpriteDictionary.Crux[126];
                    case 2: return SpriteDictionary.Crux[126];
                    case 3: return SpriteDictionary.Crux[130];
                    case 4: return SpriteDictionary.Crux[130];
                    case 5: return SpriteDictionary.Crux[130];
                    case 6: return SpriteDictionary.Crux[134];
                    case 7: return SpriteDictionary.Crux[134];
                    case 8: return SpriteDictionary.Crux[134];
                }
            }
            // For the secondary colour belly bodies.
            if (actor.IsErect())
            {
                if (actor.GetStomachSize(24) <= 6)
                {
                    Dick.layer = 17;
                    switch (actor.Unit.DickSize)
                    {
                        case 0: return SpriteDictionary.Crux[139];
                        case 1: return SpriteDictionary.Crux[140];
                        case 2: return SpriteDictionary.Crux[141];
                        case 3: return SpriteDictionary.Crux[143];
                        case 4: return SpriteDictionary.Crux[144];
                        case 5: return SpriteDictionary.Crux[145];
                        case 6: return SpriteDictionary.Crux[147];
                        case 7: return SpriteDictionary.Crux[148];
                        case 8: return SpriteDictionary.Crux[149];
                    }
                }
                Dick.layer = 6;
                return SpriteDictionary.Crux[159 + actor.Unit.DickSize];
            }

            Dick.layer = 6;
            switch (actor.Unit.DickSize)
            {
                case 0: return SpriteDictionary.Crux[138];
                case 1: return SpriteDictionary.Crux[138];
                case 2: return SpriteDictionary.Crux[138];
                case 3: return SpriteDictionary.Crux[142];
                case 4: return SpriteDictionary.Crux[142];
                case 5: return SpriteDictionary.Crux[142];
                case 6: return SpriteDictionary.Crux[146];
                case 7: return SpriteDictionary.Crux[146];
                case 8: return SpriteDictionary.Crux[146];
            }

            return null;
        }

        if (Config.HideCocks == false) // If genitals aren't hidden, show a vulva. This is only reached if the unit doesn't have a dick, regardless of whether it is hidden or not.
        {
            Dick.layer = 6;

            if (actor.IsUnbirthing)
            {
                if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2)
                    return SpriteDictionary.Crux[124];
                return SpriteDictionary.Crux[125];
            }

            if (actor.Unit.BodySize == 0 || actor.Unit.BodySize == 2)
            {
                switch (actor.Unit.VulvaType)
                {
                    case 0: return SpriteDictionary.Crux[118];
                    case 1: return SpriteDictionary.Crux[120];
                    case 2: return SpriteDictionary.Crux[122];
                }
            }
            switch (actor.Unit.VulvaType)
            {
                case 0: return SpriteDictionary.Crux[119];
                case 1: return SpriteDictionary.Crux[121];
                case 2: return SpriteDictionary.Crux[123];
            }
        }

        return null;
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (!actor.Targetable) return null;

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListDrool.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListDrool.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return SpriteDictionary.Crux[291 + frameListDrool.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite BreastsShadowSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return null;

        if (actor.IsUnbirthing)
        {
            actor.AnimationController.frameLists[1].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentFrame = 0;
            actor.AnimationController.frameLists[1].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListWet.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListWet.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            return SpriteDictionary.Crux[300 + frameListWet.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (actor.Unit.DickSize == -1 && actor.PredatorComponent?.Fullness > 0 && Config.ErectionsFromVore && State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return null;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodySize = State.Rand.Next(0, BodySizes);
        unit.HeadType = State.Rand.Next(0, HeadTypes);
        unit.TailType = State.Rand.Next(0, TailTypes);
        unit.FurType = State.Rand.Next(0, FurTypes);
        unit.EarType = State.Rand.Next(0, EarTypes);
        unit.BallsSize = State.Rand.Next(0, BallsSizes);
        unit.VulvaType = State.Rand.Next(0, VulvaTypes);
        unit.BodyAccentType1 = State.Rand.Next(0, BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(0, BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(0, BodyAccentTypes3);

        unit.BasicMeleeWeaponType = State.Rand.Next(0, BasicMeleeWeaponTypes);
        unit.AdvancedMeleeWeaponType = State.Rand.Next(0, AdvancedMeleeWeaponTypes);
        unit.BasicRangedWeaponType = State.Rand.Next(0, BasicRangedWeaponTypes);
        unit.AdvancedRangedWeaponType = State.Rand.Next(0, AdvancedRangedWeaponTypes);

        if (!Config.HideCocks)
            if (State.Rand.Next(0, 100) < 20)
            {
                unit.BasicMeleeWeaponType = 2;
            }

        unit.ClothingColor = State.Rand.Next(0, clothingColors);
        unit.ClothingColor2 = State.Rand.Next(0, clothingColors);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(CruxClothingTypes.Rags);
            if (unit.ClothingType == -1) //Covers rags not in the list
                unit.ClothingType = 1;
        }
    }
}


