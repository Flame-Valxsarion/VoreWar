using OdinSerializer;
using UnityEngine;

enum MiscDiscardType
{
    Scat,
    Bones,
    Cum,
    Honey,
    Urine,
    DisposedCondom
}

class MiscDiscard
{
    [OdinSerialize]
    internal Vec2i location;
    [OdinSerialize]
    internal MiscDiscardType type;
    [OdinSerialize]
    internal int spriteNum;
    [OdinSerialize]
    internal int sortOrder;
    [OdinSerialize]
    internal string description;
    [OdinSerialize]
    internal int color;

    public MiscDiscard(Vec2i location, MiscDiscardType type, int spriteNum, int sortOrder, int color, string description = "")
    {
        this.location = location;
        this.type = type;
        this.spriteNum = spriteNum;
        this.sortOrder = sortOrder;
        this.description = description;
        this.color = color;
    }

    virtual public void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f));

        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite.sortingOrder = sortOrder;
        var sprite2 = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite2.sortingOrder = sortOrder;
        var sprite3 = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite3.sortingOrder = sortOrder;
        switch (type)
        {
            case MiscDiscardType.Scat:
                sprite.sprite = State.GameManager.SpriteDictionary.Scat[spriteNum];
                if (color != -1)
                {
                    sprite.color = ColorPaletteMap.GetSlimeBaseColor(color);
                }
                break;
            case MiscDiscardType.Bones:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[spriteNum];
                if (color != -1)
                {
                    if (spriteNum == (int)BoneTypes.CrypterBonePile)
                        sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CrypterWeapon, color).colorSwapMaterial;
                    else if (spriteNum == (int)BoneTypes.SlimePile)
                        sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, color).colorSwapMaterial;
                }
                break;
            case MiscDiscardType.Cum:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[spriteNum];
                sprite.sortingOrder = int.MinValue;
                if (color == 0) sprite.color = new Color(.51f, .89f, .98f);
                break;
            case MiscDiscardType.Honey:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[27];
                sprite.sortingOrder = int.MinValue;
                //sprite.color = new Color(120.70f, 110.80f, .20f);
                break;
            case MiscDiscardType.Urine:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[28 + State.Rand.Next(3)];
                sprite.sortingOrder = int.MinValue;
                //sprite.color = new Color(120.70f, 110.80f, .20f);
                break;
            case MiscDiscardType.DisposedCondom:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[25];
                sprite2.sprite = State.GameManager.SpriteDictionary.Bones[3];
                sprite3.sprite = State.GameManager.SpriteDictionary.Bones[26];
                int hue  = Random.Range(1, 10);
                if (hue == 1) 
                {
                    int r = 255; int g = 0; int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 2) 
                {
                    int r = 255; int g = 125; int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 3) 
                {
                    int r = 0; int g = 255; int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 4) 
                {
                    int r = 100; int g = 100; int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 5) 
                {
                    int r = 255; int g = 0; int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 6) 
                {
                    int r = 255; int g = 255; int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 7) 
                {
                    int r = 0; int g = 255; int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 8) 
                {
                    int r = 255; int g = 255; int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                if (hue == 9) 
                {
                    int r = 0; int g = 0; int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }
                break;

        }
    }
}
