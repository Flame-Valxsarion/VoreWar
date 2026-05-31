using System.Linq;
using UnityEngine;

public enum SpriteType
{
    Body,
    Head,
    BodyAccent,
    BodyAccent2,
    BodyAccent3,
    BodyAccent4,
    BodyAccent5,
    BodyAccent6,
    BodyAccent7,
    BodyAccent8,
    BodyAccent9,
    BodyAccent10,
    BodyAccent11,
    BodyAccent12,
    BodyAccent13,
    BodyAccent14,
    BodyAccent15,
    BodyAccent16,
    BodyAccent17,
    BodyAccent18,
    BodyAccent19,
    BodyAccent20,
    Hair,
    Hair2,
    Hair3,
    Beard,
    BodyAccessory,
    SecondaryAccessory,
    Belly,
    SecondaryBelly,
    Weapon,
    BackWeapon,
    BodySize,
    Eyes,
    SecondaryEyes,
    Breasts,
    SecondaryBreasts,
    BreastShadow,
    Dick,
    Balls,
    Pussy,
    PussyIn,
    Anus,
    AnusIn,
    Mouth,
    Clothing,
    Clothing2,
    Clothing3,
    Clothing4,
    Clothing5,
    Clothing6,
    Clothing7,
    Clothing8,
    Clothing9,
    Clothing10,
    Clothing11,
    Clothing12,
    Clothing13,
    Clothing14,
    Clothing15,
    Clothing16, //Unlikely to ever happen, but if you add more, change the code below that checks for 16 so that they reset properly.  
    LastIndex
}

class CompleteSprite
{
    SpriteContainer[] sprites;
    readonly bool[] needRefresh;
    readonly GameObject type;
    readonly Transform folder;
    Actor_Unit actor;
    SpriteExtraInfo belly;
    Vector3 clothingShift;
    readonly GameObject animatedType;
    SpriteType nextClothing;

    Vector2 overallOffset;

    public CompleteSprite(GameObject type, GameObject animatedType, Transform folder)
    {
        sprites = new SpriteContainer[(int)SpriteType.LastIndex];
        needRefresh = new bool[(int)SpriteType.LastIndex];
        this.animatedType = animatedType;
        this.type = type;
        this.folder = folder;
    }

    public void SetActor(Actor_Unit actor)
    {
        if (this.actor != actor)
        {
            this.actor = actor;
            for (int i = 0; i < (int)SpriteType.LastIndex; i++)
            {
                needRefresh[i] = true;
            }
        }

    }
    /// <summary>
    /// Normalizes sprite scale in UI to matche the set pixelsPerUnit based on actual sprite width.
    /// </summary>
    public void AdjustSpriteScale()
    {
        if (actor.Unit.Race == Race.Centaur ||
            actor.Unit.Race == Race.Ghosts)
        {
            foreach (var sprite in sprites)
            {
                if (sprite != null)
                {
                    if (sprite.Sprite != null)
                    {
                        var bodyres = sprite.Sprite.pixelsPerUnit;
                        var bodywidth = sprite.Sprite.rect.width;
                        float mult = bodywidth / bodyres;
                        sprite.GameObject.transform.localScale = new Vector3(mult, mult, sprite.GameObject.transform.localScale.z);
                    }
                }
            }
        }

    }

    public SpriteContainer GetSpriteOfType(SpriteType spriteType)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null)
            return sprites[typeInt];
        if (spriteType == SpriteType.Belly && animatedType != null)
            sprites[typeInt] = new SpriteContainer(animatedType, folder, spriteType.ToString(), 0, 0, null);
            else if (spriteType == SpriteType.SecondaryBelly && animatedType != null)
            sprites[typeInt] = new SpriteContainer(animatedType, folder, spriteType.ToString(), 0, 0, null);
        else if (spriteType == SpriteType.Balls && animatedType != null)
            sprites[typeInt] = new SpriteContainer(animatedType, folder, spriteType.ToString(), 0, 0, null);
        else if (spriteType == SpriteType.Breasts && animatedType != null)
            sprites[typeInt] = new SpriteContainer(animatedType, folder, spriteType.ToString(), 0, 0, null);
        else if (spriteType == SpriteType.SecondaryBreasts && animatedType != null)
            sprites[typeInt] = new SpriteContainer(animatedType, folder, spriteType.ToString(), 0, 0, null);
        else
            sprites[typeInt] = new SpriteContainer(type, folder, spriteType.ToString(), 0, 0, null);
        return sprites[typeInt];
    }

    internal void SetNextClothingSprite(SpriteExtraInfo sprite)
    {
        if (nextClothing > SpriteType.Clothing16)
        {
            Debug.LogWarning("Number of clothing slots exceeded");
            return;
        }
        SetSprite(nextClothing, sprite);
        UpdatePosition(nextClothing, sprite, true);
        nextClothing++;
    }

    internal void SetSprite(SpriteType spriteType, SpriteExtraInfo sprite)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null && (sprite == null || sprite.GetSprite == null || sprite.GetSprite(actor) == null))
        {
            sprites[typeInt].GameObject.SetActive(false);
            return;
        }
        if (sprites[typeInt] == null && (sprite == null || sprite.GetSprite == null || sprite.GetSprite(actor) == null))
            return;
        if (sprites[typeInt] == null)
        {
            sprites[typeInt] = new SpriteContainer(type, folder, spriteType.ToString(), sprite.XOffset + overallOffset.x, sprite.YOffset + overallOffset.y, sprite.GetPalette != null ? sprite.GetPalette(actor) : null);
        }

        if (spriteType < SpriteType.Clothing)
        {
            sprite.XOffset = 0;
            sprite.YOffset = 0;
        }

        if (sprite.GetPalette != null)
        {
            sprites[typeInt].UpdatePalette(sprite.GetPalette(actor));
            sprites[typeInt].Color = Color.white;
        }
        else
        {
            sprites[typeInt].UpdatePalette(null);
            if (sprite.GetColor != null)
                sprites[typeInt].Color = sprite.GetColor(actor);
            else
                sprites[typeInt].Color = Color.white;
        }

        sprites[typeInt].GameObject.SetActive(true);
        sprites[typeInt].Sprite = sprite.GetSprite(actor);
        int sortOrder = sprite.layer + actor.spriteLayerOffset;
        sprites[typeInt].SortOrder = sortOrder;

    }

    private void UpdatePosition(SpriteType spriteType, SpriteExtraInfo sprite, bool clothing = false)
    {
        if (sprite == null || sprite.GetSprite == null)
            return;
        int typeInt = (int)spriteType;
        if (sprites[typeInt] == null)
            return;
        if (clothing)
        {
            clothingShift = Races.GetRace(actor.Unit).ClothingShift;
            sprites[typeInt].SetOffSets(clothingShift.x + sprite.XOffset + overallOffset.x, clothingShift.y + sprite.YOffset + overallOffset.y);
        }
        else
            sprites[typeInt].SetOffSets(sprite.XOffset + overallOffset.x, sprite.YOffset + overallOffset.y);
    }

    //internal void SetSprite(SpriteType spriteType, Sprite sprite, Color color, int sortOrder)
    //{        
    //    int typeInt = (int)spriteType;
    //    if (sprites[typeInt] != null && sprite == null)
    //    {
    //        sprites[typeInt].GameObject.SetActive(false);
    //        return;
    //    }
    //    if (sprites[typeInt] == null && sprite == null)
    //        return;
    //    if (sprites[typeInt] == null)
    //        sprites[typeInt] = new SpriteContainer(type, folder, spriteType.ToString(), 0, 0, null);
    //    if (needRefresh[typeInt])
    //    {
    //        sprites[typeInt].SetOffSets(clothingShift.x, clothingShift.y);
    //        needRefresh[typeInt] = false;
    //    }
    //    sprites[typeInt].GameObject.SetActive(true);
    //    sprites[typeInt].Sprite = sprite;
    //    sortOrder += actor.spriteLayerOffset;
    //    sprites[typeInt].SortOrder = sortOrder;
    //    sprites[typeInt].Color = color;
    //    sprites[typeInt].UpdatePalette(null);
    //}

    internal void ChangeSprite(SpriteType spriteType, Sprite sprite)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null && sprite == null)
        {
            sprites[typeInt].GameObject.SetActive(false);
            return;
        }
        if (sprites[typeInt] == null && sprite == null)
            return;
        if (sprites[typeInt] == null)
            return;
        sprites[typeInt].GameObject.SetActive(true);
        sprites[typeInt].Sprite = sprite;
    }

    internal void HideSprite(SpriteType spriteType)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null)
            sprites[typeInt].GameObject.SetActive(false);
    }

    internal void ChangeColor(SpriteType spriteType, Color color)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null)
            sprites[typeInt].Color = color;
    }

    internal void ChangeColorPalette(SpriteType spriteType, ColorSwapPalette color)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null)
        {
            sprites[typeInt].UpdatePalette(color);
            sprites[typeInt].Color = Color.white;
        }
    }

    internal void ChangeOffset(SpriteType spriteType, Vector2 offset)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null)
        {
            sprites[typeInt].SetOffSets(clothingShift.x + offset.x, clothingShift.y + offset.y);
        }
    }

    internal void ChangeLayer(SpriteType spriteType, int layer)
    {
        int typeInt = (int)spriteType;
        if (sprites[typeInt] != null)
            sprites[typeInt].SortOrder = layer += actor.spriteLayerOffset;
    }

    internal void ResetBellyScale()
    {
        if (sprites[(int)SpriteType.Belly].GameObject != null)
            sprites[(int)SpriteType.Belly].GameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateSprite()
    {
        var race = Races.GetRace(actor.Unit);        
        if (sprites[(int)SpriteType.Belly] == null) GetSpriteOfType(SpriteType.Belly);
        race.RunFirst(actor);
        SetSprite(SpriteType.Body, race.Body);
        SetSprite(SpriteType.Head, race.Head);
        SetSprite(SpriteType.BodyAccent, race.BodyAccent);
        SetSprite(SpriteType.BodyAccent2, race.BodyAccent2);
        SetSprite(SpriteType.BodyAccent3, race.BodyAccent3);
        SetSprite(SpriteType.BodyAccent4, race.BodyAccent4);
        SetSprite(SpriteType.BodyAccent5, race.BodyAccent5);
        SetSprite(SpriteType.BodyAccent6, race.BodyAccent6);
        SetSprite(SpriteType.BodyAccent7, race.BodyAccent7);
        SetSprite(SpriteType.BodyAccent8, race.BodyAccent8);
        SetSprite(SpriteType.BodyAccent9, race.BodyAccent9);
        SetSprite(SpriteType.BodyAccent10, race.BodyAccent10);
        SetSprite(SpriteType.BodyAccent11, race.BodyAccent11);
        SetSprite(SpriteType.BodyAccent12, race.BodyAccent12);
        SetSprite(SpriteType.BodyAccent13, race.BodyAccent13);
        SetSprite(SpriteType.BodyAccent14, race.BodyAccent14);
        SetSprite(SpriteType.BodyAccent15, race.BodyAccent15);
        SetSprite(SpriteType.BodyAccent16, race.BodyAccent16);
        SetSprite(SpriteType.BodyAccent17, race.BodyAccent17);
        SetSprite(SpriteType.BodyAccent18, race.BodyAccent18);
        SetSprite(SpriteType.BodyAccent19, race.BodyAccent19);
        SetSprite(SpriteType.BodyAccent20, race.BodyAccent20);
        SetSprite(SpriteType.BodyAccessory, race.BodyAccessory);
        SetSprite(SpriteType.Hair, race.Hair);
        SetSprite(SpriteType.Hair2, race.Hair2);
        SetSprite(SpriteType.Hair3, race.Hair3);
        SetSprite(SpriteType.Beard, race.Beard);
        SetSprite(SpriteType.SecondaryAccessory, race.SecondaryAccessory);
        belly = race.Belly; //This is done because of the belly sprite affecting the scale
        if (belly != null)
            belly.GetSprite = (s) => race.BellySprite(actor, sprites[(int)SpriteType.Belly].GameObject);
        if (actor.Unit.Race == Race.Umbreon && sprites[(int)SpriteType.BodyAccent5] != null)
            sprites[(int)SpriteType.BodyAccent5].GameObject.transform.SetParent(sprites[(int)SpriteType.Belly].GameObject.transform.parent, false);
        if (actor.Unit.Race == Race.Imps && sprites[(int)SpriteType.BodyAccent6] != null)
            sprites[(int)SpriteType.BodyAccent6].GameObject.transform.SetParent(sprites[(int)SpriteType.Belly].GameObject.transform.parent, false);
        SetSprite(SpriteType.Belly, belly);
        SetSprite(SpriteType.SecondaryBelly, race.SecondaryBelly);
        SetSprite(SpriteType.Eyes, race.Eyes);
        SetSprite(SpriteType.Weapon, race.Weapon);
        SetSprite(SpriteType.BackWeapon, race.BackWeapon);
        SetSprite(SpriteType.BodySize, race.BodySize);
        SetSprite(SpriteType.Mouth, race.Mouth);
        SetSprite(SpriteType.SecondaryEyes, race.SecondaryEyes);

        //Test Code
        //var bodyres = race.Body.GetSprite(actor).pixelsPerUnit;
        //var bodywidth = race.Body.GetSprite(actor).rect.width;
        //float mult = bodyres / bodywidth;
        //if (mult < 1)           
        //sprites[0].GameObject.transform.parent.localPosition = new Vector3(0, 1 - mult, 0);
        //End Test Code

        if (actor.Unit.IsDead == false && actor.UnitSprite != null && actor.UnitSprite.transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            actor.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            actor.UnitSprite.LevelText.gameObject.SetActive(true);
            actor.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            actor.UnitSprite.HealthBar.gameObject.SetActive(true);
        }



        if (Config.HideBreasts == false)
        {
            SetSprite(SpriteType.Breasts, race.Breasts);
        if (actor.Unit.Race == Race.Umbreon && sprites[(int)SpriteType.BodyAccent3] != null && actor.Unit.HasBreasts)
            sprites[(int)SpriteType.BodyAccent3].GameObject.transform.SetParent(sprites[(int)SpriteType.Breasts].GameObject.transform.parent, false);
            SetSprite(SpriteType.BreastShadow, race.BreastShadow);
            SetSprite(SpriteType.SecondaryBreasts, race.SecondaryBreasts);
        if (actor.Unit.Race == Race.Umbreon && sprites[(int)SpriteType.BodyAccent4] != null && actor.Unit.HasBreasts)
            sprites[(int)SpriteType.BodyAccent4].GameObject.transform.SetParent(sprites[(int)SpriteType.SecondaryBreasts].GameObject.transform.parent, false);
        }
        else
        {
            HideSprite(SpriteType.Breasts);
            HideSprite(SpriteType.BreastShadow);
            HideSprite(SpriteType.SecondaryBreasts);
        }

        if (Config.HideCocks == false)
        {
            SetSprite(SpriteType.Dick, race.Dick);
            SetSprite(SpriteType.Balls, race.Balls);
            SetSprite(SpriteType.Pussy, race.Pussy);
            SetSprite(SpriteType.PussyIn, race.PussyIn);
            SetSprite(SpriteType.Anus, race.Anus);
            SetSprite(SpriteType.AnusIn, race.AnusIn);
        }
        else
        {
            HideSprite(SpriteType.Dick);
            HideSprite(SpriteType.Balls);
            HideSprite(SpriteType.Pussy);
            HideSprite(SpriteType.PussyIn);
            HideSprite(SpriteType.Anus);
            HideSprite(SpriteType.AnusIn);
        }       
        race.SetBaseOffsets(actor);
        overallOffset = race.WholeBodyOffset;
        //Done this way so that any offsets that changed other parts will always work correctly
        UpdatePosition(SpriteType.Body, race.Body);
        UpdatePosition(SpriteType.Head, race.Head);
        UpdatePosition(SpriteType.BodyAccent, race.BodyAccent);
        UpdatePosition(SpriteType.BodyAccent2, race.BodyAccent2);
        UpdatePosition(SpriteType.BodyAccent3, race.BodyAccent3);
        UpdatePosition(SpriteType.BodyAccent4, race.BodyAccent4);
        UpdatePosition(SpriteType.BodyAccent5, race.BodyAccent5);
        UpdatePosition(SpriteType.BodyAccent6, race.BodyAccent6);
        UpdatePosition(SpriteType.BodyAccent7, race.BodyAccent7);
        UpdatePosition(SpriteType.BodyAccent8, race.BodyAccent8);
        UpdatePosition(SpriteType.BodyAccent9, race.BodyAccent9);
        UpdatePosition(SpriteType.BodyAccent10, race.BodyAccent10);
        UpdatePosition(SpriteType.BodyAccent11, race.BodyAccent11);
        UpdatePosition(SpriteType.BodyAccent12, race.BodyAccent12);
        UpdatePosition(SpriteType.BodyAccent13, race.BodyAccent13);
        UpdatePosition(SpriteType.BodyAccent14, race.BodyAccent14);
        UpdatePosition(SpriteType.BodyAccent15, race.BodyAccent15);
        UpdatePosition(SpriteType.BodyAccent16, race.BodyAccent16);
        UpdatePosition(SpriteType.BodyAccent17, race.BodyAccent17);
        UpdatePosition(SpriteType.BodyAccent18, race.BodyAccent18);
        UpdatePosition(SpriteType.BodyAccent19, race.BodyAccent19);
        UpdatePosition(SpriteType.BodyAccent20, race.BodyAccent20);
        UpdatePosition(SpriteType.BodyAccessory, race.BodyAccessory);
        UpdatePosition(SpriteType.Hair, race.Hair);
        UpdatePosition(SpriteType.Hair2, race.Hair2);
        UpdatePosition(SpriteType.Hair3, race.Hair3);
        UpdatePosition(SpriteType.Beard, race.Beard);
        UpdatePosition(SpriteType.SecondaryAccessory, race.SecondaryAccessory);
        UpdatePosition(SpriteType.Belly, belly);
        UpdatePosition(SpriteType.SecondaryBelly, race.SecondaryBelly);
        UpdatePosition(SpriteType.Eyes, race.Eyes);
        UpdatePosition(SpriteType.Weapon, race.Weapon);
        UpdatePosition(SpriteType.BackWeapon, race.BackWeapon);
        UpdatePosition(SpriteType.BodySize, race.BodySize);
        UpdatePosition(SpriteType.Mouth, race.Mouth);
        UpdatePosition(SpriteType.SecondaryEyes, race.SecondaryEyes);
        UpdatePosition(SpriteType.Breasts, race.Breasts);
        UpdatePosition(SpriteType.BreastShadow, race.BreastShadow);
        UpdatePosition(SpriteType.SecondaryBreasts, race.SecondaryBreasts);
        UpdatePosition(SpriteType.Dick, race.Dick);
        UpdatePosition(SpriteType.Balls, race.Balls);
        UpdatePosition(SpriteType.Pussy, race.Pussy);
        UpdatePosition(SpriteType.PussyIn, race.PussyIn);
        UpdatePosition(SpriteType.Anus, race.Anus);
        UpdatePosition(SpriteType.AnusIn, race.AnusIn);

        if (sprites[(int)SpriteType.BackWeapon] != null)
        {
            SpriteContainer container = sprites[(int)SpriteType.BackWeapon];
            if (container.IsImage)
            {
                container.GameObject.transform.localPosition = new Vector3(-6, 50, 0);
                container.GameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else
            {
                container.GameObject.transform.localPosition = new Vector3(-.08f, .22f, 0);
                container.GameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
        }       
      
        actor.SquishedBreasts = false;

        for (int i = 0; i < 16; i++)
        {
            HideSprite(SpriteType.Clothing + i);
        }

        nextClothing = SpriteType.Clothing;

        if (actor.Unit.ClothingType > 0)
        {
            if (actor.Unit.ClothingType <= race.AllowedMainClothingTypes.Count)
            {
                race.AllowedMainClothingTypes[actor.Unit.ClothingType - 1].Configure(this, actor);
                if (actor.Unit.ClothingType2 > 0 && actor.Unit.ClothingType2 <= race.AllowedWaistTypes.Count && race.AllowedMainClothingTypes[actor.Unit.ClothingType - 1].OccupiesAllSlots == false)
                {
                    race.AllowedWaistTypes[actor.Unit.ClothingType2 - 1].Configure(this, actor);
                }
            }
            else
            {
                Debug.Log("Invalid Clothing Type Detected and Nullified");
                actor.Unit.ClothingType = 0;
            }

        }
        else
        {
            if (actor.Unit.ClothingType2 > 0 && actor.Unit.ClothingType2 <= race.AllowedWaistTypes.Count)
            {
                race.AllowedWaistTypes[actor.Unit.ClothingType2 - 1].Configure(this, actor);
            }
        }
        if (actor.Unit.ClothingExtraType1 > 0 && actor.Unit.ClothingExtraType1 <= race.ExtraMainClothing1Types.Count)
            race.ExtraMainClothing1Types[actor.Unit.ClothingExtraType1 - 1].Configure(this, actor);
        if (actor.Unit.ClothingExtraType2 > 0 && actor.Unit.ClothingExtraType2 <= race.ExtraMainClothing2Types.Count)
            race.ExtraMainClothing2Types[actor.Unit.ClothingExtraType2 - 1].Configure(this, actor);
        if (actor.Unit.ClothingExtraType3 > 0 && actor.Unit.ClothingExtraType3 <= race.ExtraMainClothing3Types.Count)
            race.ExtraMainClothing3Types[actor.Unit.ClothingExtraType3 - 1].Configure(this, actor);
        if (actor.Unit.ClothingExtraType4 > 0 && actor.Unit.ClothingExtraType4 <= race.ExtraMainClothing4Types.Count)
            race.ExtraMainClothing4Types[actor.Unit.ClothingExtraType4 - 1].Configure(this, actor);
        if (actor.Unit.ClothingExtraType5 > 0 && actor.Unit.ClothingExtraType5 <= race.ExtraMainClothing5Types.Count)
            race.ExtraMainClothing5Types[actor.Unit.ClothingExtraType5 - 1].Configure(this, actor);
        if (actor.Unit.ClothingHatType > 0 && actor.Unit.ClothingHatType <= race.AllowedClothingHatTypes.Count)
        {
            race.AllowedClothingHatTypes[actor.Unit.ClothingHatType - 1].Configure(this, actor);
        }
        if (actor.Unit.ClothingAccessoryType > 0 && actor.Unit.ClothingAccessoryType <= race.AllowedClothingAccessoryTypes.Count)
        {
            race.AllowedClothingAccessoryTypes[actor.Unit.ClothingAccessoryType - 1].Configure(this, actor);
        }
        else if (actor.Unit.EarnedMask && actor.Unit.ClothingAccessoryType > 0 && actor.Unit.ClothingAccessoryType - 1 == race.AllowedClothingAccessoryTypes.Count)
        {
            MainAccessories.AsuraMask.Configure(this, actor);
        }

        if (sprites[0] != null && sprites[0].IsImage) //Manual sort for Images
        {
            SpriteContainer[] containers = sprites.Where(s => s != null).OrderBy(s => s.SortOrder).ToArray();
            for (int i = 0; i < containers.Length; i++)
            {
                containers[i].GameObject.transform.SetSiblingIndex(i + 1);
            }
        }
    }


    internal void DarkenSprites()
    {
        float tint = 0.6f;
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == null)
                continue;
            sprites[i].Color = Darken(sprites[i].Color, tint);
        }
    }

    internal void ApplyDeadEffect()
    {
        float tint = .4f;
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == null)
                continue;
            sprites[i].Color = Bluify(sprites[i].Color, tint);
        }
    }

    internal void RedifySprite(float tint)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == null)
                continue;
            sprites[i].Color = Redify(sprites[i].Color, tint);
        }
    }

    Color Redify(Color color, float fraction)
    {
        color.r = 1 - ((1 - color.r) * fraction);
        color.g = color.g * (1 - fraction);
        color.b = color.b * (1 - fraction);
        return color;
    }

    Color Bluify(Color color, float fraction)
    {
        color.r = color.r * (1 - fraction);
        color.g = color.g * (1 - fraction);
        color.b = 1 - ((1 - color.b) * fraction);
        return color;
    }

    Color Darken(Color color, float fraction)
    {
        color.r = color.r * (1 - fraction);
        color.g = color.g * (1 - fraction);
        color.b = color.b * (1 - fraction);
        return color;
    }

    //Color Lighten(Color color, float fraction)
    //{
    //    color.r = 1 - ((1 - color.r) * (1 - fraction));
    //    color.g = 1 - ((1 - color.g) * (1 - fraction));
    //    color.b = 1 - ((1 - color.b) * (1 - fraction));
    //    return color;
    //}

}
