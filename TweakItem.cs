namespace MinonTweaks;

// Модель одного твика: заголовок/описание на RU и EN, бейдж (Safe/Risky),
// и либо тумблер, либо одна кнопка-действие, либо пара кнопок (Применить/Откатить).
public class TweakItem
{
    public string TitleRu { get; set; } = "";
    public string TitleEn { get; set; } = "";
    public string DescriptionRu { get; set; } = "";
    public string DescriptionEn { get; set; } = "";

    // "safe" | "risky" | null (без бейджа)
    public string? Badge { get; set; }

    // Актуально только если ActionLabel и ActionLabels оба null — тогда рисуем тумблер
    public bool IsOn { get; set; }

    // Одна кнопка-действие (RU/EN)
    public string? ActionLabelRu { get; set; }
    public string? ActionLabelEn { get; set; }

    // Пара кнопок (RU/EN), например ["Применить","Откатить"] / ["Apply","Revert"]
    public string[]? ActionLabelsRu { get; set; }
    public string[]? ActionLabelsEn { get; set; }

    public string Title(string lang) => lang == "en" ? TitleEn : TitleRu;
    public string Description(string lang) => lang == "en" ? DescriptionEn : DescriptionRu;
    public string? ActionLabel(string lang) => lang == "en" ? ActionLabelEn : ActionLabelRu;
    public string[]? ActionLabels(string lang) => lang == "en" ? ActionLabelsEn : ActionLabelsRu;
}

public class TweakGroup
{
    public string LabelRu { get; set; } = "";
    public string LabelEn { get; set; } = "";
    public List<TweakItem> Items { get; set; } = new();

    public string Label(string lang) => lang == "en" ? LabelEn : LabelRu;
}
