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

		public IEnumerable<TileFile> GetTileFiles()
		{
			return tileFiles.Values;
		}

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
				TileFile file = new TileFile();
				file.ReadFile(f);
				file.FileName = Path.GetFileNameWithoutExtension(f);
				Add(file);
			}
		}

		public void AddAndSave(TileFile file)
		{
			file.FileName = "Tile" + DateTime.Now.Ticks.ToString();
			tileFiles.Add(file.FileName, file);
			file.Write(SavePath + file.FileName + ".tf");
		}

		public void Add(TileFile file)
		{
			tileFiles.Add(file.FileName, file);
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
