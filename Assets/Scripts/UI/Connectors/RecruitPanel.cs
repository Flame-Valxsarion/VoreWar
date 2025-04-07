using UnityEngine;
using UnityEngine.UI;

public class RecruitPanel : MonoBehaviour
{
    public Button StockWeapons;
    public Button RecruitSoldier;
    public Button HireSoldier;
    public Button HireVillageMerc;
    public Button ResurrectLeader;
    public Button AddUnit;
    public Button Back;
    public Button CheapUpgrade;
    public Button VillageView;
    public Text TownName;
    public Text Population;
    public Text Income;
    public Text Gold;
    public Text DefenderCount;

    public Button ImprintUnit;
    public Button ApplyPotion;

    public void OpenRenameTown()
    {
        var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
        box.SetData(State.GameManager.Recruit_Mode.RenameVillage, "Rename", "Cancel", "Rename this village?", 150);

    }


}
