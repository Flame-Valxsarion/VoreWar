using System;
using System.Collections.Generic;
using UnityEngine;

class Auri : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Auri;
    readonly Sprite[] Sprites2 = SpriteDictionary.AuriVore;

    RaceFrameList EarAnimation = new RaceFrameList(new int[3] { 22, 23, 22 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList FaceAnimation = new RaceFrameList(new int[3] { 18, 19, 18 }, new float[3] { .25f, .25f, .25f });

    bool oversize = false;

    internal List<MainClothing> AllClothing;

    const float stomachMult = 1.7f;

    internal Auri()
    {
        CanBeGender = new List<Gender>() { Gender.Female };

        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        ExtendedBreastSprites = true;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 2;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        TailTypes = 2;
        BodyAccentTypes1 = 2;
        clothingColors = 1;

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new AuriTop(),
        };

        AllowedWaistTypes = new List<MainClothing>() //Bottoms
        {
            new GenericBottom(52, 52, 0, 56, 8, SpriteDictionary.Auri, 840),
            new GenericBottom(101, 101, 0, 101, 8, SpriteDictionary.Auri, 841),
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();

        ExtraMainClothing1Types = new List<MainClothing>() //Over
        {
            new Kimono(true),
            new Kimono(false),
            new KimonoHoliday(true),
            new KimonoHoliday(false),

        };

        ExtraMainClothing2Types = new List<MainClothing>() //Stocking
        {
            new Stocking(48, 0, 48, 3, SpriteDictionary.Auri, 901),
        };

        ExtraMainClothing3Types = new List<MainClothing>() //Hat
        {
            new Hat(50, 0, 50, 20, SpriteDictionary.Auri, 903),
        };

        AllClothing = new List<MainClothing>();
        AllClothing.AddRange(AllowedMainClothingTypes);
        AllClothing.AddRange(AllowedWaistTypes);
        AllClothing.AddRange(ExtraMainClothing1Types);
        AllClothing.AddRange(ExtraMainClothing2Types);
        AllClothing.AddRange(ExtraMainClothing3Types);
        AllClothing.AddRange(ExtraMainClothing4Types);
        AllClothing.AddRange(ExtraMainClothing5Types);

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(8, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = new SpriteExtraInfo(6, HairSprite, WhiteColored);
        Hair2 = new SpriteExtraInfo(1, HairSprite2, WhiteColored);
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(0, SecondaryAccessorySprite, WhiteColored);
        Weapon = new SpriteExtraInfo(13, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, WhiteColored);
        SecondaryBreasts = new SpriteExtraInfo(16, SecondaryBreastsSprite, WhiteColored);
        Belly = new SpriteExtraInfo(14, null, WhiteColored);
        BreastShadow = null;
        Dick = null;
        Balls = null;
    }
    internal override int BreastSizes => 7;
    internal override int DickSizes => 1;

    internal override void RunFirst(Actor_Unit actor)
    {
        oversize = false;
        base.RunFirst(actor);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        { new AnimationController.FrameList(),
          new AnimationController.FrameList(), };
        actor.AnimationController.frameLists[0].currentlyActive = false;
        actor.AnimationController.frameLists[1].currentlyActive = false;
    }
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null)
            SetUpAnimations(actor);

        if (State.Rand.Next(650) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        if (actor.AnimationController.frameLists[0].currentlyActive == false)
        {
            return Sprites[21];
        }


        if (actor.AnimationController.frameLists[0].currentTime >= EarAnimation.times[actor.AnimationController.frameLists[0].currentFrame])
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= EarAnimation.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentlyActive = false;
                actor.AnimationController.frameLists[0].currentTime = 0;
                actor.AnimationController.frameLists[0].currentFrame = 0;
            }
        }
        return Sprites[EarAnimation.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(32, stomachMult);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites2[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites2[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites2[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites2[102];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites2[101];
            }
            if (size > 30)
                size = 30;
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
            }

            if (actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 9 && size <= 14)
                return Sprites[105];

            return Sprites2[70 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return null;
        int weightMod = actor.Unit.BodySize * 4;
        if (actor.Unit.BodyAccentType1 == 0)
        {
            if (actor.IsAttacking)
                return Sprites[27 + weightMod];
            return Sprites[24 + weightMod];
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[35 + weightMod];
            return Sprites[32 + weightMod];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return null;
        return Sprites[40 + actor.Unit.BodySize];
    }

    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int weightMod = actor.Unit.BodySize * 4;
        if (actor.Unit.BodyAccentType1 == 0)
        {
            if (actor.IsAttacking)
                return Sprites[3 + weightMod];
            return Sprites[0 + weightMod];
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[11 + weightMod];
            return Sprites[8 + weightMod];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[17];
        if (actor.Unit.IsDead && actor.Unit.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
            return Sprites[20];

        if (actor.AnimationController.frameLists == null)
            SetUpAnimations(actor);

        if (State.Rand.Next(1600) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        if (actor.AnimationController.frameLists[1].currentlyActive == false)
        {
            return Sprites[16];
        }

        if (actor.AnimationController.frameLists[1].currentTime >= EarAnimation.times[actor.AnimationController.frameLists[1].currentFrame])
        {
            actor.AnimationController.frameLists[1].currentFrame++;
            actor.AnimationController.frameLists[1].currentTime = 0f;

            if (actor.AnimationController.frameLists[1].currentFrame >= EarAnimation.frames.Length)
            {
                actor.AnimationController.frameLists[1].currentlyActive = false;
                actor.AnimationController.frameLists[1].currentTime = 0;
                actor.AnimationController.frameLists[1].currentFrame = 0;
            }
        }

        return Sprites[FaceAnimation.frames[actor.AnimationController.frameLists[1].currentFrame]];
    }
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;

        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return Sprites2[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites2[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites2[29];
            }

            if (leftSize > 28)
                leftSize = 28;


            return Sprites2[0 + leftSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return Sprites2[0];
            if (actor.SquishedBreasts && actor.Unit.BreastSize < 6 && actor.Unit.BreastSize >= 4)
                return Sprites2[31 + actor.Unit.BreastSize - 3];
            return Sprites2[0 + actor.Unit.BreastSize];
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
                return Sprites2[66];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites2[65];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites2[64];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites2[35 + rightSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return Sprites2[35];
            if (actor.SquishedBreasts && actor.Unit.BreastSize < 6 && actor.Unit.BreastSize >= 4)
                return Sprites2[66 + actor.Unit.BreastSize - 3];
            return Sprites2[35 + actor.Unit.BreastSize];
        }

    }
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites[42];
    }
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites[43];
    }
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[47];
        return Sprites[46];
    }
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        return Sprites[44 + actor.Unit.TailType];
    }


    class AuriTop : MainClothing
    {
        public AuriTop()
        {
            DiscardSprite = SpriteDictionary.Auri[64];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            Type = 1422;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Aurilika.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Auri[62];
            }
            else if (actor.Unit.HasBreasts)
            {
                actor.SquishedBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.Auri[56 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Auri[56];
            }


            base.Configure(sprite, actor);
        }

    }


    class GenericBottom : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        Sprite[] sheet;
        public GenericBottom(int femaleSprite, int maleSprite, int bulge, int discard, int layer, Sprite[] sheet, int type)
        {
            coversBreasts = false;
            blocksDick = true;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasDick)
                clothing1.GetSprite = (s) => sheet[sprM];
            else
                clothing1.GetSprite = (s) => sheet[sprF + actor.Unit.BodySize];
            if (actor.Unit.HasDick && bulge > 0)
            {
                if (actor.Unit.DickSize > 2)
                    clothing2.GetSprite = (s) => sheet[bulge + 1];
                else
                    clothing2.GetSprite = (s) => sheet[bulge + 1];
            }
            else
                clothing2.GetSprite = null;
            base.Configure(sprite, actor);


        }


    }

    class Kimono : MainClothing
    {
        bool Skirt = false;
        public Kimono(bool skirt)
        {
            Skirt = skirt;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(20, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(11, null, WhiteColored);
            blocksDick = false;
            coversBreasts = false;
            DiscardSprite = SpriteDictionary.Auri[95];
            DiscardUsesPalettes = true;
            Type = 444;
            OccupiesAllSlots = true;
            clothing1.YOffset = 0 * .625f;
            clothing2.YOffset = 0 * .625f;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            actor.SquishedBreasts = true;
            if (Skirt)
            {
                int skirtMod = 0;
                if (actor.Unit.BodySize > 0 || actor.Unit.BodyAccentType1 == 1)
                    skirtMod = 26;
                if (actor.IsUnbirthing || actor.IsAnalVoring)
                    clothing1.GetSprite = (s) => SpriteDictionary.Auri[86 + skirtMod];
                else
                {
                    if (actor.GetStomachSize(32, stomachMult) < 8)
                        clothing1.GetSprite = (s) => SpriteDictionary.Auri[80 + skirtMod + actor.GetStomachSize(32, stomachMult)];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.Auri[88];
                }
            }
            else
            {
                clothing1.GetSprite = null;
            }
            int kimMod = Skirt ? 0 : 7;
            if (Races.Aurilika.oversize)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Auri[93 + kimMod];
            }
            else if (actor.Unit.BreastSize < 3)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Auri[89 + kimMod];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Auri[89 + kimMod + actor.Unit.BreastSize - 2];
            }
            int mod = actor.Unit.BodySize * 4;
            if (mod > 4)
                mod = 4;
            if (actor.Unit.BodyAccentType1 == 1)
                mod += 8;
            if (actor.IsAttacking)
                clothing3.GetSprite = (s) => SpriteDictionary.Auri[67 + mod];
            else
                clothing3.GetSprite = (s) => SpriteDictionary.Auri[64 + mod];
            base.Configure(sprite, actor);
        }
    }

    class KimonoHoliday : MainClothing
    {
        bool Skirt = false;
        public KimonoHoliday(bool skirt)
        {
            Skirt = skirt;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(20, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(11, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(0, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(15, null, WhiteColored);
            blocksDick = false;
            coversBreasts = false;
            DiscardSprite = SpriteDictionary.Auri[95];
            DiscardUsesPalettes = true;
            Type = 444;
            ReqWinterHoliday = true;
            OccupiesAllSlots = true;
            clothing1.YOffset = 0 * .625f;
            clothing2.YOffset = 0 * .625f;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            actor.SquishedBreasts = true;
            if (Skirt)
            {
                int skirtMod = 0;
                if (actor.Unit.BodySize > 0 || actor.Unit.BodyAccentType1 == 1)
                    skirtMod = 2;
                if (actor.IsUnbirthing || actor.IsAnalVoring)
                    clothing1.GetSprite = (s) => SpriteDictionary.AuriHoliday[23 + skirtMod];
                else
                {
                    if (actor.GetStomachSize(32, stomachMult) < 4 && actor.Unit.BodyAccentType1 == 0)
                        clothing1.GetSprite = (s) => SpriteDictionary.AuriHoliday[22 + skirtMod];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.AuriHoliday[26 + skirtMod];
                }
            }
            else
            {
                clothing1.GetSprite = null;
            }
            if (Races.Aurilika.oversize)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.AuriHoliday[20];
            }
            else if (actor.Unit.BreastSize < 3)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.AuriHoliday[16];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.AuriHoliday[16 + actor.Unit.BreastSize - 2];
            }
            int mod = actor.Unit.BodySize * 4;
            if (mod > 4)
                mod = 4;
            if (actor.Unit.BodyAccentType1 == 1)
                mod += 8;
            if (actor.IsAttacking)
                clothing3.GetSprite = (s) => SpriteDictionary.AuriHoliday[3 + mod];
            else
                clothing3.GetSprite = (s) => SpriteDictionary.AuriHoliday[0 + mod];
            clothing4.GetSprite = (s) => SpriteDictionary.AuriHoliday[21];
            if (actor.GetStomachSize(32, stomachMult) >= 4)
                clothing5.GetSprite = (s) => SpriteDictionary.AuriHoliday[32];
            else
                clothing5.GetSprite = null;
            base.Configure(sprite, actor);
        }
    }
    class Stocking : MainClothing
    {
        int sprM;
        int sprF;
        Sprite[] sheet = SpriteDictionary.Auri;

        public Stocking(int femaleSprite, int maleSprite, int discard, int layer, Sprite[] sheet, int type)
        {
            coversBreasts = false;
            blocksDick = false;
            this.sheet = sheet;
            sprM = maleSprite;
            sprF = femaleSprite;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 == 1)
                clothing1.GetSprite = null;
            else if (actor.Unit.HasBreasts)
                clothing1.GetSprite = (s) => sheet[sprF + actor.Unit.BodySize];
            else
                clothing1.GetSprite = (s) => sheet[sprM];
            base.Configure(sprite, actor);
        }
    }
    class Hat : MainClothing
    {
        int sprM;
        int sprF;
        Sprite[] sheet = SpriteDictionary.PantherHats;

        public Hat(int femaleSprite, int maleSprite, int discard, int layer, Sprite[] sheet, int type)
        {
            coversBreasts = false;
            blocksDick = false;
            this.sheet = sheet;
            sprM = maleSprite;
            sprF = femaleSprite;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
                clothing1.GetSprite = (s) => sheet[sprF];
            else
                clothing1.GetSprite = (s) => sheet[sprM];
            base.Configure(sprite, actor);
        }
    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Auri";
        unit.SetDefaultBreastSize(4, true);
        unit.BodySize = 0;
        unit.BodyAccentType1 = 0;
        unit.ClothingExtraType1 = 1;
        unit.TailType = 0;
        if (Config.WinterActive())
            unit.ClothingExtraType1 = 3;
    }

}

