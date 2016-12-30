using Boo.Lang;

public class ProgressType
{
    public string Name;

    public int WholeTime;

    public int CycleTime;

    public int InhaleRatio;

    public int RetainRatio;

    public int ExhaleRatio;

    public int SustainRatio;
}

public static class ProgressTypes
{
    public static List<ProgressType> ProgressTypeList = new List<ProgressType>
    {
       new ProgressType
       {
            Name = "Clear Mind",
            WholeTime = 420,
            CycleTime = 12,
            InhaleRatio = 3,
            RetainRatio = 0,
            ExhaleRatio = 9,
            SustainRatio = 0
       },
       new ProgressType
       {
            Name = "Relax",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 3,
            RetainRatio = 0,
            ExhaleRatio = 6,
            SustainRatio = 6
        },
        new ProgressType
        {
            Name = "Calming",
            WholeTime = 432,
            CycleTime = 18,
            InhaleRatio = 3,
            RetainRatio = 6,
            ExhaleRatio = 3,
            SustainRatio = 6
        },
        new ProgressType
        {
            Name = "Power",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 3,
            RetainRatio = 6,
            ExhaleRatio = 6,
            SustainRatio = 0
        },
        new ProgressType
        {
            Name = "Harmony",
            WholeTime = 420,
            CycleTime = 21,
            InhaleRatio = 3,
            RetainRatio = 9,
            ExhaleRatio = 6,
            SustainRatio = 3
        },
        new ProgressType
        {
            Name = "Anti-Stress",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 12,
            RetainRatio = 0,
            ExhaleRatio = 2,
            SustainRatio = 0
        },
        new ProgressType
        {
            Name = "Anti-Appetite",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 5,
            RetainRatio = 0,
            ExhaleRatio = 5,
            SustainRatio = 5
        }
    };
}