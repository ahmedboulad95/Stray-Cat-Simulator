using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public string Name {get; set;}

    private int baseStatValue_;
    private int currentStatValue_;
    private List<StatModifier> modifiers_;

    public Stat(string name, int baseStatValue) {
        Name = name;
        currentStatValue_ = baseStatValue_ = baseStatValue;
        modifiers_ = new List<StatModifier>();
    }

    public Stat(string name, int baseStatValue, int currentStatValue) {
        Name = name;
        baseStatValue_ = baseStatValue;
        currentStatValue_ = currentStatValue;
        modifiers_ = new List<StatModifier>();
    }

    public int GetStatValue() {
        int statValue = currentStatValue_;

        foreach(StatModifier modifier in modifiers_) {
            statValue += modifier.ApplyModifier(currentStatValue_);
        }
        return statValue;
    }

    public void AddModifier(StatModifier modifier) {
        if(modifier == null) return;
        modifiers_.Add(modifier);
    }
}
