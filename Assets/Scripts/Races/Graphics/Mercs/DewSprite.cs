using System.Collections.Generic;
using UnityEngine;

class DewSprite : BlankSlate
{
    Sprite[] Sprites = SpriteDictionary.DewSprite;
    public DewSprite()
    {
        BodySizes = 3;
        EyeTypes = 6;
        MouthTypes = 6;
        HairStyles = 3;
        CanBeGender = new List<Gender>() { Gender.Female };
        Body = new SpriteExtraInfo(4, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(11, BodyAccentSprite, WhiteColored);

        Hair = new SpriteExtraInfo(8, HairSprite, WhiteColored);
        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored);
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(7, EyesSprite, WhiteColored);
        Belly = new SpriteExtraInfo(15, null, WhiteColored);
        Weapon = new SpriteExtraInfo(10, WeaponSprite, WhiteColored);
        Breasts = new SpriteExtraInfo(16, BreastsSprite, WhiteColored);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new Top()
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            new Bottom1(),
            new Bottom2(),
            new Bottom3(),
            new Bottom4(),
        };
    }
    internal override int BreastSizes => 9;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            belly.transform.localScale = new Vector3(1, 1, 1);
            return Sprites[0 + actor.GetStomachSize(11)];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[12 + actor.Unit.BreastSize];
        }
        return null;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize == 0)
            return Sprites[52];
        else if (actor.Unit.BodySize == 1)
            return Sprites[21];
        else
            return Sprites[57];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        int sprite = actor.GetWeaponSprite();
        switch (sprite)
        {
            case 0:
                return Sprites[44];
            case 1:
                return Sprites[51];
            case 2:
                return Sprites[40];
            case 3:
                return Sprites[39];
            case 4:
            case 5:
                return Sprites[42];
            case 6:
            case 7:
                return Sprites[38];
        }
        return Sprites[51];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        int sprite = actor.GetWeaponSprite();
        switch (sprite)
        {
            case 1:
                return Sprites[43];
            case 5:
                return Sprites[41];
            case 7:
                return Sprites[37];
        }
        return null;
    }



    protected override Sprite HeadSprite(Actor_Unit actor) => Sprites[36];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[27 + actor.Unit.EyeType];
    protected override Sprite MouthSprite(Actor_Unit actor) => Sprites[45 + actor.Unit.MouthType];

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[33 + actor.Unit.HairStyle];


    class Bottom1 : MainClothing
    {
        public Bottom1()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodySize == 0)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[53];
            else if (actor.Unit.BodySize == 1)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[22];
            else
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[58];
            base.Configure(sprite, actor);
        }
    }

    class Bottom2 : MainClothing
    {
        public Bottom2()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodySize == 0)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[54];
            else if (actor.Unit.BodySize == 1)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[23];
            else
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[59];
            base.Configure(sprite, actor);
        }
    }

    class Bottom3 : MainClothing
    {
        public Bottom3()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodySize == 0)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[55];
            else if (actor.Unit.BodySize == 1)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[24];
            else
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[60];
            base.Configure(sprite, actor);
        }
    }

    class Bottom4 : MainClothing
    {
        public Bottom4()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodySize == 0)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[56];
            else if (actor.Unit.BodySize == 1)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[25];
            else
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[61];
            base.Configure(sprite, actor);
        }
    }

    class Top : MainClothing
    {
        public Top()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[63 + actor.Unit.BreastSize];
            else
                clothing1.GetSprite = s => SpriteDictionary.DewSprite[62];
            base.Configure(sprite, actor);
        }
    }

}
