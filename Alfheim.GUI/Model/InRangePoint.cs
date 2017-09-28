namespace Alfheim.GUI.Model
{
    public class InRangePoint
    {
        public InRangePoint(string name, string leftPoint, string rightPoint)
        {
            Name = name;
            LeftPointName = leftPoint;
            RightPointName = rightPoint;
        }

        public string Name { get; set; }

        public string LeftPointName { get; set; }

        public string RightPointName { get; set; }
    }
}
