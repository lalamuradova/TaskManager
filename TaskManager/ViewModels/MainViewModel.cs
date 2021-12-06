using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TaskManager.Command;

namespace TaskManager.ViewModels
{
   public class MainViewModel:BaseViewModel
    {
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand AddBlackListCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand EndTaskCommand { get; set; }

        public TextBox Searchtxtbox { get; set; }
        public TextBox Createtxtbox { get; set; }
        public TextBox AddBlackListtxtbox { get; set; }
        public ListView Proceslistview { get; set; }
        public ListBox BlackList { get; set; }

        private Process process;

        public Process Process
        {
            get { return process; }
            set { process = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Process> allProcess;

        public ObservableCollection<Process> AllProcess
        {
            get { return allProcess; }
            set { allProcess = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            AllProcess = new ObservableCollection<Process>(Process.GetProcesses());
            timer.Start();


            CreateCommand = new RelayCommand((sender) =>
            {
                try
                {

                    foreach (var item in AllProcess)
                    {
                        if (BlackList.Items.Count != 0)
                        {
                            foreach (var black in BlackList.Items)
                            {
                                if (Createtxtbox.Text != black.ToString())
                                {
                                    Process p = new Process();
                                    p.StartInfo.FileName = Createtxtbox.Text;
                                    p.Start();

                                    AllProcess.Add(p);
                                }
                                else
                                {
                                    MessageBox.Show("Program is Black List");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = Createtxtbox.Text;
                            p.Start();

                            AllProcess.Add(p);
                        }

                           

                    }
                    AllProcess = new ObservableCollection<Process>(Process.GetProcesses());

                }
                catch (Exception)
                {


                }

                Createtxtbox.Text = string.Empty;
            });

            EndTaskCommand = new RelayCommand((sender) =>
            {
                try
                {                   
                        foreach (var item in AllProcess)
                        {
                            if (Proceslistview.SelectedItem != null)
                            {
                                var process = Proceslistview.SelectedItem as Process;

                                if (item.ProcessName == process.ProcessName)
                                {
                                    if (!item.WaitForExit(1000))
                                    {
                                        MessageBox.Show("Process ended");
                                        for (int J = 0; J < 100; J++)
                                        {
                                            if (!item.HasExited) item.Kill();

                                        }

                                    }
                                }                               
                            }
                            else
                            {
                                MessageBox.Show("Choose process");
                                break;
                            }

                        }

                        AllProcess = new ObservableCollection<Process>(Process.GetProcesses());                    
                }
                catch (Exception)
                {


                }
            });


            SearchCommand = new RelayCommand((sender) =>
            {
                try
                {
                    Proceslistview.ItemsSource = null;

                    if (string.IsNullOrEmpty(Searchtxtbox.Text) == false)
                    {
                        Proceslistview.ItemsSource = null;
                        Proceslistview.Items.Clear();

                        foreach (var item in AllProcess)
                        {
                            if (item.ProcessName.StartsWith(Searchtxtbox.Text))
                            {
                                Proceslistview.Items.Add(item);
                            }
                            Proceslistview.ItemsSource = null;

                        }

                        AllProcess = new ObservableCollection<Process>(Process.GetProcesses());


                    }

                    else
                    {
                        Proceslistview.Items.Clear();

                        foreach (var item in AllProcess)
                        {

                            Proceslistview.Items.Add(item);

                        }
                        AllProcess = new ObservableCollection<Process>(Process.GetProcesses());
                    }
                }
                catch (Exception)
                {


                }

            });

                  

            AddBlackListCommand = new RelayCommand((sender) =>
            {

                BlackList.Items.Add(AddBlackListtxtbox.Text);


                try
                {

                    foreach (var item in BlackList.Items)
                    {
                        var processes = AllProcess.Where(p => p.ProcessName == item.ToString());


                        if (processes != null)
                        {

                            foreach (var item2 in processes)
                            {


                                var process = AllProcess.FirstOrDefault(p => p.ProcessName == item.ToString());

                                if (item2.ProcessName == process.ProcessName)
                                {
                                    if (!item2.WaitForExit(1000))
                                    {
                                        MessageBox.Show("Added...");
                                        for (int J = 0; J < 100; J++)
                                        {
                                            if (!item2.HasExited) item2.Kill();

                                        }

                                    }
                                }

                            }

                            AllProcess = new ObservableCollection<Process>(Process.GetProcesses());

                        }


                    }

                }
                catch (Exception)
                {


                }
            });
        }

        public void GetProcess()
        {
            Process = Process.GetCurrentProcess();
            //var processes = Process.GetProcesses();
            
            //var current = Process.GetCurrentProcess();
            //Console.WriteLine(current.ProcessName);
            // Process.Start("notepad.exe");
            //var processes = Process.GetProcesses();
            //foreach (var item in processes)
            //{
            //    if (item.ProcessName == "notepad")
            //    {
            //        item.Kill();
            //    }
            //    Console.WriteLine(item.ProcessName);
            //}


            //var processes = Process.GetProcesses()
            //               .Where(p => p.ProcessName == "notepad");
            //foreach (var item in processes)
            //{
            //    Console.WriteLine(item.MachineName);
            //    Console.WriteLine(item.Threads.Count);
            //    Console.WriteLine(item.StartTime.ToLongTimeString());
            //    item.Kill();
            //}

            //Process.Start("https://stackoverflow.com/");


        }
    }
}




