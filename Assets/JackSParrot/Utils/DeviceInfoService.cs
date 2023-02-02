using UnityEngine;

namespace Game.Services
{
    public enum DeviceType
    {
        Console,
        Desktop,
        Handheld,
        Unknown
    }

    public interface IDeviceInfoService : System.IDisposable
    {
        string DeviceUId { get; }
        float BatteryLevel { get; }
        string DeviceModel { get; }
        string DeviceName { get; }
        DeviceType DeviceType { get; }
        bool HasInternetConnection();
    }
    public class DeviceInfoService : IDeviceInfoService
    {
        public string DeviceUId => SystemInfo.deviceUniqueIdentifier;
        public float BatteryLevel => SystemInfo.batteryLevel;
        public string DeviceModel => SystemInfo.deviceModel;
        public string DeviceName => SystemInfo.deviceName;
        public DeviceType DeviceType
        {
            get
            {
                switch (SystemInfo.deviceType)
                {
                    case UnityEngine.DeviceType.Console:
                        return DeviceType.Console;
                    case UnityEngine.DeviceType.Desktop:
                        return DeviceType.Desktop;
                    case UnityEngine.DeviceType.Handheld:
                        return DeviceType.Handheld;
                    default:
                        return DeviceType.Unknown;
                }
            }
        }

        public bool HasInternetConnection()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        public void Dispose()
        {

        }
    }
}