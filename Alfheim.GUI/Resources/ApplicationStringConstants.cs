namespace Alfheim.GUI.Resources
{
    internal static class ApplicationStringConstants
    {
        internal static string Error
        {
            get
            {
                return ApplicationStringResourcesController
                    .Instance.GetResource("cError");
            }
        }

        internal static string NameIsExist
        {
            get
            {
                return ApplicationStringResourcesController
                    .Instance.GetResource("cNameIsExist");
            }
        }

        internal static string TriangleFunction
        {
            get
            {
                return ApplicationStringResourcesController
                    .Instance.GetResource("cTriangleFunction");
            }
        }

        internal static string TrapezoidalFunction
        {
            get
            {
                return ApplicationStringResourcesController
                    .Instance.GetResource("cTrapezoidalFunction");
            }
        }

        internal static string GaussianFunction
        {
            get
            {
                return ApplicationStringResourcesController
                    .Instance.GetResource("cGaussianFunction");
            }
        }
    }
}
