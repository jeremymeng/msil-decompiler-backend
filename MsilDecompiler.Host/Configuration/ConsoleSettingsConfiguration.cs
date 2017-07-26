﻿using ICSharpCode.Decompiler;
using Microsoft.Extensions.Configuration;

namespace MsilDecompiler.Host.Configuration
{
    public class ConsoleSettingsConfiguration: IDecompilationConfiguration
    {
        public DecompilerSettings DecompilerSettings { get; private set; } = new DecompilerSettings();

        public string FilePath { get; private set; }

        public ConsoleSettingsConfiguration(ConsoleArgs args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args.Args)
                .Build();

            FilePath = config["AssemblyPath"];

            // Decompiler options can be set via commandline.
            DecompilerSettings.AnonymousMethods = CheckBooleanSetting(config, "DecompilerSettings:AnonymousMethods");
        }

        private bool CheckBooleanSetting(IConfigurationRoot config, string settingName)
        {
            var result = false;
            bool.TryParse(config[settingName], out result);
            return result;
        }
    }
}