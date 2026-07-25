using MermenClothing;
using System.Collections.Generic;
using UnityEngine;

class Mermen : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Mermen;
    readonly MermenLeader LeaderClothes;
    readonly MermenRags Rags;

    public Mermen()
    {
        BodySizes = 4;
        EyeTypes = 8;
        HairStyles = 12;
        SpecialAccessoryCount = 12; //ears
        MouthTypes = 8;
        AccessoryColors = 0;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin); // fish parts colors
        BodyAccentTypes2 = 12; // tail fins
        BodyAccentTypes3 = 7; // arm fins
        BodyAccentTypes4 = 4; // eyebrows

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(8, HeadSprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(4, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.ExtraColor1)); // ears
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.ExtraColor1)); // fish tail
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.ExtraColor1)); //tail fins
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.ExtraColor1)); // arm fins
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, s.Unit.HairColor)); // eyebrows
        BodyAccent5 = new SpriteExtraInfo(2, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor)); // arms
        Mouth = new SpriteExtraInfo(5, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(7, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, s.Unit.HairColor));
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor));

        LeaderClothes = new MermenLeader();
        Rags = new MermenRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new MermenTop1(),
            new MermenTop2(),
            new MermenTop3(),
            new MermenBodySuit(),
            new MermenArmour(),
            Rags,
            LeaderClothes
        };
        AvoidedMainClothingTypes = 2;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new MermenFishTailLink1(),
            new MermenFishTailLink2(),
            new MermenFishTailLink3(),
            new MermenLoincloth(),
            new MermenBot()
        };
        AllowedClothingAccessoryTypes = new List<ClothingAccessory>()
        {
            new MermenShell1(),
            new MermenTiara(),
            new MermenStarfish(),
            new MermenShell2(),
            new MermenHairpin(),
            new MermenNecklace1(),
            new MermenNecklace2(),
            new MermenNecklace3(),
            new MermenNecklace4(),
            new MermenNecklace5(),
            new MermenNecklace6(),
            new MermenNecklace7(),
            new MermenNecklace8()
        };
        clothingColors = 0;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == -1) //Covers rags not in the list
                unit.ClothingType = 1;
        }
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(6);
            else
                unit.HairStyle = State.Rand.Next(HairStyles);
        }
        else if (unit.HasDick && Config.FemaleHairForMales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else if (unit.HasDick == false && Config.MaleHairForFemales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else
        {
            if (unit.HasDick)
            {
                unit.HairStyle = 6 + State.Rand.Next(6);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(6);
            }
        }

        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);

        if (State.Rand.Next(2) == 0)
            unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3 - 1);
        else
        {
            unit.BodyAccentType3 = (BodyAccentTypes3 - 1);
        }
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.Predator && actor.GetStomachSize(16) > 5)
        {
            AddOffset(Belly, 0, 13);
            AddOffset(Dick, 0, 7);
            AddOffset(Balls, 0, 7);
        }
        else if (actor.Unit.Predator && actor.GetStomachSize(16) > 3)
        {
            AddOffset(Belly, 0, 12);
            AddOffset(Dick, 0, 7);
            AddOffset(Balls, 0, 7);
        }
        else if (actor.Unit.Predator && actor.GetStomachSize(16) > 2)
        {
            AddOffset(Belly, 0, 11);
            AddOffset(Dick, 0, 7);
            AddOffset(Balls, 0, 7);
        }
        else
        {
            AddOffset(Belly, 0, 10);
            AddOffset(Dick, 0, 7);
            AddOffset(Balls, 0, 7);
        }
    }

    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[4 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[20];
        if (actor.IsAttacking)
            return Sprites[21];
        return null;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[72 + actor.Unit.SpecialAccessoryType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // fish tail
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[12 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[16 + actor.Unit.BodySize];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[60 + actor.Unit.BodyAccentType2]; // tail fins

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // arm fins
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[32 + (actor.IsAttacking ? 1 : 0) + (4 * actor.Unit.BodyAccentType3)];
        }
        else
        {
            return Sprites[34 + (actor.IsAttacking ? 1 : 0) + (4 * actor.Unit.BodyAccentType3)];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[116 + actor.Unit.BodyAccentType4]; // eyebrows

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // arms
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[8 + (actor.IsAttacking ? 1 : 0)];
        }
        else
        {
            return Sprites[10 + (actor.IsAttacking ? 1 : 0)];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[22];
        if (actor.IsAttacking)
            return Sprites[23];
        return Sprites[24 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[84 + actor.Unit.HairStyle];

    protected override Sprite HairSprite2(Actor_Unit actor) => Sprites[96 + actor.Unit.HairStyle];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[108 + actor.Unit.EyeType];

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.IsAttacking)
            {
                Weapon.layer = 20;
                return Sprites[128 + actor.GetWeaponSprite()];
            }
            else
            {
                Weapon.layer = 3;
                return Sprites[128 + actor.GetWeaponSprite()];
            }

        }
        else
        {
            return null;
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor) => actor.Unit.HasBreasts ? Sprites[120 + actor.Unit.BreastSize] : null;

}

namespace MermenClothing
{

    class MermenTop1 : MainClothing
    {
        public MermenTop1()
        {
            DiscardSprite = SpriteDictionary.Mermen2[137];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 680;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[24 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }

    }

    class MermenTop2 : MainClothing
    {
        public MermenTop2()
        {
            DiscardSprite = SpriteDictionary.Mermen2[138];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 681;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[40 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }

    }

    class MermenTop3 : MainClothing
    {
        public MermenTop3()
        {
            DiscardSprite = SpriteDictionary.Mermen2[140];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 682;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[52 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }

    }

    class MermenBodySuit : MainClothing
    {
        public MermenBodySuit()
        {
            DiscardSprite = null;
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(11, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[60 + actor.Unit.BreastSize];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, actor.Unit.ExtraColor1);
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[68 + actor.Unit.BodySize];
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, actor.Unit.ExtraColor1);
            }
            else
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[132 + actor.Unit.BodySize];
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, actor.Unit.ExtraColor1);
            }
            base.Configure(sprite, actor);
        }

    }

    class MermenArmour : MainClothing
    {
        public MermenArmour()
        {
            DiscardSprite = SpriteDictionary.Mermen2[142];
            coversBreasts = false;
            OccupiesAllSlots = true;
            blocksDick = false;
            inFrontOfDick = 2;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
            Type = 683;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[98 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[90 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[106];
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[94 + actor.Unit.BodySize];
            }
            base.Configure(sprite, actor);
        }

    }

    class MermenRags : MainClothing
    {
        public MermenRags()
        {
            DiscardSprite = SpriteDictionary.Mermen2[143];
            coversBreasts = false;
            OccupiesAllSlots = true;
            blocksDick = false;
            inFrontOfDick = 2;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
            Type = 685;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[129];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[130];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[131];

                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[120 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[128];
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[124 + actor.Unit.BodySize];
            }
            base.Configure(sprite, actor);
        }
    }

    class MermenLeader : MainClothing
    {
        public MermenLeader()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.Mermen2[141];
            coversBreasts = false;
            OccupiesAllSlots = true;
            blocksDick = false;
            inFrontOfDick = 2;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(10, null, WhiteColored);
            Type = 686;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[80 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[72 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[89];
                clothing2.GetSprite = (s) => SpriteDictionary.Mermen2[76 + actor.Unit.BodySize];
            }
            clothing3.GetSprite = (s) => SpriteDictionary.Mermen2[88];
            base.Configure(sprite, actor);
        }
    }

    class MermenFishTailLink1 : MainClothing
    {
        public MermenFishTailLink1()
        {
            clothing1 = new SpriteExtraInfo(10, null, null);
            coversBreasts = false;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[0 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[4 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, actor.Unit.ExtraColor1);
            base.Configure(sprite, actor);
        }
    }

    class MermenFishTailLink2 : MainClothing
    {
        public MermenFishTailLink2()
        {
            clothing1 = new SpriteExtraInfo(10, null, null);
            coversBreasts = false;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[8 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[12 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, actor.Unit.ExtraColor1);
            base.Configure(sprite, actor);
        }
    }

    class MermenFishTailLink3 : MainClothing
    {
        public MermenFishTailLink3()
        {
            clothing1 = new SpriteExtraInfo(10, null, null);
            coversBreasts = false;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[32 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[36 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, actor.Unit.ExtraColor1);
            base.Configure(sprite, actor);
        }
    }

    class MermenLoincloth : MainClothing
    {
        public MermenLoincloth()
        {
            DiscardSprite = SpriteDictionary.Mermen2[136];
            coversBreasts = false;
            blocksDick = false;
            inFrontOfDick = 1;
            clothing1 = new SpriteExtraInfo(11, null, WhiteColored);
            Type = 687;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[16 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[20 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class MermenBot : MainClothing
    {
        public MermenBot()
        {
            DiscardSprite = SpriteDictionary.Mermen2[139];
            femaleOnly = true;
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(11, null, WhiteColored);
            Type = 688;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[48 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = null;
            }

            base.Configure(sprite, actor);
        }
    }

    class MermenShell1 : ClothingAccessory
    {
        public MermenShell1()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[107];
            base.Configure(sprite, actor);
        }
    }

    class MermenTiara : ClothingAccessory
    {
        public MermenTiara()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[108];
            base.Configure(sprite, actor);
        }
    }

    class MermenStarfish : ClothingAccessory
    {
        public MermenStarfish()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[109];
            base.Configure(sprite, actor);
        }
    }

    class MermenShell2 : ClothingAccessory
    {
        public MermenShell2()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[110];
            base.Configure(sprite, actor);
        }
    }

    class MermenHairpin : ClothingAccessory
    {
        public MermenHairpin()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[111];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace1 : ClothingAccessory
    {
        public MermenNecklace1()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[112];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace2 : ClothingAccessory
    {
        public MermenNecklace2()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[113];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace3 : ClothingAccessory
    {
        public MermenNecklace3()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[114];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace4 : ClothingAccessory
    {
        public MermenNecklace4()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[115];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace5 : ClothingAccessory
    {
        public MermenNecklace5()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[116];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace6 : ClothingAccessory
    {
        public MermenNecklace6()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[117];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace7 : ClothingAccessory
    {
        public MermenNecklace7()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[118];
            base.Configure(sprite, actor);
        }
    }

    class MermenNecklace8 : ClothingAccessory
    {
        public MermenNecklace8()
        {
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Mermen2[119];
            base.Configure(sprite, actor);
        }
    }








}