//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  lukasz@istrib.org
//
//  Copyright (C) 2008-2009 Lukasz Kwiecinski. 
//

using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.DirectX.DirectSound;

namespace Istrib.Sound
{
    /// <summary>
    /// Represents a capture device like a microphone or line-in.
    /// </summary>
    public struct SoundCaptureDevice
        : IEquatable<SoundCaptureDevice>
    {
        /// <summary>
        /// Not a specific device but a "choose OS current" special case.
        /// </summary>
        public static readonly SoundCaptureDevice Default = new SoundCaptureDevice("Default", Guid.Empty, "");

        /// <summary>
        /// Lists Sound Capture Devices which are currently available in the OS.
        /// </summary>
        public static IEnumerable<SoundCaptureDevice> AllAvailable
        {
            get
            {
                CaptureDevicesCollection devices = new CaptureDevicesCollection();
                foreach (DeviceInformation deviceInfo in devices)
                {
                    yield return new SoundCaptureDevice(
                        deviceInfo.Description,
                        deviceInfo.DriverGuid,
                        deviceInfo.ModuleName);
                }
            }
        }

        private string description;
        private Guid driverGuid;
        private string moduleName;

        /// <summary>
        /// Gets a textual description of the Microsoft DirectSound device.
        /// </summary>
        public string Description { get { return description; } }
        
        /// <summary>
        /// Gets the globally unique identifier (GUID) of a Microsoft DirectSound
        /// driver.
        /// </summary>
        public Guid DriverGuid { get { return driverGuid; } }

        /// <summary>
        /// Gets the module name of the Microsoft DirectSound driver corresponding
        /// to this device.
        /// </summary>
        public string ModuleName { get { return moduleName; } }

        public static bool operator ==(SoundCaptureDevice device1, SoundCaptureDevice device2)
        {
            return device1.Equals(device2);
        }

        public static bool operator !=(SoundCaptureDevice device1, SoundCaptureDevice device2)
        {
            return !(device1 == device2);
        }

        public bool Equals(SoundCaptureDevice other)
        {
            return driverGuid == other.driverGuid;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SoundCaptureDevice))
            {
                return false;
            }

            return base.Equals((SoundCaptureDevice)obj);
        }

        public override int GetHashCode()
        {
            return driverGuid.GetHashCode();
        }

        public override string ToString()
        {
            return Description;
        }

        public SoundCaptureDevice(string description, Guid driverGuid, string moduleName)
        {
            this.description = description;
            this.driverGuid = driverGuid;
            this.moduleName = moduleName;
        }
    }
}
