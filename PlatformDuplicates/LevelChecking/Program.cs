using System;
using System.Collections.Generic;
using System.IO;

namespace LevelChecking
{
	// mklink C:\PlatformerSMS E:\GitHub\StevePro7\PlatformerSMS\dev\banks\
	class Program
	{
		static void Main()
		{
			IList<string> inpList = new List<String>();
			IList<string> inpLinesX = new List<String>();

			const string root = @"C:\PlatformerSMS\";

			string inpDir = "bank4\\";
			string path = root + inpDir;
			var inpFiles = Directory.GetFiles(path);
			for (int count = 0; count < inpFiles.Length; count++)
			{
				string inpFile = inpFiles[count];
				var inpLines = File.ReadAllLines(inpFile);
				var inpLine = String.Join(String.Empty, inpLines);
				inpLinesX.Add(inpLine);
				inpFile = inpFile.Replace(path, String.Empty);
				inpList.Add(inpFile);
			}

			inpDir = "bank5\\";
			path = root + inpDir;
			inpFiles = Directory.GetFiles(path);
			for (int count = 0; count < inpFiles.Length; count++)
			{
				string inpFile = inpFiles[count];
				var inpLines = File.ReadAllLines(inpFile);
				var inpLine = String.Join(String.Empty, inpLines);
				inpLinesX.Add(inpLine);
				inpFile = inpFile.Replace(path, String.Empty);
				inpList.Add(inpFile);
			}

			for (int index = 0; index < inpList.Count; index++)
			{
				string inpLine1 = inpLinesX[index];
				for (int count = index + 1; count < inpList.Count; count++)
				{
					if (index == count)
					{
						continue;
					}

					string inpLine2 = inpLinesX[count];
					if (inpLine1 == inpLine2)
					{
						string name1 = inpFiles[index];
						string name2 = inpFiles[count];
						//string msg = String.Format("Srce:{0} Dest:{1}", inpLine1, inpLine2);
						string msg = String.Format("Srce:{0} Dest:{1}", name1, name2);
						Console.WriteLine(msg);
					}
				}
			}
			
			Console.WriteLine("Hello World!");
		}
	}
}
