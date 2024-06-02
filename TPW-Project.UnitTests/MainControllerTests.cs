using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_Project.ViewModel;

namespace TPW_Project.UnitTests
{
    public class MainControllerTests
    {
        private MainController mainController;

        [SetUp]
        public void Setup()
        {
            mainController = new MainController();
        }

        [Test]
        public void Constructor_InitializesCurrentViewModel()
        {
            // Assert
            ClassicAssert.IsNotNull(mainController.CurrentViewModel);
            ClassicAssert.IsInstanceOf<SimualtionController>(mainController.CurrentViewModel);
        }
    }
}
