using System;
using System.ComponentModel;
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
        private readonly WebView m_WebView;
        private readonly SimpleViewModel m_SimpleViewModel = new SimpleViewModel();

        public MainForm()
        {
            CEF.Initialize(new Settings());

            m_WebView = new WebView(GetPageLocation(), new BrowserSettings())
                {
                    Dock = DockStyle.Fill
                };

            m_WebView.RegisterForKnockout(m_SimpleViewModel);
            m_WebView.PropertyChanged += OnWebBrowserInitialized;
            
            Controls.Add(m_WebView);
            
            Menu = GetMenu();
        }

        private void OnWebBrowserInitialized(object sender, PropertyChangedEventArgs args)
        {
            if (String.Equals(args.PropertyName, "IsBrowserInitialized", StringComparison.OrdinalIgnoreCase))
            {
                m_WebView.Load(GetPageLocation());
            }
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

            var fileMenu = new MenuItem("File");
            fileMenu.MenuItems.Add(showDevToolsItem);

            return new MainMenu(new[]
                {
                    fileMenu,
                });
        }
    }
}
