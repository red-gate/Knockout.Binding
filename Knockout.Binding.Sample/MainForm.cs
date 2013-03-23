using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Knockout.Binding.ExtensionMethods;

namespace Knockout.Binding.Sample
{
    public partial class MainForm : Form
    {
        private WebView m_WebView;
        private readonly SimpleViewModel m_SimpleViewModel = new SimpleViewModel();

        public MainForm()
        {
            InitializeComponent();

            CEF.Initialize(new Settings());

            Menu = GetMenu();

            ChangePage("http://www.red-gate.com");
        }

        private void ChangePage(string page)
        {
            Controls.Clear();

            m_WebView = new WebView(page, new BrowserSettings())
                {
                    Dock = DockStyle.Fill
                };

            m_WebView.RegisterForKnockout(m_SimpleViewModel);

            Controls.Add(m_WebView);
        }

        private static string GetPageLocation()
        {
            var runtimeDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

            return Path.Combine(runtimeDirectory, "SomePage.htm");
        }

        private MainMenu GetMenu()
        {
            var showDevToolsItem = new MenuItem("Show Dev Tools");
            showDevToolsItem.Click += (sender, args) => m_WebView.ShowDevTools();

            var showDebugPage = new MenuItem("Show Initial Page");
            showDebugPage.Click += (sender, args) => ChangePage(GetPageLocation());

            var fileMenu = new MenuItem("File");
            fileMenu.MenuItems.Add(showDevToolsItem);
            fileMenu.MenuItems.Add(showDebugPage);

            return new MainMenu(new[]
                {
                    fileMenu
                });
        }
    }
}
