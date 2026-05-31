using UnityEngine;

static class TacticalGraphicalEffects
{
    internal enum SpellEffectIcon
    {
        None,
        Heal,
        Resurrect,
        PurplePlus,
        Poison,
        Buff,
        Debuff,
        Managen,
        Web,
    }

    internal static void CreateProjectile(Actor_Unit actor, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var arrow = Object.Instantiate(State.GameManager.TacticalMode.ArrowPrefab).GetComponent<ArrowEffect>();
        var sprite = ArrowType(actor, out Material material);
        if (actor.Unit.Race == Race.Panthers)
            PantherSetup(arrow, actor);
        if (actor.Unit.Race == Race.Bears)
            BearSetup(arrow, actor);
        if (actor.Unit.Race == Race.Cherub)
            CherubSetup(arrow, actor);
        if (actor.Unit.Race == Race.Seraph)
            SeraphSetup(arrow, actor);
        if (actor.Unit.Race == Race.SoulSprite)
            SoulSpriteSetup(arrow, actor);
        if (sprite != null) arrow.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        if (material != null) arrow.GetComponentInChildren<SpriteRenderer>().material = material;
        arrow.Setup(actor.Position, target.Position, target);
    }

    private static void PantherSetup(ArrowEffect obj, Actor_Unit actor)
    {
        Weapon weapon = actor.BestRanged;
        if (weapon.Graphic == 4)
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.PantherBase;
            anim.Frame = new Sprite[]
            {
                sprites[36],
                sprites[37],
                sprites[38],
                sprites[39],
            };
            anim.FrameTime = new float[] { .05f, .05f, .05f, .05f };
        }
        else if (weapon.Graphic == 6)
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.PantherBase;
            anim.Frame = new Sprite[]
            {
                sprites[42],
                sprites[43],
                sprites[44],
                sprites[45],
            };
            anim.FrameTime = new float[] { .05f, .05f, .05f, .05f };
        }

    }

    private static void BearSetup(ArrowEffect obj, Actor_Unit actor)
    {
        Weapon weapon = actor.BestRanged;
//        if (weapon.Graphic == 4)
//        {
//            Sprite[] sprites = State.GameManager.SpriteDictionary.Bears ;
//            anim.Frame = new Sprite[]
//            {
//                sprites[34],
//            };
//            anim.FrameTime = new float[] { 1f };
//        }
        if (weapon.Graphic == 6)
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.Bears;
            anim.Frame = new Sprite[]
            {
                sprites[36],
                sprites[37],
            };
            anim.FrameTime = new float[] { .025f, .025f};
        }

    }
    private static void CherubSetup(ArrowEffect obj, Actor_Unit actor)
    {
        Weapon weapon = actor.BestRanged;
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.AngelProjectile;
            anim.Frame = new Sprite[]
            {
                sprites[0],
                sprites[1],
                sprites[2],
                sprites[3],
            };
            anim.FrameTime = new float[] { .1f, .1f, .1f, .1f};
        }

    }
    private static void SeraphSetup(ArrowEffect obj, Actor_Unit actor)
    {
        Weapon weapon = actor.BestRanged;
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.AngelProjectile;
            anim.Frame = new Sprite[]
            {
                sprites[4],
                sprites[5],
                sprites[6],
                sprites[7],
            };
            anim.FrameTime = new float[] { .1f, .1f, .1f, .1f};
        }

    }
    private static void SoulSpriteSetup(ArrowEffect obj, Actor_Unit actor)
    {
        Weapon weapon = actor.BestRanged;
        {
            var anim = obj.gameObject.AddComponent<AnimationEffectComponent>();
            anim.Repeat = true;
            Sprite[] sprites = State.GameManager.SpriteDictionary.AngelProjectile;
            anim.Frame = new Sprite[]
            {
                sprites[8],
                sprites[9],
                sprites[10],
                sprites[11],
                sprites[12],
                sprites[13],
                sprites[14],
            };
            anim.FrameTime = new float[] { .1f, .1f, .1f, .1f, .1f, .1f, .1f};
        }

    }
    static Sprite ArrowType(Actor_Unit actor, out Material material)
    {
        Weapon weapon = actor.BestRanged;
        material = null;
        if (actor.Unit.Race == Race.Harpies)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Harpies[27];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Harpies[28];
        }
        else if (actor.Unit.Race == Race.Imps)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.NewimpBase[93];
        }

        else if (actor.Unit.Race == Race.Scylla)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Scylla[22];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Scylla[23];
        }
        else if (actor.Unit.Race == Race.Slimes)
        {
            if (weapon.Graphic == 4)
            {
                material = Races.Slimes.GetSlimeAccentMaterial(actor);
                return State.GameManager.SpriteDictionary.Slimes[16];
            }
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Slimes[17];
        }
        else if (actor.Unit.Race == Race.Crypters)
        {
            if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Crypters[27];
        }
        else if (actor.Unit.Race == Race.Kangaroos)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Kangaroos[125];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Kangaroos[126];
        }
        else if (actor.Unit.Race == Race.Tigers && actor.Unit.ClothingType == 1)
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (actor.Unit.Race == Race.Kobolds)
            return State.GameManager.SpriteDictionary.Kobolds[20];
        else if (actor.Unit.Race == Race.Equines && weapon.Graphic == 6)
            return State.GameManager.SpriteDictionary.EquineClothes[48];
        else if (actor.Unit.Race == Race.Alraune && (weapon.Graphic == 4 || weapon.Graphic == 6))
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (actor.Unit.Race == Race.SpitterSlugs)
            return State.GameManager.SpriteDictionary.SpitterSlug[10];
        else if (actor.Unit.Race == Race.EarthDryad)
            return State.GameManager.SpriteDictionary.DryadSprites3[17];
        else if (actor.Unit.Race == Race.RiverDryad)
            return State.GameManager.SpriteDictionary.DryadSprites5[17];
        else if (actor.Unit.Race == Race.Bats)
            return State.GameManager.SpriteDictionary.Demibats1[132];
        else if (actor.Unit.Race == Race.RwuMercenaries && (weapon.Graphic == 4 || weapon.Graphic == 6))
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (actor.Unit.Race == Race.Tatltuae)
            return State.GameManager.SpriteDictionary.Equaleon[37];// intentionally blank sprite
        else if (actor.Unit.Race == Race.Hamsters && (weapon.Graphic == 4 || weapon.Graphic == 6))
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (actor.Unit.Race == Race.Lupine && (weapon.Graphic == 4 || weapon.Graphic == 6))
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (actor.Unit.Race == Race.Cherub)
            return State.GameManager.SpriteDictionary.AngelProjectile[0];
        else if (actor.Unit.Race == Race.Seraph)
            return State.GameManager.SpriteDictionary.AngelProjectile[4];
        else if (actor.Unit.Race == Race.Seraph)
            return State.GameManager.SpriteDictionary.AngelProjectile[8];
        else if (actor.Unit.Race == Race.Bears)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Bears[34];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Bears[36];
        }
        else if (actor.Unit.Race == Race.Panthers)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.PantherBase[36];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.PantherBase[42];
        }
        else if (actor.Unit.Race == Race.Bees)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Bees1[89];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Bees1[89];
        }
        else if (actor.Unit.Race == Race.Merfolk && weapon.Graphic == 4 || weapon.Graphic == 6)
            return State.GameManager.SpriteDictionary.Slimes[17];
        else if (actor.Unit.Race == Race.Vipers)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Vipers1[20];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Vipers1[20];
        }
        else if (actor.Unit.Race == Race.Bears)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.Bears[34];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.Bears[36];
        }
        else if (actor.Unit.Race == Race.Yordles)
        {
            if (weapon.Graphic == 4)
                return State.GameManager.SpriteDictionary.YordlesCustomization2[189];
            else if (weapon.Graphic == 6)
                return State.GameManager.SpriteDictionary.YordlesCustomization2[190];
        }
        return null;
    }

    internal static void SuccubusSwordEffect(Vector2 location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var obj = Object.Instantiate(State.GameManager.SpriteRendererPrefab);
        obj.transform.position = location;
        obj.AddComponent<Assets.Scripts.Entities.Animations.SuccubusSword>();

    }

    internal static void EntropicChaosEffect(Vector2 location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var obj = Object.Instantiate(State.GameManager.SpriteRendererPrefab);
        obj.transform.position = location;
        obj.transform.localScale = new Vector3(2, 2, 1);
        obj.AddComponent<Assets.Scripts.Entities.Animations.EntropicChaos>();

    }

    internal static void VenomBiteEffect(Vector2 location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var obj = Object.Instantiate(State.GameManager.SpriteRendererPrefab);
        obj.transform.position = location;
        obj.transform.localScale = new Vector3(3, 3, 1);
        obj.AddComponent<Assets.Scripts.Entities.Animations.VenomBite>();

    }

    internal static void CreateIceBlast(Vec2 location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.IceBlast;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());

        MiscUtilities.DelayedInvoke(() => EightRing(prefab, location, .5f), .1f);
        MiscUtilities.DelayedInvoke(() => EightRing(prefab, location, 1), .2f);
    }

    internal static void CreateFireBall(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Fireball;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreateFireBomb(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.FireBomb;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreateBola(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Bola;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreatePotion(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Potion;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreateCaptureNet(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.CaptureNet;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreateIcicle(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Icicle;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreateDiamondStorm(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.DiamondStorm;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreateCrossShock(Vec2 location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.CrossShock;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());

    }

    internal static void CreateHeartProjectile(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.Charm;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);

    }

    internal static void CreatePollenCloud(Vec2i location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.PollenCloud;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());

    }

    internal static void CreatePoisonCloud(Vec2i location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.PoisonCloud;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());

    }

    internal static void CreateGasCloud(Vec2i location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.GasCloud;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());

    }

    internal static void CreateSmokeCloud(Vec2i location, float scale)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.SmokeCloud;
        GameObject obj = Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());
        obj.transform.localScale *= scale;
        
    }

    internal static void CreateGlueBomb(Vec2i startLocation, Vec2i endLocation)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var arrow = Object.Instantiate(State.GameManager.TacticalMode.ArrowPrefab).GetComponent<ArrowEffect>();
        var sprite = State.GameManager.SpriteDictionary.SpitterSlug[10];
        if (sprite != null) arrow.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        System.Action hitEffect = () =>
        {
            var prefab = State.GameManager.TacticalEffectPrefabList.GlueBomb;
            Object.Instantiate(prefab, new Vector3(endLocation.x, endLocation.y, 0), new Quaternion());
        };
        arrow.Setup(startLocation, endLocation, null, null, hitEffect);

    }

    internal static void CreateSpiderWeb(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.SpiderWeb;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);
        effect.totalTime *= 2;
        effect.extraTime = 2.25f;
        var fade = effect.GetComponent<FadeInFadeOut>();
        fade.HoldTime = effect.totalTime + .75f;

    }

    internal static void CreateHugeMagic(Vec2i startLocation, Vec2i endLocation, Actor_Unit target, bool landed)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.HugeMagic;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        effect.Setup(startLocation, endLocation, target, null, null);
        effect.totalTime *= 2;
        System.Action hitEffect = null;
        if (landed)
        {
           hitEffect = () =>
            {
                CreateMagicExplosion(new Vec2i(endLocation.x, endLocation.y));
            };
        }   
        effect.Setup(startLocation, endLocation, target, null, hitEffect);
    }

    internal static void CreateMagicExplosion(Vec2i location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.MagicExplosion;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());

    }

    internal static void CreateDivineNova(Vec2 location)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.DivineNovaBlast;
        Object.Instantiate(prefab, new Vector3(location.x, location.y, 0), new Quaternion());
    }

    internal static void CreateGenericMagic(Vec2i startLocation, Vec2i endLocation, Actor_Unit target, SpellEffectIcon icon = SpellEffectIcon.None)
    {
        if (State.GameManager.TacticalMode.turboMode)
            return;
        var prefab = State.GameManager.TacticalEffectPrefabList.GenericMagic;
        var effect = Object.Instantiate(prefab, new Vector3(startLocation.x, startLocation.y, 0), new Quaternion()).GetComponent<ArrowEffect>();
        System.Action hitEffect = null;
        if (icon != SpellEffectIcon.None)
        {
            //Sprite sprite;
            //switch (icon)
            //{
            //    case SpellEffectIcon.Heal:
            //        break;
            //    case SpellEffectIcon.Resurrect:
            //        break;
            //    case SpellEffectIcon.PurplePlus:
            //        break;
            //    case SpellEffectIcon.Poison:
            //        break;
            //    case SpellEffectIcon.Buff:
            //        break;
            //    case SpellEffectIcon.Debuff:
            //        break;
            //}
            hitEffect = () =>
            {
                var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.FadeInFadeOut).GetComponent<FadeInFadeOut>();
                obj.transform.SetPositionAndRotation(new Vector3(target.Position.x, target.Position.y, 0), new Quaternion());
                obj.SpriteRenderer.sprite = State.GameManager.SpriteDictionary.SpellIcons[(int)icon - 1];
            };
        }

        effect.Setup(startLocation, endLocation, target, null, hitEffect);

    }



    static void EightRing(GameObject prefab, Vec2 location, float distance)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 & y == 0)
                    continue;
                Object.Instantiate(prefab, new Vector3(location.x + x * distance, location.y + y * distance, 0), new Quaternion());
            }
        }
    }


}

