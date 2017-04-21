using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Globalisation
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;		

        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
			this.DataContext = vm;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            var currentResourceDictionary = (from d in BaseModel.Instance.ImportCatalog.ResourceDictionaryList
                                             where d.Metadata.ContainsKey("Culture")
                                             && d.Metadata["Culture"].ToString().Equals(vm.SelectedLanguage.Code)
                                             select d).FirstOrDefault();
            if (currentResourceDictionary != null)
            {
                var previousResourceDictionary = (from d in BaseModel.Instance.ImportCatalog.ResourceDictionaryList
                                                  where d.Metadata.ContainsKey("Culture")
                                                  && d.Metadata["Culture"].ToString().Equals(vm.PreviousLanguage.Code)
                                                  select d).FirstOrDefault();
                if (previousResourceDictionary != null && previousResourceDictionary != currentResourceDictionary)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(previousResourceDictionary.Value);
                    Application.Current.Resources.MergedDictionaries.Add(currentResourceDictionary.Value);
                    CultureInfo cultureInfo = new CultureInfo(vm.SelectedLanguage.Code);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    Application.Current.MainWindow.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

                    vm.PreviousLanguage = vm.SelectedLanguage;
                }
            }
		}
    }
}
