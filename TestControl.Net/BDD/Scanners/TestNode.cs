// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Collections;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;

namespace  TestControl.Net.BDD.Scanners
{
    public abstract class TestNode : ITestNode
    {
        private readonly string _text;
        private int _currentIdx;
        private IFixtureResult _fixtureResult;
        private IPersistanceTreeNode _persistanceNode;

        public TestNode(string name)
        {
            _text = name;
            ID = Guid.NewGuid();
        }

        #region ITestNode Members

        public string Description { get; set; }

        public IServices Services { get; set; }

        public virtual void Load()
        {
            throw new NotImplementedException();
        }

        public virtual void Load(ITestScriptProvider scriptProvider)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            PersistanceNode.Clear();
        }

        public IFixtureResult TestResult
        {
            get
            {
                if (_fixtureResult == null)
                {
                    _fixtureResult = Services.Get<IFixtureResult>();
                }
                return _fixtureResult;
            }
        }

        public Guid ID { get; set; }

        public ITestScriptProvider ScriptProvider { get; set; }


        public IPersistanceTreeNode PersistanceNode
        {
            get
            {
                if (_persistanceNode == null)
                {
                    _persistanceNode = Services.Get<IPersistanceTreeNode>();
                    _persistanceNode.TestItem = this;
                    _persistanceNode.Text = _text;
                }

                return _persistanceNode;
            }
        }

        public int Count
        {
            get { return PersistanceNode.Count; }
        }

        public ITestNode ParentNode
        {
            get
            {
                if ((PersistanceNode == null) || (PersistanceNode.ParentNode == null))
                    return null;
                return PersistanceNode.ParentNode.TestItem;
            }
        }

        public ITestNode PreviousNode
        {
            get
            {
                if ((PersistanceNode == null) || (PersistanceNode.PreviousNode == null))
                    return null;
                return PersistanceNode.PreviousNode.TestItem;
            }
        }


        public ITestNode NextNode
        {
            get
            {
                if ((PersistanceNode == null) || (PersistanceNode.NextNode == null))
                    return null;
                return PersistanceNode.NextNode.TestItem;
            }
        }

        public int LevelNo { get; set; }


        public ITestNode this[int idx]
        {
            get { return PersistanceNode.getChild(idx).TestItem; }
        }

        public virtual string Name
        {
            get { return PersistanceNode.Text; }
            set { PersistanceNode.Text = value; }
        }

        public ITestNode AddChildNode(ITestNode node)
        {
            PersistanceNode.AddChildNode(node.PersistanceNode);
            return node;
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }


        public virtual bool IsRunnable { get; set; }

        public object Current
        {
            get
            {
                ITestNode obj = PersistanceNode.getChild(_currentIdx).TestItem;
                _currentIdx++;
                return obj;
            }
        }

        public bool MoveNext()
        {
            if (_currentIdx < Count)
            {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            _currentIdx = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public ITreeChildEnumerator GetTreeChildEnumerator()
        {
            return new TreeChildEnumerator(this);
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}