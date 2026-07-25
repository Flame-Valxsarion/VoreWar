using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Salix : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Salix;
    readonly Sprite[] Sprites2 = SpriteDictionary.SalixVore;
    readonly Sprite[] Sprites3 = SpriteDictionary.SalixGen;

    bool oversize = false;

    internal List<MainClothing> AllClothing;

    const float stomachMult = 1f;

    internal Salix()
    {
        //CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite};

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
        BodySizes = 3;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        clothingColors = 1;

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new SalixTop(),
        };

        AllowedWaistTypes = new List<MainClothing>() //Bottoms
        {
            new GenericBottom(26, 26, 0, 63, 13, Sprites, 1300),
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();

        ExtraMainClothing1Types = new List<MainClothing>() //Over
        {
            new Cloak(true),
            new Cloak(false),

        };

        ExtraMainClothing2Types = new List<MainClothing>() //Shoes
        {
            new SalixShoes(),
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
        BodyAccessory = new SpriteExtraInfo(6, AccessorySprite, WhiteColored);
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
        Hair = new SpriteExtraInfo(5, HairSprite, WhiteColored);
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
        Dick = new SpriteExtraInfo(4, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(3, BallsSprite, WhiteColored);
    }
    internal override int BreastSizes => 8;
    internal override int DickSizes => 6;

    internal override void RunFirst(Actor_Unit actor)
    {
        oversize = false;
        base.RunFirst(actor);
    }



    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        return Sprites[18];
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
                return Sprites2[106];

            return Sprites2[70 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => null;

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => null;

    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int weightMod = actor.Unit.BodySize * 4;
        if (actor.IsAttacking)
            return Sprites[3 + weightMod];
        return Sprites[2 + weightMod];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.BreastSize >= 0)
        {
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[16];
            if (actor.Unit.IsDead && actor.Unit.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
                return Sprites[17];
            return Sprites[15];
        }
        else
        {
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[13];
            if (actor.Unit.IsDead && actor.Unit.Items != null)
                return Sprites[14];
            return Sprites[12];
        }
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
            if (actor.SquishedBreasts && actor.Unit.BreastSize < 7 && actor.Unit.BreastSize >= 4)
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
            if (actor.SquishedBreasts && actor.Unit.BreastSize < 7 && actor.Unit.BreastSize >= 4)
                return Sprites2[66 + actor.Unit.BreastSize - 3];
            return Sprites2[35 + actor.Unit.BreastSize];
        }

    }
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 20;
                return Sprites3[1 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
            else
            {
                Dick.layer = 13;
                return Sprites3[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
        }

        Dick.layer = 11;
        return Sprites3[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
    }
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, 0.8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites3[83];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites3[82];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites3[81];
        }
        else if (offset >= 17)
        {
            AddOffset(Balls, 0, -22 * .625f);
        }
        else if (offset == 16)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 15)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 14)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 13)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 12)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 11)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 10)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 9)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }


        if (offset > 0)
            return Sprites3[Math.Min(62 + offset, 80)];
        return Sprites3[48 + size];
    }
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites[20];
    }
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites[21];
    }
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[23];
        return Sprites[22];
    }
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        return Sprites[19];
    }


    class SalixTop : MainClothing
    {
        public SalixTop()
        {
            DiscardSprite = SpriteDictionary.Salix[64];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            Type = 1301;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Salix.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.BreastSize < 2)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Salix[29];
            }
            else if (actor.Unit.HasBreasts)
            {
                actor.SquishedBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.Salix[29 + actor.Unit.BreastSize-1];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Salix[29];
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
            blocksDick = false;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => sheet[sprF + actor.Unit.BodySize];
            base.Configure(sprite, actor);
        }


    }

    class Cloak : MainClothing
    {
        bool Whole = false;
        public Cloak(bool whole)
        {
            Whole = whole;
            clothing1 = new SpriteExtraInfo(20, null, WhiteColored); //Shirt
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored); //Sleeves
            clothing3 = new SpriteExtraInfo(3, null, WhiteColored); //BackCloak 
            blocksDick = false;
            coversBreasts = false;
            DiscardSprite = SpriteDictionary.Salix[65 + (whole ? 1 : 0)];
            DiscardUsesPalettes = false;
            Type = 1302;
            OccupiesAllSlots = true;
            clothing1.YOffset = 0 * .625f;
            clothing2.YOffset = 0 * .625f;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            actor.SquishedBreasts = true;
            int mod = actor.Unit.BreastSize + (actor.Unit.HasBreasts ? 0 : 1);
            if (Whole) // Full cloak sleeves
            {
                if (Races.Salix.oversize) // Cloak Shirt
                    clothing1.GetSprite = (s) => SpriteDictionary.Salix[59];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Salix[52 + mod];

                if (actor.IsAttacking)
                    clothing2.GetSprite = (s) => SpriteDictionary.Salix[51];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Salix[50];
                clothing3.GetSprite = (s) => SpriteDictionary.Salix[60 + actor.Unit.BodySize];
            }
            else // Shoulderless sleeves
            {
                clothing1.GetSprite = null;
                if (Races.Salix.oversize)
                    mod = 8;
                int sleeveMod = (mod - 5) * 4;
                if (0 > sleeveMod)
                    sleeveMod = 0;
                if (actor.IsAttacking)
                    clothing2.GetSprite = (s) => SpriteDictionary.Salix[39 + sleeveMod];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Salix[38 + sleeveMod];
                clothing3.GetSprite = null;

            }
            base.Configure(sprite, actor);
        }
    }

    class SalixShoes : MainClothing
    {
        public SalixShoes()
        {
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(3, null, WhiteColored);
            Type = 9764;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodySize >= 2)
                clothing1.GetSprite = (s) => SpriteDictionary.Salix[25];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.Salix[24];

            base.Configure(sprite, actor);


        }


    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.ClothingType = 1;
        unit.ClothingType2 = 1;
        
        unit.Name = "Salix";
    }
}
