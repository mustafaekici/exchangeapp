using ExchangeService;
using GkfxDomain;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ExchangeRateFetcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //10.00 Birinci Ortalama
            //11.00 İkinci Ortalama
            //12.00 Üçüncü Ortalama
            //13.00 Dördüncü Ortalama
            //14.00 Beşinci Ortalama
            //15.00 Altıncı Ortalama

            Console.WriteLine("Service started...");
            Console.WriteLine("Fetching initial exchange rates...");

            FetcherJob jobfirst = new ExchangeRateFetcher.FetcherJob();
            jobfirst.FetchRates();

            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<FetcherJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .WithDailyTimeIntervalSchedule(x => x.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 5)) //from 10.05  Birinci Ortalama
                    .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(15, 5))                                        //to 15:05 Altıncı Ortalama
                    .OnMondayThroughFriday().WithIntervalInHours(1))                                           //Monday-Friday, Every hour
                    .Build();
                
                scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(60));

                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            Console.WriteLine("Press S to stop Service");
            Console.ReadLine();
        }

    }
}
