public class StatModifier
{
    public enum ModifierType { ADD, MULTIPLY }

    public string LinkedStatName {get; set;}

    private ModifierType type_;
    private float modifierValue_;

    public StatModifier(string linkedStatName, ModifierType type, float modifierValue) {
        LinkedStatName = linkedStatName;
        type_ = type;
        modifierValue_ = modifierValue;
    }

    public int ApplyModifier(int statValue) {
        int modifiedValue = statValue;
        switch(type_) {
            case ModifierType.ADD:
                modifiedValue = (int)modifierValue_;
                break;
            case ModifierType.MULTIPLY:
                modifiedValue = (int)(modifiedValue * modifierValue_);
                break;
        }
        return modifiedValue;
    }

    public float GetModifierValue() {
        return modifierValue_;
    }
}
