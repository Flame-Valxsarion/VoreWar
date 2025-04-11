using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class ScatV2Discard : MiscDiscard
{
    [OdinSerialize]
    internal ScatInfo scatInfo;

    public ScatV2Discard(Vec2i location, int sortOrder, ScatInfo scatInfo, MiscDiscardType type = 0, int spriteNum = 0, int color = 0, string description = "") : base(location, type, spriteNum, sortOrder, color, description)
    {
        this.scatInfo = scatInfo;
        this.color = scatInfo.color;
        this.description = scatInfo.GetDescription();
    }

    override public void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f));

        var scatBack = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        scatBack.sortingOrder = sortOrder;

        var scatFront = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        scatFront.sortingOrder = sortOrder + 1 + scatInfo.bonesInfos.Count;


        if (scatInfo.predRace == Race.Aabayx)
        {}// No color changes needed
        else if (color != -1 && scatInfo.predRace != Race.Aabayx)
        {
            scatBack.color = ColorPaletteMap.GetSlimeBaseColor(color);
            scatFront.color = ColorPaletteMap.GetSlimeBaseColor(color);
        }
        else
        {
            int r = 135 + Random.Range(-20, 20);
            int g = 107 + Random.Range(-5, 5);
            int b = 80 + Random.Range(-5, 5);
            Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
            scatBack.color = defaultColor;
            scatFront.color = defaultColor;
        }

        Vector3 scatSpriteScalingGloble = new Vector3(1f, 1f);

        if (scatInfo.preySize < 9)
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.ScatV2SBack.Length);
            if(scatInfo.predRace == Race.Aabayx)
            {
                scatBack.sprite = State.GameManager.SpriteDictionary.ScatViralV2SBack[rndNum];
                scatFront.sprite = State.GameManager.SpriteDictionary.ScatViralV2SFront[rndNum];
            }
            else
            {
                scatBack.sprite = State.GameManager.SpriteDictionary.ScatV2SBack[rndNum];
                scatFront.sprite = State.GameManager.SpriteDictionary.ScatV2SFront[rndNum];
            }
        }
        else if (scatInfo.preySize > 15)
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.ScatV2LBack.Length);
            if(scatInfo.predRace == Race.Aabayx)
            {
                scatBack.sprite = State.GameManager.SpriteDictionary.ScatViralV2SBack[rndNum];
                scatFront.sprite = State.GameManager.SpriteDictionary.ScatViralV2SFront[rndNum];
            }
            else
            {
                scatBack.sprite = State.GameManager.SpriteDictionary.ScatV2SBack[rndNum];
                scatFront.sprite = State.GameManager.SpriteDictionary.ScatV2SFront[rndNum];
            }
            int baseSize = scatInfo.preySize - 16; // min = 0
            float xy = 1f + baseSize / (100.0f + baseSize);
            scatSpriteScalingGloble = new Vector3(xy, xy);
            scatBack.transform.localScale = scatSpriteScalingGloble;
            scatFront.transform.localScale = scatSpriteScalingGloble;
        }
        else
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.ScatV2MBack.Length);
            if(scatInfo.predRace == Race.Aabayx)
            {
                scatBack.sprite = State.GameManager.SpriteDictionary.ScatViralV2SBack[rndNum];
                scatFront.sprite = State.GameManager.SpriteDictionary.ScatViralV2SFront[rndNum];
            }
            else
            {
                scatBack.sprite = State.GameManager.SpriteDictionary.ScatV2SBack[rndNum];
                scatFront.sprite = State.GameManager.SpriteDictionary.ScatV2SFront[rndNum];
            }
        }

        //insert bones
        List<SpriteRenderer> boneSprites = new List<SpriteRenderer>();
        foreach (BoneInfo bonesInfo in scatInfo.bonesInfos)
        {
            boneSprites.Add(Object.Instantiate(State.GameManager.DiscardedClothing, bonesInfo.GetBonePosForScat(loc), new Quaternion(), folder).GetComponent<SpriteRenderer>());
            boneSprites.Last().transform.localScale = Vector3.Scale(bonesInfo.GetBoneScalingForScat(), scatSpriteScalingGloble);
            boneSprites.Last().sortingOrder = sortOrder + boneSprites.Count;
            boneSprites.Last().sprite = State.GameManager.SpriteDictionary.Bones[(int)bonesInfo.boneTypes];
            if (color != -1)
            {
                if (bonesInfo.boneTypes == BoneTypes.CrypterBonePile)
                    boneSprites.Last().GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CrypterWeapon, color).colorSwapMaterial;
                else if (bonesInfo.boneTypes == BoneTypes.SlimePile)
                    boneSprites.Last().GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, color).colorSwapMaterial;
            }
        }
    }
}
