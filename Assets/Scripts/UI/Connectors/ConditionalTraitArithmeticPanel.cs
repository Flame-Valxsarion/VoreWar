using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionalTraitArithmeticPanel : MonoBehaviour
{
    enum VariableMode
    {
        All,
        Arithmetic,
        None,
    }

    public Transform VarFolder;
    public TextMeshProUGUI EqText;
    public InputField conditionValue;

    public Button arithmeticAdd;
    public Button arithmeticSub;
    public Button arithmeticMul;
    public Button arithmeticDiv;

    public TMP_Dropdown conditionOp;

    public ConditionalTrait ConditionalTrait;

    internal Button VariableBtnInstance;
    public Button VarBtn;

    public Button clearButton;

    public Button closeButton;

    bool variable_turn = true;
    int currentIndex;
    VariableMode current_mode = VariableMode.All;
    Dictionary<TraitCondition, Button> condFolder;
    ConditionalTraitOpBlockPrefab currentPrefab;

    internal List<TraitCondition> conditionVariable;
    internal List<TraitConditionArithmeticOperator> arithmeticOperator;
    internal void Open(ConditionalTraitOpBlockPrefab inc_prefab)
    {
        gameObject.SetActive(true);
        ConditionalTrait.gameObject.SetActive(false);

        condFolder = new Dictionary<TraitCondition, Button>();
        conditionVariable = new List<TraitCondition>();
        arithmeticOperator = new List<TraitConditionArithmeticOperator>();
        EqText.text = "";
        currentPrefab = inc_prefab;
        DisableArithButtons();
        Setup();
        if (currentPrefab.associetedBlock.filled)
        {
            variable_turn = false;
            CheckVis(currentPrefab.associetedBlock.conditionVariable.LastOrDefault());

            conditionValue.text = currentPrefab.associetedBlock.compareValue.ToString();
            conditionOp.value = (int)currentPrefab.associetedBlock.compareOp;
            foreach (var item in currentPrefab.associetedBlock.arithmeticOperator)
            {
                arithmeticOperator.Add(item);
            }
            int counter = 0;
            foreach (var vari in currentPrefab.associetedBlock.conditionVariable)
            {
                conditionVariable.Add(vari);
                EqText.text += vari.ToString();
                if (current_mode == VariableMode.None) //if singleton variable
                {
                    break;
                }
                TraitConditionArithmeticOperator op = currentPrefab.associetedBlock.arithmeticOperator[counter];
                if (op != TraitConditionArithmeticOperator.none)
                {
                    switch (op)
                    {
                        case TraitConditionArithmeticOperator.add:
                            EqText.text += " + ";
                            break;
                        case TraitConditionArithmeticOperator.sub:
                            EqText.text += " - ";
                            break;
                        case TraitConditionArithmeticOperator.mul:
                            EqText.text += " × ";
                            break;
                        case TraitConditionArithmeticOperator.div:
                            EqText.text += " ÷ ";
                            break;
                        default:
                            currentPrefab.conditionOp.text += conditionOp.value.ToString();
                            break;
                    }
                }
                counter++;
            }
        }
    }

    private void Setup()
    {
        int maxvalue = (int)TraitCondition.conditionCounter;
        for (int j = 0; maxvalue - 1 >= j; j++)
        {
            condFolder.Add((TraitCondition)j, CreateCondButton(((TraitCondition)j).ToString(), (TraitCondition)j));
        }
        CheckVis(TraitCondition.conditionCounter);
    }

    public void Clear()
    {
        conditionVariable.Clear();
        arithmeticOperator.Clear();
        EqText.text = string.Empty;
        currentPrefab.associetedBlock.filled = false;
        CheckVis(TraitCondition.conditionCounter);
    }

    private Button CreateCondButton(string name, TraitCondition cond)
    {
        VariableBtnInstance = Instantiate(VarBtn, VarFolder);
        var btn = VariableBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = name;
        btn.onClick.AddListener(() =>
        {
            variable_turn = false;
            currentPrefab.associetedBlock.filled = true;
            closeButton.interactable = true;
            CheckVis(cond);
            EqText.text += name;
            conditionVariable.Add(cond);
        });
        return btn;
    }

    private void ArithmeticButtonPress()
    {
        variable_turn = true;
        closeButton.interactable = false;
        CheckVis(conditionVariable.LastOrDefault());
        DisableArithButtons();
    }
    public void AddArithmeticButton()
    {
        ArithmeticButtonPress();
        EqText.text += " + ";
        arithmeticOperator.Add(TraitConditionArithmeticOperator.add);
    }

    public void SubArithmeticButton()
    {
        ArithmeticButtonPress();
        EqText.text += " - ";
        arithmeticOperator.Add(TraitConditionArithmeticOperator.sub);
    }
    public void MulArithmeticButton()
    {
        ArithmeticButtonPress();
        EqText.text += " × ";
        arithmeticOperator.Add(TraitConditionArithmeticOperator.mul);

    }
    public void DivArithmeticButton()
    {
        ArithmeticButtonPress();
        EqText.text += " ÷ ";
        arithmeticOperator.Add(TraitConditionArithmeticOperator.div);
    }
    public void Delete()
    {
        ConditionalTrait.OpBlocks.Remove(currentPrefab.associetedBlock);
        gameObject.SetActive(false);
        ConditionalTrait.gameObject.SetActive(true);
        ConditionalTrait.OpBlocksPrefabList.Clear();
        ConditionalTrait.Refresh();
    }

    private void CheckVis(TraitCondition cond)
    {
        if (cond == TraitCondition.conditionCounter)
        {
            current_mode = VariableMode.All;
            DisableArithButtons(); 
            conditionValue.interactable = true;
            conditionOp.interactable = true;
        }
        else if (cond >= TraitCondition.Male) 
        {
            current_mode = VariableMode.None;
            DisableArithButtons();
            conditionValue.text = string.Empty;
            conditionValue.interactable = false;
            conditionOp.interactable = false;
        }
        else if (cond >= TraitCondition.Level) 
        {
            current_mode = VariableMode.Arithmetic;
            arithmeticAdd.interactable = true;
            arithmeticSub.interactable = true;
            arithmeticMul.interactable = true;
            arithmeticDiv.interactable = true;
            conditionValue.interactable = true;
            conditionOp.interactable = true;
        }

        foreach (var item in condFolder)
        {
            Button button = item.Value;
            if (current_mode == VariableMode.All)
            {
                button.interactable = true;
            }
            else if (current_mode == VariableMode.None || !variable_turn)
            {
                button.interactable = false;
            }
            else if (current_mode == VariableMode.Arithmetic)
            {
                button.interactable = true;
                if (item.Key >= TraitCondition.Male)
                {
                    button.interactable = false;
                }
            }
        }
    }

    public void DisableArithButtons()
    {
        arithmeticAdd.interactable = false;
        arithmeticSub.interactable = false;
        arithmeticMul.interactable = false;
        arithmeticDiv.interactable = false;
    }

    public void Close()
    {
        arithmeticOperator.Add(TraitConditionArithmeticOperator.none);

        currentPrefab.arithmeticSummary.text = EqText.text;
        if (current_mode != VariableMode.None)
        {
            currentPrefab.associetedBlock.compareOp = (TraitConditionCompareOperator)conditionOp.value;
            currentPrefab.associetedBlock.compareValue = int.TryParse(conditionValue.text, out int cv) ? cv : -1;
            currentPrefab.associetedBlock.conditionVariable.Clear();
            foreach (var item in conditionVariable)
            {
                currentPrefab.associetedBlock.conditionVariable.Add(item);
            }
            currentPrefab.associetedBlock.arithmeticOperator.Clear();
            foreach (var item in arithmeticOperator)
            {
                currentPrefab.associetedBlock.arithmeticOperator.Add(item);
            }
            currentPrefab.conditionValue.text = conditionValue.text;
            switch ((TraitConditionCompareOperator)conditionOp.value)
            {
                case TraitConditionCompareOperator.leq:
                    currentPrefab.conditionOp.text = "<=";
                    break;
                case TraitConditionCompareOperator.eq:
                    currentPrefab.conditionOp.text = "=";
                    break;
                case TraitConditionCompareOperator.geq:
                    currentPrefab.conditionOp.text = ">=";
                    break;
                default:
                    currentPrefab.conditionOp.text = conditionOp.value.ToString();
                    break;
            }
        }
        else
        {
            currentPrefab.associetedBlock.conditionVariable.Clear();
            currentPrefab.associetedBlock.arithmeticOperator.Clear();
            foreach (var item in conditionVariable)
            {
                currentPrefab.associetedBlock.conditionVariable.Add(item);
            }
            currentPrefab.conditionOp.text = "";
            currentPrefab.conditionValue.text = "";
            currentPrefab.associetedBlock.compareOp = TraitConditionCompareOperator.none;
        }
        currentPrefab.associetedBlock.summary = EqText.text;

        int children = VarFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(VarFolder.GetChild(i).gameObject);
        }

        EqText.text = "";
        conditionValue.text = "";
        conditionOp.value = 0;

        gameObject.SetActive(false);
        ConditionalTrait.gameObject.SetActive(true);
    }
}
