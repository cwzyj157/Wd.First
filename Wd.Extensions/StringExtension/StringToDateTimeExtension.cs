﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wd.Extensions
{
    /// <summary>
    /// 时间转换
    /// </summary>
    public static class StringToDateTimeExtension
    {
        public static string[][] TimeZones = new string[][] {
                                                new string[] {"ACDT", "-1030", "Australian Central Daylight"},
                                                new string[] {"ACST", "-0930", "Australian Central Standard"},
                                                new string[] {"ADT", "+0300", "(US) Atlantic Daylight"},
                                                new string[] {"AEDT", "-1100", "Australian East Daylight"},
                                                new string[] {"AEST", "-1000", "Australian East Standard"},
                                                new string[] {"AHDT", "+0900", ""},
                                                new string[] {"AHST", "+1000", ""},
                                                new string[] {"AST", "+0400", "(US) Atlantic Standard"},
                                                new string[] {"AT", "+0200", "Azores"},
                                                new string[] {"AWDT", "-0900", "Australian West Daylight"},
                                                new string[] {"AWST", "-0800", "Australian West Standard"},
                                                new string[] {"BAT", "-0300", "Bhagdad"},
                                                new string[] {"BDST", "-0200", "British Double Summer"},
                                                new string[] {"BET", "+1100", "Bering Standard"},
                                                new string[] {"BST", "+0300", "Brazil Standard"},
                                                new string[] {"BT", "-0300", "Baghdad"},
                                                new string[] {"BZT2", "+0300", "Brazil Zone 2"},
                                                new string[] {"CADT", "-1030", "Central Australian Daylight"},
                                                new string[] {"CAST", "-0930", "Central Australian Standard"},
                                                new string[] {"CAT", "+1000", "Central Alaska"},
                                                new string[] {"CCT", "-0800", "China Coast"},
                                                new string[] {"CDT", "+0500", "(US) Central Daylight"},
                                                new string[] {"CED", "-0200", "Central European Daylight"},
                                                new string[] {"CET", "-0100", "Central European"},
                                                new string[] {"CST", "+0600", "(US) Central Standard"},
                                                new string[] {"EAST", "-1000", "Eastern Australian Standard"},
                                                new string[] {"EDT", "+0400", "(US) Eastern Daylight"},
                                                new string[] {"EED", "-0300", "Eastern European Daylight"},
                                                new string[] {"EET", "-0200", "Eastern Europe"},
                                                new string[] {"EEST", "-0300", "Eastern Europe Summer"},
                                                new string[] {"EST", "+0500", "(US) Eastern Standard"},
                                                new string[] {"FST", "-0200", "French Summer"},
                                                new string[] {"FWT", "-0100", "French Winter"},
                                                new string[] {"GMT", "+0000", "Greenwich Mean"},
                                                new string[] {"GST", "-1000", "Guam Standard"},
                                                new string[] {"HDT", "+0900", "Hawaii Daylight"},
                                                new string[] {"HST", "+1000", "Hawaii Standard"},
                                                new string[] {"IDLE", "-1200", "Internation Date Line East"},
                                                new string[] {"IDLW", "+1200", "Internation Date Line West"},
                                                new string[] {"IST", "-0530", "Indian Standard"},
                                                new string[] {"IT", "-0330", "Iran"},
                                                new string[] {"JST", "-0900", "Japan Standard"},
                                                new string[] {"JT", "-0700", "Java"},
                                                new string[] {"MDT", "+0600", "(US) Mountain Daylight"},
                                                new string[] {"MED", "-0200", "Middle European Daylight"},
                                                new string[] {"MET", "-0100", "Middle European"},
                                                new string[] {"MEST", "-0200", "Middle European Summer"},
                                                new string[] {"MEWT", "-0100", "Middle European Winter"},
                                                new string[] {"MST", "+0700", "(US) Mountain Standard"},
                                                new string[] {"MT", "-0800", "Moluccas"},
                                                new string[] {"NDT", "+0230", "Newfoundland Daylight"},
                                                new string[] {"NFT", "+0330", "Newfoundland"},
                                                new string[] {"NT", "+1100", "Nome"},
                                                new string[] {"NST", "-0630", "North Sumatra"},
                                                new string[] {"NZ", "-1100", "New Zealand "},
                                                new string[] {"NZST", "-1200", "New Zealand Standard"},
                                                new string[] {"NZDT", "-1300", "New Zealand Daylight"},
                                                new string[] {"NZT", "-1200", "New Zealand"},
                                                new string[] {"PDT", "+0700", "(US) Pacific Daylight"},
                                                new string[] {"PST", "+0800", "(US) Pacific Standard"},
                                                new string[] {"ROK", "-0900", "Republic of Korea"},
                                                new string[] {"SAD", "-1000", "South Australia Daylight"},
                                                new string[] {"SAST", "-0900", "South Australia Standard"},
                                                new string[] {"SAT", "-0900", "South Australia Standard"},
                                                new string[] {"SDT", "-1000", "South Australia Daylight"},
                                                new string[] {"SST", "-0200", "Swedish Summer"},
                                                new string[] {"SWT", "-0100", "Swedish Winter"},
                                                new string[] {"USZ3", "-0400", "USSR Zone 3"},
                                                new string[] {"USZ4", "-0500", "USSR Zone 4"},
                                                new string[] {"USZ5", "-0600", "USSR Zone 5"},
                                                new string[] {"USZ6", "-0700", "USSR Zone 6"},
                                                new string[] {"UT", "+0000", "Universal Coordinated"},
                                                new string[] {"UTC", "+0000", "Universal Coordinated"},
                                                new string[] {"UZ10", "-1100", "USSR Zone 10"},
                                                new string[] {"WAT", "+0100", "West Africa"},
                                                new string[] {"WET", "+0000", "West European"},
                                                new string[] {"WST", "-0800", "West Australian Standard"},
                                                new string[] {"YDT", "+0800", "Yukon Daylight"},
                                                new string[] {"YST", "+0900", "Yukon Standard"},
                                                new string[] {"ZP4", "-0400", "USSR Zone 3"},
                                                new string[] {"ZP5", "-0500", "USSR Zone 4"},
                                                new string[] {"ZP6", "-0600", "USSR Zone 5"}
                                                };

        /// <summary>
        /// 时间转换（包括字符串转换以及其它时区时间转换为当前服务器时区时间）
        /// </summary>
        /// <param name="dateTime">待转换时间字符串</param>
        /// <param name="result">转换结果</param>
        /// <returns>转换是否成功</returns>
        public static bool TryParseToDatetime(this string dateTime, out DateTime result)
        {
            //获取时区代码
            Match m = Regex.Match(dateTime.Trim(), @"(\b[\s]\w{2,4}|[+-]?\d{4})$");

            if (m.Length == 0)
            {
                return DateTime.TryParse(dateTime, out result);
            }
            else
            {
                // Date w/ time zone. m.Value holds the time zone info
                // (either a time zone name (e.g. PST), or the
                // numeric offset (e.g. -0800).
                return ConvertToLocalDateTime(dateTime, m.Value.Trim(), out result);
            }
        }

        /// <summary>
        /// 时间转换（默认从当前服务器转换为指定的时区时间）
        /// </summary>
        /// <param name="datetime">待转换时间字符串</param>
        /// <param name="remoteLocatioin">转换为时区编号</param>
        /// <param name="result">转换结果</param>
        /// <returns>转换是否成功</returns>
        public static bool TryParseToDatetime(this string datetime, string timeZoneId, out DateTime result)
        {
            result = new DateTime(1900, 1, 1);
            if (string.IsNullOrEmpty(timeZoneId)) return false;
            return ConvertToLocalDateTime(datetime, timeZoneId, out result);
        }

        private static bool ConvertToLocalDateTime(string dateTime, string timeZoneId, out DateTime result)
        {
            bool success = false;
            result = new DateTime(1900, 1, 1);
            // Strip the time zone ID from the end of the dateTime string.
            dateTime = dateTime.Replace(timeZoneId, "").Trim();

            // Convert the timeZoneId to a TimeSpan.
            // (Leading + signs aren't allowed in the TimeSpan.Parse
            // parameter, although leading - signs are.
            // The purpose of the [+]*? at the beginning of the
            // regex is to account for, and ignore, any leading + sign).
            timeZoneId = GetTimeZoneOffset(timeZoneId);
            string ts = Regex.Replace(timeZoneId,
            @"^[+]*?(?<hours>[-]?\d\d)(?<minutes>\d\d)$",
            "${hours}:${minutes}:00");

            TimeSpan timeZoneOffset;
            success = TimeSpan.TryParse(ts, out timeZoneOffset);
            if (!success) return success;

            TimeSpan localUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);

            // Get the absolute time difference between the given
            // datetime's time zone and the local datetime's time zone.
            TimeSpan absoluteOffset;
            absoluteOffset = -(timeZoneOffset + localUtcOffset);

            success = DateTime.TryParse(dateTime, out result);
            result = result + absoluteOffset;
            return success;
        }

        /// <summary>
        /// Converts a time zone (e.g. "PST") to an offset string
        /// (e.g. "-0700").
        /// </summary>
        /// <param name="tz">The time zone to convert.</param>
        /// <returns>The offset value (e.g. -0700).</returns>
        private static string GetTimeZoneOffset(string tz)
        {
            // If the time zone is already in number format,
            // just return it.
            if (Regex.IsMatch(tz, @"^[+-]?\d{4}$")) return tz;

            string result = string.Empty;

            foreach (string[] sa in TimeZones)
            {
                if (sa[0].ToUpper() == tz)
                {
                    result = sa[1];
                    break;
                }
            }
            return result;
        }
    }
}
