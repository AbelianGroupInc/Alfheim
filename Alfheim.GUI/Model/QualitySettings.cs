namespace Alfheim.GUI.Model
{
    public class QualitySettings : IQualitySettings
    {
        #region Properties

        public int PiecewiseLinearQuality { get; set; }

        public int SmoothnessQuality { get; set; }

        public int HybridQuality { get; set; }

        public int UndefinedQuality { get; set; }

        #endregion
    }
}