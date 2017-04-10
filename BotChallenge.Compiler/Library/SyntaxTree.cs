using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.Compiler.Exceptions;

namespace BotChallenge.Compiler.Library
{
    public class SyntaxTree<T, V>
    {
        private SyntaxNode<T, V> root;
        public SyntaxTree() { }

        private SyntaxNode<T, V> findParent(SyntaxNode<T, V> root, T item)
        {
            if (root.BranchValue == null)
            {
                return null;
            }

            if (root.BranchValue.Equals(item))
            {
                return root;
            }

            foreach (SyntaxNode<T, V> child in root.BranchChildrens)
            {
                SyntaxNode<T, V> searchResult = findParent(child, item);

                if (searchResult != null)
                {
                    return searchResult;
                }
            }
            return null;
        }

        public void AddNode(T parent, V item)
        {
            SyntaxNode<T, V> node = new SyntaxNode<T, V>(item);
            if (root == null)
            {
                throw new TreeException("Try add leave to empty tree");
            }
            SyntaxNode<T, V> parentNode = findParent(this.root, parent);
            parentNode.LeavesChildrens.Add(node);
        }

        public void AddNode(T parent, T item)
        {
            SyntaxNode<T, V> node = new SyntaxNode<T, V>(item);
            if (root == null)
            {
                throw new TreeException("Try add leave to empty tree");
            }
            SyntaxNode<T, V> parentNode = findParent(this.root, parent);
            parentNode.BranchChildrens.Add(node);
        }
    }
}
