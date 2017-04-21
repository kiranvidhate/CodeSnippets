﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace WPF_Globalisation
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadResources();
            SelectedLanguage = LanguageList.FirstOrDefault();
            PreviousLanguage = SelectedLanguage;
        }

        private List<Languages> _languageList;
        public List<Languages> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                RaisePropertyChanged("LanguageList");
            }
        }

        private Languages _selectedLanguage;
        public Languages SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                RaisePropertyChanged("SelectedLanguage");
            }
        }

        private Languages _previousLanguage;
        public Languages PreviousLanguage
        {
            get { return _previousLanguage; }
            set
            {
                _previousLanguage = value;
                RaisePropertyChanged("PreviousLanguage");
            }
        }

        private void LoadResources()
        {
            LanguageList = new List<Languages>();
            LanguageList.Add(new Languages() { Code = "en-US", Name = "English" });
            LanguageList.Add(new Languages() { Code = "fr-FR", Name = "French" });
        }
    }
}
