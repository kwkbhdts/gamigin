namespace Gamigin
{
    internal class GamiginApp
    {
        /// <summary>
        /// 監視を開始する
        /// </summary>
        /// <returns>true:開始に成功 false:失敗</returns>
        public bool StartMonitoring()
        {
            IsMonitoring = true;

            if (!File.Exists(TargetFilePath))
            {
                return false;
            }
            
            var directoryPath = Path.GetDirectoryName(TargetFilePath);
            if (directoryPath == null)
            {
                return false;
            }
            ParentDirPath = directoryPath;

            var fileName = Path.GetFileName(TargetFilePath);
            if (fileName == null)
            {
                return false;
            }

            fileWatcher = new();
            fileWatcher.Path = directoryPath;
            fileWatcher.Filter = fileName;
            fileWatcher.NotifyFilter = 
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName;
            fileWatcher.Created +=
                new FileSystemEventHandler(OnTargetFileCreated);
            fileWatcher.Changed +=
                new FileSystemEventHandler(OnTargetFileChanged);
            fileWatcher.Deleted +=
                new FileSystemEventHandler(OnTargetFileDeleted);
            fileWatcher.Renamed +=
                new RenamedEventHandler(OnTargetFileRenamed);
            fileWatcher.EnableRaisingEvents = true;

            CreateDestDir();
            CopyTargetFile();

            return true;
        }

        /// <summary>
        /// 監視を終了する
        /// </summary>
        public void EndMonitoring() {
            if (fileWatcher != null)
            {
                fileWatcher.EnableRaisingEvents = false;
                fileWatcher.Dispose();
                fileWatcher = null;
            }
            IsMonitoring = false;
        }

        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        public static GamiginApp Instance => instance;

        /// <summary>
        /// メインフォームのインスタンス
        /// </summary>
        public MainForm? MainFormInstance { get; set; }

        /// <summary>
        /// 対象ファイルのパス
        /// </summary>
        public string TargetFilePath { get; set; }

        /// <summary>
        /// 親ディレクトリパス
        /// </summary>
        public string ParentDirPath { get; set; }

        /// <summary>
        /// 監視中か否か
        /// </summary>
        public bool IsMonitoring { get; set; }

        /// <summary>
        /// コピー先ディレクトリ名
        /// </summary>
        public string DestDirName { get; set; }

        /// <summary>
        /// 接尾詞フォーマット
        /// </summary>
        public string PostfixFormat { get; set; }

        /// <summary>
        /// privateコンストラクタ
        /// </summary>
        private GamiginApp()
        {
            MainFormInstance = null;
            TargetFilePath = "";
            ParentDirPath = "";
            IsMonitoring = false;
            DestDirName = "Gamigin";
            PostfixFormat = "_yyyyMMdd_HHmm_ss";
        }

        /// <summary>
        /// ファイル作成イベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnTargetFileCreated(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(WAIT_MILLISECONDS);
            CreateDestDir();
            CopyTargetFile();
        }

        /// <summary>
        /// ファイル変更イベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnTargetFileChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(WAIT_MILLISECONDS);
            CreateDestDir();
            CopyTargetFile();
        }

        /// <summary>
        /// ファイル削除イベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnTargetFileDeleted(object sender, FileSystemEventArgs e)
        {
            TargetFilePath = string.Empty;
            if (MainFormInstance != null)
            {
                MainFormInstance.InvokeUpdateTargetFilePath(TargetFilePath);
                MainFormInstance.InvokeEndMonitoring();
            }
        }

        /// <summary>
        /// ファイル名変更イベント
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント</param>
        private void OnTargetFileRenamed(object sender, RenamedEventArgs e)
        {
            if (TargetFilePath == e.FullPath)
            {
                Thread.Sleep(WAIT_MILLISECONDS);
                CreateDestDir();
                CopyTargetFile();
            }
        }

        /// <summary>
        /// 出力先ディレクトリを作る
        /// </summary>
        private void CreateDestDir()
        {
            if (ParentDirPath != null)
            {
                var destDirPath = Path.Join(ParentDirPath, DestDirName);
                if (!Directory.Exists(destDirPath))
                {
                    try
                    {
                        Directory.CreateDirectory(destDirPath);
                    }
                    catch (Exception)
                    {
                        // TODO:メインフォームにメッセージボックスを出させる
                    }
                }
            }
        }

        /// <summary>
        /// 対象ファイルを出力先ディレクトリに接尾詞つきでコピー
        /// </summary>
        private void CopyTargetFile()
        {
            var baseName = Path.GetFileNameWithoutExtension(TargetFilePath);
            var extension = Path.GetExtension(TargetFilePath);
            if ((baseName == null) || (extension == null))
            {
                return;
            }

            var postfix = DateTime.Now.ToString(PostfixFormat);
            if (postfix == null)
            {
                return;
            }

            var destFileName = baseName + postfix + extension;
            var destFilePath = 
                Path.Combine(ParentDirPath, DestDirName, destFileName);
            try
            {
                File.Copy(TargetFilePath, destFilePath, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// privateインスタンス
        /// </summary>
        private static readonly GamiginApp instance = new();

        /// <summary>
        /// ファイル監視インスタンス
        /// </summary>
        private FileSystemWatcher? fileWatcher = null;


        /// <summary>
        /// イベント発生から保存開始までどれだけスリープするか
        /// </summary>
        private const int WAIT_MILLISECONDS = 2000;
    }
}
