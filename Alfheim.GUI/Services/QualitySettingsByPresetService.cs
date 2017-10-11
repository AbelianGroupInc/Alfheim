using Alfheim.GUI.Model;

namespace Alfheim.GUI.Services
{
    public static class QualitySettingsByPresetService
    {
        private static IQualitySettings mLowQualitySettings;
        private static IQualitySettings mHighQualitySettings;

        static QualitySettingsByPresetService()
        {
            mLowQualitySettings = new QualitySettings {
                PiecewiseLinearQuality = 1000,
                HybridQuality = 155,
                SmoothnessQuality = 25,
                UndefinedQuality = 200 };

            mHighQualitySettings = new QualitySettings
            {
                PiecewiseLinearQuality = 2500,
                HybridQuality = 255,
                SmoothnessQuality = 55,
                UndefinedQuality = 355
            };
        }

        public static IQualitySettings GetSettings(QualityPreset preset)
        {
            if (preset == QualityPreset.Low)
                return mLowQualitySettings;
            else
                return mHighQualitySettings;
        }
    }
}
