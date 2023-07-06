using System;
namespace GraphModel
{
	public class CC
	{

			// 用来标记已经访问过的顶点，保证每个顶点值访问一次
    private bool[] marked;
        // 为每个连通分量标示一个id
        private int[] id;
        // 连通分量的个数
        private int count;

        public CC(Graph graph)
        {
            marked = new bool[graph.vertexNum];
            id = new int[graph.vertexNum];
            for (int s = 0; s < graph.vertexNum; s++)
            {
                if (!marked[s])
                {
                    dfs(graph, s);
                    // 一次dfs调用就是一个连通分量，第一个连通分量id为0。
                    // 之后分配的id要自增，第二个连通分量的id为1，以此类推
                    count++;
                }
            }
        }

        private void dfs(Graph graph, int v)
        {
            // 将刚访问到的顶点设置标志
            marked[v] = true;
            id[v] = count;
            // 从v的所有邻接点中选择一个没有被访问过的顶点
            foreach (int w in graph.adj(v))
            {
                if (!marked[w])
                {
                    dfs(graph, w);
                }
            }
        }

        public bool connected(int v, int w)
        {
            return id[v] == id[w];
        }

        public int Id(int v)
        {
            return id[v];
        }

        public int Count()
        {
            return count;
        }
    }
	
}

