using System.Collections.Generic;

namespace Apphbify.Data
{
    public static class Addons
    {
        // Maintain a list of supported addons and their default plan level
        public static Dictionary<string, string> Supported = new Dictionary<string, string>() {
            { "airbreak", "free" },
            { "bitline", "AH_Developer" },
            { "blitz", "250" },
            { "cloudamqp", "lemur" },
            { "cloudant", "basic" },
            { "cloudmailin", "developer" },
            { "ironmq", "rust" },
            { "justonedb", "lambda" },
            { "logentries", "Trial" },
            { "mailgun", "free" },
            { "memcacher", "pico" },
            { "mongolab", "starter" },
            { "mongohq", "sandbox" },
            { "mysql", "yocto" },
            { "newrelic", "standard" },
            { "ravenhq", "6652C2B8-7F7B-455D-9C76-3B31213C0C51" },
            { "redistogo", "nano" },
            { "searchifyindextank", "starter" },
            { "sendgrid", "free" },
            { "sqlserver", "yocto" },
            { "stillalive", "basic" }
        };
    }
}