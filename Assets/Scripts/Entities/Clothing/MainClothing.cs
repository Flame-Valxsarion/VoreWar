using UnityEngine;

/// <summary>
/// The main clothing type, suitable for Primary clothing or waist clothing.  Covers breasts and blocks dick by default
/// </summary>
abstract class MainClothing
{
    /// <summary>Only wearable by units with the leader type</summary>
    protected bool leaderOnly = false;
    /// <summary>Only wearable by females / herms</summary>
    protected bool femaleOnly = false;
    /// <summary>Only wearable by males</summary>
    protected bool maleOnly = false;
    protected SpriteExtraInfo clothing1;
    protected SpriteExtraInfo clothing2;
    protected SpriteExtraInfo clothing3;
    protected SpriteExtraInfo clothing4;
    protected SpriteExtraInfo clothing5;
    protected SpriteExtraInfo clothing6;
    protected SpriteExtraInfo clothing7;
    protected SpriteExtraInfo clothing8;
    protected SpriteExtraInfo clothing9;
    /// <summary>If not null, replaces the character's breast sprite, used for cleavage windows or other such things</summary>
    protected Sprite breastSprite = null;

    /// <summary>If not null, overwrites the character's normal weapon sprite</summary>
    protected SpriteExtraInfo weaponOverride = null;

    internal bool ReqWinterHoliday = false;

    /// <summary>Prevents Clothing2 (Waist Clothing) from being used at the same time</summary>
    internal bool OccupiesAllSlots = false;
    /// <summary>Discarded sprite uses palettes instead of the solid color</summary>
    internal bool DiscardUsesPalettes = false;
    /// <summary>Defaults to true, if true lowers breast layer to 8 so that it will be under clothing</summary>
    protected bool coversBreasts = true;
    /// <summary>Turns off the breast sprites entirely</summary>
    protected bool blocksBreasts;
    /// <summary>Turns off the dick sprites entirely</summary>
    protected bool blocksDick = true;
    /// <summary>Doesn't turn off the dick, but is in front of it</summary>
    protected bool inFrontOfDick = false;
    /// <summary>Whether clothing applies a color to the belly</summary>
    protected bool colorsBelly = false;
    protected bool HidesFluff = false;
    /// <summary>Color to use if colorsBelly is on</summary>
    protected Color bellyColor = Color.white;
    /// <summary>Palette to use if colorsBelly is on</summary>
    protected ColorSwapPalette bellyPalette = null;

    /// <summary>Clothing will be swapped to this if the actor no longer meets the 'Only' requirements. Value is the index of the clothing you want to change in clothing list + 1. 0 usually means naked, -1 means disabled. </summary>
    protected int SwapedClothing = -1;





    /// <summary>Whether the clothing is considered to be always the default color, also affects the discard</summary>
    internal bool FixedColor = false;

    public Sprite DiscardSprite { get; protected set; } = null;
    /// <summary>
    /// A unique type number, only used in relation to discarded sprites
    /// </summary>
    public int Type { get; protected set; }

    /// <summary>Whether discards will use clothing 1 color instead of clothing 2 color</summary>
    internal bool DiscardUsesColor2 = false;

    protected Color WhiteColored(Actor_Unit actor) => Color.white;
    protected Color DefaultClothingColor(Actor_Unit actor) => Races.GetRace(actor.Unit).ClothingColor(actor);

    public virtual bool CanWear(Unit unit)
    {
        //if (unit.HasBreasts && unit.HasDick == false)
        //    return false;

        if (maleOnly && (unit.HasBreasts || unit.HasDick == false))
            return false;

        if (femaleOnly && unit.HasDick && unit.HasBreasts == false)
            return false;

        if (leaderOnly && unit.Type != UnitType.Leader)
            return false;

        if (ReqWinterHoliday && Config.WinterActive() == false)
            return false;

        return true;
    }
    public int ExposeSwapValue()
    {
        return SwapedClothing;
    }
    /// <summary>
    /// Customizes the clothing for a character -- if it doesn't need to run every frame it should be in the clothing constructor
    /// </summary>
    public virtual void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        if ((blocksDick || inFrontOfDick) && Config.CockVoreHidesClothes && actor.PredatorComponent?.BallsFullness > 0)
            return;
        Apply(sprite, actor);
    }

    /// <summary>
    /// Customizes the clothing for a character -- if it doesn't need to run every frame it should be in the clothing constructor. 
    /// This is a special variant that ignores the clothing being disappeared by the clothing removed by cock vore variant for blocksDick or inFrontOfDick
    /// </summary>
    public virtual void ConfigureIgnoreHidingRules(CompleteSprite sprite, Actor_Unit actor)
    {
        Apply(sprite, actor);
    }


    protected void Apply(CompleteSprite sprite, Actor_Unit actor)
    {
        if (clothing1 != null)
        {
            sprite.SetNextClothingSprite(clothing1);
        }

        if (clothing2 != null)
        {
            sprite.SetNextClothingSprite(clothing2);
        }

        if (clothing3 != null)
        {
            sprite.SetNextClothingSprite(clothing3);
        }

        if (clothing4 != null)
        {
            sprite.SetNextClothingSprite(clothing4);
        }

        if (clothing5 != null)
        {
            sprite.SetNextClothingSprite(clothing5);
        }
        if (clothing6 != null)
        {
            sprite.SetNextClothingSprite(clothing6);
        }
        if (clothing7 != null)
        {
            sprite.SetNextClothingSprite(clothing7);
        }
        if (clothing8 != null)
        {
            sprite.SetNextClothingSprite(clothing8);
        }
        if (clothing9 != null)
        {
            sprite.SetNextClothingSprite(clothing9);
        }


        if (breastSprite != null)
            sprite.ChangeSprite(SpriteType.Breasts, breastSprite);
        if (blocksBreasts)
        {
            sprite.HideSprite(SpriteType.Breasts);
            sprite.HideSprite(SpriteType.SecondaryBreasts);
            if (actor.Unit.Race == Race.Succubi)
            {
                sprite.HideSprite(SpriteType.BreastShadow); //Used for other things in newgraphics
            }
            if (actor.Unit.Race == Race.Umbreon)
            {
                sprite.HideSprite(SpriteType.BodyAccent3); //Used for Breast Ring colors
                sprite.HideSprite(SpriteType.BodyAccent4);
            }

        }
        else if (coversBreasts)
        {
            sprite.ChangeLayer(SpriteType.Breasts, 8);
            sprite.ChangeLayer(SpriteType.SecondaryBreasts, 8);
            if (actor.Unit.Race == Race.Succubi)
            {
                sprite.ChangeLayer(SpriteType.Breasts, 7);
                sprite.ChangeLayer(SpriteType.BreastShadow, 8);
            }
        }
        if (blocksDick)
        {
            sprite.HideSprite(SpriteType.Dick);
            sprite.HideSprite(SpriteType.Balls);
            if (actor.Unit.Race == Race.Umbreon)
            {
                sprite.HideSprite(SpriteType.BodyAccent2); //Used for Dick Ring colors
            }
        }
        if (inFrontOfDick)
        {
            sprite.ChangeLayer(SpriteType.Dick, (clothing1.layer - 1));
            sprite.ChangeLayer(SpriteType.Balls, (clothing1.layer - 1));
        }

        if (colorsBelly)
        {
            if (bellyPalette == null)
                sprite.ChangeColor(SpriteType.Belly, bellyColor != Color.white ? bellyColor : clothing1.GetColor(actor));
            else
            {
                sprite.ChangeColorPalette(SpriteType.Belly, bellyPalette);
            }
        }
        if (HidesFluff)
            sprite.HideSprite(SpriteType.BodyAccent3);
    }


}

