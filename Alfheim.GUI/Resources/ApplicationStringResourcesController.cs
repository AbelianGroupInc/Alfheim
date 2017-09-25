using System;
using System.Windows;

namespace Alfheim.GUI.Resources
{
    internal class ApplicationStringResourcesController
    {
        #region Members

        private static readonly Lazy<ApplicationStringResourcesController> mLazy =
            new Lazy<ApplicationStringResourcesController>(() => new ApplicationStringResourcesController());

        #endregion

        #region Properties

        public static ApplicationStringResourcesController Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public Application Application { get; set; }

        public string GetResource(string name)
        {
            return (string)this.Application.FindResource(name);
        }

        #endregion
    }
}
