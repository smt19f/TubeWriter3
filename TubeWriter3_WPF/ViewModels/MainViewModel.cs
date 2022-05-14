using System.ComponentModel;
using TubeWriter3_WPF.Interfaces;
using System.Windows;
using System.Collections.Generic;

namespace TubeWriter3_WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MainViewModel()
        {
            showDate = new List<bool>() { false, false, false, false, false };
            boundDate = new List<string>() { "", "", "", "", "" };
            boundDogs = new List<string>() { "", "", "", "", "" };
            boundHorses = new List<string>() { "", "", "", "", "" };
            boundBirds = new List<string>() { "", "", "", "", "" };
            boundDoubles = new List<string>() { "", "", "", "", "" };
            boundRetest = new List<bool>() { false, false, false, false, false };
            boundStart = "";
        }

        public void Submit()
        {
            var report = ExcelSender.Send(this);

            if (!report.Boolean)
            {
                MessageBox.Show(report.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }       
        }

        public void Clear()
        {
            ShowDate = new List<bool>() { false, false, false, false, false };
            BoundDate = new List<string>() { "", "", "", "", "" };
            BoundDogs = new List<string>() { "", "", "", "", "" };
            BoundHorses = new List<string>() { "", "", "", "", "" };
            BoundBirds = new List<string>() { "", "", "", "", "" };
            BoundDoubles = new List<string>() { "", "", "", "", "" };
            BoundRetest = new List<bool>() { false, false, false, false, false };
            BoundStart = "";
            DateCount = 0;
            OnPropertyChanged("ShowDate");
        }

        public void Add()
        {
            if (DateCount >= 4)
            {
                MessageBox.Show("Can't display any more dates.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }

            ShowDate[DateCount] = true;
            DateCount++;
            OnPropertyChanged("ShowDate");
        }

        public int DateCount = 0;

        private List<bool> showDate;
        public List<bool> ShowDate
        {
            get
            {
                return showDate;
            }
            set
            {
                if (value == showDate)
                {
                    return;
                }
                showDate = value;
                OnPropertyChanged("ShowDate");
            }
        }

        private List<string> boundDate;
        public List<string> BoundDate
        {
            get
            {
                return boundDate;
            }
            set
            {
                if (value == boundDate)
                {
                    return;
                }
                boundDate = value;
                OnPropertyChanged("BoundDate");
            }
        }

        private List<string> boundDogs;
        public List<string> BoundDogs
        {
            get
            {
                return boundDogs;
            }
            set
            {
                if (value == boundDogs)
                {
                    return;
                }
                boundDogs = value;
                OnPropertyChanged("BoundDogs");
            }
        }

        private List<string> boundHorses;
        public List<string> BoundHorses
        {
            get
            {
                return boundHorses;
            }
            set
            {
                if (value == boundHorses)
                {
                    return;
                }
                boundHorses = value;
                OnPropertyChanged("BoundHorses");
            }
        }

        private List<string> boundBirds;
        public List<string> BoundBirds
        {
            get
            {
                return boundBirds;
            }
            set
            {
                if (value == boundBirds)
                {
                    return;
                }
                boundBirds = value;
                OnPropertyChanged("BoundBirds");
            }
        }

        private List<string> boundDoubles;
        public List<string> BoundDoubles
        {
            get
            {
                return boundDoubles;
            }
            set
            {
                if (value == boundDoubles)
                {
                    return;
                }
                boundDoubles = value;
                OnPropertyChanged("BoundDoubles");
            }
        }

        private List<bool> boundRetest;
        public List<bool> BoundRetest
        {
            get { return boundRetest; }
            set
            {
                if (value == boundRetest)
                {
                    return;
                }
                boundRetest = value;
                OnPropertyChanged("BoundRetest");
            }
        }

        private string boundStart;
        public string BoundStart
        {
            get
            {
                return boundStart;
            }
            set
            {
                if (value == boundStart)
                {
                    return;
                }
                boundStart = value;
                OnPropertyChanged("BoundStart");
            }
        }

    }
}
