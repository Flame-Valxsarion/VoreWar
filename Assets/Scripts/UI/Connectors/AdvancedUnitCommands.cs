using System;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedUnitCommands : MonoBehaviour
{
    public GameObject ButtonPrefab;
    internal Button[] Buttons;

    int index = 0;

    internal void ClearButtons()
    {
        if (Buttons == null)
            Buttons = new Button[25];
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i] != null)
            {
                Buttons[i].gameObject.SetActive(false);
            }
        }
    }

    internal void SetUpButtons(Actor_Unit actor)
    {

        ClearButtons();
        index = 0;

        foreach (var action in TacticalActionList.TargetedActions)
        {
            if (action.AppearConditional(actor) && (action.RequiresPred == false || actor.Unit.Predator))
            {
                if (action.Name == "Tail Vore" && actor.Unit.Race == Race.Terrorbird)
                    SetButton("Crop Vore", action.OnClicked, action.ButtonColor);
                else if (action.Name == "Breast Vore" && actor.Unit.Race == Race.Kangaroos)
                    SetButton("Pouch Vore", action.OnClicked, action.ButtonColor);
                else if (action.ManaCost > 0)
                    SetButton(action.Name, action.OnClicked, action.ButtonColor, false, actor, action.ManaCost);
                else
                    SetButton(action.Name, action.OnClicked, action.ButtonColor);
            }
        }

        foreach (var action in TacticalActionList.UntargetedActions)
        {
            if (action.AppearConditional(actor))
                SetButton(action.Name, action.OnClicked, action.ButtonColor, false);
        }

        foreach (var spell in actor.Unit.UseableSpells)
        {
            SetButtonSpell(actor, spell);
        }
        

        foreach (var potion in actor.Unit.EquippedPotions)
        {
            if (potion.Value[0] > 0)
            {
                SetButtonPotion(actor, potion.Key);
            }
        }

        int maxSize = Math.Min(800 / index, 60);
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i] != null)
            {
                Buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(160, maxSize);
            }
        }

    }

    internal Button SetButton(string text, Action action, Color color, bool marksSelected = true, Actor_Unit actor = null, int manaCost = 0)
    {
        Button button;
        if (Buttons[index] == null)
        {
            button = Instantiate(ButtonPrefab, transform).GetComponent<Button>();
            var trans = button.GetComponent<RectTransform>();
            trans.sizeDelta = new Vector2(160, 60);
            Buttons[index] = button;
        }
        else
        {
            button = Buttons[index];
            button.onClick.RemoveAllListeners();
        }
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color * 1.2f;
        Color pressed = color * .7f;
        pressed.a = 1;
        cb.pressedColor = pressed;
        button.colors = cb;

        button.GetComponentInChildren<Text>().text = text;
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(action));
        if (marksSelected)
        {
            button.onClick.AddListener(() =>
            {
                if (State.GameManager.TacticalMode.ActionMode == 4)
                    State.GameManager.TacticalMode.CommandsUI.SelectorIcon.transform.position = button.transform.position;
            });
        }

        button.gameObject.SetActive(true);

        if (actor != null)
        {
            bool has_mana = actor.Unit.Mana >= manaCost;
            button.interactable = has_mana;
            if (!has_mana)
            {
                button.GetComponentInChildren<Text>().text = text + "\n(no mana)";
            }
        }
        else
        {
            button.interactable = true;
        }
        index++;
        return button;
    }

    internal Button SetButtonSpell(Actor_Unit actor, Spell spell)
    {
        Color color = new Color(.669f, .753f, 1);
        Button button;
        if (Buttons[index] == null)
        {
            button = Instantiate(ButtonPrefab, transform).GetComponent<Button>();
            var trans = button.GetComponent<RectTransform>();
            trans.sizeDelta = new Vector2(160, 60);
            Buttons[index] = button;
        }
        else
        {
            button = Buttons[index];
            button.onClick.RemoveAllListeners();
        }
        int ModifiedManaCost = spell.ManaCost + (spell.ManaCost * (actor.Unit.GetStatusEffect(StatusEffectType.SpellForce) != null ? actor.Unit.GetStatusEffect(StatusEffectType.SpellForce).Duration/10: 0));
        button.GetComponentInChildren<Text>().text = spell.Name + ((actor.Unit.Mana >= ModifiedManaCost || spell.IsFree) ? "" : "\n(no mana)");
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => State.GameManager.TacticalMode.SetMagicMode(spell)));

        button.onClick.AddListener(() =>
        {
            if (State.GameManager.TacticalMode.ActionMode == 6)
                State.GameManager.TacticalMode.CommandsUI.SelectorIcon.transform.position = button.transform.position;
        });

        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color * 1.2f;
        Color pressed = color * .7f;
        pressed.a = 1;
        cb.pressedColor = pressed;
        button.colors = cb;

        button.interactable = (actor.Unit.Mana >= ModifiedManaCost || spell.IsFree);
        button.gameObject.SetActive(true);
        index++;
        return button;
    }
    internal Button SetButtonPotion(Actor_Unit actor, int potion)
    {
        Color color = new Color(.669f, .753f, 1);
        Button button;
        if (Buttons[index] == null)
        {
            button = Instantiate(ButtonPrefab, transform).GetComponent<Button>();
            var trans = button.GetComponent<RectTransform>();
            trans.sizeDelta = new Vector2(160, 60);
            Buttons[index] = button;
        }
        else
        {
            button = Buttons[index];
            button.onClick.RemoveAllListeners();
        }
        button.GetComponentInChildren<Text>().text = State.World.ItemRepository.GetItem(potion).Name;
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => State.GameManager.TacticalMode.SetPotionMode((Potion)State.World.ItemRepository.GetItem(potion))));

        button.onClick.AddListener(() =>
        {
            if (State.GameManager.TacticalMode.ActionMode == 7)
                State.GameManager.TacticalMode.CommandsUI.SelectorIcon.transform.position = button.transform.position;
        });

        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color * 1.2f;
        Color pressed = color * .7f;
        pressed.a = 1;
        cb.pressedColor = pressed;
        button.colors = cb;

        button.gameObject.SetActive(true);
        index++;
        return button;
    }

    public GameObject SelectorIcon;
}
