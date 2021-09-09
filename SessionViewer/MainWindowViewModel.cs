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

        public string EventName => _sessionData?.First().EventName;
        public string SessionName => _sessionData?.First().SessionName;

        private ObservableCollection<SessionData> _sessionData;

        public ObservableCollection<SessionData> SessionData
        {
            get => _sessionData; 
            set
            {
                _sessionData = value;
                RaisePropertyChanged();
                RaisePropertyChanged(EventName);
                RaisePropertyChanged(SessionName);
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
                        var SessionDataList = await SessionFileLoading.LoadSessionCSV(openFileDialog.FileName);

                        SessionDataList = SessionDataList.OrderBy(o => o.FastLapTime).ToList();

                        SessionData = new ObservableCollection<SessionData>(SessionDataList);
                    }
                });
            }
        }


    }
}