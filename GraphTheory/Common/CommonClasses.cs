namespace GraphTheory.Common
{
    public class SharedClasses
    {
        public interface IEdge
        {
            int FromNode { get; set; }
            int ToNode { get; set; }
        }

        public interface IEdgeWithWeight:IEdge
        {
            int Weight { get; set; }
        }

        public class EdgeWithWeight : IEdgeWithWeight
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
