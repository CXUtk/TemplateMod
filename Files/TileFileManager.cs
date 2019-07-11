using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terraria.ModLoader;

namespace TemplateMod.Files
{
	public class TileFileManager
	{
		private Dictionary<string, TileFile> tileFiles;

		public TileFileManager()
		{
			if (!Directory.Exists(SavePath))
			{
				Directory.CreateDirectory(SavePath);
			}
			ReadAll();
		}

		private string SavePath
		{
			get
			{
				return ModLoader.ModPath + "/TileFiles/";
			}
		}

		public void ReadAll()
		{
			tileFiles?.Clear();
			tileFiles = new Dictionary<string, TileFile>();
			var files = Directory.GetFiles(SavePath);
			foreach(var f in files)
			{
				MessageBox.Show(f);
			}
		}

		public void Add(TileFile file)
		{
			file.FileName = "Data_" + tileFiles.Count;
			tileFiles.Add(file.FileName, file);
			file.Write(SavePath + file.FileName + ".tf");
		}

		public void SaveAll()
		{
			foreach(var pair in tileFiles)
			{
				pair.Value.Write(SavePath + pair.Key + ".tf");
			}
		}
	}
}
