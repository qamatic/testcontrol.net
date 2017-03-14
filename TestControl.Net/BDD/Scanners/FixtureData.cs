// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections;
using TestControl.Net.BDD.Interfaces;

namespace  TestControl.Net.BDD.Scanners
{
    public class FixtureData : CollectionBase, IFixtureData
    {
        private readonly string _fixtureName;


        public FixtureData(string fixtureName)
        {
            _fixtureName = fixtureName;
        }

        #region IFixtureData Members

        public string FixtureName
        {
            get { return _fixtureName; }
        }


        public string[] this[int index]
        {
            get { return ((string[]) (List[index])); }
            set { List[index] = value; }
        }

        public int MaxColumn
        {
            get
            {
                int result = 0;
                foreach (string[] sts in this)
                {
                    if (result < sts.GetLength(0))
                    {
                        result = sts.GetLength(0);
                    }
                }
                return result;
            }
        }

        public int Add(string[] entry)
        {
            return List.Add(entry);
        }

        public void Insert(int index, string[] entry)
        {
            List.Insert(index, entry);
        }

        public void Remove(string[] entry)
        {
            List.Remove(entry);
        }

        #endregion
    }
}