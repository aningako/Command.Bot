﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Command.Bot.Core.Responders;

namespace Command.Bot.Core.Runner
{
    public class BatchFile : BaseCommandLineRunner, IRunner
    {
        #region Implementation of IRunner

        public string Extension => ".bat";

        public FileRunner GetRunner(string filePath)
        {
            if (this.IsExtensionMatch(filePath))
            {
                var command = Path.GetFileNameWithoutExtension(filePath);
                return new FileRunner(command, $"run {Path.GetFileName(filePath)} command.",ProcessFile(filePath), filePath);
            }
            return null;
        }

        private Func<IMessageContext, IEnumerable<string>> ProcessFile(string filePath)
        {
            return (context) => {
                ExecuteCommand(context,filePath);
                return new string[0];
            };
        }

        #endregion

        void ExecuteCommand(IMessageContext context, string command)
        {
            RunCommand(context, "cmd.exe", "/c " + CommandWithArguments(context.Text,command));
        }
    }
}