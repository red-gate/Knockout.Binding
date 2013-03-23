using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        private readonly Dictionary<string, ViewModelBase> m_Examples = new Dictionary<string, ViewModelBase>()
            {
                {GetPageLocation("SomePage.htm"), new SimpleViewModel()},
                {GetPageLocation("HelloWorld.html"), new HelloWorldViewModel()}
            };

        public MainForm()
        {
            InitializeComponent();

            CEF.Initialize(new Settings());

            Menu = GetMenu();

            ChangePage("http://www.red-gate.com", null);
        }

        private void ChangePage(string page, ViewModelBase viewmodel)
        {
            Controls.Clear();

            m_WebView = new WebView(page, new BrowserSettings())
                {
                    Dock = DockStyle.Fill
                };

            if (viewmodel != null)
                m_WebView.RegisterForKnockout(viewmodel);

            Controls.Add(m_WebView);
        }

        private static string GetPageLocation(string page)
        {
            var runtimeDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

            return Path.Combine(runtimeDirectory, "Pages", page);
        }

        private MainMenu GetMenu()
        {
            var showDevToolsItem = new MenuItem("Show Dev Tools");
            showDevToolsItem.Click += (sender, args) => m_WebView.ShowDevTools();

            var examples = m_Examples.Select(x =>
                {
                    var showDebugPage = new MenuItem(x.Key);
                    showDebugPage.Click += (sender, args) => ChangePage(x.Key, x.Value);
                    return showDebugPage;
                });

            var fileMenu = new MenuItem("File");
            fileMenu.MenuItems.Add(showDevToolsItem);
            fileMenu.MenuItems.AddRange(examples.ToArray());
            
            return new MainMenu(new[]
                {
                    fileMenu
                });
        }
    }
}
