using UnityEngine;

class Terrorbird : BlankSlate
{

    readonly Sprite[] Sprites = SpriteDictionary.Terrorbird;

    public Terrorbird()
    {
        SpecialAccessoryCount = 8; // head plumage type
        clothingColors = 0;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TerrorbirdSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TerrorbirdSkin);

        Body = new SpriteExtraInfo(8, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(9, HeadSprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(13, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // head plumage
        BodyAccent = new SpriteExtraInfo(9, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // front legs feathers
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // back leg feathers
        BodyAccent3 = new SpriteExtraInfo(10, BodyAccentSprite3, WhiteColored); // front leg claws
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, WhiteColored); // back leg claws
        BodyAccent5 = new SpriteExtraInfo(11, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // wings
        BodyAccent6 = new SpriteExtraInfo(3, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // back tail feather
        BodyAccent7 = new SpriteExtraInfo(5, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // belly cover
        BodyAccent8 = new SpriteExtraInfo(12, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor)); // crop
        Eyes = new SpriteExtraInfo(10, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(6, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, s.Unit.SkinColor));

    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[10];
        return Sprites[9];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // head plumage
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[24 + actor.Unit.SpecialAccessoryType];
        return Sprites[16 + actor.Unit.SpecialAccessoryType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // front leg feathers
    {
        if (actor.GetStomachSize(29) >= 27)
            return Sprites[14];
        return Sprites[6];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[7]; // back leg feathers

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // front leg claws
    {
        if (actor.GetStomachSize(29) >= 27)
            return Sprites[15];
        return Sprites[4];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[5]; // back leg claws

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // wings
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[3];
        return Sprites[2];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites[13]; // // back tail feather

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) => Sprites[8]; // // belly cover

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // crop
    {
        int sizet = actor.GetTailSize(4);
        if (actor.Unit.Predator == false || actor.PredatorComponent?.TailFullness == 0)
        {
            return null;
        }
        else if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[67 + (2 * sizet)];
        }
        else
        {
            return Sprites[66 + (2 * sizet)];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[12];
        return Sprites[11];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {

        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            return Sprites[65];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
        {
            if (actor.GetStomachSize(29, .75f) == 29)
                return Sprites[64];
            else if (actor.GetStomachSize(29, .8275f) == 29)
                return Sprites[63];
            else if (actor.GetStomachSize(29, .9f) == 29)
                return Sprites[62];
        }
        return Sprites[32 + actor.GetStomachSize(29)];
    }





}
