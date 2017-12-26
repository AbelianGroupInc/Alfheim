﻿using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Fis;
using System;
using System.IO;

namespace Alfheim.GUI.Model
{
    public static class PorjectCreator
    {
        public static void Create(ProjectParameters parameters)
        {
            string dirPath = $"{Properties.Settings.Default.ProjectsFolder}/{parameters.Name}";

            if (Directory.Exists(dirPath))
                throw new ArgumentException(nameof(parameters.Name));

            Directory.CreateDirectory(dirPath);

            FuzzyProject.Instance.Name = parameters.Name;
            FuzzyProject.Instance.Version = parameters.Version;

            FisWriter.Write($"{dirPath}/{parameters.Name}");
        }
    }
}
