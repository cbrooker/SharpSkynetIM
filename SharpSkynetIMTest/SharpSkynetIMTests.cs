using System;
using System.Collections.Generic;
using System.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SharpSkynetIMTest
{
    [TestClass]
    public class SharpSkynetIMTests
    {
        [TestMethod]
        public void SkynetGetStatus()
        {
            //arrange
            var sky = new SharpSkynetIM.SkynetIMClient();

            //act
            var skynetstaus = sky.SkynetStatus();

            //assert
            Assert.IsNotNull(skynetstaus);
            Assert.AreEqual(skynetstaus.skynet.Value, "online");

        }

        [TestMethod]
        public void SkynetGetIpAddress()
        {
            //arrange
            var sky = new SharpSkynetIM.SkynetIMClient();

            //act
            var skynetstaus = sky.IpAddress();

            //assert
            Assert.IsNotNull(skynetstaus);
            Assert.IsNotNull(skynetstaus.ipAddress.Value);

        }

        [TestMethod]
        public void SkynetGetDevicesNoParameters()
        {
            //arrange
            var sky = new SharpSkynetIM.SkynetIMClient();

            //act
            var skynetstaus = sky.Devices();

            //assert
            Assert.IsNotNull(skynetstaus);
        }


        [TestMethod]
        public void SkynetAddDevice()
        {
            //arrange
            var sky = new SharpSkynetIM.SkynetIMClient();

            //act
            var para = new List<KeyValuePair<String, String>>();
            para.Add(new KeyValuePair<string, string>("test", "test"));

            var skynetResponse = sky.AddDevice(para);

            //assert
            Assert.IsNotNull(skynetResponse);
        }


    }
}
