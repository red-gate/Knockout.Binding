using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Knockout.Binding;
using Knockout.Binding.ExtensionMethods;

namespace CEF_Demo
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
        }

        private void OnWebBrowserInitialized(object sender, PropertyChangedEventArgs args)
        {
            if (String.Equals(args.PropertyName, "IsBrowserInitialized", StringComparison.OrdinalIgnoreCase))
            {
                m_WebView.Load(GetPageLocation());
                m_WebView.ShowDevTools();
            }
        }

        private static string GetPageLocation()
        {
            var runtimeDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

            return Path.Combine(runtimeDirectory, "SomePage.htm");
        }
    }
}
