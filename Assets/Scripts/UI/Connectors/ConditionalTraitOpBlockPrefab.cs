using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionalTraitOpBlockPrefab : MonoBehaviour
{
    public TextMeshProUGUI arithmeticSummary;
    public TextMeshProUGUI conditionOp;
    public TextMeshProUGUI conditionValue;
    public TMP_Dropdown logicOp;
    public Button editCondition;

    internal ConditionalTraitOperationBlock associetedBlock;
}
