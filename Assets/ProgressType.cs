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
            ExhaleRatio = 3,
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

public static class BreathControl
{
    public static string[] BreathControlText = new string[7]
        {
            @"Clear mind pattern produces optimal function to both sides of the brain: stimulates creativity and logical/verbal activity.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs.This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs.This expands the rib cage.
3. Fill the upper portion of your lungs.This expands the collarbones.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.",
            @"Relax pattern relieves both nervous and physical tension. Helps ease into restful mode.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs.This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs.This expands the rib cage.
3. Fill the upper portion of your lungs.This expands the collarbones.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.
Sustain: Leave 10/15% of your air volume in the lungs after exhalation and exhale in the last second of your training cycle.",
            @"Calm pattern balances out emotional tension, enables better emotional control.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs. This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs. This expands the rib cage.
3. Fill the upper portion of your lungs. This expands the collarbones.
Retain: Fill the lungs up to 80/90% to prevent diziness. If you find it hard to retain air for a period of time, close the nostrils with your fingers.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.
Sustain: Leave 10/15% of your air volume in the lungs after exhalation and exhale in the last second of your training cycle.",
            @"Power pattern promotes concetration and mobilizes physically.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs. This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs. This expands the rib cage.
3. Fill the upper portion of your lungs. This expands the collarbones.
Retain: Fill the lungs up to 80/90% to prevent diziness. If you find it hard to retain air for a period of time, close the nostrils with your fingers.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.",
            @"Harmony pattern balances psychological and emotional processes into a feeling of well-being and integrity.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs. This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs. This expands the rib cage.
3. Fill the upper portion of your lungs. This expands the collarbones.
Retain: Fill the lungs up to 80/90% to prevent diziness. If you find it hard to retain air for a period of time, close the nostrils with your fingers.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.
Sustain: Leave 10/15% of your air volume in the lungs after exhalation and exhale in the last second of your training cycle.",
            @"Anti-stress pattern reduces stress and anxiety. To be used sparingly.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs. This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs. This expands the rib cage.
3. Fill the upper portion of your lungs. This expands the collarbones.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.",
            @"Anti-appetite pattern reduces emotional dependency of food. Indispensable for fasting and reducing body weight, especially tummy fat.
Inhale: Smoothly flow from each of the following stages:
1. Fill the bottom portion of your lungs. This expands the belly, while chest remains immobile.
2. Fill the middle portion of your lungs. This expands the rib cage.
3. Fill the upper portion of your lungs. This expands the collarbones.
Exhale: Perform it inversely to inhaling -- first the top portion of lungs, then middle and finally bottom, contracting the abdominal muscles at the end.
Sustain: Leave 10/15% of your air volume in the lungs after exhalation and exhale in the last second of your training cycle."
        };
}