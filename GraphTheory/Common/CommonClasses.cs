namespace GraphTheory
{
    public class Common
    {
        public interface IEdge
        {
            int FromNode { get; set; }
            int ToNode { get; set; }
        }

        public class EdgeWithWeight : IEdge
        {
            public int FromNode { get; set; }
            public int ToNode { get; set; }
            public int Weight { get; set; }
        }

        public class Edge : IEdge
        {
            public int FromNode { get; set; }
            public int ToNode { get; set; }
        }
    }
}
