namespace Alfheim.GUI.Model
{
    public class ProjectParameters
    {
        public ProjectParameters(string name, double version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; private set; }

        public double Version { get; private set; }
    }
}