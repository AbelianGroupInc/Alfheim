namespace Alfheim.GUI.Model
{
    public interface IQualitySettings
    {
        int HybridQuality { get; set; }
        int PiecewiseLinearQuality { get; set; }
        int SmoothnessQuality { get; set; }
        int UndefinedQuality { get; set; }
    }
}