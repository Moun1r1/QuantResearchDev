using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Moon.Resources.Management;

namespace Moon.Resources
{
    public class Perf

    {
        public ObservableCollection<PlanifiedOperation> ToManage { get; set; } = new ObservableCollection<PlanifiedOperation>();
        public ObservableCollection<PlanifiedOperation> ToManage2 { get; set; } = new ObservableCollection<PlanifiedOperation>();
        public ObservableCollection<PlanifiedOperation> Finished { get; set; } = new ObservableCollection<PlanifiedOperation>();
        public long MemoryUsage { get; set; } = 0;
        public DateTime LastCheck { get; set; } = DateTime.Now;
        public DateTime LastClean { get; set; } = DateTime.Now;
        public int GenerationSupported { get; set; } = GC.MaxGeneration;
        public long Threeshold { get; set; } = 50747520;
        public long Freedmemory { get; set; } = 0;
        public int NumberOfRunningOperation { get; set; } = 0;
        public string Report { get; set; } = "";
        /// <summary>
        /// Prepare Memory for large allocation before doing it
        /// Could check if memory wanted is present before doing it and should be used before call with hasenoughmemory
        /// </summary>
        public void PrepareLargeOperation()
        {
            try
            {
                GC.AddMemoryPressure(5000000);
            }
            catch
            {

            }
        }
        public Perf()
        {
            ToManage.CollectionChanged += ToManage_CollectionChanged;
            ToManage2.CollectionChanged += ToManage_CollectionChanged;
            MonitoringRunningThread();
            Task.Factory.StartNew(() =>
            {
                while (Transversal.IsShutingDown != true)
                {
                    Process Me = Process.GetCurrentProcess();
                    MemoryUsage = Me.PrivateMemorySize64;
                    LastCheck = DateTime.Now;
                    if (MemoryUsage > Threeshold)
                    {
                        //Console.WriteLine("Perf - Memory : Last check was : {0}", LastCheck);
                        //Console.WriteLine("Perf - Memory : Check {0} bytes is higher than Threeshold : {1} bytes", MemoryUsage, Threeshold);
                        //Console.WriteLine("Perf - Memory : Starting Memory Garbage Collection");
                        //Report = Report + string.Format("Perf - Memory : Last check was : {0}", LastCheck);
                        //Report = Report + string.Format("Perf - Memory : Check {0} bytes is higher than Threeshold : {1} bytes", MemoryUsage, Threeshold);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        LastClean = DateTime.Now;
                        Freedmemory = MemoryUsage - Process.GetCurrentProcess().PrivateMemorySize64;
                        Console.WriteLine("Perf : Garbage Collection deleted {0} bytes ", Freedmemory);

                    }
                    Thread.Sleep(new TimeSpan(0, 0, 50));
                }

            });
        }


        private void MonitoringRunningThread()
        {
            Task.Factory.StartNew(() =>
            {
                int CurrentOperationRunning = 0;
                int LastOperationCheck = 0;
                bool HasChanged = CurrentOperationRunning != LastOperationCheck;

                while (!Transversal.IsShutingDown)
                {
                    if (HasChanged)
                    {
                        //Console.WriteLine("Perf - Thread : Currently Running : {0} Operations", this.NumberOfRunningOperation);
                        //Console.WriteLine("Perf - Thread : Main Task list count : {0}", this.ToManage.Count);
                        //Console.WriteLine("Perf - Thread : Second Task list : {0}", this.ToManage2.Count);
                        CurrentOperationRunning = this.NumberOfRunningOperation;
                    }
                    LastOperationCheck = NumberOfRunningOperation;

                    System.Threading.Thread.Sleep(30000);
                }
            });

        }
        private void ToManage_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {

                PlanifiedOperation Operation = (PlanifiedOperation)e.NewItems[0];
                if (Operation != null)
                {
                    //Console.WriteLine("Perf - TaskManagement :  New operation will start at : {0} for {1} ", Operation.Start, Operation.OperationName);
                    Task.Factory.StartNew(() =>
                    {
                        object lo = new object();
                        System.Threading.Monitor.Enter(lo);
                        bool OperationExecuted = false;
                        while (!OperationExecuted)
                        {
                            DateTime StartOperationDate = DateTime.Parse(Operation.Start.ToString("HH:mm:ss"));
                            DateTime Current = DateTime.Parse(DateTime.Now.ToString("HH:mm:ss"));
                            if (StartOperationDate == Current & StartOperationDate.Minute == Current.Minute & Operation.State == OperationStatus.Pending)
                            {
                                try
                                {
                                    NumberOfRunningOperation++;
                                    Exception ex = new Exception();
                                    Stopwatch RealOperationTime = new Stopwatch();
                                    RealOperationTime.Start();
                                    //Operation.OperationCode.Invoke();
                                    RealOperationTime.Stop();
                                    Operation.End = DateTime.Now;
                                    Operation.RealDuration = RealOperationTime.Elapsed;
                                    //Operation.ResultedObject = Operation.OperationCode.Target;
                                    Operation.State = OperationStatus.Finished;
                                    OperationExecuted = true;
                                    Finished.Add(Operation);
                                    ToManage.Remove(Operation);

                                    if (Operation.ContiniousOperation & Operation.State == OperationStatus.Finished)
                                    {

                                        Operation.Start = DateTime.Now.Add(Operation.Every);
                                        Operation.State = OperationStatus.Pending;
                                        ToManage.Add(Operation);
                                        Console.WriteLine("Perf - TaskManagement : scheduling new operation for continious logic at : {0}", Operation.Start);

                                        break;
                                    }
                                    else
                                    {
                                        //Operation.OperationCode.Dispose();

                                    }

                                }
                                catch (Exception ex)
                                {
                                    if (Operation.AllowMultipleException)
                                    {
                                        Stopwatch Retry = new Stopwatch();
                                        Retry.Start();

                                        //Operation.OperationCode.Invoke();
                                        Retry.Stop();
                                        Operation.End = DateTime.Now;
                                        Operation.RealDuration = Retry.Elapsed;
                                        //Operation.ResultedObject = Operation.OperationCode.Target;
                                        Operation.State = OperationStatus.Exception;
                                        OperationExecuted = true;
                                        Operation.Errors.Add(ex);
                                    }
                                    else
                                    {
                                        //Operation.ResultedObject = Operation.OperationCode.Target;
                                        Operation.State = OperationStatus.Exception;
                                        OperationExecuted = true;
                                        Operation.Errors.Add(ex);
                                    }


                                }
                            }
                            else
                            {
                                Thread.Sleep(200);
                            }
                        }
                        NumberOfRunningOperation--;
                        System.Threading.Monitor.Exit(lo);
                    });
                }
            }

        }
    }

}
