﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public static class Tools
    {
        public static void ThrowIfSettingsAreWrong(this IEnumerable<IChannelSettings> settings)
        {
            if(settings==null)
                throw new ArgumentNullException("settings");
            var channelsNums = new List<int>();
            foreach (var setting in settings) {
                if(channelsNums.Contains(setting.Num))
                    throw new ArgumentException("Two or more settings have same channel num "+ setting.Num);
                channelsNums.Add(setting.Num);
            }
        }
        public static void WriteNormalized(this IServoChannel channel, float target) {
            if (target < 0 || target > 1)
                throw new ArgumentException("Normalized servo value have to be in [0,1] interval");
            var raw = Proportion(
                value:  target,
                valueA: 0,
                valueB: 1,
                returnsAtA: channel.Min,
                returnsAtB: channel.Max);
            channel.WriteTarget(raw);
        }

        public static void WriteDegrees(this IServoChannel channel, float target) {
            var raw = Proportion(
                value:  target,
                valueA: channel.DegreesAtMin,
                valueB: channel.DegreesAtMax,
                returnsAtA: channel.Min,
                returnsAtB: channel.Max);
            channel.WriteTarget(raw);
        }

        public static float GetActualNormalized(this IChannel channel) {
            var mm = Math.Min(Math.Max(channel.Actual, channel.Min), channel.Max);
            return Proportion(
                value: mm, 
                valueA: channel.Min, 
                valueB: channel.Max, 
                returnsAtA: (float)0, 
                returnsAtB: (float)1);
        }

        public static float GetActualInDegrees(this IServoChannel channel) {
            return Proportion(
                value: channel.Status.Actual, 
                valueA: channel.Min, 
                valueB: channel.Max,
                returnsAtA: channel.DegreesAtMin, 
                returnsAtB: channel.DegreesAtMax);
        }

        public static ushort Proportion(float value, float valueA, float valueB, ushort returnsAtA, ushort returnsAtB) {
            var k = (value - valueA) / (valueB - valueA);
            return (ushort)(returnsAtA + Convert.ToUInt16((returnsAtB - returnsAtA) * k));
        }
        
        public static float Proportion(float value, float valueA, float valueB, float returnsAtA, float returnsAtB) {
            var k = (value - valueA) / (valueB - valueA);
            return returnsAtA + (returnsAtB - returnsAtA) * k;
        }
    }
}