using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using CefSharp;
using CefSharp.WinForms;
using KnckoutBindingGenerater;

namespace CEF_Demo
{
    public partial class MainForm : Form
    {
        private readonly WebView m_WebView;
        private readonly SimpleViewModel m_SimpleViewModel = new SimpleViewModel();

        public MainForm()
        {
            CEF.Initialize(new Settings());

            m_WebView = new WebView(GetPageLocation(), new BrowserSettings());
            m_WebView.RegisterForKnockout(m_SimpleViewModel);

            m_WebView.PropertyChanged +=
                (sender, args) =>
                {
                    if (args.PropertyName == "IsBrowserInitialized")
                    {
                        m_WebView.Load(GetPageLocation());
                        m_WebView.ShowDevTools();
                    }
                };

            m_WebView.Dock =DockStyle.Fill;
            
            Controls.Add(m_WebView);
        }

        private static string GetPageLocation()
        {
            var runtimeDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

            return Path.Combine(runtimeDirectory, "SomePage.htm");
        }
    }
}
