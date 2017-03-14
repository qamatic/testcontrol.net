// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================


using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Net.BDD.TreeNodes;

namespace TestControl.Runner
{
    public class ServiceContext : IServiceContext
    {
        private readonly IServices _services = new Services();

        public ServiceContext()
        {
            RegisterServicesManually();
        }

        #region IServiceContext Members

        public IServices Services
        {
            get { return _services; }
        }

        #endregion

        public virtual void RegisterServicesManually()
        {
            _services.AddService<IPersistanceTreeNode>(typeof (TreeViewTreeNode));
            _services.AddService<IPersistanceTreeNode>(typeof (TreeViewTreeNode));
        }
    }
}