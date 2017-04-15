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
    public string Description;
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
            SustainRatio = 0,
            Description = "Clear mind pattern produces optimal function to both sides of the brain: stimulates creativity and logical/verbal activity."
       },
       new ProgressType
       {
            Name = "Anti-Stress",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 12,
            RetainRatio = 0,
            ExhaleRatio = 3,
            SustainRatio = 0,
            Description = "Anti-stress pattern reduces stress and anxiety. To be used sparingly."
       },
       new ProgressType
       {
            Name = "Anti-Appetite",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 5,
            RetainRatio = 0,
            ExhaleRatio = 5,
            SustainRatio = 5,
            Description = "Anti-appetite pattern reduces emotional dependency of food. Indispensable for fasting and reducing body weight, especially tummy fat."
       },
       new ProgressType
       {
            Name = "Relax",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 3,
            RetainRatio = 0,
            ExhaleRatio = 6,
            SustainRatio = 6,
            Description = "Relax pattern relieves both nervous and physical tension. Helps ease into restful mode."
        },
        new ProgressType
        {
            Name = "Power",
            WholeTime = 420,
            CycleTime = 15,
            InhaleRatio = 3,
            RetainRatio = 6,
            ExhaleRatio = 6,
            SustainRatio = 0,
            Description = "Power pattern promotes concetration and mobilizes physically."
        },
        new ProgressType
        {
            Name = "Calming",
            WholeTime = 432,
            CycleTime = 18,
            InhaleRatio = 3,
            RetainRatio = 6,
            ExhaleRatio = 3,
            SustainRatio = 6,
            Description = "Calm pattern balances out emotional tension, enables better emotional control."
        },
        new ProgressType
        {
            Name = "Harmony",
            WholeTime = 420,
            CycleTime = 21,
            InhaleRatio = 3,
            RetainRatio = 9,
            ExhaleRatio = 6,
            SustainRatio = 3,
            Description = "Harmony pattern balances psychological and emotional processes into a feeling of well-being and integrity."
        }
    };
}