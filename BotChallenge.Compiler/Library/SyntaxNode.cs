using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Compiler.Library
{
    class SyntaxNode<T, V>
    {
        public SyntaxNode(T value)
        {
            this.BranchValue = value;
            this.BranchChildrens = new List<SyntaxNode<T, V>>();
            this.LeavesChildrens = new List<SyntaxNode<T, V>>();
        }

        public SyntaxNode(V value)
        {
            LeavesValue = value;
        }

        public T BranchValue { get; }
        public V LeavesValue { get; }

        public ICollection<SyntaxNode<T,V>> BranchChildrens { get; }
        public ICollection<SyntaxNode<T, V>> LeavesChildrens { get; }
    }
}
