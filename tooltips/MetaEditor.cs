using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tooltips
{
	internal class MetaEditor : IDisposable
	{
		public string MetaFileName { get; } = ".memeta";
		string[] Separators = new string[] { "//" };
		public MetaEditor()
		{
		}
		/// <summary>
		/// this function will delete invalid data field it doesn't relate to existing files.
		/// contains a Task
		/// </summary>
		public void CleanUnexisting()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// reads meta message from meta file if it does exist and message was written.
		/// when there is no meta file this function will throw FileNotFoundException
		/// when there is no message or failed read meta file this function will return other exception.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public string GetInformation(string fileName)
		{
			var currentMetaFileName = Path.Combine(Path.GetDirectoryName(fileName), MetaFileName);
			if (!File.Exists(currentMetaFileName))
			{
				throw new FileNotFoundException(currentMetaFileName);
			}
			var onlyFileName = Path.GetFileName(fileName);
			using (var fileStream = new FileStream(currentMetaFileName, FileMode.Open, FileAccess.Read))
			{
				using (var reader = new StreamReader(fileStream))
				{
					// ReadLine() is not fast but easy.
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var splitedLine = line.Split(Separators, StringSplitOptions.None);
						// on windows file names don't care their cases.
						if (string.Compare(onlyFileName, splitedLine[0], true) == 0)
						{
							return DisescapeCharacter(splitedLine[1]);
						}
					}
					throw new Exception("there is no data field.");
				}
			}
		}
		public void SetInformation(string fileName, string message)
		{
			if (message == null || message.Length == 0)
			{
				RemoveInformation(fileName);
				return;
			}
			var onlyFileName = Path.GetFileName(fileName);
			// if there is no meta file this function will make new meta file
			var currentMetaFileName = Path.Combine(Path.GetDirectoryName(fileName), MetaFileName);
			using (var stringWriter = new StringWriter())
			{
				using (var file = new FileStream(currentMetaFileName, FileMode.OpenOrCreate, FileAccess.Read))
				{
					using (var reader = new StreamReader(file))
					{
						bool foundTargetLine = false;
						while (!reader.EndOfStream)
						{
							var line = reader.ReadLine();
							if (!foundTargetLine)
							{
								var splitedLine = line.Split(Separators, StringSplitOptions.None);
								if (string.Compare(onlyFileName, splitedLine[0], true) == 0)
								{
									foundTargetLine = true;
									continue;
								}
							}
							stringWriter.WriteLine(line);
						}
						stringWriter.WriteLine($"{onlyFileName}{Separators[0]}{EscapeCharacter(message)}");
					}
				}
				using (var file = new FileStream(currentMetaFileName, FileMode.Open, FileAccess.Write))
				{
					using (var writer = new StreamWriter(file))
					{
						writer.Write(stringWriter.ToString());
					}
				}
			}
			var fileAttribute = File.GetAttributes(currentMetaFileName);
			fileAttribute |= FileAttributes.Hidden;
			File.SetAttributes(currentMetaFileName, fileAttribute);
		}
		public void RemoveInformation(string fileName)
		{
		}
		private string EscapeCharacter(string source)
		{
			return source.Replace("/", @"\/").Replace("\\", @"\\").Replace("\r\n", @"\r\n").Replace("\n", @"\r\n").Replace("\r", @"\r\n");
		}
		private string DisescapeCharacter(string source)
		{
			return source.Replace(@"\/", "/").Replace(@"\r\n", "\r\n").Replace(@"\\", "\\");
		}

		#region IDisposable Support
		private bool disposedValue = false; // 重複する呼び出しを検出するには

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: マネージド状態を破棄します (マネージド オブジェクト)。
				}

				// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
				// TODO: 大きなフィールドを null に設定します。

				disposedValue = true;
			}
		}

		// TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
		// ~MetaEditor() {
		//   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
		//   Dispose(false);
		// }

		// このコードは、破棄可能なパターンを正しく実装できるように追加されました。
		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
			Dispose(true);
			// TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
