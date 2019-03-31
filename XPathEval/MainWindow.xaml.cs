using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.XPath;
using XPathEval.Properties;

namespace XPathEval
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NameValueCollection setting => ConfigurationSettings.AppSettings;
        private XDocument xDoc { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                try
                {
                    xPathInput.Text = setting["xmlInput_Text"]?.ToString() ?? string.Empty;
                    xPathInput.Text = setting["xPathInput_Text"]?.ToString() ?? string.Empty;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                  
                }
            };
        }

        private void XmlInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                setting["xmlInput_Text"] = xPathInput.Text;
              //  setting.Save();
                xDoc = XDocument.Parse(xmlInput.Text);

                xPathOutput.Text = string.Empty;
                errorOutput.Text = string.Empty;
            }
            catch (Exception exception)
            {
                errorOutput.Text = exception.ToString();
            }
        }

        private void XPathInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(xDoc?.ToString())) return;
            try
            {
                setting["xPathInput_Text"] = xPathInput.Text;
              //  setting.Save();
                xPathOutput.Text = string.Join(Environment.NewLine,xDoc.XPathSelectElements(xPathInput.Text)) ;

                errorOutput.Text = string.Empty;
            }
            catch (Exception exception)
            {
                errorOutput.Text = exception.ToString();
            }
        }
    }
}
