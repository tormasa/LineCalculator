using System;
using System.IO;

namespace LineCalculator {
	class Program {
		static void Main(string[] args) {
			string settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "settings.dat");
			string pathToRead = "";
			int lineCount = 0;
			int lineCountWithoutBlankLines = 0;
			int fileCount = 0;

			try {
				pathToRead = System.IO.File.ReadAllText(@settingsPath);
			}
			catch(FileNotFoundException) {
				Console.WriteLine("No settings.dat file. Create settings.dat file and write path of the directory you want to use.");
			}

			try {
				foreach (string file in Directory.EnumerateFiles(pathToRead, "*.cs", SearchOption.AllDirectories)) {
					string contents = File.ReadAllText(file);

					string[] lines = contents.Split('\n');
					lineCount += lines.Length;

					for (int i = 0; i < lines.Length; i++) {
						// Use this if you want to count all lines with more than one char
						//if (lines[i].Length > 1) lineCountWithoutBlankLines++;
						
						// Counts only lines that has letter or digit in them
						for (int j = 0; j < lines[i].Length; j++) {
							if (Char.IsLetterOrDigit(lines[i], j)) {
								lineCountWithoutBlankLines++;
								break;
							}
						}
					}

					fileCount++;
				}
			}
			catch (DirectoryNotFoundException) {
				Console.WriteLine("Directory specified in the settings.dat does not exist.");
			}

			Console.WriteLine($"In the specified directory ({pathToRead}) and its subdirectories there are {lineCount} lines in {fileCount} .cs files.");
			Console.WriteLine($"{lineCountWithoutBlankLines} line when not counting blank lines.");

			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey();
		}
	}
}
