using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Application = FlaUI.Core.Application;

namespace Graduation_Tests_
{
    [TestClass]
    public class MasterTest
    {
        private ConditionFactory _conditionFactory;
        private Application _application;
        private Window _mainWindow;

        [TestMethod]
        public void AuthorisationTest()
        {
            _application = Application.Launch($@"C:\Users\{Environment.UserName}\source\repos\Graduation\Graduation\bin\Debug\net8.0-windows\Graduation.exe");
            _conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());
            _mainWindow = _application.GetMainWindow(new UIA3Automation());
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("LoginTextBox")).AsTextBox().Enter("petr");
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("PasswordBox")).AsTextBox().Enter("petr1");
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("LogInButton")).AsButton().Click();
            Thread.Sleep(2000);
            Mouse.MoveBy(40, -10);
            Mouse.Click();
            Thread.Sleep(1500);
        }

        [TestCategory("WorkOrder")]
        [TestMethod]
        public void CreateWorkOrderTest()
        {
            AuthorisationTest();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderCreateButton")).AsButton().Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderIdTextBox")).AsTextBox().Enter("6");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("ReservationIdComboBox")).AsComboBox().Select(3);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("PauNameComboBox")).AsComboBox().Select(3);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("PauCountTextBox")).AsTextBox().Enter("100");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("EmployeeSurnameComboBox")).AsComboBox().Select(1);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("AreaIdComboBox")).AsComboBox().Select(2);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("OperationNameComboBox")).AsComboBox().Select(4);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("OperationStartDateTextBox")).AsTextBox().Enter("10042024");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("OperationStartTimeTextBox")).AsTextBox().Enter("0900");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("OperationEndDateTextBox")).AsTextBox().Enter("10042024");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("OperationEndTimeTextBox")).AsTextBox().Enter("1100");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderCreateButton")).AsButton().Click();
            Mouse.MoveBy(180, -210);
            Mouse.Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("BackButton")).AsButton().Click();
            Thread.Sleep(2000);
        }

        [TestCategory("WorkOrder")]
        [TestMethod]
        public void UpdateWorkOrderTest()
        {
            AuthorisationTest();
            Thread.Sleep(1500);
            Mouse.MoveBy(0, -300);
            Mouse.RightClick();
            Mouse.MoveBy(40, 20);
            Mouse.Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("PauCountTextBox")).AsTextBox().Enter("100");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderCloseCheckBox")).AsCheckBox().Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderCreateButton")).AsButton().Click();
            Thread.Sleep(1500);
            Mouse.MoveBy(180, -210);
            Mouse.Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("BackButton")).AsButton().Click();
            Thread.Sleep(2000);
        }

        [TestCategory("Invoice")]
        [TestMethod] 
        public void CreateInvoiceTest()
        {
            AuthorisationTest();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("InvoicesViewItem")).AsButton().Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("InvoiceCreateButton")).AsButton().Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("InvoiceIdTextBox")).AsTextBox().Enter("5");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderIdComboBox")).AsComboBox().Select(1);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("WorkOrderCompilationDateComboBox")).AsComboBox().Select(0);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("DepartmentIdComboBox")).AsComboBox().Select(2);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("DepartmentReceiverIdComboBox")).AsComboBox().Select(3);
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("FactCountTextBox")).AsTextBox().Enter("175");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("InvoiceCreateButton")).AsButton().Click();
            Thread.Sleep(1500);
            Mouse.MoveBy(160, -180);
            Mouse.Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("BackButton")).AsButton().Click();
            Thread.Sleep(2000);
        }

        [TestCategory("Invoice")]
        [TestMethod]
        public void UpdateInvoiceTest()
        {
            AuthorisationTest();
            Thread.Sleep(1250);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("InvoicesViewItem")).AsButton().Click();
            Thread.Sleep(1500);
            Mouse.MoveBy(0, 200);
            Mouse.RightClick();
            Mouse.MoveBy(40, 20);
            Mouse.Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("FactCountTextBox")).AsTextBox().Enter("150");
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("InvoiceCreateButton")).AsButton().Click();
            Thread.Sleep(1500);
            Mouse.MoveBy(160, -180);
            Mouse.Click();
            Thread.Sleep(1500);
            _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("BackButton")).AsButton().Click();
            Thread.Sleep(2000);
        }
    }
}
