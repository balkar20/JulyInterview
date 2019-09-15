using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Incapsulation.Failures
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Failure
    {
        public FailureType FailureType { get; set; }
        public DateTime FalureDate { get; set; }
        public Device Device { get; set; }
    }

    public enum FailureType
    {
        Serious, 
        NotSerious
    }

    public class Common
    {
        public static int IsFailureSerious(int failureType)
        {
            if (failureType % 2 == 0) return 1;
            return 0;
        }
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            DateTime beforeTime = new DateTime(year, month, day);
            List<Device> devicesList = ConvertDictionariesToDevices(devices);
            List<Failure> failures = new List<Failure>();
            for (int i = 0; i < failureTypes.Length; i++)
            {
                failures.Add(new Failure()
                {
                    FailureType = Common.IsFailureSerious(failureTypes[i]) == 1 
                        ? FailureType.Serious : FailureType.NotSerious,
                    FalureDate = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]),
                    Device = devicesList.Find(d => d.Id == deviceId[i])
                });
            }

            var result = FindDevicesFailedBeforeDate(beforeTime, failures, devicesList);
            return result;
        }

        public static List<string> FindDevicesFailedBeforeDate(
            DateTime date,
            List<Failure> failures,
            List<Device> devices)
        {
            var problematicDevices = new HashSet<int>();
            for (int i = 0; i < failures.Count; i++)
            {
                if (failures[i].FailureType == FailureType.Serious && failures[i].FalureDate.Date < date.Date)
                {
                    problematicDevices.Add(failures[i].Device.Id);
                }
            }

            var result = new List<string>();
            foreach (var device in devices)
                if (problematicDevices.Contains(device.Id))
                    result.Add(device.Name);

            return result;
        }

        private static List<Device> ConvertDictionariesToDevices(List<Dictionary<string, object>> dictionaries)
        {
            var result =  new List<Device>();
            foreach (var dictionary in dictionaries)
            {
                    result.Add(new Device()
                    {
                        Id = (int)dictionary["DeviceId"],
                        Name = (string)dictionary["Name"]
                    });
            }
            return result;
        }
    }
}
