using System;

namespace Alfheim.FuzzyLogic.Functions
{
    public class CroppedFunction : FuzzyFunction
    {
        #region Members

        private IFuzzyFunction mTargetFunction;
        private double mCropLevel;

        #endregion

        #region Properties

        public double CropLevel
        {
            get
            {
                return mCropLevel;
            }
            set
            {
                if (value < mTargetFunction.MinOutputValue ||
                    value > mTargetFunction.MaxOutputValue)
                    throw new ArgumentException();

                mCropLevel = value;
            }
        }

        public double CropValue
        {
            set
            {
                mCropLevel = mTargetFunction.GetValue(value);
            }
        }

        #endregion

        public CroppedFunction(IFuzzyFunction targetFunction, double cropValue)
        {
            mTargetFunction = targetFunction;
            Type = FuzzyFunctionType.PiecewiseLinear | mTargetFunction.Type;
        }

        #region Protected methods

        protected override void InitProperties() { }

        protected override double Function(double x)
        {
            return Crop(mTargetFunction.GetValue(x));
        }

        #endregion

        private double Crop(double y)
        {
            return y > CropLevel ? CropLevel : y;
        }

    }
}
