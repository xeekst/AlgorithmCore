using System.Collections.Generic;

namespace RegularExpression
{
    //NFA 是一个由 ε 转换构造的一个有向图（转换有两种：1. 字母表匹配转换 2.  ε 转换）
    public class NFA
    {
        private char[] _re;
        private Digraph G;
        public Digraph Graph => G;
        private int M;
        public NFA(string regexp)
        {
            Stack<int> ops = new Stack<int>();
            _re = regexp.ToCharArray();
            M = _re.Length;
            G = new Digraph(M + 1);

            for (int i = 0; i < M; i++)
            {
                int lp = i;
                if (_re[i] == '(' || _re[i] == '|')
                {
                    ops.Push(i);
                }
                else if (_re[i] == ')')
                {
                    int or = ops.Pop();
                    if (_re[or] == '|')
                    {
                        lp = ops.Pop();
                        G.AddEdge(lp, or + 1);
                        G.AddEdge(or, i);
                    }
                    else
                    {
                        lp = or;
                    }
                }
                if (i < M - 1 && _re[i + 1] == '*')
                {
                    G.AddEdge(lp, i + 1);
                    G.AddEdge(i + 1, lp);
                }
                if (_re[i] == '(' || _re[i] == '*' || _re[i] == ')')
                {
                    G.AddEdge(i, i + 1);
                }
            }
        }

        public bool Recognizes(string text)
        {
            HashSet<int> pc = new HashSet<int>();
            DirectedDFS dfs = new DirectedDFS(G, 0);
            for (int v = 0; v < G.V(); v++)
            {
                if (dfs.Reachable(v)) pc.Add(v);
            }
            for (int i = 0; i < text.Length; i++)
            {
                HashSet<int> match = new HashSet<int>();
                foreach (int v in pc)
                {
                    if (v < _re.Length)
                    {
                        if (_re[v] == text[i] || _re[v] == '.')
                        {
                            match.Add(v + 1);
                        }
                    }
                }
                pc = new HashSet<int>();
                dfs = new DirectedDFS(G, match);
                for (int v = 0; v < G.V(); v++)
                {
                    if (dfs.Reachable(v)) pc.Add(v);
                }
            }
            foreach (int v in pc) if (v == _re.Length) return true;
            return false;
        }
    }
}