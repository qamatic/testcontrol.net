using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Net.StdControls;

namespace TestControlTests
{
    [TestClass]
    public class GridViewTest
    {

        private ApplicationUnderTest _testApp;

        [TestInitialize]
        public void BaseState()
        {
            _testApp = new ApplicationUnderTest("", "TestControlTests.exe");
            _testApp.Terminate();
            // _testApp.ShowDesktop();
            _testApp.Run();
        }

        [TestMethod]
        public void TestGridRole()
        {
            IGridViewControl grid = LocateGrid();
            Assert.IsTrue(grid.IsVisible);
            Assert.AreEqual("table", grid.Role);    
        }

      
        [TestMethod]
        public void TestGridRowCount()
        {
            IGridViewControl grid = LocateGrid();
            Assert.IsTrue(grid.IsVisible);
            Assert.AreEqual(6, grid.RowCount);

        }

        [TestMethod]
        public void TestGridColCount()
        {
            IGridViewControl grid = LocateGrid();
            Assert.IsTrue(grid.IsVisible);
            Assert.AreEqual(4, grid.Rows[0].ColumnCount);

        }


        [TestMethod]
        public void TestGridContentValues()
        {
            IGridViewControl grid = LocateGrid();


            Assert.AreEqual("FirstName	LastName	City", grid.Rows[0].ToString());
            Assert.AreEqual("F1	L1	C1", grid.Rows[1].ToString());
            Assert.AreEqual("F2	L2	C2", grid.Rows[2].ToString());
            Assert.AreEqual("F3	L3	C3", grid.Rows[3].ToString());
            Assert.AreEqual("F4	L4	C4", grid.Rows[4].ToString());
        }

        private static IGridViewControl LocateGrid()
        {
            IGridViewControl grid = new GridViewControl();
            grid.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("dataGridView1")
                    ));
            return grid;
        }

    }
}
