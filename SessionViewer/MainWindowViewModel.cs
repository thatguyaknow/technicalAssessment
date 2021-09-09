using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SessionViewer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SessionViewer
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
        }

        private string _eventName = "Session Viewer";

        public string EventName
        {
            get => _eventName; set
            {
                _eventName = $"Session Viewer {value}";
                RaisePropertyChanged();
            }
        }

        private string _sessionName = "";

        public string SessionName
        {
            get => _sessionName; set
            {
                _sessionName = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<SessionData> _sessionData;

        public ObservableCollection<SessionData> SessionData
        {
            get => _sessionData;
            set
            {
                _sessionData = value;
                RaisePropertyChanged();
                EventName = SessionData.First().EventName;
                SessionName = SessionData.First().SessionName;
            }
        }

        public DelegateCommand LoadSession
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        Filter = "csv files (*.csv)|*.csv",
                        FilterIndex = 2,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == true)
                    {
                        SessionData = new ObservableCollection<SessionData>(await SessionFileLoading.LoadSessionCSV(openFileDialog.FileName));
                    }
                });
            }
        }
    }
}